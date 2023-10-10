using Assets.Models;
using Assets.Models.Enums;
using Assets.Services;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Material = UnityEngine.Material;

public class RoundedGirderGenerator : MonoBehaviour
{
    private Girder girder;

    private void Start()
    {
        girder = GameObject.FindGameObjectsWithTag("Girder")[0].GetComponent<GirderGenerator>().girder;
        Mesh mesh = _3dObjectConstructor.CreateRoundedCorner(girder.Material.Radius, girder.Material.Radius, girder.Length
            , girder.Material.Thickness, girder.Material.Radius);
        ApplyMaterial(mesh, "Standard", Color.black);
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
