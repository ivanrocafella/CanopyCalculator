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
    public KindProfilePipe KindProfileColumn;
    public KindTruss KindTrussBeam;
    public KindTruss KindTrussRafter;
    public float StepRafter;
    public KindProfilePipe KindProfileGirder;
    public float StepGirder;
    public KindMaterial KindMaterial;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public PlanCanopy MakePlanCanopy()
    {
        PlanCanopy planColumn = new PlanCanopy()
        {
            SizeByX = SizeByX,
            SizeByZ = SizeByZ,
            SizeByY = SizeByY,
            SlopeInDegree = SlopeInDegree,
            CountStep = CountStep,
            OutputRafter = OutputRafter,
            OutputGirder = OutputGirder,
            KindProfileColumn = KindProfileColumn,
            KindTrussBeam = KindTrussBeam,
            KindTrussRafter = KindTrussRafter,
            StepRafter = StepRafter,
            KindProfileGirder = KindProfileGirder,
            StepGirder = StepGirder,
            KindMaterial = KindMaterial
        };
        return planColumn;
    }
}
