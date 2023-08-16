using Assets.Models;
using Assets.Models.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RafterTrussBeltSideFlatTransform : MonoBehaviour
{
    private readonly RafterTruss rafterTruss = new();
    public Direction Direction;
    public KindLength KindLength;
    // Start is called before the first frame update
    void Start()
    {
        //transform.localScale = new Vector3(0.1f, 1f, 0.1f);

        float length = KindLength switch
        {
            KindLength.Short => rafterTruss.LengthBottom,
            _ => rafterTruss.LengthTop
        };

        switch (Direction)
        {
            case Direction.Right:
                transform.localPosition = new Vector3((rafterTruss.ProfileBelt.Length - rafterTruss.ProfileBelt.Thickness) / 2, length / 2, 0);
                break;
            case Direction.Left:
                transform.localPosition = new Vector3(-(rafterTruss.ProfileBelt.Length - rafterTruss.ProfileBelt.Thickness) / 2, length / 2, 0);
                break;
            case Direction.Back:
                transform.localPosition = new Vector3(0, length / 2, (rafterTruss.ProfileBelt.Length - rafterTruss.ProfileBelt.Thickness) / 2);
                transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
                break;
            default:
                transform.localPosition = new Vector3(0, length / 2, -(rafterTruss.ProfileBelt.Length - rafterTruss.ProfileBelt.Thickness) / 2);
                transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
