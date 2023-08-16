using Assets.Models;
using Assets.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateRafterTrussFlatSideGenerator : MonoBehaviour
{
    private readonly RafterTruss rafterTruss = new();

    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = _3dObjectConstructor.CreateFlatSidePipe(rafterTruss.ProfileCrate.Thickness, rafterTruss.LengthCrate, rafterTruss.ProfileCrate.Width, rafterTruss.ProfileCrate.Radius);
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
