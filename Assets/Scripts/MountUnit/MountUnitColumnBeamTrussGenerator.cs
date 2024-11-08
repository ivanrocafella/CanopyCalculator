using Assets.Models;
using Assets.Scripts.SOdata;
using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MountUnitColumnBeamTrussGenerator : MonoBehaviour
{
    public float xCoord; 
    public float yCoord;
    public float zCoord;
    public bool backSideLocation;
    public bool onHighColumns;
    [SerializeField]
    private MountUnitColumnBeamTrussDataList MountUnitColumnBeamTrussDataList;
    private MountUnitColumnBeamTrussData MountUnitColumnBeamTrussData;
    private Canopy canopy;
    private GameObject flangeColumnPrefab;
    private GameObject flangeBeamTrussPrefab;
    private GameObject screwPrefab;
    private GameObject washerPrefab;
    private GameObject nutPrefab;
    private GameObject flangeColumn;
    private GameObject flangeBeamTruss;
    private readonly List<GameObject> screws = new();
    private readonly List<GameObject> washers = new();
    private readonly List<GameObject> nuts = new();
    public MountUnitColumnBeamTruss MountUnitColumnBeamTruss { get; private set; }
    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetMountUnitColumnBeamTrussData());
        StartCoroutine(MakeMountUnitColumnBeamTruss());
    }   
    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator GetMountUnitColumnBeamTrussData()
    {
        canopy = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(e => e.CompareTag("Canopy")).GetComponent<CanopyGenerator>().Canopy;
        MountUnitColumnBeamTruss = ScriptObjectsAction.GetMountUnitColumnBeamTrussByName(canopy.BeamTruss.Truss.Name, MountUnitColumnBeamTrussDataList);
        MountUnitColumnBeamTrussData = MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas.FirstOrDefault(e => e.BeamTrussName == canopy.BeamTruss.Truss.Name);
        flangeColumnPrefab = MountUnitColumnBeamTrussData.FlangeColumn;
        flangeColumnPrefab.tag = "FlangeColumnMUCBT";
        flangeBeamTrussPrefab = MountUnitColumnBeamTrussData.FlangeBeamTruss;
        flangeBeamTrussPrefab.tag = "FlangeBeamTrussMUCBT";
        screwPrefab = MountUnitColumnBeamTrussData.Screw;
        screwPrefab.tag = "ScrewMUCBT";
        washerPrefab = MountUnitColumnBeamTrussData.Washer;
        washerPrefab.tag = "WasherMUCBT";
        nutPrefab = MountUnitColumnBeamTrussData.Nut;
        nutPrefab.tag = "NutMUCBT";
        yield return null;
    }

    IEnumerator MakeMountUnitColumnBeamTruss()
    {
        if (onHighColumns)
            yCoord = canopy.PlanColumn.SizeByY;
        else
        {
            yCoord = canopy.PlanColumn.SizeByYLow;
            xCoord = canopy.PlanColumn.SizeByX;
        }
        gameObject.transform.SetLocalPositionAndRotation(new Vector3(xCoord, yCoord, zCoord), backSideLocation ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0));
        flangeColumn = Instantiate(flangeColumnPrefab);
        flangeBeamTruss = Instantiate(flangeBeamTrussPrefab);
        for (int i = 0; i < 2; i++)
        {
            screws.Add(Instantiate(screwPrefab));
            nuts.Add(Instantiate(nutPrefab));
            for (int j = 0; j < 2; j++)
                washers.Add(Instantiate(washerPrefab));
        }
        flangeColumn.transform.SetParent(gameObject.transform);
        flangeColumn.transform.localPosition = new Vector3(0
            , -(MountUnitColumnBeamTrussData.ThicknessFlangeBeamTruss - canopy.ColumnPlug.Thickness
            + MountUnitColumnBeamTrussData.ThicknessFlangeColumn / 2)
            , canopy.ColumnBodyHigh.Profile.Length / 2 + MountUnitColumnBeamTrussData.WidthFlangeColumn / 2);
        flangeBeamTruss.transform.SetParent(gameObject.transform);
        flangeBeamTruss.transform.localPosition = new Vector3(0
            , canopy.ColumnPlug.Thickness - MountUnitColumnBeamTrussData.ThicknessFlangeBeamTruss / 2
            , canopy.ColumnBodyHigh.Profile.Length / 2 + MountUnitColumnBeamTrussData.WidthFlangeColumn / 2);
        // Instantiating of 1st screw connection 
        screws[0].transform.SetParent(gameObject.transform);
        screws[0].transform.localPosition = new Vector3(MountUnitColumnBeamTrussData.CenterCenterDistance / 2
           , canopy.ColumnPlug.Thickness + MountUnitColumnBeamTrussData.ThicknessWasher + MountUnitColumnBeamTrussData.ThicknessHeadScrew
           , canopy.ColumnBodyHigh.Profile.Length / 2 + MountUnitColumnBeamTrussData.WidthFlangeColumn / 2);
        washers[0].transform.SetParent(gameObject.transform);
        washers[0].transform.localPosition = new Vector3(MountUnitColumnBeamTrussData.CenterCenterDistance / 2
           , canopy.ColumnPlug.Thickness + MountUnitColumnBeamTrussData.ThicknessWasher / 2
           , canopy.ColumnBodyHigh.Profile.Length / 2 + MountUnitColumnBeamTrussData.WidthFlangeColumn / 2);
        washers[1].transform.SetParent(gameObject.transform);
        washers[1].transform.localPosition = new Vector3(MountUnitColumnBeamTrussData.CenterCenterDistance / 2
           , canopy.ColumnPlug.Thickness - MountUnitColumnBeamTrussData.ThicknessWasher / 2
           - MountUnitColumnBeamTrussData.ThicknessFlangeColumn - MountUnitColumnBeamTrussData.ThicknessFlangeBeamTruss
           , canopy.ColumnBodyHigh.Profile.Length / 2 + MountUnitColumnBeamTrussData.WidthFlangeColumn / 2);
        nuts[0].transform.SetParent(gameObject.transform);
        nuts[0].transform.localPosition = new Vector3(MountUnitColumnBeamTrussData.CenterCenterDistance / 2
           , canopy.ColumnPlug.Thickness - MountUnitColumnBeamTrussData.ThicknessWasher - MountUnitColumnBeamTrussData.ThicknessHeadNut
           - MountUnitColumnBeamTrussData.ThicknessFlangeColumn - MountUnitColumnBeamTrussData.ThicknessFlangeBeamTruss
           , canopy.ColumnBodyHigh.Profile.Length / 2 + MountUnitColumnBeamTrussData.WidthFlangeColumn / 2);
        // Instantiating of 2nd screw connection 
        screws[1].transform.SetParent(gameObject.transform);
        screws[1].transform.localPosition = new Vector3(-MountUnitColumnBeamTrussData.CenterCenterDistance / 2
           , canopy.ColumnPlug.Thickness + MountUnitColumnBeamTrussData.ThicknessWasher + MountUnitColumnBeamTrussData.ThicknessHeadScrew
           , canopy.ColumnBodyHigh.Profile.Length / 2 + MountUnitColumnBeamTrussData.WidthFlangeColumn / 2);
        washers[2].transform.SetParent(gameObject.transform);
        washers[2].transform.localPosition = new Vector3(-MountUnitColumnBeamTrussData.CenterCenterDistance / 2
           , canopy.ColumnPlug.Thickness + MountUnitColumnBeamTrussData.ThicknessWasher / 2
           , canopy.ColumnBodyHigh.Profile.Length / 2 + MountUnitColumnBeamTrussData.WidthFlangeColumn / 2);
        washers[3].transform.SetParent(gameObject.transform);
        washers[3].transform.localPosition = new Vector3(-MountUnitColumnBeamTrussData.CenterCenterDistance / 2
           , canopy.ColumnPlug.Thickness - MountUnitColumnBeamTrussData.ThicknessWasher / 2
           - MountUnitColumnBeamTrussData.ThicknessFlangeColumn - MountUnitColumnBeamTrussData.ThicknessFlangeBeamTruss
           , canopy.ColumnBodyHigh.Profile.Length / 2 + MountUnitColumnBeamTrussData.WidthFlangeColumn / 2);
        nuts[1].transform.SetParent(gameObject.transform);
        nuts[1].transform.localPosition = new Vector3(-MountUnitColumnBeamTrussData.CenterCenterDistance / 2
           , canopy.ColumnPlug.Thickness - MountUnitColumnBeamTrussData.ThicknessWasher - MountUnitColumnBeamTrussData.ThicknessHeadNut
           - MountUnitColumnBeamTrussData.ThicknessFlangeColumn - MountUnitColumnBeamTrussData.ThicknessFlangeBeamTruss
           , canopy.ColumnBodyHigh.Profile.Length / 2 + MountUnitColumnBeamTrussData.WidthFlangeColumn / 2);
        yield return null;
    }
}
