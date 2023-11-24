using Assets.Models;
using Assets.Models.Enums;
using Assets.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class GirderGenerator : MonoBehaviour
{
    [NonSerialized]
    public KindProfilePipe KindRofile;
    [NonSerialized]
    public float Step;
    public Girder girder;
    private PlanCanopy planColumn;
    private string nameProfile;
    private ProfilePipe profilePipe;
    public ProfilePipeDataList ProfilePipeDataList;
    // Start is called before the first frame update
    private void Awake()
    {
        planColumn = GameObject.FindGameObjectWithTag("PlanCanopy").GetComponent<PlanCanopyGenerator>().MakePlanCanopy();
        KindRofile = planColumn.KindProfileGirder;
        nameProfile = KindRofile.ToString().Insert(5, " ").Replace("_", ".");
        Step = planColumn.StepGirder;
        profilePipe = ScriptObjectsAction.GetProfilePipeByName(nameProfile, ProfilePipeDataList);
        girder = new(profilePipe, planColumn)
        {
            Step = Step = Step != 0 ? Step : 500
        };
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
