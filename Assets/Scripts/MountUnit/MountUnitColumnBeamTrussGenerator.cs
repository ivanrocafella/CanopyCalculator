using Assets.Models;
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

    private void Awake()
    {
        flangeColumnPrefab = MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].FlangeColumn;
        flangeColumnPrefab.gameObject.tag = "FlangeColumnMUCBT";
        flangeBeamTrussPrefab = MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].FlangeBeamTruss;
        flangeBeamTrussPrefab.gameObject.tag = "FlangeBeamTrussMUCBT";
        screwPrefab = MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].Screw;
        screwPrefab.gameObject.tag = "ScrewMUCBT";
        washerPrefab = MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].Washer;
        washerPrefab.gameObject.tag = "WasherMUCBT";
        nutPrefab = MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].Nut;
        nutPrefab.gameObject.tag = "NutMUCBT";
        canopy = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(e => e.CompareTag("Canopy")).GetComponent<CanopyGenerator>().Canopy;
    }
    // Start is called before the first frame update
    void Start()
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
            , - (MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].ThicknessFlangeBeamTruss - canopy.ColumnPlug.Thickness
            + MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].ThicknessFlangeColumn / 2) 
            , canopy.ColumnBodyHigh.Profile.Length / 2 + MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].WidthFlangeColumn / 2);
        flangeBeamTruss.transform.SetParent(gameObject.transform);
        flangeBeamTruss.transform.localPosition = new Vector3(0
            , canopy.ColumnPlug.Thickness - MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].ThicknessFlangeBeamTruss / 2
            , canopy.ColumnBodyHigh.Profile.Length / 2 + MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].WidthFlangeColumn / 2);
        // Instantiating of 1st screw connection 
        screws[0].transform.SetParent(gameObject.transform);
        screws[0].transform.localPosition = new Vector3(MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].CenterCenterDistance / 2
           , canopy.ColumnPlug.Thickness + MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].ThicknessWasher + MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].ThicknessHeadScrew
           , canopy.ColumnBodyHigh.Profile.Length / 2 + MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].WidthFlangeColumn / 2);
        washers[0].transform.SetParent(gameObject.transform);
        washers[0].transform.localPosition = new Vector3(MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].CenterCenterDistance / 2
           , canopy.ColumnPlug.Thickness + MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].ThicknessWasher / 2
           , canopy.ColumnBodyHigh.Profile.Length / 2 + MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].WidthFlangeColumn / 2);
        washers[1].transform.SetParent(gameObject.transform);
        washers[1].transform.localPosition = new Vector3(MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].CenterCenterDistance / 2
           , canopy.ColumnPlug.Thickness - MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].ThicknessWasher/2
           - MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].ThicknessFlangeColumn - MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].ThicknessFlangeBeamTruss
           , canopy.ColumnBodyHigh.Profile.Length / 2 + MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].WidthFlangeColumn / 2);
        nuts[0].transform.SetParent(gameObject.transform);
        nuts[0].transform.localPosition = new Vector3(MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].CenterCenterDistance / 2
           , canopy.ColumnPlug.Thickness - MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].ThicknessWasher - MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].ThicknessHeadNut
           - MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].ThicknessFlangeColumn - MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].ThicknessFlangeBeamTruss
           , canopy.ColumnBodyHigh.Profile.Length / 2 + MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].WidthFlangeColumn / 2);
        // Instantiating of 2st screw connection 
        screws[1].transform.SetParent(gameObject.transform);
        screws[1].transform.localPosition = new Vector3(-MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].CenterCenterDistance / 2
           , canopy.ColumnPlug.Thickness + MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].ThicknessWasher + MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].ThicknessHeadScrew
           , canopy.ColumnBodyHigh.Profile.Length / 2 + MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].WidthFlangeColumn / 2);
        washers[2].transform.SetParent(gameObject.transform);
        washers[2].transform.localPosition = new Vector3(-MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].CenterCenterDistance / 2
           , canopy.ColumnPlug.Thickness + MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].ThicknessWasher / 2
           , canopy.ColumnBodyHigh.Profile.Length / 2 + MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].WidthFlangeColumn / 2);
        washers[3].transform.SetParent(gameObject.transform);
        washers[3].transform.localPosition = new Vector3(-MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].CenterCenterDistance / 2
           , canopy.ColumnPlug.Thickness - MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].ThicknessWasher / 2
           - MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].ThicknessFlangeColumn - MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].ThicknessFlangeBeamTruss
           , canopy.ColumnBodyHigh.Profile.Length / 2 + MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].WidthFlangeColumn / 2);
        nuts[1].transform.SetParent(gameObject.transform);
        nuts[1].transform.localPosition = new Vector3(-MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].CenterCenterDistance / 2
           , canopy.ColumnPlug.Thickness - MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].ThicknessWasher - MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].ThicknessHeadNut
           - MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].ThicknessFlangeColumn - MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].ThicknessFlangeBeamTruss
           , canopy.ColumnBodyHigh.Profile.Length / 2 + MountUnitColumnBeamTrussDataList.mountUnitColumnBeamTrussDatas[0].WidthFlangeColumn / 2);
    }   

    // Update is called once per frame
    void Update()
    {
        
    }
}
