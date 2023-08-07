using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformPositionRotationColumnLow
    : MonoBehaviour
{
    private readonly PlanColumn planColumn = new();
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(planColumn.SizeByX, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
