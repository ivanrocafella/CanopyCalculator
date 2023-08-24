using Assets.Models;
using Assets.Models.Enums;
using Assets.Services;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Material = UnityEngine.Material;

public class CrateRafterTrussRoundedGenerator : MonoBehaviour
{
    private string path;
    private RafterTruss rafterTruss;
    private Vector3[] Vertices { get; set; }
    private Vector3[] Normals;
    public StandartNonStandart StandartNonStandart;

    private void Start()
    {
        rafterTruss = GameObject.FindGameObjectsWithTag("RafterTruss")[0].GetComponent<RafterTrussGenerator>().rafterTrussForRead;
        Mesh mesh = StandartNonStandart == StandartNonStandart.NonStandart ? _3dObjectConstructor.CreateRoundedCorner(rafterTruss.ProfileCrate.Radius, rafterTruss.ProfileCrate.Radius, rafterTruss.LengthNonStandartCrate
            , rafterTruss.ProfileCrate.Thickness, rafterTruss.ProfileCrate.Radius)
            : _3dObjectConstructor.CreateRoundedCorner(rafterTruss.ProfileCrate.Radius, rafterTruss.ProfileCrate.Radius, rafterTruss.LengthCrate
            , rafterTruss.ProfileCrate.Thickness, rafterTruss.ProfileCrate.Radius);
        Vertices = mesh.vertices;
        Normals = mesh.normals;
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

    //private void OnDrawGizmos()
    //{
    //    if (Vertices == null)
    //    {
    //        return;
    //    }
    //    for (int i = 0; i < Vertices.Length; i++)
    //    {
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawSphere(Vertices[i], 0.1f);
    //        Gizmos.color = Color.yellow;
    //        Gizmos.DrawRay(Vertices[i], Normals[i]);
    //    }
    //}
}
