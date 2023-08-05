using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformPositionRotationColumnHigh
    : MonoBehaviour
{
    private readonly ColumnBody columnBody = new();
    private readonly ColumnPlug columnPlug = new();
    public float y = -1000;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0f, 0, y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
