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
    // Start is called before the first frame update
    void Start()
    {
        planColumn = GameObject.FindGameObjectsWithTag("Canopy")[0].GetComponent<CanopyGenerator>().planColumn;
        beamTruss = GameObject.FindGameObjectsWithTag("BeamTruss")[0].GetComponent<BeamTrussGenerator>().beamTrussForRead;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
