using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RafterTrussTransform : MonoBehaviour
{
    private readonly ColumnPlug columnPlug = new();
    private PlanCanopy planColumn;
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
        planColumn = GameObject.FindGameObjectWithTag("PlanCanopy").GetComponent<PlanCanopyGenerator>().MakePlanCanopy();
        beamTruss = GameObject.FindGameObjectWithTag("BeamTruss").GetComponent<BeamTrussGenerator>().beamTrussForRead;
        rafterTruss = GameObject.FindGameObjectWithTag("RafterTruss").GetComponent<RafterTrussGenerator>().rafterTrussForRead;
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
