using Assets.Models.Enums;
using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;

public class CrateRafterTrussFlatSideTransform : MonoBehaviour
{
    private readonly RafterTruss rafterTruss = new();
    public Direction Direction;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"rafterTruss.CountCratesStandart:{rafterTruss.CountCratesStandart}");
        Debug.Log($"rafterTruss.LengthDiagonalNonStandartCrate:{rafterTruss.LengthDiagonalNonStandartCrate}");
        Debug.Log($"rafterTruss.PlaceOneCrateStandart:{rafterTruss.PlaceOneCrateStandart}");
        Debug.Log($"rafterTruss.PlaceOneCrateNonStandart:{rafterTruss.DimenOneCrateNonStandart}");
        Debug.Log($"rafterTruss.PlaceAllStandartCrates:{rafterTruss.PlaceAllStandartCrates}");
        Debug.Log($"rafterTruss.LengthTop:{rafterTruss.LengthTop}");
        Debug.Log($"rafterTruss.AngleDiagonalNonStandartCrate:{rafterTruss.AngleDiagonalNonStandartCrate}");
        Debug.Log($"rafterTruss.PieceMidToExter:{rafterTruss.PieceMidToExter}");
        Debug.Log($"rafterTruss.PlaceAllNonStandartCrates:{rafterTruss.PlaceAllNonStandartCrates}");
        Debug.Log($"rafterTruss.FormulaAngle: Math.Atan(({rafterTruss.Height} - {rafterTruss.ProfileBelt.Width}) / {rafterTruss.DimenOneCrateNonStandart}) * 180 / Math.PI)");
        Debug.Log($"rafterTruss.AngleNonStandartCrate:{rafterTruss.AngleNonStandartCrate}");
        Debug.Log($"rafterTruss.LengthNonStandartCrate:{rafterTruss.LengthNonStandartCrate}");

        



        switch (Direction)
        {
            case Direction.Right:
                transform.localPosition = new Vector3((rafterTruss.ProfileCrate.Width - rafterTruss.ProfileCrate.Thickness) / 2, rafterTruss.LengthCrate / 2, 0);
                break;
            case Direction.Left:
                transform.localPosition = new Vector3(-(rafterTruss.ProfileCrate.Width - rafterTruss.ProfileCrate.Thickness) / 2, rafterTruss.LengthCrate / 2, 0);
                break;
            case Direction.Back:
                transform.localPosition = new Vector3(0, rafterTruss.LengthCrate / 2, (rafterTruss.ProfileCrate.Width - rafterTruss.ProfileCrate.Thickness) / 2);
                transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
                break;
            default:
                transform.localPosition = new Vector3(0, rafterTruss.LengthCrate / 2, -(rafterTruss.ProfileCrate.Width - rafterTruss.ProfileCrate.Thickness) / 2);
                transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
