using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CrateRafterTrussTransform : MonoBehaviour
{
    private readonly RafterTruss rafterTruss = new();
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3 (rafterTruss.Height - rafterTruss.ProfileBelt.Width, rafterTruss.LengthTop / 2 + rafterTruss.GapHalf, 0);
        transform.localRotation = Quaternion.Euler(0f, 0f, rafterTruss.AngleCrate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
