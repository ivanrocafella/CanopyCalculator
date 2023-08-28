using Assets.Models.Enums;
using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RoundedCrateBeamTrussTransform : MonoBehaviour
{
    private string path;
    private BeamTruss beamTruss;
    public Direction Direction;
    // Start is called before the first frame update
    void Start()    
    {
        beamTruss = GameObject.FindGameObjectsWithTag("BeamTruss")[0].GetComponent<BeamTrussGenerator>().beamTrussForRead;
        transform.localScale = new Vector3(0.1f, 1, 0.1f);
        switch (Direction)
        {
            case Direction.Right:
                transform.localPosition = new Vector3(beamTruss.Truss.ProfileCrate.Height / 2 - beamTruss.Truss.ProfileCrate.Radius, 0, -beamTruss.Truss.ProfileCrate.Length / 2);
                break;
            case Direction.Left:
                transform.localPosition = new Vector3(-beamTruss.Truss.ProfileCrate.Height / 2, 0, -(beamTruss.Truss.ProfileCrate.Length / 2 - beamTruss.Truss.ProfileCrate.Radius));
                transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
                break;
            case Direction.Back:
                transform.localPosition = new Vector3(-beamTruss.Truss.ProfileCrate.Height / 2 + beamTruss.Truss.ProfileCrate.Radius, 0, beamTruss.Truss.ProfileCrate.Length / 2);
                transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                break;
            default:
                transform.localPosition = new Vector3(beamTruss.Truss.ProfileCrate.Height / 2, 0, (beamTruss.Truss.ProfileCrate.Length / 2 - beamTruss.Truss.ProfileCrate.Radius));
                transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
