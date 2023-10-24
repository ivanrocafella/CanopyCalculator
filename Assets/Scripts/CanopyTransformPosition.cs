using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanopyTransformPosition : MonoBehaviour
{
    private PlanCanopy planColumn;
    // Start is called before the first frame update
    private void Awake()
    {
        planColumn = GameObject.FindGameObjectWithTag("PlanCanopy").GetComponent<PlanCanopyGenerator>().MakePlanCanopy();
    }
    void Start()
    {
        transform.position = new Vector3(-planColumn.SizeByX / 2, 0, -planColumn.SizeByZ / 2);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
