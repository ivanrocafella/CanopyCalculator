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
    public KindProfilePipe KindProfile;
    public ColumnBody ColumnBody;
    public PlanCanopy planColumn;

    private void Awake()
    {
        planColumn = GameObject.FindGameObjectWithTag("PlanCanopy").GetComponent<PlanCanopyGenerator>().MakePlanCanopy();
        KindProfile = planColumn.KindProfileColumn;
        path = Path.Combine(Application.dataPath, "Resources", "ProfilesPipe.json");
        string nameProfile = KindProfile.ToString().Insert(5, " ").Replace("_", ".");
        Debug.Log($"path: {path}");
        ColumnBody = new(nameProfile, path, planColumn);
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
