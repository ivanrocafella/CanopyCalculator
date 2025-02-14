using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BeltBottomRafterTrussTransform : MonoBehaviour
{
    private RafterTruss rafterTruss;
    // Start is called before the first frame update
    void Start()
    {
        rafterTruss = GameObject.FindGameObjectWithTag("RafterTruss").GetComponent<RafterTrussGenerator>().rafterTrussForRead;
        transform.localPosition = new Vector3(rafterTruss.Truss.Height - rafterTruss.Truss.ProfileBelt.Height
           , (rafterTruss.LengthTop - rafterTruss.LengthBottom) / 2, 0);

    }

    // Update is called once per frame
    void Update()
    {
    }
}
