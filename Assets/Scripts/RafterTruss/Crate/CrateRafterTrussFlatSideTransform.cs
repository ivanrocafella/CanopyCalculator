using Assets.Models.Enums;
using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.IO;

public class CrateRafterTrussFlatSideTransform : MonoBehaviour
{
    private string path;
    private RafterTruss rafterTruss;
    public Direction Direction;
    public StandartNonStandart StandartNonStandart;
    public HeigthLengthProfile HeigthLengthProfile;
    // Start is called before the first frame update
    void Start()
    {
        path = Path.Combine(Application.dataPath, "JSONs", "JSON.json");
        rafterTruss = new("тл 300", path);
        Debug.Log($"rafterTruss.CountCratesStandart:{rafterTruss.CountCratesStandart}");
        Debug.Log($"rafterTruss.LengthDiagonalNonStandartCrate:{rafterTruss.LengthDiagonalNonStandartCrate}");
        Debug.Log($"rafterTruss.PlaceOneCrateStandart:{rafterTruss.PlaceOneCrateStandart}");
        Debug.Log($"rafterTruss.DimenOneCrateNonStandart:{rafterTruss.DimenOneCrateNonStandart}");
        Debug.Log($"rafterTruss.PlaceAllStandartCrates:{rafterTruss.PlaceAllStandartCrates}");
        Debug.Log($"rafterTruss.LengthTop:{rafterTruss.LengthTop}");
        Debug.Log($"rafterTruss.AngleDiagonalNonStandartCrate:{rafterTruss.AngleDiagonalNonStandartCrate}");
        Debug.Log($"rafterTruss.PieceMidToExter:{rafterTruss.PieceMidToExter}");
        Debug.Log($"rafterTruss.PlaceAllNonStandartCrates:{rafterTruss.PlaceAllNonStandartCrates}");
        Debug.Log($"rafterTruss.FormulaAngle: Math.Atan(({rafterTruss.Height} - {rafterTruss.ProfileBelt.Height}) / {rafterTruss.DimenOneCrateNonStandart}) * 180 / Math.PI)");
        Debug.Log($"rafterTruss.AngleNonStandartCrate:{rafterTruss.AngleNonStandartCrate}");
        Debug.Log($"rafterTruss.LengthNonStandartCrate:{rafterTruss.LengthNonStandartCrate}");
        Debug.Log($"rafterTruss.PerspectWidthHalfNonStandartCrate:{rafterTruss.PerspectWidthHalfNonStandartCrate}");


        float dimen = HeigthLengthProfile == HeigthLengthProfile.Heigth ? rafterTruss.ProfileCrate.Height : rafterTruss.ProfileCrate.Length;
        float length = StandartNonStandart == StandartNonStandart.NonStandart ? rafterTruss.LengthNonStandartCrate : rafterTruss.LengthCrate;
        switch (Direction)
        {
            case Direction.Right:
                transform.localPosition = new Vector3((dimen - rafterTruss.ProfileCrate.Thickness) / 2, length / 2, 0);
                break;
            case Direction.Left:
                transform.localPosition = new Vector3(-(dimen - rafterTruss.ProfileCrate.Thickness) / 2, length / 2, 0);
                break;
            case Direction.Back:
                transform.localPosition = new Vector3(0, length / 2, (dimen - rafterTruss.ProfileCrate.Thickness) / 2);
                transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
                break;
            default:
                transform.localPosition = new Vector3(0, length / 2, -(dimen - rafterTruss.ProfileCrate.Thickness) / 2);
                transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
