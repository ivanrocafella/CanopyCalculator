using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RafterTrussPositionTransform : MonoBehaviour
{
    private readonly RafterTruss rafterTruss = new();
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(1000, rafterTruss.LengthTop / 2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
