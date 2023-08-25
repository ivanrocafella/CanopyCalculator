using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformColumnLow
    : MonoBehaviour
{
    private PlanColumn planColumn;
    // Start is called before the first frame update
    void Start()
    {
        planColumn = GameObject.FindGameObjectsWithTag("Canopy")[0].GetComponent<CanopyGenerator>().MakePlanColumn();
        transform.localPosition = new Vector3(planColumn.SizeByX, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
    