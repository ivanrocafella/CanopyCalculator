using Assets.Models.Enums;
using Assets.Models;
using Assets.Services;
using Assets.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfilePipeGenerator : MonoBehaviour
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
        StartCoroutine(MakeProfile());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MakeProfile()
    {
        planColumn = GameObject.FindGameObjectWithTag("PlanCanopy").GetComponent<PlanCanopyGenerator>().MakePlanCanopy();
        KindProfile = planColumn.KindProfileColumn;
        nameProfile = KindProfile.ToString().Insert(5, " ").Replace("_", ".");
        profilePipe = ScriptObjectsAction.GetProfilePipeByName(nameProfile, profilePipeDataList);
        ColumnBody = new(profilePipe, planColumn);
        yield return null;
        CombineInstance[] combine = new CombineInstance[8];
        for (int i = 0, j = 4; i < 4; i++, j++)
        { 
            transform.GetChild(0).GetChild(i).GetComponent<ColFlatSideGeneratorProfilePipe>().MakeMesh();
            combine[i].mesh = transform.GetChild(0).GetChild(i).GetComponent<MeshFilter>().mesh;
            combine[i].transform = transform.GetChild(0).GetChild(i).GetComponent<MeshFilter>().transform.localToWorldMatrix;
            transform.GetChild(0).GetChild(i).gameObject.SetActive(false);

            transform.GetChild(0).GetChild(j).GetComponent<ColRoundedCornerProfilePipe>().MakeMesh();
            combine[j].mesh = transform.GetChild(0).GetChild(j).GetComponent<MeshFilter>().mesh;
            combine[j].transform = transform.GetChild(0).GetChild(j).GetComponent<MeshFilter>().transform.localToWorldMatrix;
            transform.GetChild(0).GetChild(j).gameObject.SetActive(false);
        }
        Mesh mesh = new();
        mesh.CombineMeshes(combine);
        transform.GetComponent<MeshFilter>().mesh = mesh;
        ApplyMaterial(mesh, "Standard", Color.black);
    }

    void ApplyMaterial(Mesh mesh, string shaderName, Color color)
    {
        GetComponent<MeshFilter>().mesh = mesh;
        MeshRenderer meshRenderer = !gameObject.GetComponent<MeshRenderer>()
            ? gameObject.AddComponent<MeshRenderer>()
            : gameObject.GetComponent<MeshRenderer>();
        UnityEngine.Material material = new(Shader.Find(shaderName))
        {
            color = color
        };
        meshRenderer.material = material;
    }
}
