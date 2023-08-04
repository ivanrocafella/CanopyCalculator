using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusRightFrontTransformRotation : MonoBehaviour
{
    private readonly ColumnBody columnBody = new();
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(columnBody.Material.Length / 2 - columnBody.Material.Radius, 0, -columnBody.Material.Length / 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
