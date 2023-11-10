using Assets.Models;
using Assets.Models.Enums;
using Assets.Services;
using Assets.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEditor.Formats.Fbx.Exporter;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;
using Button = UnityEngine.UI.Button;
using Material = Assets.Models.Material;

public class LoadPrefab : MonoBehaviour
{
    private GameObject canopyPrefab;
    private GameObject planCanopy;
    private int MultipleForMeter = 1000;
    private int MultipleForSentimeter = 10;
    private string pathMaterial;
    private string pathProfilesPipe;
    private string pathTrusses;
    public Button toFbxButton;

    private void Awake()
    {
        canopyPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(Path.Combine("Assets", "Prefabs", "Canopy.prefab"), typeof(GameObject));
        Instantiate(canopyPrefab);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CalculateButtonClick()
    {
        planCanopy = GameObject.FindGameObjectWithTag("PlanCanopy");

        GameObject spanInputGB = GameObject.FindGameObjectWithTag("SpanInput");
        GameObject lengthInputGB = GameObject.FindGameObjectWithTag("LengthInput");
        GameObject heightInputGB = GameObject.FindGameObjectWithTag("HeightInput");
        GameObject slopeInputGB = GameObject.FindGameObjectWithTag("SlopeInput");
        GameObject ñountStepColumnInputGB = GameObject.FindGameObjectWithTag("CountStepColumnInput");
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
        planCanopy.GetComponent<PlanCanopyGenerator>().CountStep = (int)ToFloat(ñountStepColumnInputGB.GetComponent<TMP_InputField>().text);
        planCanopy.GetComponent<PlanCanopyGenerator>().StepRafter = ToFloat(stepRafterInputGB.GetComponent<TMP_InputField>().text) * MultipleForSentimeter;
        planCanopy.GetComponent<PlanCanopyGenerator>().StepGirder = ToFloat(stepGirderInputGB.GetComponent<TMP_InputField>().text) * MultipleForSentimeter;
        planCanopy.GetComponent<PlanCanopyGenerator>().OutputRafter = ToFloat(outputRafterInputGB.GetComponent<TMP_InputField>().text) * MultipleForSentimeter;
        planCanopy.GetComponent<PlanCanopyGenerator>().OutputGirder = ToFloat(outputGirderInputGB.GetComponent<TMP_InputField>().text) * MultipleForSentimeter;
        string nameMaterial = planCanopy.GetComponent<PlanCanopyGenerator>().KindMaterial.ToString();
        float cargo = 85f;

        pathMaterial = Path.Combine(Application.dataPath, "JSONs", "Materials.json");
        Material material = FileAction<Material>.ReadAndDeserialyze(pathMaterial).Find(e => e.Name == nameMaterial);
        pathProfilesPipe = Path.Combine(Application.dataPath, "JSONs", "ProfilesPipe.json");
        List<ProfilePipe> profilePipes = FileAction<ProfilePipe>.ReadAndDeserialyze(pathProfilesPipe);
        pathTrusses = Path.Combine(Application.dataPath, "JSONs", "Trusses.json");
        List<Truss> trusses = FileAction<Truss>.ReadAndDeserialyze(pathTrusses);

        try
        {
            planCanopy.GetComponent<PlanCanopyGenerator>().KindProfileColumn = (KindProfilePipe)profilePipes.IndexOf(CalculationColumn.CalculateColumn(planCanopy.GetComponent<PlanCanopyGenerator>().SizeByX
             , planCanopy.GetComponent<PlanCanopyGenerator>().SizeByZ
             , planCanopy.GetComponent<PlanCanopyGenerator>().SizeByY
             , planCanopy.GetComponent<PlanCanopyGenerator>().CountStep, cargo, material, profilePipes));
            planCanopy.GetComponent<PlanCanopyGenerator>().KindTrussBeam = (KindTruss)trusses.IndexOf(CalculationBeamTruss.CalculateBeamTruss(planCanopy.GetComponent<PlanCanopyGenerator>().SizeByX
             , planCanopy.GetComponent<PlanCanopyGenerator>().SizeByZ
             , planCanopy.GetComponent<PlanCanopyGenerator>().CountStep, cargo, material, trusses));
            planCanopy.GetComponent<PlanCanopyGenerator>().KindTrussRafter = (KindTruss)trusses.IndexOf(CalculationRafterTruss.CalculateRafterTruss(planCanopy.GetComponent<PlanCanopyGenerator>().SizeByX
             , planCanopy.GetComponent<PlanCanopyGenerator>().StepRafter
             , planCanopy.GetComponent<PlanCanopyGenerator>().OutputRafter
             , cargo, material, trusses));
            planCanopy.GetComponent<PlanCanopyGenerator>().KindProfileGirder = (KindProfilePipe)profilePipes.IndexOf(CalculationGirder.CalculateGirder(planCanopy.GetComponent<PlanCanopyGenerator>().StepRafter
             , planCanopy.GetComponent<PlanCanopyGenerator>().StepGirder
             , planCanopy.GetComponent<PlanCanopyGenerator>().OutputGirder
             , cargo, material, profilePipes));
            Instantiate(canopyPrefab);
            toFbxButton.interactable = true;
        }
        catch (Exception)
        {
            print("Ïðåâûøåíû ðàçìåðû!");
        }
        

        //Destroy(mainCamera.GetComponent<CameraTransform>());
        //mainCamera.AddComponent<CameraTransform>();
        //mainCamera.transform.localPosition = new Vector3(0, 2f * planCanopy.GetComponent<PlanCanopyGenerator>().SizeByY,
        // -(planCanopy.GetComponent<PlanCanopyGenerator>().SizeByZ / 2 + planCanopy.GetComponent<PlanCanopyGenerator>().SizeByX * 1.5f * Mathf.Tan(50 * Mathf.Deg2Rad)));
        //mainCamera.transform.localRotation = Quaternion.Euler(35, 0, 0);
        print(CalculationColumn.CalculateColumn(planCanopy.GetComponent<PlanCanopyGenerator>().SizeByX
            , planCanopy.GetComponent<PlanCanopyGenerator>().SizeByZ
            , planCanopy.GetComponent<PlanCanopyGenerator>().SizeByY
            , planCanopy.GetComponent<PlanCanopyGenerator>().CountStep, cargo, material, profilePipes).Name);
    }

    public void ToFbxButtonClick()
    {
        if (!Directory.Exists(Path.Combine(Application.dataPath, "FbxModels")))
            Directory.CreateDirectory(Path.Combine(Application.dataPath, "FbxModels"));
        string format = "dd.MM.yyyy_hh:mm:ss";
        string dateTimeNow = DateTime.Now.ToString(format);
        string filePath = Path.Combine(Application.dataPath, "FbxModels", $"canopy_{dateTimeNow}.fbx");
        GameObject canopy = GameObject.FindGameObjectWithTag("Canopy");
        ModelExporter.ExportObject(filePath, canopy);
        toFbxButton.interactable = false;
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
}
