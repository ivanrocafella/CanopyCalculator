using Assets.Models;
using Assets.Services;
using Assets.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Material = UnityEngine.Material;

public class ColumnPlugGenerator : MonoBehaviour
{
    private readonly KindLength kindLength = KindLength.Long;
    private ColumnBody columnBody;
    private readonly ColumnPlug columnPlug = new();
    public Material material;
    // Start is called before the first frame update
    void Start()
    {
        columnBody = GameObject.FindGameObjectsWithTag("ColumnHigh")[0].GetComponent<ColumnGenerator>().ColumnBody;
        columnBody.SetHeight(kindLength);
        Mesh mesh = _3dObjectConstructor.CreatePlate(columnBody.Profile.Length + columnPlug.Thickness * 2, columnPlug.Thickness, columnBody.Profile.Height + columnPlug.Thickness * 2, 0);
        ValAction.ApplyMaterial(mesh, transform.gameObject, material);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
