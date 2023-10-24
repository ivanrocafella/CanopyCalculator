using Assets.Models;
using Assets.Models.Enums;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class CrateBeamTrussTransform : MonoBehaviour
{
    private BeamTruss beamTruss;
    public StandartNonStandart StandartNonStandart;
    // Start is called before the first frame update
    void Start()
    {
        beamTruss = GameObject.FindGameObjectsWithTag("BeamTruss")[0].GetComponent<BeamTrussGenerator>().beamTrussForRead;
        float positionY;
        float angle;
        switch (StandartNonStandart)
        {
            case StandartNonStandart.NonStandart:
                positionY = beamTruss.LengthTop - beamTruss.Tail - beamTruss.PerspectWidthHalfNonStandartCrate;
                angle = 180 + beamTruss.AngleNonStandartCrate;
                break;
            default:
                positionY = beamTruss.Tail + beamTruss.PieceMidToExter;
                angle = -beamTruss.Truss.AngleCrateInDegree;
                break;
        }
        transform.SetLocalPositionAndRotation(new Vector3 (0, positionY, 0), Quaternion.Euler(0f, 0f, angle));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
