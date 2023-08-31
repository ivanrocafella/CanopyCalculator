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

public class FlatSideGirderGenerator : MonoBehaviour
{
    private Girder girder;

    // Start is called before the first frame update
    void Start()
    {
        girder = GameObject.FindGameObjectsWithTag("Girder")[0].GetComponent<GirderGenerator>().girder;
        Mesh mesh = _3dObjectConstructor.CreateFlatSidePipe(girder.Material.Thickness, girder.Length, girder.Material.Height, girder.Material.Radius);
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
