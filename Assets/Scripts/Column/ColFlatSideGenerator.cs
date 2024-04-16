using Assets.Models;
using Assets.Services;
using Assets.Utils;
using System;
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
        
    }

    private void Awake()
    {
        columnBody = GameObject.FindGameObjectWithTag("ColumnHigh").GetComponent<ColumnGenerator>().ColumnBody;
        columnBody.SetHeight(selectedKindLength);
        // Setting roundness profile or not 
        Mesh mesh = ValAction.withRadius ? _3dObjectConstructor.CreateFlatSidePipe(columnBody.Profile.Thickness, columnBody.Height, columnBody.Profile.Height, columnBody.Profile.Radius) :
                                 _3dObjectConstructor.CreateFlatSidePipe(columnBody.Profile.Thickness, columnBody.Height, columnBody.Profile.Height, 0);
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
