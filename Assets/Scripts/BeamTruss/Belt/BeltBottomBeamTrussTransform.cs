using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BeltBottomBeamTrussTransform : MonoBehaviour
{
    private BeamTruss beamTruss;
    // Start is called before the first frame update
    void Start()
    {
        beamTruss = GameObject.FindGameObjectsWithTag("BeamTruss")[0].GetComponent<BeamTrussGenerator>().beamTrussForRead;
        transform.localPosition = new Vector3(beamTruss.Truss.Height - beamTruss.Truss.ProfileBelt.Height
           , (beamTruss.LengthTop - beamTruss.LengthBottom) / 2, 0);

    }

    // Update is called once per frame
    void Update()
    {
    }
}
