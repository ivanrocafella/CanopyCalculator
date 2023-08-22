using Assets.Models;
using Assets.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Material = UnityEngine.Material;

public class ColumnPlugGenerator : MonoBehaviour
{
    private KindLength kindLength = KindLength.Long;
    private ColumnBody columnBody;
    private readonly ColumnPlug columnPlug = new ColumnPlug();
    // Start is called before the first frame update
    void Start()
    {
        columnBody = new ColumnBody(kindLength);
        Mesh mesh = _3dObjectConstructor.CreateFlatSidePipe(columnBody.Material.Length + columnPlug.Thickness * 2, columnPlug.Thickness, columnBody.Material.Height + columnPlug.Thickness * 2, 0);
        ApplyMaterial(mesh, "Standard", Color.black);
    }

    // Update is called once per frame
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
