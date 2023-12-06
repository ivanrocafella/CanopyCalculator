using AsciiFBXExporter;
using Assets.Models;
using Assets.Models.Enums;
using Assets.Services;
using Assets.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;
using Button = UnityEngine.UI.Button;
using Material = Assets.Models.Material;
using Object = UnityEngine.Object;
using System.Numerics;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using Vector3 = UnityEngine.Vector3;
using Random = UnityEngine.Random;
using Assimp;
using Assimp.Configs;

public class LoadPrefab : MonoBehaviour
{
    public GameObject canopyPrefab;
    private GameObject planCanopy;
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
    [SerializeField]
    private Button calculateButton;
    [SerializeField]
    private Button toFbxButton;
    [SerializeField]
    private GameObject loadingTextBox;

    private void Awake()
    {
        if (canopyPrefab != null)
            Debug.Log($"[{DateTime.Now}]: {canopyPrefab} is not null");
        Instantiate(canopyPrefab);
    }
    // Start is called before the first frame update
    void Start()
    {
        calculateButton.onClick.AddListener(ButtonClickHandlerForCalculate);
        toFbxButton.onClick.AddListener(ButtonClickHandlerForFbx);
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

        GameObject canopy = GameObject.FindGameObjectWithTag("Canopy");
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        DestroyImmediate(canopy);

        planCanopy.GetComponent<PlanCanopyGenerator>().SizeByX = ToFloat(spanInputGB.GetComponent<TMP_InputField>().text) * MultipleForMeter;
        planCanopy.GetComponent<PlanCanopyGenerator>().SizeByZ = ToFloat(lengthInputGB.GetComponent<TMP_InputField>().text) * MultipleForMeter;
        planCanopy.GetComponent<PlanCanopyGenerator>().SizeByY = ToFloat(heightInputGB.GetComponent<TMP_InputField>().text) * MultipleForMeter;
        planCanopy.GetComponent<PlanCanopyGenerator>().SlopeInDegree = ToFloat(slopeInputGB.GetComponent<TMP_InputField>().text);
        planCanopy.GetComponent<PlanCanopyGenerator>().CountStep = (int)ToFloat(сountStepColumnInputGB.GetComponent<TMP_InputField>().text);
        planCanopy.GetComponent<PlanCanopyGenerator>().StepRafter = ToFloat(stepRafterInputGB.GetComponent<TMP_InputField>().text) * MultipleForSentimeter;
        planCanopy.GetComponent<PlanCanopyGenerator>().StepGirder = ToFloat(stepGirderInputGB.GetComponent<TMP_InputField>().text) * MultipleForSentimeter;
        planCanopy.GetComponent<PlanCanopyGenerator>().OutputRafter = ToFloat(outputRafterInputGB.GetComponent<TMP_InputField>().text) * MultipleForSentimeter;
        planCanopy.GetComponent<PlanCanopyGenerator>().OutputGirder = ToFloat(outputGirderInputGB.GetComponent<TMP_InputField>().text) * MultipleForSentimeter;

        string nameMaterial = planCanopy.GetComponent<PlanCanopyGenerator>().KindMaterial.ToString();
        float cargo = 85f * coefficientReliability;

        Material material = ScriptObjectsAction.GetMaterialByName(nameMaterial, materialDataList);
        List<ProfilePipe> profilePipes = ScriptObjectsAction.GetListProfilePipes(profilePipeDataList);
        List<Truss> trusses = ScriptObjectsAction.GetListTrusses(trussDataList);

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
            Instantiate(canopyPrefab);
            toFbxButton.interactable = true;
            EmProfilePipeCol.GetComponent<TMP_Text>().text = string.Empty;
        }
        else
        {
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
        //print(profilePipeColumn.Name);
        //print(trussBeam.Name);
        //print(trussRafter.Name);
        //print(profilePipeGirder.Name);      
    }

    IEnumerator ToFbxButtonClick()
    {
        if (!Directory.Exists(Path.Combine(Application.dataPath, "FbxModels")))
            Directory.CreateDirectory(Path.Combine(Application.dataPath, "FbxModels"));
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
    }

    private float ToFloat(string textInput)
    {
        string commoTextInput;
        float value;
        if (textInput.Contains('.'))
        {
            commoTextInput = textInput.Replace('.', ',');
            value = float.Parse(commoTextInput);
        }
        else
            value = float.Parse(textInput);
        return value;
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
}
