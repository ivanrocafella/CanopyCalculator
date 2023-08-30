using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanopyGenerator : MonoBehaviour
{
    public PlanColumn planColumn;
    private GameObject[] columnsHigh;
    private GameObject[] columnsLow;
    private GameObject[] beamTrussesOnHigh;
    private GameObject[] beamTrussesOnLow;
    private GameObject[] rafterTrusses;
    private GameObject canopy;
    private ColumnBody columnBodyHigh;
    private ColumnBody columnBodyLow;
    private BeamTruss beamTruss;
    private RafterTruss rafterTruss;
    private int countStepRafterTruss;
    private ColumnPlug columnPlug = new();
    public int SizeByX;
    public int SizeByZ;
    public int SizeByY;
    public int SlopeInDegree;
    public int CountStep;
    public int OutputBeam;
    public int OutputRafter;

    public PlanColumn MakePlanColumn()
    {
        planColumn = new PlanColumn()
        {
            SizeByX = SizeByX,
            SizeByZ = SizeByZ,
            SizeByY = SizeByY,
            SlopeInDegree = SlopeInDegree,
            CountStep = CountStep,
            OutputBeam = OutputBeam,
            OutputRafter = OutputRafter
        };
        return planColumn;
    }

    void Start()
    {
        canopy = GameObject.FindGameObjectsWithTag("Canopy")[0];
        columnsHigh = new GameObject[planColumn.CountStep + 1];
        columnsLow = new GameObject[planColumn.CountStep + 1];
        beamTrussesOnHigh = new GameObject[planColumn.CountStep];
        beamTrussesOnLow = new GameObject[beamTrussesOnHigh.Length];
        columnBodyHigh = GameObject.FindGameObjectsWithTag("ColumnHigh")[0].GetComponent<ColumnGenerator>().ColumnBody;
        columnBodyHigh.SetHeight(KindLength.Long);
        beamTruss = GameObject.FindGameObjectsWithTag("BeamTruss")[0].GetComponent<BeamTrussGenerator>().beamTrussForRead;
        rafterTruss = GameObject.FindGameObjectsWithTag("RafterTruss")[0].GetComponent<RafterTrussGenerator>().rafterTrussForRead;
        countStepRafterTruss = Mathf.FloorToInt(planColumn.SizeByZ / rafterTruss.Step);
        rafterTrusses = new GameObject[countStepRafterTruss + 1];

        float partAdditFromAngle = Mathf.Tan(planColumn.Slope)
            * (beamTruss.Truss.ProfileBelt.Length / 2 - beamTruss.Truss.ProfileBelt.Radius + planColumn.OutputRafter);
        float partAdditHalfBeltAngle = rafterTruss.Truss.ProfileBelt.Height / 2 / Mathf.Cos(planColumn.Slope);

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
        for (int i = 0; i < beamTrussesOnHigh.Length - 1; i++)
        {
            beamTrussesOnHigh[i] = Object.Instantiate(GameObject.FindGameObjectsWithTag("BeamTruss")[0]);
            beamTrussesOnHigh[i].transform.SetParent(canopy.transform);
            Destroy(beamTrussesOnHigh[i].GetComponent("BeamTrussTransform"));
            beamTrussesOnHigh[i].transform.localPosition = new Vector3(0
           , planColumn.SizeByY + columnPlug.Thickness + beamTruss.Truss.ProfileBelt.Height / 2
           , planColumn.Step + planColumn.Step * i);
            beamTrussesOnHigh[i].transform.localRotation = Quaternion.Euler(0f, -90f, -90f);
        }
        for (int i = 0; i < beamTrussesOnLow.Length; i++)
        {
            beamTrussesOnLow[i] = Object.Instantiate(GameObject.FindGameObjectsWithTag("BeamTruss")[0]);
            beamTrussesOnLow[i].transform.SetParent(canopy.transform);
            Destroy(beamTrussesOnLow[i].GetComponent("BeamTrussTransform"));
            beamTrussesOnLow[i].transform.localPosition = new Vector3(planColumn.SizeByX
           , planColumn.SizeByYLow + columnPlug.Thickness + beamTruss.Truss.ProfileBelt.Height / 2
           , planColumn.Step * i);
            beamTrussesOnLow[i].transform.localRotation = Quaternion.Euler(0f, -90f, -90f);
        }
        for (int i = 0; i < rafterTrusses.Length; i++)
        {
            rafterTrusses[i] = Object.Instantiate(GameObject.FindGameObjectsWithTag("RafterTruss")[0]);
            rafterTrusses[i].transform.SetParent(canopy.transform);
            Destroy(rafterTrusses[i].GetComponent("RafterTrussTransform"));
            if (i == rafterTrusses.Length - 1)
            {
                rafterTrusses[i].transform.localPosition = new Vector3(-planColumn.OutputRafter
                             , planColumn.SizeByY + columnPlug.Thickness + partAdditFromAngle
                             + partAdditHalfBeltAngle + beamTruss.Truss.ProfileBelt.Height
                             , planColumn.SizeByZ);
                rafterTrusses[i].transform.localRotation = Quaternion.Euler(0, 0, -(90 + planColumn.SlopeInDegree));
            }
            else
            {
                rafterTrusses[i].transform.localPosition = new Vector3(-planColumn.OutputRafter
                             , planColumn.SizeByY + columnPlug.Thickness + partAdditFromAngle
                             + partAdditHalfBeltAngle + beamTruss.Truss.ProfileBelt.Height
                             , rafterTruss.Step + rafterTruss.Step * i);
                rafterTrusses[i].transform.localRotation = Quaternion.Euler(0, 0, -(90 + planColumn.SlopeInDegree));
            }
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
