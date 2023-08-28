using Assets.Models;
using Assets.Services;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Material = UnityEngine.Material;

public class BeltRoundedCornerBeamTrussGenerator : MonoBehaviour
{
    private string path;
    private BeamTruss beamTruss;
    public KindLength KindLength;
    private Vector3[] Vertices { get; set; }
    private Vector3[] Normals;

    private void Start()
    {
        beamTruss = GameObject.FindGameObjectsWithTag("BeamTruss")[0].GetComponent<BeamTrussGenerator>().beamTrussForRead;
        float length = KindLength switch
        {
            KindLength.Short => beamTruss.LengthBottom,
            _ => beamTruss.LengthTop
        };
        Mesh mesh = _3dObjectConstructor.CreateRoundedCorner(beamTruss.Truss.ProfileBelt.Radius, beamTruss.Truss.ProfileBelt.Radius, length
            , beamTruss.Truss.ProfileBelt.Thickness, beamTruss.Truss.ProfileBelt.Radius);
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
}
