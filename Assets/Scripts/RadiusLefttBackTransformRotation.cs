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
        transform.position = new Vector3(-columnBody.LengthProfile / 2 + columnBody.RadiusProfile, 0, columnBody.LengthProfile / 2);
        transform.Rotate(0f, -180f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
