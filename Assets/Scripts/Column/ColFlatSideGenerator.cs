using Assets.Models;
using Assets.Services;
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
        if (columnBody != null)
            Debug.Log($"[{DateTime.Now}]: {columnBody} is not null HeightHigh {columnBody.Height}");
        if (columnBody.PlanColumn != null)
            Debug.Log($"[{DateTime.Now}]: {columnBody.PlanColumn} is not null HeightLow {columnBody.PlanColumn.SizeByYLow}");
        Mesh mesh = _3dObjectConstructor.CreateFlatSidePipe(columnBody.Profile.Thickness, columnBody.Height, columnBody.Profile.Height, columnBody.Profile.Radius);
        if (mesh == null)
            Debug.Log($"[{DateTime.Now}]: {mesh} is null");
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
