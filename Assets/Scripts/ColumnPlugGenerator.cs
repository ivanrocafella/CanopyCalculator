using Assets.Models;
using Assets.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Material = UnityEngine.Material;

public class ColumnPlugGenerator : MonoBehaviour
{
    private readonly ColumnBody columnBody = new();
    private readonly ColumnPlug columnPlug = new ColumnPlug();
    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = _3dObjectConstructor.CreateParallelepiped(columnBody.Material.Length + columnPlug.Thickness * 2, columnPlug.Thickness, columnBody.Material.Width + columnPlug.Thickness * 2, 0);
        ApplyMaterial(mesh, "Standard", Color.black);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ApplyMaterial(Mesh mesh, string shaderName, Color color)
    {
        GetComponent<MeshFilter>().mesh = mesh;
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        Material material = new(Shader.Find(shaderName))
        {
            color = color
        };
        meshRenderer.material = material;
    }

}
