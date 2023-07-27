using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusLeftFrontTransformRotation : MonoBehaviour
{
    private readonly ColumnBody columnBody = new();
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3((columnBody.RadiusProfile - columnBody.LengthProfile) / 2, columnBody.Height/2, (columnBody.RadiusProfile - columnBody.LengthProfile) / 2);
    }

    // Update is called once per frame
    void Update()
    {   

    }
}
