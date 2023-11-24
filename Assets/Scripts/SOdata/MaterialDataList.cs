using Assets.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MaterialDataList", menuName = "Material DataList", order = 56)]
public class MaterialDataList : ScriptableObject
{
    public MaterialData[] materialsData;
}
