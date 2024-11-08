using Assets.Models;
using Assets.Scripts.SOdata;
using Assets.Services;
using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Material = UnityEngine.Material;

public class MountUnitBeamRafterTrussGenerator : MonoBehaviour
{
    public float xCoord;
    public float yCoord;
    public float zCoord;
    public bool onHighColumns;
    [SerializeField]
    private MountUnitBeamRafterTrussDataList MountUnitBeamRafterTrussDataList;
    private MountUnitBeamRafterTrussData MountUnitBeamRafterTrussData;
    [SerializeField]
    private MountUnitColumnBeamTrussDataList MountUnitColumnBeamTrussDataList;
    private MountUnitColumnBeamTrussData MountUnitColumnBeamTrussData;
    private Canopy canopy;
    private GameObject shelfTableMount;
    private GameObject wallTableMountLong;
    private GameObject wallTableMountShort;
    private GameObject flangeBeamTruss;
    private GameObject flangeRafterTruss;
    private GameObject screwPrefab;
    private GameObject washerwPrefab;
    private GameObject nutwPrefab;
    private const int gapBeamRafterFlange = 2;
    private float widthShelfTableMount;
    private float offsetHorizFlangeBeamTruss;
    private float diagonalWidthFlangeBeamTruss;
    private readonly List<GameObject> screws = new();
    private readonly List<GameObject> washers = new();
    private readonly List<GameObject> nuts = new();
    public MountUnitBeamRafterTruss MountUnitBeamRafterTruss { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetMountUnitBeamRafterTrussData());
        StartCoroutine(MakeMountBeamRafterTruss());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GetMountUnitBeamRafterTrussData()
    {
        canopy = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(e => e.CompareTag("Canopy")).GetComponent<CanopyGenerator>().Canopy;
        MountUnitBeamRafterTruss = ScriptObjectsAction.GetMountUnitBeamRafterTrussByName(canopy.RafterTruss.Truss.Name, MountUnitBeamRafterTrussDataList);
        MountUnitBeamRafterTrussData = MountUnitBeamRafterTrussDataList.mountUnitBeamRafterTrussDatas.FirstOrDefault(e => e.RafterTrussName == canopy.RafterTruss.Truss.Name);
        MountUnitColumnBeamTrussData = MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas.FirstOrDefault(e => e.BeamTrussName == canopy.BeamTruss.Truss.Name);
        wallTableMountShort = GetGOfromMeshes("WallTableMount"
            , "MountUnitBeam"
            , canopy.BeamTruss.Truss.ProfileBelt.Height
            , MountUnitBeamRafterTrussData.LengthFlangeBeamTruss
            , MountUnitBeamRafterTrussData.ThicknessTable);
        wallTableMountLong = Instantiate(GameObject.FindGameObjectWithTag("WallTableMount"));
        shelfTableMount = GetGOfromMeshes("ShelfTableMount"
            , "MountUnitBeam"
            , canopy.BeamTruss.Truss.ProfileBelt.Height / Mathf.Cos(canopy.PlanColumn.Slope)
            , MountUnitBeamRafterTrussData.LengthFlangeBeamTruss
            , MountUnitBeamRafterTrussData.ThicknessTable);
        widthShelfTableMount = canopy.BeamTruss.Truss.ProfileBelt.Height / Mathf.Cos(canopy.PlanColumn.Slope);
        flangeBeamTruss = Instantiate(MountUnitBeamRafterTrussData.FlangeBeamTruss);
        flangeBeamTruss.tag = "FlangeBeamTrussMUBRT";
        flangeRafterTruss = Instantiate(MountUnitBeamRafterTrussData.FlangeRafterTruss);
        flangeRafterTruss.tag = "FlangeRafterTrussMUBRT";
        screwPrefab = MountUnitBeamRafterTrussData.Screw;
        screwPrefab.tag = "ScrewMUBRT";
        nutwPrefab = MountUnitBeamRafterTrussData.Nut;
        nutwPrefab.tag = "NutMUBRT";
        washerwPrefab = MountUnitBeamRafterTrussData.Washer;
        washerwPrefab.tag = "WasherMUBRT";
        yield return null;
    }

    IEnumerator MakeMountBeamRafterTruss()
    {
        if (onHighColumns)
            yCoord = canopy.PlanColumn.SizeByY;
        else
        {
            yCoord = canopy.PlanColumn.SizeByYLow;
            xCoord = canopy.PlanColumn.SizeByX;
        }
        gameObject.transform.SetLocalPositionAndRotation(new Vector3(xCoord, yCoord, zCoord), Quaternion.Euler(0, 0, 0));
        for (int i = 0; i < 2; i++)
        {
            screws.Add(Instantiate(screwPrefab));
            nuts.Add(Instantiate(nutwPrefab));
            for (int j = 0; j < 2; j++)
                washers.Add(Instantiate(washerwPrefab));
        }
        wallTableMountShort.transform.SetParent(gameObject.transform);
        wallTableMountShort.transform.SetLocalPositionAndRotation(new Vector3((canopy.BeamTruss.Truss.ProfileBelt.Length + MountUnitBeamRafterTrussData.ThicknessTable) / 2
            , canopy.ColumnPlug.Thickness + canopy.BeamTruss.Truss.ProfileBelt.Height / 2
            , (canopy.ColumnBodyHigh.Profile.Height + MountUnitBeamRafterTrussData.LengthFlangeBeamTruss) / 2 + MountUnitColumnBeamTrussData.WidthFlangeColumn)
            , Quaternion.Euler(0,90,90));
        wallTableMountLong.transform.SetParent(gameObject.transform);
        wallTableMountLong.transform.SetLocalPositionAndRotation(new Vector3(-(canopy.BeamTruss.Truss.ProfileBelt.Length + MountUnitBeamRafterTrussData.ThicknessTable) / 2
           , canopy.ColumnPlug.Thickness + canopy.BeamTruss.Truss.ProfileBelt.Height / 2 + canopy.BeamTruss.Truss.ProfileBelt.Length * Mathf.Tan(canopy.PlanColumn.Slope)
           , (canopy.ColumnBodyHigh.Profile.Height + MountUnitBeamRafterTrussData.LengthFlangeBeamTruss) / 2 + MountUnitColumnBeamTrussData.WidthFlangeColumn)
           , Quaternion.Euler(0, 90, 90));
        shelfTableMount.transform.SetParent(gameObject.transform);
        shelfTableMount.transform.SetLocalPositionAndRotation(new Vector3(MountUnitBeamRafterTrussData.ThicknessTable / 2 * Mathf.Sin(canopy.PlanColumn.Slope)
           , canopy.ColumnPlug.Thickness + canopy.BeamTruss.Truss.ProfileBelt.Height + MountUnitBeamRafterTrussData.ThicknessTable / 2 + canopy.BeamTruss.Truss.ProfileBelt.Length / 2 * Mathf.Tan(canopy.PlanColumn.Slope)
           //- (widthShelfTableMount - canopy.BeamTruss.Truss.ProfileBelt.Height) / 2 * Mathf.Tan(canopy.PlanColumn.Slope)
           , (canopy.ColumnBodyHigh.Profile.Height + MountUnitBeamRafterTrussData.LengthFlangeBeamTruss) / 2 + MountUnitColumnBeamTrussData.WidthFlangeColumn)
           , Quaternion.Euler(90 + canopy.PlanColumn.SlopeInDegree, 90, 90));
        flangeBeamTruss.transform.SetParent(gameObject.transform);
        offsetHorizFlangeBeamTruss = MountUnitBeamRafterTrussData.WidthFlangeBeamTruss / 2 - (MountUnitBeamRafterTrussData.WidthFlangeBeamTruss - MountUnitBeamRafterTrussData.WidthFlangeBeamTruss * Mathf.Cos(canopy.PlanColumn.Slope)) / 2
            + MountUnitBeamRafterTrussData.ThicknessFlangeBeamTruss / 2 * Mathf.Sin(canopy.PlanColumn.Slope);
        diagonalWidthFlangeBeamTruss = Mathf.Sqrt(Mathf.Pow(MountUnitBeamRafterTrussData.WidthFlangeBeamTruss / 2, 2) + Mathf.Pow(MountUnitBeamRafterTrussData.ThicknessFlangeBeamTruss / 2, 2));
        flangeBeamTruss.transform.SetLocalPositionAndRotation(new Vector3(canopy.BeamTruss.Truss.ProfileBelt.Length / 2 + MountUnitBeamRafterTrussData.ThicknessTable + offsetHorizFlangeBeamTruss
            , canopy.ColumnPlug.Thickness + canopy.BeamTruss.Truss.ProfileBelt.Height - MountUnitBeamRafterTrussData.ThicknessTable * Mathf.Tan(canopy.PlanColumn.Slope)
            - Mathf.Sqrt(Mathf.Pow(diagonalWidthFlangeBeamTruss, 2) - Mathf.Pow(offsetHorizFlangeBeamTruss, 2))
            - (MountUnitBeamRafterTrussData.ThicknessFlangeBeamTruss - MountUnitBeamRafterTrussData.ThicknessTable + MountUnitBeamRafterTrussData.ThicknessFlangeRafterTruss + gapBeamRafterFlange) / Mathf.Cos(canopy.PlanColumn.Slope)
            , (canopy.ColumnBodyHigh.Profile.Height + MountUnitBeamRafterTrussData.LengthFlangeBeamTruss) / 2 + MountUnitColumnBeamTrussData.WidthFlangeColumn)
            , Quaternion.Euler(90 + canopy.PlanColumn.SlopeInDegree, 90, 0));
        flangeRafterTruss.transform.SetParent(gameObject.transform);
        flangeRafterTruss.transform.SetLocalPositionAndRotation(new Vector3(canopy.BeamTruss.Truss.ProfileBelt.Length / 2 + MountUnitBeamRafterTrussData.ThicknessTable + offsetHorizFlangeBeamTruss
           + ((MountUnitBeamRafterTrussData.ThicknessFlangeBeamTruss + MountUnitBeamRafterTrussData.ThicknessFlangeRafterTruss) / 2 + gapBeamRafterFlange) * Mathf.Sin(canopy.PlanColumn.Slope)
           , canopy.ColumnPlug.Thickness + canopy.BeamTruss.Truss.ProfileBelt.Height - MountUnitBeamRafterTrussData.ThicknessTable * Mathf.Tan(canopy.PlanColumn.Slope)
           - Mathf.Sqrt(Mathf.Pow(diagonalWidthFlangeBeamTruss, 2) - Mathf.Pow(offsetHorizFlangeBeamTruss, 2))
           - (MountUnitBeamRafterTrussData.ThicknessFlangeBeamTruss - MountUnitBeamRafterTrussData.ThicknessTable + MountUnitBeamRafterTrussData.ThicknessFlangeRafterTruss + gapBeamRafterFlange) / Mathf.Cos(canopy.PlanColumn.Slope)
           + ((MountUnitBeamRafterTrussData.ThicknessFlangeBeamTruss + MountUnitBeamRafterTrussData.ThicknessFlangeRafterTruss) / 2 + gapBeamRafterFlange) * Mathf.Cos(canopy.PlanColumn.Slope)
           , (canopy.ColumnBodyHigh.Profile.Height + MountUnitBeamRafterTrussData.LengthFlangeBeamTruss) / 2 + MountUnitColumnBeamTrussData.WidthFlangeColumn)
           , Quaternion.Euler(90 + canopy.PlanColumn.SlopeInDegree, 90, 0));
        // Instantiating of 1st screw connection 
        screws[0].transform.SetParent(gameObject.transform);
        screws[0].transform.SetLocalPositionAndRotation(new Vector3(flangeRafterTruss.transform.localPosition.x
            + (MountUnitBeamRafterTrussData.ThicknessFlangeRafterTruss / 2 + MountUnitBeamRafterTrussData.ThicknessWasher + MountUnitBeamRafterTrussData.ThicknessHeadScrew) * Mathf.Sin(canopy.PlanColumn.Slope)
            , flangeRafterTruss.transform.localPosition.y
            + (MountUnitBeamRafterTrussData.ThicknessFlangeRafterTruss / 2 + MountUnitBeamRafterTrussData.ThicknessWasher + MountUnitBeamRafterTrussData.ThicknessHeadScrew) * Mathf.Cos(canopy.PlanColumn.Slope)
            , flangeRafterTruss.transform.localPosition.z - MountUnitBeamRafterTrussData.CenterCenterDistance / 2)
          , Quaternion.Euler(canopy.PlanColumn.SlopeInDegree - 90, 90, 0));
        washers[0].transform.SetParent(gameObject.transform);
        washers[0].transform.SetLocalPositionAndRotation(new Vector3(flangeRafterTruss.transform.localPosition.x
            + (MountUnitBeamRafterTrussData.ThicknessFlangeRafterTruss + MountUnitBeamRafterTrussData.ThicknessWasher) / 2 * Mathf.Sin(canopy.PlanColumn.Slope)
            , flangeRafterTruss.transform.localPosition.y
            + (MountUnitBeamRafterTrussData.ThicknessFlangeRafterTruss + MountUnitBeamRafterTrussData.ThicknessWasher) / 2 * Mathf.Cos(canopy.PlanColumn.Slope)
            , flangeRafterTruss.transform.localPosition.z - MountUnitBeamRafterTrussData.CenterCenterDistance / 2)
          , Quaternion.Euler(canopy.PlanColumn.SlopeInDegree, 90, 0));
        washers[1].transform.SetParent(gameObject.transform);
        washers[1].transform.SetLocalPositionAndRotation(new Vector3(flangeRafterTruss.transform.localPosition.x
            - (MountUnitBeamRafterTrussData.ThicknessFlangeRafterTruss / 2 + gapBeamRafterFlange + MountUnitBeamRafterTrussData.ThicknessFlangeBeamTruss + MountUnitBeamRafterTrussData.ThicknessWasher / 2) * Mathf.Sin(canopy.PlanColumn.Slope)
            , flangeRafterTruss.transform.localPosition.y
            - (MountUnitBeamRafterTrussData.ThicknessFlangeRafterTruss / 2 + gapBeamRafterFlange + MountUnitBeamRafterTrussData.ThicknessFlangeBeamTruss + MountUnitBeamRafterTrussData.ThicknessWasher / 2) * Mathf.Cos(canopy.PlanColumn.Slope)
            , flangeRafterTruss.transform.localPosition.z - MountUnitBeamRafterTrussData.CenterCenterDistance / 2)
          , washers[0].transform.localRotation);
        nuts[0].transform.SetParent(gameObject.transform);
        nuts[0].transform.SetLocalPositionAndRotation(new Vector3(washers[1].transform.localPosition.x
            - (MountUnitBeamRafterTrussData.ThicknessWasher / 2 + MountUnitBeamRafterTrussData.ThicknessHeadNut) * Mathf.Sin(canopy.PlanColumn.Slope)
            , washers[1].transform.localPosition.y
            - (MountUnitBeamRafterTrussData.ThicknessWasher / 2 + MountUnitBeamRafterTrussData.ThicknessHeadNut) * Mathf.Cos(canopy.PlanColumn.Slope)
            , washers[1].transform.localPosition.z)
            , screws[0].transform.localRotation);
        // Instantiating of 2st screw connection 
        screws[1].transform.SetParent(gameObject.transform);
        screws[1].transform.SetLocalPositionAndRotation(new Vector3(screws[0].transform.localPosition.x
          , screws[0].transform.localPosition.y
          , screws[0].transform.localPosition.z + MountUnitBeamRafterTrussData.CenterCenterDistance)
          , screws[0].transform.localRotation);
        washers[2].transform.SetParent(gameObject.transform);
        washers[2].transform.SetLocalPositionAndRotation(new Vector3(washers[0].transform.localPosition.x
          , washers[0].transform.localPosition.y
          , screws[1].transform.localPosition.z)
          , washers[0].transform.localRotation);
        washers[3].transform.SetParent(gameObject.transform);
        washers[3].transform.SetLocalPositionAndRotation(new Vector3(washers[1].transform.localPosition.x
          , washers[1].transform.localPosition.y
          , screws[1].transform.localPosition.z)
          , washers[1].transform.localRotation);
        nuts[1].transform.SetParent(gameObject.transform);
        nuts[1].transform.SetLocalPositionAndRotation(new Vector3(nuts[0].transform.localPosition.x
          , nuts[0].transform.localPosition.y
          , screws[1].transform.localPosition.z)
          , nuts[0].transform.localRotation);
        yield return null;
    }
    GameObject GetGOfromMeshes(string nameGameObject, string nameMaterial, float width, float length, float thickness)
    {
        GameObject gameObject = new(nameGameObject)
        {
            tag = nameGameObject
        };
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        meshFilter.mesh = _3dObjectConstructor.CreatePlate(width, length, thickness, 0);
        Material material = Resources.Load<Material>($"Materials/{nameMaterial}");
        ValAction.ApplyMaterial(meshFilter.mesh, gameObject, material);
        return gameObject;
    }
}
