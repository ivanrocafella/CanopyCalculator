using AsciiFBXExporter;
using Assets.Models;
using Assets.Models.Enums;
using Assets.Services;
using Assets.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;
using Material = Assets.Models.Material;
using Debug = UnityEngine.Debug;
using Vector3 = UnityEngine.Vector3;
using Assets.ModelsRequest;
using Newtonsoft.Json;
using UnityEngine.Networking;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System.Linq;

public class LoadPrefab : MonoBehaviour
{
    public GameObject canopyPrefab;
    private GameObject planCanopy;
    private GameObject canopyObj;
    private const int MultipleForMeter = 1000;
    private const int MultipleForSentimeter = 10;
    private string pathMaterial;
    private string pathProfilesPipe;
    private string pathTrusses;
    public GameObject EmProfilePipeCol;
    private BeamTruss BeamTruss;
    private ColumnBody ColumnBodyHigh;
    private ColumnBody ColumnBodyLow;
    private const float coefficientReliability = 1.4f;
    public MaterialDataList materialDataList;
    public TrussDataList trussDataList;
    public ProfilePipeDataList profilePipeDataList;
    public MountUnitColumnBeamTrussDataList mountUnitColumnBeamTrussDataList;
    public MountUnitBeamRafterTrussDataList mountUnitBeamRafterTrussDataList;
    [SerializeField]
    private Button calculateButton;
    [SerializeField]
    private Button toFbxButton;
    [SerializeField]
    private Button toResultButton;
    [SerializeField]
    private GameObject loadingTextBox;
    public List<ProfilePipe> profilePipes;
    public List<Truss> trusses;
    public List<MountUnitBeamRafterTruss> mountUnitBeamRafterTrusses;
    public List<MountUnitColumnBeamTruss> mountUnitColumnBeamTrusses;
    public DollarRate dollarRate;

    private void Awake()
    {
        StartCoroutine(StartApp());
    }
    // Start is called before the first frame update
    void Start()
    {
        calculateButton.onClick.AddListener(ButtonClickHandlerForCalculate);
        toFbxButton.onClick.AddListener(ButtonClickHandlerForFbx);
        toResultButton.onClick.AddListener(ButtonClickHandlerForResultPage);
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator CalculateButtonClick()
    {
        loadingTextBox.transform.localPosition = new Vector3(-45, -150, 0);
        loadingTextBox.GetComponent<TMP_Text>().fontSize = 36;
        loadingTextBox.GetComponent<TMP_Text>().text = "Загрузка...";
        yield return new WaitForSeconds(0.001f);

        planCanopy = GameObject.FindGameObjectWithTag("PlanCanopy");
        GameObject spanInputGB = GameObject.FindGameObjectWithTag("SpanInput");
        GameObject lengthInputGB = GameObject.FindGameObjectWithTag("LengthInput");
        GameObject heightInputGB = GameObject.FindGameObjectWithTag("HeightInput");
        GameObject slopeInputGB = GameObject.FindGameObjectWithTag("SlopeInput");
        GameObject сountStepColumnInputGB = GameObject.FindGameObjectWithTag("CountStepColumnInput");
        GameObject stepRafterInputGB = GameObject.FindGameObjectWithTag("StepRafterInput");
        GameObject stepGirderInputGB = GameObject.FindGameObjectWithTag("StepGirderInput");
        GameObject outputRafterInputGB = GameObject.FindGameObjectWithTag("OutputRafterInput");
        GameObject outputGirderInputGB = GameObject.FindGameObjectWithTag("OutputGirderInput");
        GameObject workLoadInputGB = GameObject.FindGameObjectWithTag("WorkLoadInput");

        GameObject canopy = GameObject.FindGameObjectWithTag("Canopy");
        StartCoroutine(DestroyGO(canopy));
        StartCoroutine(RunGC());
        StartCoroutine(UnloadUnusedAssets());

        planCanopy.GetComponent<PlanCanopyGenerator>().SizeByX = ValAction.ToFloat(spanInputGB.GetComponent<TMP_InputField>().text) * MultipleForMeter;
        planCanopy.GetComponent<PlanCanopyGenerator>().SizeByZ = ValAction.ToFloat(lengthInputGB.GetComponent<TMP_InputField>().text) * MultipleForMeter;
        planCanopy.GetComponent<PlanCanopyGenerator>().SizeByY = ValAction.ToFloat(heightInputGB.GetComponent<TMP_InputField>().text) * MultipleForMeter;
        planCanopy.GetComponent<PlanCanopyGenerator>().SlopeInDegree = ValAction.ToFloat(slopeInputGB.GetComponent<TMP_InputField>().text);
        planCanopy.GetComponent<PlanCanopyGenerator>().CountStep = (int)ValAction.ToFloat(сountStepColumnInputGB.GetComponent<TMP_InputField>().text);
        planCanopy.GetComponent<PlanCanopyGenerator>().StepRafter = ValAction.ToFloat(stepRafterInputGB.GetComponent<TMP_InputField>().text) * MultipleForSentimeter;
        planCanopy.GetComponent<PlanCanopyGenerator>().StepGirder = ValAction.ToFloat(stepGirderInputGB.GetComponent<TMP_InputField>().text) * MultipleForSentimeter;
        planCanopy.GetComponent<PlanCanopyGenerator>().OutputRafter = ValAction.ToFloat(outputRafterInputGB.GetComponent<TMP_InputField>().text) * MultipleForSentimeter;
        planCanopy.GetComponent<PlanCanopyGenerator>().OutputGirder = ValAction.ToFloat(outputGirderInputGB.GetComponent<TMP_InputField>().text) * MultipleForSentimeter;
        float cargo = ValAction.ToFloat(workLoadInputGB.GetComponent<TMP_InputField>().text) * coefficientReliability;

        string nameMaterial = planCanopy.GetComponent<PlanCanopyGenerator>().KindMaterial.ToString();
        Material material = ScriptObjectsAction.GetMaterialByName(nameMaterial, materialDataList);

        ProfilePipe profilePipeColumn = CalculationColumn.CalculateColumn(planCanopy.GetComponent<PlanCanopyGenerator>().SizeByX
             , planCanopy.GetComponent<PlanCanopyGenerator>().SizeByZ
             , planCanopy.GetComponent<PlanCanopyGenerator>().SizeByY
             , planCanopy.GetComponent<PlanCanopyGenerator>().CountStep, cargo, material, profilePipes);
        Truss trussBeam = CalculationBeamTruss.CalculateBeamTruss(planCanopy.GetComponent<PlanCanopyGenerator>().SizeByX
             , planCanopy.GetComponent<PlanCanopyGenerator>().SizeByZ
             , planCanopy.GetComponent<PlanCanopyGenerator>().CountStep, cargo, material, trusses);
        Truss trussRafter = CalculationRafterTruss.CalculateRafterTruss(planCanopy.GetComponent<PlanCanopyGenerator>().SizeByX
             , planCanopy.GetComponent<PlanCanopyGenerator>().StepRafter
             , planCanopy.GetComponent<PlanCanopyGenerator>().OutputRafter
             , cargo, material, trusses);
        ProfilePipe profilePipeGirder = CalculationGirder.CalculateGirder(planCanopy.GetComponent<PlanCanopyGenerator>().StepRafter
             , planCanopy.GetComponent<PlanCanopyGenerator>().StepGirder
             , planCanopy.GetComponent<PlanCanopyGenerator>().OutputGirder
             , cargo, material, profilePipes);

        if (profilePipeColumn != null && trussBeam != null && trussRafter != null && profilePipeGirder != null)
        {
            planCanopy.GetComponent<PlanCanopyGenerator>().KindProfileColumn = (KindProfilePipe)profilePipes.IndexOf(profilePipeColumn);
            planCanopy.GetComponent<PlanCanopyGenerator>().KindTrussBeam = (KindTruss)trusses.IndexOf(trussBeam);
            planCanopy.GetComponent<PlanCanopyGenerator>().KindTrussRafter = (KindTruss)trusses.IndexOf(trussRafter);
            planCanopy.GetComponent<PlanCanopyGenerator>().KindProfileGirder = (KindProfilePipe)profilePipes.IndexOf(profilePipeGirder);

            MountUnitBeamRafterTruss mountUnitBeamRafterTruss = mountUnitBeamRafterTrusses.Find(e => e.RafterTrussName == trussRafter.Name);
            planCanopy.GetComponent<PlanCanopyGenerator>().KindMountUnitBeamRafterTruss = (KindMountUnitBeamRafterTruss)mountUnitBeamRafterTrusses.IndexOf(mountUnitBeamRafterTruss);
            MountUnitColumnBeamTruss mountUnitColumnBeamTruss = mountUnitColumnBeamTrusses.Find(e => e.BeamTrussName == trussBeam.Name);
            planCanopy.GetComponent<PlanCanopyGenerator>().KindMountUnitColumnBeamTruss = (KindMountUnitColumnBeamTruss)mountUnitColumnBeamTrusses.IndexOf(mountUnitColumnBeamTruss);

            Instantiate(canopyPrefab);
            toFbxButton.interactable = true;
            EmProfilePipeCol.GetComponent<TMP_Text>().text = string.Empty;
        }
        else
        {
            loadingTextBox.GetComponent<TMP_Text>().text = string.Empty;
            List<string> errorMessages = new();
            if (profilePipeColumn == null)
                errorMessages.Add("Превышен допустимый профиль стойки!");
            if (trussBeam == null)
                errorMessages.Add("Превышен допустимый размер балочной фермы!");
            if (trussRafter == null)
                errorMessages.Add("Превышен допустимый размер стропильной фермы!");
            if (profilePipeGirder == null)
                errorMessages.Add("Превышен допустимый профиль прогона!");
            EmProfilePipeCol.GetComponent<TMP_Text>().text = string.Join(" ", errorMessages);
        }

        yield return new WaitForSeconds(0.001f);
        canopyObj = GameObject.FindGameObjectWithTag("Canopy");
        if (canopyObj != null)
            DontDestroyOnLoad(canopyObj);
#if UNITY_WEBGL
        print("UNITY_WEBGL");
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR    
        toFbxButton.gameObject.SetActive(true);
#endif
        toResultButton.gameObject.SetActive(true);   
    }

    IEnumerator ToFbxButtonClick()
    {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        try
        {
            if (!Directory.Exists(Path.Combine(Application.dataPath, "FbxModels")))
                Directory.CreateDirectory(Path.Combine(Application.dataPath, "FbxModels"));
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
        string format = "dd.MM.yyyy_hh.mm.ss";
        string dateTimeNow = DateTime.Now.ToString(format);
        GameObject canopy = GameObject.FindGameObjectWithTag("Canopy");
        string filePath = Path.Combine(Application.dataPath, "FbxModels", $"{canopy.tag}_{dateTimeNow}.fbx");
        Debug.Log(filePath);
        loadingTextBox.GetComponent<TMP_Text>().text = "Сохранение...";
        yield return new WaitForSeconds(0.001f);
        FBXExporter.ExportGameObjToFBX(canopy, filePath);
        toFbxButton.interactable = false;
        loadingTextBox.transform.localPosition = new Vector3(-150, -150, 0);
        loadingTextBox.GetComponent<TMP_Text>().fontSize = 24;
        loadingTextBox.GetComponent<TMP_Text>().text = $"Файл сохранён по пути {filePath}";
#elif UNITY_WEBGL
              try
        {
            if (!Directory.Exists(Path.Combine(Application.streamingAssetsPath, "FbxModels")))
                Directory.CreateDirectory(Path.Combine(Application.streamingAssetsPath, "FbxModels"));
            string filePath = Path.Combine(Application.streamingAssetsPath, "FbxModels", $"canopy.fbx");
            Debug.Log(filePath);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
#else
#endif
        yield return new WaitForSeconds(0.001f);
    }

    void ButtonClickHandlerForCalculate()
    {
        // Запускаем корутину с задержкой
        StartCoroutine(CalculateButtonClick());
    }

    void ButtonClickHandlerForFbx()
    {
        // Запускаем корутину с задержкой
        StartCoroutine(ToFbxButtonClick());
    }

    IEnumerator InitializePrefab()
    {
        Instantiate(canopyPrefab);
        yield return null;
    }

    IEnumerator GetProfiles()
    {
#if UNITY_WEBGL
        yield return DatabaseAction<List<ProfilePipe>>.GetData("/api/ProfilePipe/ProfilePipes", (returnedProfiles) => profilePipes = returnedProfiles);
        yield return DatabaseAction<List<Truss>>.GetData("/api/Truss/Trusses", (returnedProfiles) => trusses = returnedProfiles);
        yield return DatabaseAction<DollarRate>.GetData("/api/DollarRate", (returnedDollarRate) => dollarRate = returnedDollarRate);
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
        print("UNITY_STANDALONE_WIN || UNITY_EDITOR");
        profilePipes = ScriptObjectsAction.GetListProfilePipes(profilePipeDataList);
        trusses = ScriptObjectsAction.GetListTrusses(trussDataList);
        mountUnitBeamRafterTrusses = ScriptObjectsAction.GetListMountUnitBeamRafterTrusses(mountUnitBeamRafterTrussDataList);
        mountUnitColumnBeamTrusses = ScriptObjectsAction.GetListMountUnitColumnBeamTrusses(mountUnitColumnBeamTrussDataList);
#endif
        yield return null;
    }

    IEnumerator StartApp()
    {
        yield return StartCoroutine(GetProfiles());
        canopyObj = GameObject.FindGameObjectWithTag("Canopy") ?? Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(e => e.CompareTag("Canopy"));
        GameObject CanopyDescription = GameObject.FindGameObjectWithTag("CanopyDescription");
        Debug.Log("LoadPrefab");
        if (canopyPrefab != null)
            Debug.Log($"[{DateTime.Now}]: {canopyPrefab} is not null");
        if (canopyObj == null || canopyObj.GetComponent<CanopyGenerator>().Canopy.RafterTrusses.Length == 0)
            StartCoroutine(InitializePrefab());
        else
        {
            canopyObj.SetActive(true);
            toResultButton.gameObject.SetActive(true);
            yield return StartCoroutine(canopyObj.GetComponent<CanopyGenerator>().Calculate());
            CanopyDescription.GetComponent<TMP_Text>().text = canopyObj.GetComponent<CanopyGenerator>().CanopyDescription.GetComponent<TMP_Text>().text;
        }
    }

    void ButtonClickHandlerForResultPage()
    {
        // Запускаем корутину с задержкой
        StartCoroutine(ToResultPage());
        canopyObj = GameObject.FindGameObjectWithTag("Canopy");
        canopyObj.SetActive(false);
    } 

    IEnumerator ToResultPage()
    {
        SceneManager.LoadScene("CalculationResultScene");
        yield return null;
    }

    IEnumerator RunGC()
    {
        GC.Collect();
        yield return null;
    }

    IEnumerator UnloadUnusedAssets()
    {
        Resources.UnloadUnusedAssets();
        yield return null;
    }

    IEnumerator DestroyGO(GameObject canopy)
    {
        DestroyImmediate(canopy);
        yield return new WaitForEndOfFrame();
    }
}
