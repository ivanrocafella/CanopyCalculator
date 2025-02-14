using Assets.Models;
using Assets.Models.Enums;
using Assets.Services;
using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Material = UnityEngine.Material;

public class RoundedCrateBeamTrussGenerator : MonoBehaviour
{
    private BeamTruss beamTruss;
    public StandartNonStandart StandartNonStandart;
    public Material material;

    private void Start()
    {
        beamTruss = GameObject.FindGameObjectsWithTag("BeamTruss")[0].GetComponent<BeamTrussGenerator>().beamTrussForRead;
        // Setting roundness profile or not
        if (ValAction.withRadius)
        {
            Mesh mesh = StandartNonStandart == StandartNonStandart.NonStandart ? _3dObjectConstructor.CreateRoundedCorner(beamTruss.Truss.ProfileCrate.Radius, beamTruss.LengthNonStandartCrate
            , beamTruss.Truss.ProfileCrate.Thickness)
            : _3dObjectConstructor.CreateRoundedCorner(beamTruss.Truss.ProfileCrate.Radius, beamTruss.Truss.LengthCrate
            , beamTruss.Truss.ProfileCrate.Thickness);
            ValAction.ApplyMaterial(mesh, transform.gameObject, material);
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
