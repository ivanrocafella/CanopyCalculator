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
        planColumn = GameObject.FindGameObjectWithTag("PlanCanopy").GetComponent<PlanCanopyGenerator>().MakePlanColumn();
        transform.localPosition = new Vector3(planColumn.SizeByX, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
    