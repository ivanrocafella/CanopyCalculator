using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideFlatRightPositionTransform : MonoBehaviour
{
    private readonly ColumnBody columnBody = new();
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3((columnBody.Material.Length - columnBody.Material.Thickness) / 2, columnBody.Height / 2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
