using Assets.Models.Enums;
using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoundedCornerColumnTransform : MonoBehaviour
{
    public Direction Direction;
    public KindLength KindLength;
    private ColumnBody columnBody;
    // Start is called before the first frame update
    void Start()
    {
        columnBody = new(KindLength);
        int radiusInt = (int)Math.Round(columnBody.Material.Radius, 0, MidpointRounding.AwayFromZero);
        switch (Direction)
        {
            case Direction.Right:
                transform.localPosition = new Vector3(columnBody.Material.Length / 2 - radiusInt, 0, -columnBody.Material.Length / 2);
                break;
            case Direction.Left:
                transform.localPosition = new Vector3(-columnBody.Material.Length / 2, 0, -(columnBody.Material.Length / 2 - radiusInt));
                transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
                break;
            case Direction.Back:
                transform.localPosition = new Vector3(-columnBody.Material.Length / 2 + radiusInt, 0, columnBody.Material.Length / 2);
                transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                break;
            default:
                transform.localPosition = new Vector3(columnBody.Material.Length / 2, 0, (columnBody.Material.Length / 2 - radiusInt));
                transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
