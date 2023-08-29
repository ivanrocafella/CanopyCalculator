using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BeamTrussTransform : MonoBehaviour
{
    private ColumnBody columnBody;
    private ColumnPlug columnPlug = new();
    private PlanColumn planColumn;
    private BeamTruss beamTruss;
    // Start is called before the first frame update
    void Start()
    {
        columnBody = GameObject.FindGameObjectsWithTag("ColumnHigh")[0].GetComponent<ColumnGenerator>().ColumnBody;
        columnBody.SetHeight(KindLength.Long);
        beamTruss = GameObject.FindGameObjectsWithTag("BeamTruss")[0].GetComponent<BeamTrussGenerator>().beamTrussForRead;
        transform.localPosition = new Vector3(0
            , columnBody.Height + columnPlug.Thickness + beamTruss.Truss.ProfileBelt.Height / 2
            , 0);
        transform.localRotation = Quaternion.Euler(0f, -90f, -90f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
