using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransform : MonoBehaviour
{
    private PlanColumn planColumn;
    private void Awake()
    {
        planColumn = GameObject.FindGameObjectsWithTag("Canopy")[0].GetComponent<CanopyGenerator>().MakePlanColumn();
        transform.position = new Vector3(0, 2.5f * planColumn.SizeByY, - (planColumn.SizeByZ / 2 + planColumn.SizeByX * Mathf.Tan(50 * Mathf.Deg2Rad)));
        transform.rotation = Quaternion.Euler(35,0,0);

    }
}
