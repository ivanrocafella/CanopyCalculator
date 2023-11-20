using Assets.Models;
using Assets.Models.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class GirderGenerator : MonoBehaviour
{
    private string path;
    [NonSerialized]
    public KindProfilePipe KindRofile;
    [NonSerialized]
    public float Step;
    public Girder girder;
    private PlanCanopy planColumn;     
    // Start is called before the first frame update
    private void Awake()
    {
        planColumn = GameObject.FindGameObjectWithTag("PlanCanopy").GetComponent<PlanCanopyGenerator>().MakePlanCanopy();
        KindRofile = planColumn.KindProfileGirder;
        Step = planColumn.StepGirder;
        path = Path.Combine(Application.dataPath, "Resources", "ProfilesPipe.json");
        girder = new(KindRofile.ToString().Insert(5, " ").Replace("_", "."), path, planColumn)
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
