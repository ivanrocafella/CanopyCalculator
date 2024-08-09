using Assets.Models;
using Assets.Models.Enums;
using Assets.Services;
using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Material = UnityEngine.Material;

public class FlatSideCrateBeamTrussGenerator : MonoBehaviour
{
    private BeamTruss beamTruss;
    public StandartNonStandart StandartNonStandart;
    public HeigthLengthProfile HeigthLengthProfile;
    public Material material;

    // Start is called before the first frame update
    void Start()
    {
        beamTruss = GameObject.FindGameObjectsWithTag("BeamTruss")[0].GetComponent<BeamTrussGenerator>().beamTrussForRead;
        float dimen = HeigthLengthProfile == HeigthLengthProfile.Heigth ? beamTruss.Truss.ProfileCrate.Height : beamTruss.Truss.ProfileCrate.Length;
        // Setting roundness profile or not 
        float radius = ValAction.withRadius ? beamTruss.Truss.ProfileCrate.Radius : 0;            
        Mesh mesh = StandartNonStandart == StandartNonStandart.NonStandart ? _3dObjectConstructor.CreatePlate(beamTruss.Truss.ProfileCrate.Thickness, beamTruss.LengthNonStandartCrate, dimen, radius)
            : _3dObjectConstructor.CreatePlate(beamTruss.Truss.ProfileCrate.Thickness, beamTruss.Truss.LengthCrate, dimen, radius);
        ValAction.ApplyMaterial(mesh, transform.gameObject, material);
    }

    // Update is called once per frames
    void Update()
    {

    }
}
