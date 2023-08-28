using Assets.Models;
using Assets.Models.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BeltFlatSideBeamTrussTransform : MonoBehaviour
{
    private string path;
    private BeamTruss beamTruss;
    public Direction Direction;
    public KindLength KindLength;
    // Start is called before the first frame update
    void Start()
    {
        beamTruss = GameObject.FindGameObjectsWithTag("BeamTruss")[0].GetComponent<BeamTrussGenerator>().beamTrussForRead;

        float length = KindLength switch
        {
            KindLength.Short => beamTruss.LengthBottom,
            _ => beamTruss.LengthTop
        };

        switch (Direction)
        {
            case Direction.Right:
                transform.localPosition = new Vector3((beamTruss.Truss.ProfileBelt.Length - beamTruss.Truss.ProfileBelt.Thickness) / 2, length / 2, 0);
                break;
            case Direction.Left:
                transform.localPosition = new Vector3(-(beamTruss.Truss.ProfileBelt.Length - beamTruss.Truss.ProfileBelt.Thickness) / 2, length / 2, 0);
                break;
            case Direction.Back:
                transform.localPosition = new Vector3(0, length / 2, (beamTruss.Truss.ProfileBelt.Length - beamTruss.Truss.ProfileBelt.Thickness) / 2);
                transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
                break;
            default:
                transform.localPosition = new Vector3(0, length / 2, -(beamTruss.Truss.ProfileBelt.Length - beamTruss.Truss.ProfileBelt.Thickness) / 2);
                transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
