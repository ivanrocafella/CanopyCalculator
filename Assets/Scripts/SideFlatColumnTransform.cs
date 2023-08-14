using Assets.Models;
using Assets.Models.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideFlatColumnTransform : MonoBehaviour
{
    public Direction Direction;
    public KindLength KindLength;
    private ColumnBody columnBody;
    // Start is called before the first frame update
    void Start()
    {
        columnBody = new(KindLength);
        int thicknessInt = (int)Math.Round(columnBody.Material.Thickness, 0, MidpointRounding.AwayFromZero);
        switch (Direction)
        {
            case Direction.Right:
                transform.localPosition = new Vector3((columnBody.Material.Length - thicknessInt) / 2, columnBody.Height / 2, 0);
                break;
            case Direction.Left:
                transform.localPosition = new Vector3(-(columnBody.Material.Length - thicknessInt) / 2, columnBody.Height / 2, 0);
                break;
            case Direction.Back:
                transform.localPosition = new Vector3(0, columnBody.Height / 2, (columnBody.Material.Length - thicknessInt) / 2);
                transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
                break;
            default:
                transform.localPosition = new Vector3(0, columnBody.Height / 2, -(columnBody.Material.Length - thicknessInt) / 2);
                transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
