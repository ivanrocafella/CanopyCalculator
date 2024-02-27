using Assets.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DollarRateData", menuName = "DollarRate Data", order = 57)]
public class DollarRateData : ScriptableObject
{
    [SerializeField]
    private float rate; // u.m. = usd/kgs
    public float Rate { get => rate; } // u.m. = usd/kgs
}
