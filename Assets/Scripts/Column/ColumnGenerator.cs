using Assets.Models.Enums;
using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class ColumnGenerator : MonoBehaviour
{
    private string path;
    [NonSerialized]
    public KindMaterial KindMaterial;
    public ColumnBody ColumnBody;
    public PlanColumn planColumn;

    private void Awake()
    {
        planColumn = GameObject.FindGameObjectWithTag("PlanCanopy").GetComponent<PlanCanopyGenerator>().MakePlanColumn();
        KindMaterial = planColumn.KindMaterialColumn;
        path = Path.Combine(Application.dataPath, "JSONs", "Materials.json");
        string nameMaterial = KindMaterial.ToString().Insert(5, " ").Replace("_", ".");
        ColumnBody = new(nameMaterial, path, planColumn);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
