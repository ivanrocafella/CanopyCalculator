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
using Object = UnityEngine.Object;

public class BeamTrussGenerator : MonoBehaviour
{
    public BeamTruss beamTrussForRead;
    [NonSerialized]
    public KindTruss KindTruss;
    private GameObject[] cratesStandart;
    private GameObject beamTruss;
    private PlanCanopy planColumn;
    private ProfilePipe columnProfile;
    private Truss truss;
    private string nameColumnProfile;
    private string nameTruss;
    private readonly ColumnPlug columnPlug = new();
    public ProfilePipeDataList profilePipeDataList;
    public TrussDataList trussDataList;
    // Start is called before the first frame update
    private void Awake()
    {
        planColumn = GameObject.FindGameObjectWithTag("PlanCanopy").GetComponent<PlanCanopyGenerator>().MakePlanCanopy();
        KindTruss = planColumn.KindTrussBeam;
        nameTruss = KindTruss.ToString().Insert(2, " ");
        nameColumnProfile = GameObject.FindGameObjectWithTag("ColumnHigh").GetComponent<ColumnGenerator>().KindProfile
            .ToString().Insert(5, " ").Replace("_", ".");
        truss = ScriptObjectsAction.GetTrussByName(nameTruss, trussDataList);
        columnProfile = ScriptObjectsAction.GetProfilePipeByName(nameColumnProfile, profilePipeDataList);
        beamTrussForRead = new(truss, planColumn, columnProfile.Height + columnPlug.Thickness * 2);
        StartCoroutine(MakeBeamTruss());
    }

    void Start()
    {       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MakeBeamTruss()
    {
        beamTruss = GameObject.FindGameObjectWithTag("BeamTruss");
        cratesStandart = new GameObject[beamTrussForRead.CountCratesStandart - 1];
        for (int i = 0; i < cratesStandart.Length; i++)
        {
            cratesStandart[i] = Instantiate(GameObject.FindGameObjectWithTag("StandartCrateBeamTruss"));
            cratesStandart[i].transform.SetParent(beamTruss.transform);
            Destroy(cratesStandart[i].GetComponent<CrateBeamTrussTransform>());
            if (i % 2 == 0)
            {
                cratesStandart[i].transform.localPosition = new Vector3(beamTrussForRead.Truss.Height - beamTrussForRead.Truss.ProfileBelt.Height, beamTrussForRead.Tail + beamTrussForRead.PieceMidToExter
                                 + beamTrussForRead.PlaceOneCrateStandart + beamTrussForRead.Truss.Gap
                                 + (beamTrussForRead.PlaceOneCrateStandart + beamTrussForRead.Truss.Gap) * i, 0);
                cratesStandart[i].transform.localRotation = Quaternion.Euler(0f, 0f, beamTrussForRead.Truss.AngleCrateInDegree);
            }
            else
            {
                cratesStandart[i].transform.localPosition = new Vector3(0, beamTrussForRead.Tail + beamTrussForRead.PieceMidToExter
                                + beamTrussForRead.PlaceOneCrateStandart + beamTrussForRead.Truss.Gap
                                + (beamTrussForRead.PlaceOneCrateStandart + beamTrussForRead.Truss.Gap) * i, 0);
                cratesStandart[i].transform.localRotation = Quaternion.Euler(0f, 0f, -beamTrussForRead.Truss.AngleCrateInDegree);
            }
        }
        if (beamTrussForRead.HasTwoNonStandartCrate)
        {
            GameObject nonStandartCrateSecond = Object.Instantiate(GameObject.FindGameObjectWithTag("NonStandartCrateBeamTruss"));
            nonStandartCrateSecond.transform.SetParent(beamTruss.transform);
            Destroy(nonStandartCrateSecond.GetComponent<CrateBeamTrussTransform>());
            nonStandartCrateSecond.transform.localPosition = new Vector3(beamTrussForRead.Truss.Height - beamTrussForRead.Truss.ProfileBelt.Height, beamTrussForRead.LengthTop - beamTrussForRead.Tail - beamTrussForRead.PerspectWidthHalfNonStandartCrate
                - beamTrussForRead.DimenOneCrateNonStandart - beamTrussForRead.Truss.GapExter, 0);
            nonStandartCrateSecond.transform.localRotation = Quaternion.Euler(0f, 0f, 180 - beamTrussForRead.AngleNonStandartCrate);
        }
        yield return null;
    }
}

