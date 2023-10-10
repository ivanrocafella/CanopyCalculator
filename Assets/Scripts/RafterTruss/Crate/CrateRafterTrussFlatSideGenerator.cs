using Assets.Models;
using Assets.Models.Enums;
using Assets.Services;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CrateRafterTrussFlatSideGenerator : MonoBehaviour
{
    private RafterTruss rafterTruss;
    public StandartNonStandart StandartNonStandart;
    public HeigthLengthProfile HeigthLengthProfile;

    // Start is called before the first frame update
    void Start()
    {
        rafterTruss = GameObject.FindGameObjectsWithTag("RafterTruss")[0].GetComponent<RafterTrussGenerator>().rafterTrussForRead;
        float dimen = HeigthLengthProfile == HeigthLengthProfile.Heigth ? rafterTruss.Truss.ProfileCrate.Height : rafterTruss.Truss.ProfileCrate.Length;
        Mesh mesh = StandartNonStandart == StandartNonStandart.NonStandart ? _3dObjectConstructor.CreateFlatSidePipe(rafterTruss.Truss.ProfileCrate.Thickness, rafterTruss.LengthNonStandartCrate, dimen, rafterTruss.Truss.ProfileCrate.Radius)
            : _3dObjectConstructor.CreateFlatSidePipe(rafterTruss.Truss.ProfileCrate.Thickness, rafterTruss.Truss.LengthCrate, dimen, rafterTruss.Truss.ProfileCrate.Radius);
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
