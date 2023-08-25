using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanopyGenerator : MonoBehaviour
{
    public PlanColumn planColumn;
    private GameObject[] columnsHigh;
    private GameObject[] columnsLow;
    private GameObject canopy;
    public int SizeByX;
    public int SizeByZ;
    public int SizeByY;
    public int SlopeInDegree;
    public int CountStep;

    public PlanColumn MakePlanColumn()
    {
        planColumn = new PlanColumn()
        {
            SizeByX = SizeByX,
            SizeByZ = SizeByZ,
            SizeByY = SizeByY,
            SlopeInDegree = SlopeInDegree,
            CountStep = CountStep
        };
        return planColumn;
    }

    void Start()
    {
        canopy = GameObject.FindGameObjectsWithTag("Canopy")[0];
        columnsHigh = new GameObject[planColumn.CountStep + 1];
        columnsLow = new GameObject[planColumn.CountStep + 1];
        for (int i = 0; i < planColumn.CountStep; i++)
        {
            columnsHigh[i] = Object.Instantiate(GameObject.FindGameObjectsWithTag("ColumnHigh")[0]);
            columnsHigh[i].transform.SetParent(canopy.transform);
            Destroy(columnsHigh[i].GetComponent("TransformColumnHigh"));
            columnsHigh[i].transform.localPosition = new Vector3 (0, 0, planColumn.Step + planColumn.Step * i);            
            columnsLow[i] = Object.Instantiate(GameObject.FindGameObjectsWithTag("ColumnLow")[0]);
            columnsLow[i].transform.SetParent(canopy.transform);
            Destroy(columnsLow[i].GetComponent("TransformColumnLow"));
            columnsLow[i].transform.localPosition = new Vector3(planColumn.SizeByX, 0, planColumn.Step + planColumn.Step * i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
