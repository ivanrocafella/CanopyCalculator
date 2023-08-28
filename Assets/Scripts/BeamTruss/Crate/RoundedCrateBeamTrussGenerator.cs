using Assets.Models;
using Assets.Models.Enums;
using Assets.Services;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Material = UnityEngine.Material;

public class RoundedCrateBeamTrussGenerator : MonoBehaviour
{
    private string path;
    private BeamTruss beamTruss;
    private Vector3[] Vertices { get; set; }
    private Vector3[] Normals;
    public StandartNonStandart StandartNonStandart;

    private void Start()
    {
        beamTruss = GameObject.FindGameObjectsWithTag("BeamTruss")[0].GetComponent<BeamTrussGenerator>().beamTrussForRead;
        Mesh mesh = StandartNonStandart == StandartNonStandart.NonStandart ? _3dObjectConstructor.CreateRoundedCorner(beamTruss.Truss.ProfileCrate.Radius, beamTruss.Truss.ProfileCrate.Radius, beamTruss.LengthNonStandartCrate
            , beamTruss.Truss.ProfileCrate.Thickness, beamTruss.Truss.ProfileCrate.Radius)
            : _3dObjectConstructor.CreateRoundedCorner(beamTruss.Truss.ProfileCrate.Radius, beamTruss.Truss.ProfileCrate.Radius, beamTruss.Truss.LengthCrate
            , beamTruss.Truss.ProfileCrate.Thickness, beamTruss.Truss.ProfileCrate.Radius);
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
