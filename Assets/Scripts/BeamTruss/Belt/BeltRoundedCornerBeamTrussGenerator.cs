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

public class BeltRoundedCornerBeamTrussGenerator : MonoBehaviour
{
    private BeamTruss beamTruss;
    public KindLength KindLength;

    private void Start()
    {
        beamTruss = GameObject.FindGameObjectsWithTag("BeamTruss")[0].GetComponent<BeamTrussGenerator>().beamTrussForRead;
        float length = KindLength switch
        {
            KindLength.Short => beamTruss.LengthBottom,
            _ => beamTruss.LengthTop
        };
        // Setting roundness profile or not
        if (ValAction.withRadius)
        {
            Mesh mesh = _3dObjectConstructor.CreateRoundedCorner(beamTruss.Truss.ProfileBelt.Radius, beamTruss.Truss.ProfileBelt.Radius, length
                , beamTruss.Truss.ProfileBelt.Thickness, beamTruss.Truss.ProfileBelt.Radius);
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
