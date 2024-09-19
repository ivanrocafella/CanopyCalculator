using Assets.Models;
using Assets.Scripts.SOdata;
using Assets.Services;
using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Material = UnityEngine.Material;

public class ShelfTableMountGenerator : MonoBehaviour
{
    [SerializeField]
    private MountUnitBeamRafterTrussDataList mountUnitBeamRafterTrussDataList;
    private MountUnitBeamRafterTrussData mountUnitBeamRafterTrussData; 
    private Canopy Canopy;
    public Material material;
    private const int thickness = 4; //thickness of plate = 4 mm

    // Start is called before the first frame update
    void Start()
    {  
        Canopy = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(e => e.CompareTag("Canopy")).GetComponent<CanopyGenerator>().Canopy;
        mountUnitBeamRafterTrussData = mountUnitBeamRafterTrussDataList.mountUnitBeamRafterTrussDatas.FirstOrDefault(e => e.RafterTrussName == Canopy.RafterTruss.Truss.Name);
        Mesh mesh = _3dObjectConstructor.CreatePlate(mountUnitBeamRafterTrussData.LengthFlangeBeamTruss, Canopy.BeamTruss.Truss.ProfileBelt.Height, thickness, 0);
        //ValAction.ApplyMaterial(mesh, transform.gameObject, material);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
