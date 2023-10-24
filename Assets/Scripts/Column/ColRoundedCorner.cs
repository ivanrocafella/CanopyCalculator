using Assets.Models;
using Assets.Services;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Material = UnityEngine.Material;

[RequireComponent(typeof(MeshFilter))]
public class ColRoundedCorner : MonoBehaviour
{
    public KindLength KindLength;
    private ColumnBody columnBody;

    private void Start()
    {
        columnBody = GameObject.FindGameObjectWithTag("ColumnHigh").GetComponent<ColumnGenerator>().ColumnBody;
        columnBody.SetHeight(KindLength);
        Mesh mesh = _3dObjectConstructor.CreateRoundedCorner(columnBody.Profile.Radius, columnBody.Profile.Radius, columnBody.Height
            , columnBody.Profile.Thickness, columnBody.Profile.Radius);
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
