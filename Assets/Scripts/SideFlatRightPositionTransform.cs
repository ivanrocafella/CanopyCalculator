using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideFlatRightPositionTransform : MonoBehaviour
{
    private ColumnBody columnBody = new();
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0.0475f, columnBody.height / 2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
