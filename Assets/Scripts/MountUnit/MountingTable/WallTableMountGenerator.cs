using Assets.Models;
using Assets.Services;
using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Material = UnityEngine.Material;

public class WallTableMountGenerator : MonoBehaviour
{
    [SerializeField]
    private MountUnitColumnBeamTrussDataList MountUnitColumnBeamTrussDataList;
    private Canopy Canopy;
    public Material material;
    // Start is called before the first frame update
    void Start()
    {
        //thickness of plate = 4 mm
        Canopy = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(e => e.CompareTag("Canopy")).GetComponent<CanopyGenerator>().Canopy;
        //Mesh mesh = _3dObjectConstructor.CreatePlate(columnBody.Profile.Thickness, columnBody.Height, columnBody.Profile.Height, columnBody.Profile.Radius);
        //ValAction.ApplyMaterial(mesh, transform.gameObject, material);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
