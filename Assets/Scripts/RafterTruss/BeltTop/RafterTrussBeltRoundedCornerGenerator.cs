using Assets.Models;
using Assets.Services;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Material = UnityEngine.Material;

public class RafterTrussBeltRoundedCornerGenerator : MonoBehaviour
{
    public KindLength KindLength;
    private readonly RafterTruss rafterTruss = new();
    private Vector3[] Vertices { get; set; }
    private Vector3[] Normals;

    private void Start()
    {
        float length = KindLength switch
        {
            KindLength.Short => rafterTruss.LengthBottom,
            _ => rafterTruss.LengthTop
        };
        Mesh mesh = _3dObjectConstructor.CreateRoundedCorner(rafterTruss.ProfileBelt.Radius, rafterTruss.ProfileBelt.Radius, (int)length
            , rafterTruss.ProfileBelt.Thickness, rafterTruss.ProfileBelt.Radius);
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
