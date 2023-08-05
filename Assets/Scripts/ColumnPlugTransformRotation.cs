using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Material = UnityEngine.Material;

public class ColumnPlugTransformRotation : MonoBehaviour
{
    private readonly ColumnBody columnBody = new();
    private readonly ColumnPlug columnPlug = new();
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(0, columnBody.Height + columnPlug.Thickness / 2, 0);
    }

    // Update is called once per frame  
    void Update()
    {   
        
    }
}
        