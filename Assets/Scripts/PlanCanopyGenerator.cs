using Assets.Models;
using Assets.Models.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanCanopyGenerator : MonoBehaviour
{
    public float SizeByX;
    public float SizeByZ;
    public float SizeByY;
    public float SlopeInDegree;
    public int CountStep;
    public float OutputRafter;
    public float OutputGirder;
    public KindProfilePipe KindProfileColumn;
    public KindTruss KindTrussBeam;
    public KindTruss KindTrussRafter;
    public float StepRafter;
    public KindProfilePipe KindProfileGirder;
    public float StepGirder;
    public KindMaterial KindMaterial;
    [SerializeField]
    private Button exitButton;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        exitButton.gameObject.SetActive(true);
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public PlanCanopy MakePlanCanopy()
    {
        PlanCanopy planColumn = new()
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
