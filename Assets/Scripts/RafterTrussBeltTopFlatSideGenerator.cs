using Assets.Models;
using Assets.Services;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class RafterTrussBeltTopFlatSideGenerator : MonoBehaviour
{
   // public Truss truss = new();
    private readonly PlanColumn planColumn = new();

    // Start is called before the first frame update
    void Start()
    {
       // truss.LengthTop = planColumn.SizeByX * (float)Math.Tan(planColumn.Slope) + 200;
       // Mesh mesh = _3dObjectConstructor.CreateFlatSidePipe(truss.ProfileBelt.Thickness, truss.LengthTop, truss.ProfileBelt.Width, truss.ProfileBelt.Radius);
       // ApplyMaterial(mesh, "Standard", Color.black);
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
