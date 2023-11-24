using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MaterialData", menuName = "Material Data", order = 55)]
public class MaterialData : ScriptableObject
{
    [SerializeField]
    private string materialName;
    [SerializeField]
    private float yieldStrength; // u.m. = kg/sm2
    [SerializeField]
    private float tensileStrength; // u.m. = kg/sm2
    [SerializeField]
    private float elastiModulus; // u.m. = kg/sm2

    public string Name { get => materialName; }
    public float YieldStrength { get => yieldStrength; } // u.m. = kg/sm2
    public float TensileStrength { get => tensileStrength; } // u.m. = kg/sm2
    public float ElastiModulus { get => elastiModulus; } // u.m. = kg/sm2
}
