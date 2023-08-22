using Assets.Models;
using Assets.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColFlatSideGenerator : MonoBehaviour
{
    private ColumnBody columnBody;
    public KindLength selectedKindLength;
    // Start is called before the first frame update
    void Start()
    {
        columnBody = new ColumnBody(selectedKindLength);
        Mesh mesh = _3dObjectConstructor.CreateFlatSidePipe(columnBody.Material.Thickness, columnBody.Height, columnBody.Material.Height, columnBody.Material.Radius);
        ApplyMaterial(mesh, "Standard", Color.black);
    }

    // Update is called once per frames
    void Update()
    {
    }

    void ApplyMaterial(Mesh mesh, string shaderName, Color color)
    {
        GetComponent<MeshFilter>().mesh = mesh;
        MeshRenderer meshRenderer = !gameObject.GetComponent<MeshRenderer>()
            ? gameObject.AddComponent<MeshRenderer>() 
            : gameObject.GetComponent<MeshRenderer>();
        UnityEngine.Material material = new(Shader.Find(shaderName))
        {
            color = color
        };
        meshRenderer.material = material;
    }
}
