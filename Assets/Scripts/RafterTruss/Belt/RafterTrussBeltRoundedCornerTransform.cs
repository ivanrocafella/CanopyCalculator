using Assets.Models.Enums;
using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RafterTrussBeltRoundedCornerTransform : MonoBehaviour
{
    private readonly RafterTruss rafterTruss = new();
    public Direction Direction;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0.1f, 1f, 0.1f);
        switch (Direction)
        {
            case Direction.Right:
                transform.localPosition = new Vector3(rafterTruss.ProfileBelt.Length / 2 - rafterTruss.ProfileBelt.Radius, 0, -rafterTruss.ProfileBelt.Length / 2);
                break;
            case Direction.Left:
                transform.localPosition = new Vector3(-rafterTruss.ProfileBelt.Length / 2, 0, -(rafterTruss.ProfileBelt.Length / 2 - rafterTruss.ProfileBelt.Radius));
                transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
                break;
            case Direction.Back:
                transform.localPosition = new Vector3(-rafterTruss.ProfileBelt.Length / 2 + rafterTruss.ProfileBelt.Radius, 0, rafterTruss.ProfileBelt.Length / 2);
                transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                break;
            default:
                transform.localPosition = new Vector3(rafterTruss.ProfileBelt.Length / 2, 0, (rafterTruss.ProfileBelt.Length / 2 - rafterTruss.ProfileBelt.Radius));
                transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
