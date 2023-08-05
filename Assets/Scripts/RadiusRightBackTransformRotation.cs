using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusRightBackTransformRotation : MonoBehaviour
{
    private readonly ColumnBody columnBody = new();
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(columnBody.Material.Length / 2, 0, (columnBody.Material.Length / 2 - columnBody.Material.Radius));
        transform.Rotate(0f, -90f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
