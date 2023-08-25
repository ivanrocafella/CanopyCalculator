using Assets.Models;
using Assets.Services;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;

public class RafterTrussBeltFlatSideGenerator : MonoBehaviour
{
    private string path;
    private RafterTruss rafterTruss;
    public KindLength KindLength;

    // Start is called before the first frame update
    void Start()
    {
        rafterTruss = GameObject.FindGameObjectsWithTag("RafterTruss")[0].GetComponent<RafterTrussGenerator>().rafterTrussForRead;
        float length = KindLength switch
        {
            KindLength.Short => rafterTruss.LengthBottom,
            _ => rafterTruss.LengthTop
        };
        Mesh mesh = _3dObjectConstructor.CreateFlatSidePipe(rafterTruss.Truss.ProfileBelt.Thickness, length, rafterTruss.Truss.ProfileBelt.Height, rafterTruss.Truss.ProfileBelt.Radius);
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
