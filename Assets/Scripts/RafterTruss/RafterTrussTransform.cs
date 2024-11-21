using Assets.Models;
using Assets.Scripts.SOdata;
using Assets.Utils;
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
    private ColumnBody columnBody;
    private MountUnitBeamRafterTruss mountUnitBeamRafterTruss;
    private MountUnitColumnBeamTruss mountUnitColumnBeamTruss;
    private float partAdditFromAngle;
    private float partAdditHalfBeltAngle;
    [SerializeField]
    private MountUnitBeamRafterTrussDataList mountUnitBeamRafterTrussDataList;
    [SerializeField]
    private MountUnitColumnBeamTrussDataList mountUnitColumnBeamTrussDataList;

    private void Awake()
    {
    }
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
        columnBody = GameObject.FindGameObjectWithTag("ColumnHigh").GetComponent<ColumnGenerator>().ColumnBody;
        mountUnitBeamRafterTruss = ScriptObjectsAction.GetMountUnitBeamRafterTrussByName(rafterTruss.Truss.Name, mountUnitBeamRafterTrussDataList);
        mountUnitColumnBeamTruss = ScriptObjectsAction.GetMountUnitColumnBeamTrussByName(beamTruss.Truss.Name, mountUnitColumnBeamTrussDataList);
        partAdditFromAngle = Mathf.Tan(planColumn.Slope)
            * (beamTruss.Truss.ProfileBelt.Length / 2 - beamTruss.Truss.ProfileBelt.Radius + planColumn.OutputRafter);
        partAdditHalfBeltAngle = rafterTruss.Truss.ProfileBelt.Height / 2 / Mathf.Cos(planColumn.Slope);
        float yCoord = planColumn.SizeByY + columnPlug.Thickness + partAdditFromAngle + partAdditHalfBeltAngle + beamTruss.Truss.ProfileBelt.Height;
        float zCoord = 0;
        if (planColumn.IsDemountable)
        {
            yCoord += mountUnitBeamRafterTruss.ThicknessTable / Mathf.Cos(planColumn.Slope);
            zCoord += (columnBody.Profile.Height + mountUnitBeamRafterTruss.LengthFlangeBeamTruss) / 2 + mountUnitColumnBeamTruss.WidthFlangeColumn;
        }
        transform.localPosition = new Vector3(-planColumn.OutputRafter, yCoord, zCoord);
        transform.localRotation = Quaternion.Euler(0, 0, -(90 + planColumn.SlopeInDegree));
        yield return null;
    }
}
