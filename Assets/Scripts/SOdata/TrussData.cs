using Assets.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New TrussData", menuName = "Truss Data", order = 53)]
public class TrussData : ScriptableObject
{
    [SerializeField]
    private string trussName;
    [SerializeField]
    private float height;
    [SerializeField]
    private float gap;
    [SerializeField]
    private float gapExter;
    [SerializeField]
    private float lengthCrate;
    [SerializeField]
    private float angleCrateInDegree;
    public float pricePerM; // u.m. = $ / m
    public ProfilePipeData profileBeltData;
    public ProfilePipeData profileCrateData;

    public string Name { get => trussName; }
    public float Height { get => height; }
    public float Gap { get => gap; }
    public float GapExter { get => gapExter; }
    public float GapHalf { get => gap / 2; }
    public float LengthCrate { get => lengthCrate; }
    public float AngleCrateInDegree { get => angleCrateInDegree; } // u.m. = degree
    public float AngleCrate { get => Mathf.Deg2Rad * angleCrateInDegree; } // u.m. = rad 
    public float MomentInertia { get => (float)Math.Round(2 * profileBeltData.Area * Mathf.Pow((height - profileBeltData.Height) / 20, 2), 4); } // u.m. = sm4 
    public float MomentResistance { get => (float)Math.Round(MomentInertia * 2 / ((height - profileBeltData.Height) / 10), 4); } // u.m. = sm3

}
