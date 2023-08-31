using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GirderTransform : MonoBehaviour
{
    private PlanColumn planColumn;
    private BeamTruss beamTruss;
    private Girder girder;
    private RafterTruss rafterTruss;
    private ColumnPlug columnPlug = new();
    private float partAdditFromAngle;
    private float partAdditHalfBeltAngle;
    // Start is called before the first frame update
    void Start()
    {
        planColumn = GameObject.FindGameObjectsWithTag("Canopy")[0].GetComponent<CanopyGenerator>().planColumn;
        beamTruss = GameObject.FindGameObjectsWithTag("BeamTruss")[0].GetComponent<BeamTrussGenerator>().beamTrussForRead;
        girder = GameObject.FindGameObjectsWithTag("Girder")[0].GetComponent<GirderGenerator>().girder;
        rafterTruss = GameObject.FindGameObjectsWithTag("RafterTruss")[0].GetComponent<RafterTrussGenerator>().rafterTrussForRead;
        partAdditFromAngle = Mathf.Tan(planColumn.Slope)
            * (beamTruss.Truss.ProfileBelt.Length / 2 - beamTruss.Truss.ProfileBelt.Radius + planColumn.OutputRafter);
        partAdditHalfBeltAngle = rafterTruss.Truss.ProfileBelt.Height / 2 / Mathf.Cos(planColumn.Slope);
        //columnBody.SetHeight(KindLength.Long);
        //beamTruss = GameObject.FindGameObjectsWithTag("BeamTruss")[0].GetComponent<BeamTrussGenerator>().beamTrussForRead;
        transform.localPosition = new Vector3(-planColumn.OutputRafter
            , planColumn.SizeByY + columnPlug.Thickness + beamTruss.Truss.ProfileBelt.Height
            + girder.Material.Height / 2 + partAdditFromAngle + partAdditHalfBeltAngle
            , -planColumn.OutputGirder);
        transform.localRotation = Quaternion.Euler(-planColumn.SlopeInDegree, -90, -90);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
