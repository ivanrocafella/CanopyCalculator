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
    private float partAdditBeamProfileSmall;
    private float partAdditRafterProfile;
    private float partAdditGirderProfile;
    private float partToCenterGirderProfile;
    private float partToCenterGirderProfileSmall;
    private float partOutputRufter;
    private float partAdditGirderHalfProfileVert;
    private float partAdditGirderHalfProfileHor;
    // Start is called before the first frame update
    private void Awake()
    {
        planColumn = GameObject.FindGameObjectsWithTag("Canopy")[0].GetComponent<CanopyGenerator>().planColumn;
        beamTruss = GameObject.FindGameObjectsWithTag("BeamTruss")[0].GetComponent<BeamTrussGenerator>().beamTrussForRead;
        girder = GameObject.FindGameObjectsWithTag("Girder")[0].GetComponent<GirderGenerator>().girder;
        rafterTruss = GameObject.FindGameObjectsWithTag("RafterTruss")[0].GetComponent<RafterTrussGenerator>().rafterTrussForRead;
        partAdditBeamProfileSmall = Mathf.Tan(planColumn.Slope)
            * (beamTruss.Truss.ProfileBelt.Length / 2 - beamTruss.Truss.ProfileBelt.Radius);
        partAdditRafterProfile = rafterTruss.Truss.ProfileBelt.Height / Mathf.Cos(planColumn.Slope);
        partAdditGirderProfile = girder.Material.Height / (2 * Mathf.Cos(planColumn.Slope));
        partToCenterGirderProfile = Mathf.Sin(planColumn.Slope)
            * (girder.Material.Height + rafterTruss.Truss.ProfileBelt.Height) / 2;
        partToCenterGirderProfileSmall = Mathf.Tan(planColumn.Slope) * partToCenterGirderProfile;
        partOutputRufter = Mathf.Tan(planColumn.Slope) * planColumn.OutputRafter;
        partAdditGirderHalfProfileVert = Mathf.Sin(planColumn.Slope) * (girder.Material.Length / 2);
        partAdditGirderHalfProfileHor = Mathf.Cos(planColumn.Slope) * (girder.Material.Length / 2);

        transform.localPosition = new Vector3(partToCenterGirderProfile - planColumn.OutputRafter
            + partAdditGirderHalfProfileHor
            , planColumn.SizeByY + columnPlug.Thickness + beamTruss.Truss.ProfileBelt.Height
            + partAdditBeamProfileSmall + partAdditRafterProfile + partAdditGirderProfile
            - partToCenterGirderProfileSmall + partOutputRufter - partAdditGirderHalfProfileVert
            , -planColumn.OutputGirder);
        transform.localRotation = Quaternion.Euler(-planColumn.SlopeInDegree, -90, -90);
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
