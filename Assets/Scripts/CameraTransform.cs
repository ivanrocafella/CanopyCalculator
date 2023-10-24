using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransform : MonoBehaviour
{  
    private PlanCanopy planColumn;
    void Start()
    {
        planColumn = GameObject.FindGameObjectWithTag("PlanCanopy").GetComponent<PlanCanopyGenerator>().MakePlanCanopy();
        transform.position = new Vector3(0, 2f * planColumn.SizeByY, - (planColumn.SizeByZ / 2 + planColumn.SizeByX * 1.5f * Mathf.Tan(50 * Mathf.Deg2Rad)));
        transform.rotation = Quaternion.Euler(35,0,0);       
    }
}
