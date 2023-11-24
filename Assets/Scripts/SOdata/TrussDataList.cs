using Assets.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New TrussDataList", menuName = "Truss DataList", order = 54)]
public class TrussDataList : ScriptableObject
{
    public TrussData[] trussesData;
}
