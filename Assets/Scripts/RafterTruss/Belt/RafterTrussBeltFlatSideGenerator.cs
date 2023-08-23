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
        path = Path.Combine(Application.dataPath, "JSONs", "JSON.json");
        rafterTruss = new("ФМ 300", path);
        float length = KindLength switch
        {
            KindLength.Short => rafterTruss.LengthBottom,
            _ => rafterTruss.LengthTop
        };
        Mesh mesh = _3dObjectConstructor.CreateFlatSidePipe(rafterTruss.ProfileBelt.Thickness, length, rafterTruss.ProfileBelt.Height, rafterTruss.ProfileBelt.Radius);
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
