using Assets.Models;
using Assets.Models.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private GameObject groupMainButton;
    [SerializeField]
    private Button LoginPageLink;
    [SerializeField]
    private Button MaterialsPageLink;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        groupMainButton.SetActive(true);
#endif
        UserManager userManager = UserManager.GetInstance();
        if (!UserManager.instance.isLogin)
        { 
            LoginPageLink.gameObject.SetActive(true);
            LoginPageLink.onClick.AddListener(ButtonClickHandlerForLoginPage);
        }
        else 
        {
            LoginPageLink.gameObject.SetActive(false);
            MaterialsPageLink.gameObject.SetActive(true);
            MaterialsPageLink.onClick.AddListener(ButtonClickHandlerForProfilesPage);
        }
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

    void ButtonClickHandlerForLoginPage()
    {
        // Запускаем корутину с задержкой
        StartCoroutine(ToLoginPage());
    }

    IEnumerator ToLoginPage()
    {
        SceneManager.LoadScene("LoginScene");
        yield return null;
    }

    void ButtonClickHandlerForProfilesPage()
    {
        // Запускаем корутину с задержкой
        StartCoroutine(ToProfilesPage());
    }

    IEnumerator ToProfilesPage()
    {
        SceneManager.LoadScene("ProfilesScene");
        yield return null;
    }
}
