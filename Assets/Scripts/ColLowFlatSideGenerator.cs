using Assets.Models;
using Assets.Services;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class ColLowFlatSideGenerator : MonoBehaviour
{
    private readonly ColumnBody columnBody = new();
    private readonly PlanColumn planColumn = new();

    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = _3dObjectConstructor.CreateParallelepiped(columnBody.Material.Thickness, (float)Math.Floor(columnBody.Height - Math.Tan(planColumn.Slope) * planColumn.SizeByX), columnBody.Material.Width, columnBody.Material.Radius);
        ApplyMaterial(mesh, "Standard", Color.black);
        Debug.Log($"{(float)Math.Floor(columnBody.Height - Math.Tan(planColumn.Slope) * planColumn.SizeByX) }");
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
