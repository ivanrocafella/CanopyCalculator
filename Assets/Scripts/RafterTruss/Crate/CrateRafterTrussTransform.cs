using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class CrateRafterTrussTransform : MonoBehaviour
{
    private readonly RafterTruss rafterTruss = new();
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3 (0, rafterTruss.Tail + rafterTruss.PieceMidToExter, 0);
        transform.localRotation = Quaternion.Euler(0f, 0f, -rafterTruss.AngleCrateInDegree);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
