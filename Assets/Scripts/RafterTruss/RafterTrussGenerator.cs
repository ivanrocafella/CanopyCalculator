using Assets.Models;
using Assets.Models.Enums;
using Assets.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class RafterTrussGenerator : MonoBehaviour
{
    public RafterTruss rafterTrussForRead;
    [NonSerialized]
    public float Step;
    [NonSerialized]
    public KindTruss KindTruss;
    private GameObject[] cratesStandart;
    private GameObject rafterTruss;
    private PlanCanopy planColumn;
    private ProfilePipe columnProfile;
    private Truss truss;
    private string nameColumnProfile;
    private string nameTruss;
    private readonly ColumnPlug columnPlug = new();
    public ProfilePipeDataList profilePipeDataList;
    public TrussDataList trussDataList;
    private MountUnitBeamRafterTruss mountUnitBeamRafterTruss;
    [SerializeField]
    private MountUnitBeamRafterTrussDataList mountUnitBeamRafterTrussDataList;
    private float dimensionConstr;
    // Start is called before the first frame update
    private void Awake()
    {
        planColumn = GameObject.FindGameObjectWithTag("PlanCanopy").GetComponent<PlanCanopyGenerator>().MakePlanCanopy();
        KindTruss = planColumn.KindTrussRafter;
        Step = planColumn.StepRafter;
        nameColumnProfile = GameObject.FindGameObjectWithTag("ColumnHigh").GetComponent<ColumnGenerator>().KindProfile
            .ToString().Insert(5, " ").Replace("_", ".");
        nameTruss = KindTruss.ToString().Insert(2, " ");
        columnProfile = ScriptObjectsAction.GetProfilePipeByName(nameColumnProfile, profilePipeDataList);
        truss = ScriptObjectsAction.GetTrussByName(nameTruss, trussDataList);
        mountUnitBeamRafterTruss = ScriptObjectsAction.GetMountUnitBeamRafterTrussByName(nameTruss, mountUnitBeamRafterTrussDataList);
        dimensionConstr = columnProfile.Height + columnPlug.Thickness * 2 > mountUnitBeamRafterTruss.WidthFlangeBeamTruss + mountUnitBeamRafterTruss.ThicknessTable ?
            columnProfile.Height + columnPlug.Thickness * 2 : mountUnitBeamRafterTruss.WidthFlangeBeamTruss + mountUnitBeamRafterTruss.ThicknessTable;
        rafterTrussForRead = new(truss, planColumn, dimensionConstr)
        {
            Step = Step
        };
        StartCoroutine(MakeRafterTruss());
    }
    void Start()    
    {
    }

    // Update is called once per frame
    void Update()
    {     
    }

    IEnumerator MakeRafterTruss()
    {
        rafterTruss = GameObject.FindGameObjectWithTag("RafterTruss");
        cratesStandart = new GameObject[rafterTrussForRead.CountCratesStandart - 1];
        for (int i = 0; i < cratesStandart.Length; i++)
        {
            cratesStandart[i] = Instantiate(GameObject.FindGameObjectWithTag("StandartCrateRafterTruss"));
            cratesStandart[i].transform.SetParent(rafterTruss.transform);
            Destroy(cratesStandart[i].GetComponent<CrateRafterTrussTransform>());
            if (i % 2 == 0)
            {
                cratesStandart[i].transform.localPosition = new Vector3(rafterTrussForRead.Truss.Height - rafterTrussForRead.Truss.ProfileBelt.Height, rafterTrussForRead.Tail + rafterTrussForRead.PieceMidToExter
                                 + rafterTrussForRead.PlaceOneCrateStandart + rafterTrussForRead.Truss.Gap
                                 + (rafterTrussForRead.PlaceOneCrateStandart + rafterTrussForRead.Truss.Gap) * i, 0);
                cratesStandart[i].transform.localRotation = Quaternion.Euler(0f, 0f, rafterTrussForRead.Truss.AngleCrateInDegree);
            }
            else
            {
                cratesStandart[i].transform.localPosition = new Vector3(0, rafterTrussForRead.Tail + rafterTrussForRead.PieceMidToExter
                                + rafterTrussForRead.PlaceOneCrateStandart + rafterTrussForRead.Truss.Gap
                                + (rafterTrussForRead.PlaceOneCrateStandart + rafterTrussForRead.Truss.Gap) * i, 0);
                cratesStandart[i].transform.localRotation = Quaternion.Euler(0f, 0f, -rafterTrussForRead.Truss.AngleCrateInDegree);
            }
        }
        if (rafterTrussForRead.HasTwoNonStandartCrate)
        {
            GameObject nonStandartCrateSecond = Instantiate(GameObject.FindGameObjectWithTag("NonStandartCrateRafterTruss"));
            nonStandartCrateSecond.transform.SetParent(rafterTruss.transform);
            Destroy(nonStandartCrateSecond.GetComponent<CrateRafterTrussTransform>());
            nonStandartCrateSecond.transform.localPosition = new Vector3(rafterTrussForRead.Truss.Height - rafterTrussForRead.Truss.ProfileBelt.Height, rafterTrussForRead.LengthTop - rafterTrussForRead.Tail - rafterTrussForRead.PerspectWidthHalfNonStandartCrate
                - rafterTrussForRead.DimenOneCrateNonStandart - rafterTrussForRead.Truss.GapExter, 0);
            nonStandartCrateSecond.transform.localRotation = Quaternion.Euler(0f, 0f, 180 - rafterTrussForRead.AngleNonStandartCrate);
        }
        yield return null;
    }
}
