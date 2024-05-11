using Assets.Models;
using Assets.Services;
using Assets.Utils;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;
using Material = UnityEngine.Material;

public class RafterTrussBeltFlatSideGenerator : MonoBehaviour
{
    private RafterTruss rafterTruss;
    public KindLength KindLength;
    public Material material;

    // Start is called before the first frame update
    void Start()
    {
        rafterTruss = GameObject.FindGameObjectWithTag("RafterTruss").GetComponent<RafterTrussGenerator>().rafterTrussForRead;
        float length = KindLength switch
        {
            KindLength.Short => rafterTruss.LengthBottom,
            _ => rafterTruss.LengthTop
        };
        // Setting roundness profile or not 
        Mesh mesh = ValAction.withRadius ? _3dObjectConstructor.CreateFlatSidePipe(rafterTruss.Truss.ProfileBelt.Thickness, length, rafterTruss.Truss.ProfileBelt.Height, rafterTruss.Truss.ProfileBelt.Radius) :
                                 _3dObjectConstructor.CreateFlatSidePipe(rafterTruss.Truss.ProfileBelt.Thickness, length, rafterTruss.Truss.ProfileBelt.Height, 0);
        ValAction.ApplyMaterial(mesh, transform.gameObject, material);
    }

    // Update is called once per frames
    void Update()
    {
        
    }
}
