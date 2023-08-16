using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltBottomRafterTrussTransform : MonoBehaviour
{
    private readonly RafterTruss rafterTruss = new();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(rafterTruss.Height - rafterTruss.ProfileBelt.Width
            , (rafterTruss.LengthTop - rafterTruss.LengthBottom) / 2, 0);
    }
}
