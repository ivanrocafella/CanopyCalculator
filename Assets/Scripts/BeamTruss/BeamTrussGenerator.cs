using Assets.Models;
using Assets.Models.Enums;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class BeamTrussGenerator : MonoBehaviour
{
    private string path;
    public BeamTruss beamTrussForRead;
    public KindTruss KindTruss;
    private GameObject[] cratesStandart;
    private GameObject beamTruss;
    private PlanColumn planColumn;
    // Start is called before the first frame update
    private void Awake()
    {
        planColumn = GameObject.FindGameObjectsWithTag("Canopy")[0].GetComponent<CanopyGenerator>().MakePlanColumn();
        path = Path.Combine(Application.dataPath, "JSONs", "Trusses.json");
        beamTrussForRead = new(KindTruss.ToString().Insert(2, " "), path, planColumn);
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
        beamTruss = GameObject.FindGameObjectsWithTag("BeamTruss")[0];
        cratesStandart = new GameObject[beamTrussForRead.CountCratesStandart - 1];
        for (int i = 0; i < cratesStandart.Length; i++)
        {
            cratesStandart[i] = Object.Instantiate(GameObject.FindGameObjectsWithTag("StandartCrateBeamTruss")[0]);
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
            GameObject nonStandartCrateSecond = Object.Instantiate(GameObject.FindGameObjectsWithTag("NonStandartCrateBeamTruss")[0]);
            nonStandartCrateSecond.transform.SetParent(beamTruss.transform);
            Destroy(nonStandartCrateSecond.GetComponent<CrateBeamTrussTransform>());
            nonStandartCrateSecond.transform.localPosition = new Vector3(beamTrussForRead.Truss.Height - beamTrussForRead.Truss.ProfileBelt.Height, beamTrussForRead.LengthTop - beamTrussForRead.Tail - beamTrussForRead.PerspectWidthHalfNonStandartCrate
                - beamTrussForRead.DimenOneCrateNonStandart - beamTrussForRead.Truss.GapExter, 0);
            nonStandartCrateSecond.transform.localRotation = Quaternion.Euler(0f, 0f, 180 - beamTrussForRead.AngleNonStandartCrate);
        }
        yield return null;
    }
}

