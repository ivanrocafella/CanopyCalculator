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
    private const int thicknessTableMount = 4;

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
        MountUnitBeamRafterTrussData = MountUnitBeamRafterTrussDataList.mountUnitBeamRafterTrussDatas.FirstOrDefault(e => e.RafterTrussName == canopy.RafterTruss.Truss.Name);
        MountUnitColumnBeamTrussData = MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas.FirstOrDefault(e => e.BeamTrussName == canopy.BeamTruss.Truss.Name);
        wallTableMountShort = GetGOfromMeshes("WallTableMount"
            , "MountUnitBeam"
            , canopy.BeamTruss.Truss.ProfileBelt.Height
            , MountUnitBeamRafterTrussData.LengthFlangeBeamTruss
            , thicknessTableMount);
        wallTableMountLong = Instantiate(GameObject.FindGameObjectWithTag("WallTableMount"));
        shelfTableMount = GetGOfromMeshes("ShelfTableMount"
            , "MountUnitBeam"
            , canopy.BeamTruss.Truss.ProfileBelt.Height / Mathf.Cos(canopy.PlanColumn.Slope)
            , MountUnitBeamRafterTrussData.LengthFlangeBeamTruss
            , thicknessTableMount);
        flangeBeamTruss = Instantiate(MountUnitBeamRafterTrussData.FlangeBeamTruss);
        flangeBeamTruss.tag = "FlangeBeamTrussMUBRT";
        flangeRafterTruss = Instantiate(MountUnitBeamRafterTrussData.FlangeRafterTruss);
        flangeRafterTruss.tag = "FlangeRafterTrussMUBRT";
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
        wallTableMountShort.transform.SetParent(gameObject.transform);
        wallTableMountShort.transform.SetLocalPositionAndRotation(new Vector3((canopy.BeamTruss.Truss.ProfileBelt.Length + thicknessTableMount) / 2
            , canopy.ColumnPlug.Thickness + canopy.BeamTruss.Truss.ProfileBelt.Height / 2
            , (canopy.ColumnBodyHigh.Profile.Height + MountUnitBeamRafterTrussData.LengthFlangeBeamTruss) / 2 + MountUnitColumnBeamTrussData.WidthFlangeColumn)
            , Quaternion.Euler(0,90,90));
        wallTableMountLong.transform.SetParent(gameObject.transform);
        wallTableMountLong.transform.SetLocalPositionAndRotation(new Vector3(-(canopy.BeamTruss.Truss.ProfileBelt.Length + thicknessTableMount) / 2
           , canopy.ColumnPlug.Thickness + canopy.BeamTruss.Truss.ProfileBelt.Height / 2 + canopy.BeamTruss.Truss.ProfileBelt.Length * Mathf.Tan(canopy.PlanColumn.Slope)
           , (canopy.ColumnBodyHigh.Profile.Height + MountUnitBeamRafterTrussData.LengthFlangeBeamTruss) / 2 + MountUnitColumnBeamTrussData.WidthFlangeColumn)
           , Quaternion.Euler(0, 90, 90));
        shelfTableMount.transform.SetParent(gameObject.transform);
        shelfTableMount.transform.SetLocalPositionAndRotation(new Vector3(0
           , canopy.ColumnPlug.Thickness + canopy.BeamTruss.Truss.ProfileBelt.Height + thicknessTableMount / 2 + canopy.BeamTruss.Truss.ProfileBelt.Length / 2 * Mathf.Tan(canopy.PlanColumn.Slope)
           , (canopy.ColumnBodyHigh.Profile.Height + MountUnitBeamRafterTrussData.LengthFlangeBeamTruss) / 2 + MountUnitColumnBeamTrussData.WidthFlangeColumn)
           , Quaternion.Euler(90 + canopy.PlanColumn.SlopeInDegree, 90, 90));
        flangeBeamTruss.transform.SetParent(gameObject.transform);
        flangeBeamTruss.transform.SetLocalPositionAndRotation(new Vector3(canopy.BeamTruss.Truss.ProfileBelt.Length / 2 + thicknessTableMount
            , canopy.ColumnPlug.Thickness + canopy.BeamTruss.Truss.ProfileBelt.Height / 2
            , (canopy.ColumnBodyHigh.Profile.Height + MountUnitBeamRafterTrussData.LengthFlangeBeamTruss) / 2 + MountUnitColumnBeamTrussData.WidthFlangeColumn)
            , Quaternion.Euler(0, 90, 0));
        flangeRafterTruss.transform.SetParent(gameObject.transform);
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
