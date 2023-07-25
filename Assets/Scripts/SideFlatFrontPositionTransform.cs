using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideFlatFrontPositionTransform : MonoBehaviour
{
    private readonly ColumnBody columnBody = new();
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, columnBody.Height / 2, -0.0475f);
        transform.Rotate(0f, 90f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
