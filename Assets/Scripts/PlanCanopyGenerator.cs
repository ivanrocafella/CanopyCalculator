using Assets.Models;
using Assets.Models.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanCanopyGenerator : MonoBehaviour
{
    public int SizeByX;
    public int SizeByZ;
    public int SizeByY;
    public int SlopeInDegree;
    public int CountStep;
    public int OutputRafter;
    public int OutputGirder;
    public KindMaterial KindMaterialColumn;
    public KindTruss KindTrussBeam;
    public KindTruss KindTrussRafter;
    public float StepRafter;
    public KindMaterial KindMaterialGirder;
    public float StepGirder;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public PlanColumn MakePlanColumn()
    {
        PlanColumn planColumn = new PlanColumn()
        {
            SizeByX = SizeByX,
            SizeByZ = SizeByZ,
            SizeByY = SizeByY,
            SlopeInDegree = SlopeInDegree,
            CountStep = CountStep,
            OutputRafter = OutputRafter,
            OutputGirder = OutputGirder,
            KindMaterialColumn = KindMaterialColumn,
            KindTrussBeam = KindTrussBeam,
            KindTrussRafter = KindTrussRafter,
            StepRafter = StepRafter,
            KindMaterialGirder = KindMaterialGirder,
            StepGirder = StepGirder
        };
        return planColumn;
    }
}
