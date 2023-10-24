using Assets.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Material = UnityEngine.Material;

public class ColumnPlugTransform : MonoBehaviour
{
    public KindLength kindLength;
    private ColumnBody columnBody;
    private readonly ColumnPlug columnPlug = new();
    // Start is called before the first frame update
    void Start()
    {
        columnBody = GameObject.FindGameObjectWithTag("ColumnHigh").GetComponent<ColumnGenerator>().ColumnBody;
        columnBody.SetHeight(kindLength);
        transform.localPosition = new Vector3(0, columnBody.Height + columnPlug.Thickness / 2, 0);
    }

    // Update is called once per frame  
    void Update()
    {   
        
    }
}
        