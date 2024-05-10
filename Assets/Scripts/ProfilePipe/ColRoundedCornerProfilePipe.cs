using Assets.Models;
using Assets.Services;
using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColRoundedCornerProfilePipe : MonoBehaviour
{
    public KindLength KindLength;
    private ColumnBody columnBody;

    private void Start()
    {
    }

    public void MakeMesh()
    {
        columnBody = GameObject.FindGameObjectWithTag("ProfilePipeTest").GetComponent<ProfilePipeGenerator>().ColumnBody;
        columnBody.SetHeight(KindLength);
        // Setting roundness profile or not
        if (ValAction.withRadius)
        {
            Mesh mesh = _3dObjectConstructor.CreateRoundedCorner(columnBody.Profile.Radius, columnBody.Height
                , columnBody.Profile.Thickness);
            transform.GetComponent<MeshFilter>().mesh = mesh;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
