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

public class FlatSideGirderGenerator : MonoBehaviour
{
    private Girder girder;
    public Material material;

    // Start is called before the first frame update
    void Start()
    {
        girder = GameObject.FindGameObjectWithTag("Girder").GetComponent<GirderGenerator>().girder;
        // Setting roundness profile or not
        Mesh mesh = ValAction.withRadius ? _3dObjectConstructor.CreateFlatSidePipe(girder.Profile.Thickness, girder.Length, girder.Profile.Height, girder.Profile.Radius) :
                                 _3dObjectConstructor.CreateFlatSidePipe(girder.Profile.Thickness, girder.Length, girder.Profile.Height, 0);
        ValAction.ApplyMaterial(mesh, transform.gameObject, material);
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
