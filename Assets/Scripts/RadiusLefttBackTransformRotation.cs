using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusLefttBackTransformRotation : MonoBehaviour
{
    private readonly ColumnBody columnBody = new();
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(-columnBody.Material.Length / 2 + columnBody.Material.Radius, 0, columnBody.Material.Length / 2);
        transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
