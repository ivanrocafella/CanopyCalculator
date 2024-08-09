using Assets.Models;
using Assets.Services;
using Assets.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Material = UnityEngine.Material;

public class ColFlatSideGenerator : MonoBehaviour
{
    private ColumnBody columnBody;
    public KindLength selectedKindLength;
    public Material material;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        columnBody = GameObject.FindGameObjectWithTag("ColumnHigh").GetComponent<ColumnGenerator>().ColumnBody;
        columnBody.SetHeight(selectedKindLength);
        // Setting roundness profile or not 
        Mesh mesh = ValAction.withRadius ? _3dObjectConstructor.CreatePlate(columnBody.Profile.Thickness, columnBody.Height, columnBody.Profile.Height, columnBody.Profile.Radius) :
                                 _3dObjectConstructor.CreatePlate(columnBody.Profile.Thickness, columnBody.Height, columnBody.Profile.Height, 0);
        ValAction.ApplyMaterial(mesh, transform.gameObject, material);
    }

    // Update is called once per frames
    void Update()
    {
    }
}
