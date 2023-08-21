using Assets.Models;
using Assets.Models.Enums;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class CrateRafterTrussTransform : MonoBehaviour
{
    private readonly RafterTruss rafterTruss = new();
    public StandartNonStandart StandartNonStandart;
    // Start is called before the first frame update
    void Start()
    {
        float positionY;
        float angle;
        switch (StandartNonStandart)
        {
            case StandartNonStandart.NonStandart:
                positionY = rafterTruss.LengthTop - rafterTruss.Tail - rafterTruss.PerspectWidthHalfNonStandartCrate;
                angle = 180 + rafterTruss.AngleNonStandartCrate;
                break;
            default:
                positionY = rafterTruss.Tail + rafterTruss.PieceMidToExter;
                angle = -rafterTruss.AngleCrateInDegree;
                break;
        }
        transform.localPosition = new Vector3 (0, positionY, 0);
        transform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
