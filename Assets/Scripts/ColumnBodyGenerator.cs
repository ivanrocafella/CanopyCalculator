using Assets.Models.Enums;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ColumnBodyGenerator : MonoBehaviour
{
    private string path;
    private void Awake()
    {
        path = Path.Combine(Application.dataPath, "JSONs", "Materials.json");
        path = Path.Combine(Application.dataPath, "JSONs", "Trusses.json");
        //rafterTrussForRead = new(KindTruss.ToString().Insert(2, " "), path);
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
