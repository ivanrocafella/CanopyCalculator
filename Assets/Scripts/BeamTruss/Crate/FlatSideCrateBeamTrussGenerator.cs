using Assets.Models;
using Assets.Models.Enums;
using Assets.Services;
using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FlatSideCrateBeamTrussGenerator : MonoBehaviour
{
    private BeamTruss beamTruss;
    public StandartNonStandart StandartNonStandart;
    public HeigthLengthProfile HeigthLengthProfile;

    // Start is called before the first frame update
    void Start()
    {
        beamTruss = GameObject.FindGameObjectsWithTag("BeamTruss")[0].GetComponent<BeamTrussGenerator>().beamTrussForRead;
        float dimen = HeigthLengthProfile == HeigthLengthProfile.Heigth ? beamTruss.Truss.ProfileCrate.Height : beamTruss.Truss.ProfileCrate.Length;
        // Setting roundness profile or not 
        float radius = ValAction.withRadius ? beamTruss.Truss.ProfileCrate.Radius : 0;            
        Mesh mesh = StandartNonStandart == StandartNonStandart.NonStandart ? _3dObjectConstructor.CreateFlatSidePipe(beamTruss.Truss.ProfileCrate.Thickness, beamTruss.LengthNonStandartCrate, dimen, radius)
            : _3dObjectConstructor.CreateFlatSidePipe(beamTruss.Truss.ProfileCrate.Thickness, beamTruss.Truss.LengthCrate, dimen, radius);
        ApplyMaterial(mesh, "Standard", Color.black);
    }

    // Update is called once per frames
    void Update()
    {

    }

    void ApplyMaterial(Mesh mesh, string shaderName, Color color)
    {
        GetComponent<MeshFilter>().mesh = mesh;
        MeshRenderer meshRenderer = !gameObject.GetComponent<MeshRenderer>()
             ? gameObject.AddComponent<MeshRenderer>()
             : gameObject.GetComponent<MeshRenderer>();
        UnityEngine.Material material = new(Shader.Find(shaderName));
        material.color = color;
        meshRenderer.material = material;
    }
}
