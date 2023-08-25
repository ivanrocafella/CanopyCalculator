using Assets.Models.Enums;
using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CrateRafterTrussRoundedTransform : MonoBehaviour
{
    private string path;
    private RafterTruss rafterTruss;
    public Direction Direction;
    // Start is called before the first frame update
    void Start()    
    {
        rafterTruss = GameObject.FindGameObjectsWithTag("RafterTruss")[0].GetComponent<RafterTrussGenerator>().rafterTrussForRead;
        transform.localScale = new Vector3(0.1f, 1, 0.1f);
        switch (Direction)
        {
            case Direction.Right:
                transform.localPosition = new Vector3(rafterTruss.Truss.ProfileCrate.Height / 2 - rafterTruss.Truss.ProfileCrate.Radius, 0, -rafterTruss.Truss.ProfileCrate.Length / 2);
                break;
            case Direction.Left:
                transform.localPosition = new Vector3(-rafterTruss.Truss.ProfileCrate.Height / 2, 0, -(rafterTruss.Truss.ProfileCrate.Length / 2 - rafterTruss.Truss.ProfileCrate.Radius));
                transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
                break;
            case Direction.Back:
                transform.localPosition = new Vector3(-rafterTruss.Truss.ProfileCrate.Height / 2 + rafterTruss.Truss.ProfileCrate.Radius, 0, rafterTruss.Truss.ProfileCrate.Length / 2);
                transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                break;
            default:
                transform.localPosition = new Vector3(rafterTruss.Truss.ProfileCrate.Height / 2, 0, (rafterTruss.Truss.ProfileCrate.Length / 2 - rafterTruss.Truss.ProfileCrate.Radius));
                transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
