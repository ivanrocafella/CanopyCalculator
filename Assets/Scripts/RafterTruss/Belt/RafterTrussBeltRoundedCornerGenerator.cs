using Assets.Models;
using Assets.Services;
using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Material = UnityEngine.Material;

public class RafterTrussBeltRoundedCornerGenerator : MonoBehaviour
{
    private RafterTruss rafterTruss;
    public KindLength KindLength;

    private void Start()
    {
        rafterTruss = GameObject.FindGameObjectWithTag("RafterTruss").GetComponent<RafterTrussGenerator>().rafterTrussForRead;
        float length = KindLength switch
        {
            KindLength.Short => rafterTruss.LengthBottom,
            _ => rafterTruss.LengthTop
        };
        // Setting roundness profile or not
        if (ValAction.withRadius)
        {
            Mesh mesh = _3dObjectConstructor.CreateRoundedCorner(rafterTruss.Truss.ProfileBelt.Radius, rafterTruss.Truss.ProfileBelt.Radius, length
            , rafterTruss.Truss.ProfileBelt.Thickness, rafterTruss.Truss.ProfileBelt.Radius);
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
