using Assets.Models;
using Assets.Models.Enums;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class RafterTrussGenerator : MonoBehaviour
{
    private string path;
    public RafterTruss rafterTrussForRead;
    public float Step;
    public KindTruss KindTruss;
    private GameObject[] cratesStandart;
    private GameObject rafterTruss;
    private PlanColumn planColumn;
    // Start is called before the first frame update
    private void Awake()
    {
        planColumn = GameObject.FindGameObjectsWithTag("Canopy")[0].GetComponent<CanopyGenerator>().MakePlanColumn();
        path = Path.Combine(Application.dataPath, "JSONs", "Trusses.json");
        rafterTrussForRead = new(KindTruss.ToString().Insert(2, " "), path, planColumn);
        rafterTrussForRead.Step = Step;
    }
    void Start()    
    {       
        rafterTruss = GameObject.FindGameObjectsWithTag("RafterTruss")[0];
        cratesStandart = new GameObject[rafterTrussForRead.CountCratesStandart - 1];
        for (int i = 0; i < cratesStandart.Length; i++)
        {
            cratesStandart[i] = Object.Instantiate(GameObject.FindGameObjectsWithTag("StandartCrateRafterTruss")[0]);
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
            GameObject nonStandartCrateSecond = Object.Instantiate(GameObject.FindGameObjectsWithTag("NonStandartCrateRafterTruss")[0]);    
            nonStandartCrateSecond.transform.SetParent(rafterTruss.transform);
            Destroy(nonStandartCrateSecond.GetComponent<CrateRafterTrussTransform>());
            nonStandartCrateSecond.transform.localPosition = new Vector3(rafterTrussForRead.Truss.Height - rafterTrussForRead.Truss.ProfileBelt.Height, rafterTrussForRead.LengthTop - rafterTrussForRead.Tail - rafterTrussForRead.PerspectWidthHalfNonStandartCrate
                - rafterTrussForRead.DimenOneCrateNonStandart - rafterTrussForRead.Truss.GapExter, 0);
            nonStandartCrateSecond.transform.localRotation = Quaternion.Euler(0f, 0f, 180 - rafterTrussForRead.AngleNonStandartCrate);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
