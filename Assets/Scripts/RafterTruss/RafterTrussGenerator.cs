using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RafterTrussGenerator : MonoBehaviour
{
    private readonly RafterTruss rafterTrussForRead = new();
    private GameObject[] cratesStandart;
    private GameObject rafterTruss;
    // Start is called before the first frame update
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
                cratesStandart[i].transform.localPosition = new Vector3(rafterTrussForRead.Height - rafterTrussForRead.ProfileBelt.Width, rafterTrussForRead.Tail + rafterTrussForRead.PieceMidToExter
                                 + rafterTrussForRead.PlaceOneCrateStandart + rafterTrussForRead.Gap
                                 + (rafterTrussForRead.PlaceOneCrateStandart + rafterTrussForRead.Gap) * i, 0);
                cratesStandart[i].transform.localRotation = Quaternion.Euler(0f, 0f, rafterTrussForRead.AngleCrateInDegree);               
            }
            else
            {
                cratesStandart[i].transform.localPosition = new Vector3(0, rafterTrussForRead.Tail + rafterTrussForRead.PieceMidToExter
                                + rafterTrussForRead.PlaceOneCrateStandart + rafterTrussForRead.Gap
                                + (rafterTrussForRead.PlaceOneCrateStandart + rafterTrussForRead.Gap) * i, 0);
                cratesStandart[i].transform.localRotation = Quaternion.Euler(0f, 0f, -rafterTrussForRead.AngleCrateInDegree);
            }
        }
        if (rafterTrussForRead.HasTwoNonStandartCrate)
        {
            GameObject nonStandartCrateSecond = Object.Instantiate(GameObject.FindGameObjectsWithTag("NonStandartCrateRafterTruss")[0]);
            nonStandartCrateSecond.transform.SetParent(rafterTruss.transform);
            Destroy(nonStandartCrateSecond.GetComponent<CrateRafterTrussTransform>());
            nonStandartCrateSecond.transform.localPosition = new Vector3(rafterTrussForRead.Height - rafterTrussForRead.ProfileBelt.Width, rafterTrussForRead.LengthTop - rafterTrussForRead.Tail - rafterTrussForRead.PerspectWidthHalfNonStandartCrate
                - rafterTrussForRead.DimenOneCrateNonStandart - rafterTrussForRead.GapExter, 0);
            nonStandartCrateSecond.transform.localRotation = Quaternion.Euler(0f, 0f, 180 - rafterTrussForRead.AngleNonStandartCrate);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
