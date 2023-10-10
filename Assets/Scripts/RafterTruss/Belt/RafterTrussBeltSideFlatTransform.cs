using Assets.Models;
using Assets.Models.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RafterTrussBeltSideFlatTransform : MonoBehaviour
{
    private RafterTruss rafterTruss;
    public Direction Direction;
    public KindLength KindLength;
    // Start is called before the first frame update
    void Start()
    {
        rafterTruss = GameObject.FindGameObjectsWithTag("RafterTruss")[0].GetComponent<RafterTrussGenerator>().rafterTrussForRead;

        float length = KindLength switch
        {
            KindLength.Short => rafterTruss.LengthBottom,
            _ => rafterTruss.LengthTop
        };

        switch (Direction)
        {
            case Direction.Right:
                transform.localPosition = new Vector3((rafterTruss.Truss.ProfileBelt.Length - rafterTruss.Truss.ProfileBelt.Thickness) / 2, length / 2, 0);
                break;
            case Direction.Left:
                transform.localPosition = new Vector3(-(rafterTruss.Truss.ProfileBelt.Length - rafterTruss.Truss.ProfileBelt.Thickness) / 2, length / 2, 0);
                break;
            case Direction.Back:
                transform.localPosition = new Vector3(0, length / 2, (rafterTruss.Truss.ProfileBelt.Length - rafterTruss.Truss.ProfileBelt.Thickness) / 2);
                transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
                break;
            default:
                transform.localPosition = new Vector3(0, length / 2, -(rafterTruss.Truss.ProfileBelt.Length - rafterTruss.Truss.ProfileBelt.Thickness) / 2);
                transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
