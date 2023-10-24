using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BeamTrussTransform : MonoBehaviour
{
    private ColumnBody columnBody;
    private readonly ColumnPlug columnPlug = new();
    private BeamTruss beamTruss;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BeamTrussTransformCalculation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator BeamTrussTransformCalculation()
    {
        columnBody = GameObject.FindGameObjectWithTag("ColumnHigh").GetComponent<ColumnGenerator>().ColumnBody;
        columnBody.SetHeight(KindLength.Long);
        beamTruss = GameObject.FindGameObjectWithTag("BeamTruss").GetComponent<BeamTrussGenerator>().beamTrussForRead;
        transform.SetLocalPositionAndRotation(new Vector3(0
            , columnBody.Height + columnPlug.Thickness + beamTruss.Truss.ProfileBelt.Height / 2
            , 0)
            , Quaternion.Euler(0f, -90f, -90f));
        yield return null;
    }
}
