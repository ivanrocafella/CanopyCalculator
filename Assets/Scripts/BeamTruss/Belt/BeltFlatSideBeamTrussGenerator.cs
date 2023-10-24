using Assets.Models;
using Assets.Services;
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

    private void ApplyMaterial(Mesh mesh, string shaderName, Color color)
    {
        GetComponent<MeshFilter>().mesh = mesh;
        MeshRenderer meshRenderer = !gameObject.GetComponent<MeshRenderer>()
            ? gameObject.AddComponent<MeshRenderer>()
            : gameObject.GetComponent<MeshRenderer>();
        Material material = new(Shader.Find(shaderName))
        {
            color = color
        };
        meshRenderer.material = material;
    }
}
