using Assets.Models.Enums;
using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltRoundedCornerBeamTrussTransform : MonoBehaviour
{
    private string path;
    private BeamTruss beamTruss;
    public Direction Direction;
    // Start is called before the first frame update
    void Start()
    {
        beamTruss = GameObject.FindGameObjectsWithTag("BeamTruss")[0].GetComponent<BeamTrussGenerator>().beamTrussForRead;
        transform.localScale = new Vector3(0.1f, 1f, 0.1f);
        switch (Direction)
        {
            case Direction.Right:
                transform.localPosition = new Vector3(beamTruss.Truss.ProfileBelt.Length / 2 - beamTruss.Truss.ProfileBelt.Radius, 0, -beamTruss.Truss.ProfileBelt.Length / 2);
                break;
            case Direction.Left:
                transform.localPosition = new Vector3(-beamTruss.Truss.ProfileBelt.Length / 2, 0, -(beamTruss.Truss.ProfileBelt.Length / 2 - beamTruss.Truss.ProfileBelt.Radius));
                transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
                break;
            case Direction.Back:
                transform.localPosition = new Vector3(-beamTruss.Truss.ProfileBelt.Length / 2 + beamTruss.Truss.ProfileBelt.Radius, 0, beamTruss.Truss.ProfileBelt.Length / 2);
                transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                break;
            default:
                transform.localPosition = new Vector3(beamTruss.Truss.ProfileBelt.Length / 2, 0, (beamTruss.Truss.ProfileBelt.Length / 2 - beamTruss.Truss.ProfileBelt.Radius));
                transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
                break;
        }
    }
}