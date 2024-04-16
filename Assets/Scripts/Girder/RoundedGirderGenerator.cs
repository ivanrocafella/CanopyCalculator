using Assets.Models;
using Assets.Models.Enums;
using Assets.Services;
using Assets.Utils;
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
        girder = GameObject.FindGameObjectWithTag("Girder").GetComponent<GirderGenerator>().girder;
        // Setting roundness profile or not
        if (ValAction.withRadius)
        {
            Mesh mesh = _3dObjectConstructor.CreateRoundedCorner(girder.Profile.Radius, girder.Profile.Radius, girder.Length
            , girder.Profile.Thickness, girder.Profile.Radius);
            ApplyMaterial(mesh, "Standard", Color.black);
        }
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
