using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BeltBottomRafterTrussTransform : MonoBehaviour
{
    private string path; 
    private RafterTruss rafterTruss;
    // Start is called before the first frame update
    void Start()
    {
        path = Path.Combine(Application.dataPath, "JSONs", "JSON.json");
        rafterTruss =  new("тл 300", path);
        transform.localPosition = new Vector3(rafterTruss.Height - rafterTruss.ProfileBelt.Height
           , (rafterTruss.LengthTop - rafterTruss.LengthBottom) / 2, 0);

    }

    // Update is called once per frame
    void Update()
    {
    }
}
