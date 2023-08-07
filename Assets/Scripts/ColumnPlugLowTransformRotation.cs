using Assets.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Material = UnityEngine.Material;

public class ColumnPlugLowTransformRotation : MonoBehaviour
{
    private readonly ColumnBody columnBody = new();
    private readonly ColumnPlug columnPlug = new();
    private readonly PlanColumn planColumn = new();
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(0, (float)Math.Floor(columnBody.Height - Math.Tan(planColumn.Slope) * planColumn.SizeByX) + columnPlug.Thickness / 2, 0);
    }

    // Update is called once per frame  
    void Update()
    {   
        
    }
}
        