using Assets.Models;
using Assets.Services;
using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColFlatSideGeneratorProfilePipe : MonoBehaviour
{
    private ColumnBody columnBody;
    public KindLength selectedKindLength;
    // Start is called before the first frame update
    void Start()
    {
       
        //ApplyMaterial(mesh, "Standard", Color.black);
    }

    private void Awake()
    {
       
    }

    // Update is called once per frames
    void Update()
    {

    }

    public void MakeMesh()
    {
        columnBody = GameObject.FindGameObjectWithTag("ProfilePipeTest").GetComponent<ProfilePipeGenerator>().ColumnBody;
        columnBody.SetHeight(selectedKindLength);
        // Setting roundness profile or not 
        Mesh mesh = ValAction.withRadius ? _3dObjectConstructor.CreatePlate(columnBody.Profile.Thickness, columnBody.Height, columnBody.Profile.Height, columnBody.Profile.Radius) :
                                 _3dObjectConstructor.CreatePlate(columnBody.Profile.Thickness, columnBody.Height, columnBody.Profile.Height, 0);
        transform.GetComponent<MeshFilter>().mesh = mesh;
    }
}
