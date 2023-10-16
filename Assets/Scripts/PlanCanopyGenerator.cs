using Assets.Models;
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
            OutputGirder = OutputGirder
        };
        return planColumn;
    }
}
