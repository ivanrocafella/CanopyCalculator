using Assets.Models.Enums;
using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using Assets.Utils;
using Material = Assets.Models.Material;
using System.Linq;

public class ColumnGenerator : MonoBehaviour
{
    [NonSerialized]
    public KindProfilePipe KindProfile;
    public ColumnBody ColumnBody;
    public PlanCanopy planColumn;
    public ProfilePipeDataList profilePipeDataList;
    private ProfilePipe profilePipe;
    private string nameProfile;

    private void Awake()
    {
        print("ColumnGenerator");
        planColumn = GameObject.FindGameObjectWithTag("PlanCanopy").GetComponent<PlanCanopyGenerator>().MakePlanCanopy();
        KindProfile = planColumn.KindProfileColumn;
        nameProfile = KindProfile.ToString().Insert(5, " ").Replace("_", ".");
        profilePipe = ScriptObjectsAction.GetProfilePipeByName(nameProfile, profilePipeDataList);
        ColumnBody = new(profilePipe, planColumn);
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
