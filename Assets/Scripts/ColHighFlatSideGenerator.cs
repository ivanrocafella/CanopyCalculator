using Assets.Models;
using Assets.Services;
using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class ColHighFlatSideGenerator : MonoBehaviour
{
    private readonly ColumnBody columnBody = new();

    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = _3dObjectConstructor.CreateParallelepiped(columnBody.Material.Thickness, columnBody.Height, columnBody.Material.Width, columnBody.Material.Radius);
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
