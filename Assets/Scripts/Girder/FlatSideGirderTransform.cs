using Assets.Models;
using Assets.Models.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FlatSideGirderTransform : MonoBehaviour
{
    private Girder girder;
    public Direction Direction;
    // Start is called before the first frame update
    void Start()
    {
        girder = GameObject.FindGameObjectWithTag("Girder").GetComponent<GirderGenerator>().girder;

        switch (Direction)
        {
            case Direction.Right:
                transform.localPosition = new Vector3((girder.Profile.Length - girder.Profile.Thickness) / 2, girder.Length / 2, 0);
                break;
            case Direction.Left:
                transform.localPosition = new Vector3(-(girder.Profile.Length - girder.Profile.Thickness) / 2, girder.Length / 2, 0);
                break;
            case Direction.Back:
                transform.localPosition = new Vector3(0, girder.Length / 2, (girder.Profile.Length - girder.Profile.Thickness) / 2);
                transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
                break;
            default:
                transform.localPosition = new Vector3(0, girder.Length / 2, -(girder.Profile.Length - girder.Profile.Thickness) / 2);
                transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
