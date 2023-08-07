using Assets.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideFlatFrontLowPositionTransform : MonoBehaviour
{
    private readonly ColumnBody columnBody = new();
    private readonly PlanColumn planColumn = new();    
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(0, (float)Math.Floor(columnBody.Height - Math.Tan(planColumn.Slope) * planColumn.SizeByX) / 2, - (columnBody.Material.Length - columnBody.Material.Thickness) / 2);
        transform.Rotate(0f, 90f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
