using Assets.Scripts.SOdata;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MountUnitColumnBeamTrussDataList", menuName = "MountUnitColumnBeamTruss DataList", order = 59)]
public class MountUnitColumnBeamTrussDataList : ScriptableObject
{
    public MountUnitColumnBeamTrussData[] mountUnitColumnBeamTrussDatas;
}
