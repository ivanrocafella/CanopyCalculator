using Assets.Models;
using Assets.Models.Enums;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class GirderGenerator : MonoBehaviour
{
    private string path;
    public KindMaterial KindMaterial;
    public float Step;
    public Girder girder;
    private PlanColumn planColumn;     
    // Start is called before the first frame update
    private void Awake()
    {
        planColumn = GameObject.FindGameObjectWithTag("PlanCanopy").GetComponent<PlanCanopyGenerator>().MakePlanColumn();
        path = Path.Combine(Application.dataPath, "JSONs", "Materials.json");
        girder = new(KindMaterial.ToString().Insert(5, " ").Replace("_", "."), path, planColumn);
        girder.Step = Step = Step != 0 ? Step : 500;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
