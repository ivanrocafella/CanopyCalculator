using Assets.Models;
using Assets.Models.Enums;
using Assets.Services;
using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Material = UnityEngine.Material;

public class CrateRafterTrussFlatSideGenerator : MonoBehaviour
{
    private RafterTruss rafterTruss;
    public StandartNonStandart StandartNonStandart;
    public HeigthLengthProfile HeigthLengthProfile;
    public Material material;

    // Start is called before the first frame update
    void Start()
    {
        rafterTruss = GameObject.FindGameObjectWithTag("RafterTruss").GetComponent<RafterTrussGenerator>().rafterTrussForRead;
        float dimen = HeigthLengthProfile == HeigthLengthProfile.Heigth ? rafterTruss.Truss.ProfileCrate.Height : rafterTruss.Truss.ProfileCrate.Length;
        // Setting roundness profile or not 
        float radius = ValAction.withRadius ? rafterTruss.Truss.ProfileCrate.Radius : 0;
        Mesh mesh = StandartNonStandart == StandartNonStandart.NonStandart ? _3dObjectConstructor.CreateFlatSidePipe(rafterTruss.Truss.ProfileCrate.Thickness, rafterTruss.LengthNonStandartCrate, dimen, radius)
            : _3dObjectConstructor.CreateFlatSidePipe(rafterTruss.Truss.ProfileCrate.Thickness, rafterTruss.Truss.LengthCrate, dimen, radius);
        ValAction.ApplyMaterial(mesh, transform.gameObject, material);
    }

    // Update is called once per frames
    void Update()
    {

    }
}
