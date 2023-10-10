using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RafterTrussTransform : MonoBehaviour
{
    private ColumnPlug columnPlug = new();
    private PlanColumn planColumn;
    private BeamTruss beamTruss;
    private RafterTruss rafterTruss;
    private float partAdditFromAngle;
    private float partAdditHalfBeltAngle;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RafterTrussTransformCalculation()); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RafterTrussTransformCalculation()
    {
        planColumn = GameObject.FindGameObjectsWithTag("Canopy")[0].GetComponent<CanopyGenerator>().planColumn;
        beamTruss = GameObject.FindGameObjectsWithTag("BeamTruss")[0].GetComponent<BeamTrussGenerator>().beamTrussForRead;
        rafterTruss = GameObject.FindGameObjectsWithTag("RafterTruss")[0].GetComponent<RafterTrussGenerator>().rafterTrussForRead;
        partAdditFromAngle = Mathf.Tan(planColumn.Slope)
            * (beamTruss.Truss.ProfileBelt.Length / 2 - beamTruss.Truss.ProfileBelt.Radius + planColumn.OutputRafter);
        partAdditHalfBeltAngle = rafterTruss.Truss.ProfileBelt.Height / 2 / Mathf.Cos(planColumn.Slope);
        transform.localPosition = new Vector3(-planColumn.OutputRafter
            , planColumn.SizeByY + columnPlug.Thickness + partAdditFromAngle
            + partAdditHalfBeltAngle + beamTruss.Truss.ProfileBelt.Height
            , 0);
        transform.localRotation = Quaternion.Euler(0, 0, -(90 + planColumn.SlopeInDegree));
        yield return null;
    }
}
