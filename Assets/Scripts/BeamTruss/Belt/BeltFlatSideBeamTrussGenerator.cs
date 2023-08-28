using Assets.Models;
using Assets.Services;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;

public class BeltFlatSideBeamTrussGenerator : MonoBehaviour
{
    private string path;
    private BeamTruss beamTruss;
    public KindLength KindLength;

    // Start is called before the first frame update
    void Start()
    {
        beamTruss = GameObject.FindGameObjectsWithTag("BeamTruss")[0].GetComponent<BeamTrussGenerator>().beamTrussForRead;
        float length = KindLength switch
        {
            KindLength.Short => beamTruss.LengthBottom,
            _ => beamTruss.LengthTop
        };
        Mesh mesh = _3dObjectConstructor.CreateFlatSidePipe(beamTruss.Truss.ProfileBelt.Thickness, length, beamTruss.Truss.ProfileBelt.Height, beamTruss.Truss.ProfileBelt.Radius);
        ApplyMaterial(mesh, "Standard", Color.black);
    }

    // Update is called once per frames
    void Update()
    {
        
    }

    void ApplyMaterial(Mesh mesh, string shaderName, Color color)
    {
        GetComponent<MeshFilter>().mesh = mesh;
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        UnityEngine.Material material = new(Shader.Find(shaderName));
        material.color = color;
        meshRenderer.material = material;
    }
}
