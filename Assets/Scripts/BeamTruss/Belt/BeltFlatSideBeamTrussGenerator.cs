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

public class BeltFlatSideBeamTrussGenerator : MonoBehaviour
{
    private BeamTruss beamTruss;
    public KindLength KindLength;
    public Material material;

    // Start is called before the first frame update
    void Start()
    {
        beamTruss = GameObject.FindGameObjectsWithTag("BeamTruss")[0].GetComponent<BeamTrussGenerator>().beamTrussForRead;
        float length = KindLength switch
        {
            KindLength.Short => beamTruss.LengthBottom,
            _ => beamTruss.LengthTop
        };
        // Setting roundness profile or not 
        Mesh mesh = ValAction.withRadius ? _3dObjectConstructor.CreatePlate(beamTruss.Truss.ProfileBelt.Thickness, length, beamTruss.Truss.ProfileBelt.Height, beamTruss.Truss.ProfileBelt.Radius) :
                                 _3dObjectConstructor.CreatePlate(beamTruss.Truss.ProfileBelt.Thickness, length, beamTruss.Truss.ProfileBelt.Height, 0);
        ValAction.ApplyMaterial(mesh, transform.gameObject, material);
    }

    // Update is called once per frames
    void Update()
    {
        
    }
}
