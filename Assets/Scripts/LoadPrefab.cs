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

public class LoadPrefab : MonoBehaviour
{
    public GameObject canopyPrefab;
    private GameObject planCanopy;
    private const int MultipleForMeter = 1000;
    private const int MultipleForSentimeter = 10;
    private string pathMaterial;
    private string pathProfilesPipe;
    private string pathTrusses;
    public Button toFbxButton;
    public GameObject EmProfilePipeCol;
    private BeamTruss BeamTruss;
    private ColumnBody ColumnBodyHigh;
    private ColumnBody ColumnBodyLow;
    private const float coefficientReliability = 1.4f;

    private void Awake()
    {
        if (canopyPrefab != null)
            Debug.Log($"[{DateTime.Now}]: {canopyPrefab} is not null");
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

        pathMaterial = Path.Combine(Application.dataPath, "Resources", "Materials.json");
        Material material = FileAction<Material>.ReadAndDeserialyze(pathMaterial).Find(e => e.Name == nameMaterial);
        pathProfilesPipe = Path.Combine(Application.dataPath, "Resources", "ProfilesPipe.json");
        List<ProfilePipe> profilePipes = FileAction<ProfilePipe>.ReadAndDeserialyze(pathProfilesPipe);
        pathTrusses = Path.Combine(Application.dataPath, "Resources", "Trusses.json");
        List<Truss> trusses = FileAction<Truss>.ReadAndDeserialyze(pathTrusses);

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

    public void ToFbxButtonClick()
    {
        if (!Directory.Exists(Path.Combine(Application.dataPath, "FbxModels")))
            Directory.CreateDirectory(Path.Combine(Application.dataPath, "FbxModels"));
        string format = "dd.MM.yyyy_hh.mm.ss";
        string dateTimeNow = DateTime.Now.ToString(format);
        string filePath = Path.Combine(Application.dataPath, "FbxModels", $"canopy_{dateTimeNow}.fbx");
        GameObject canopy = GameObject.FindGameObjectWithTag("Canopy");
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

    //void ExportSceneToFBX(string exportPath)
    //{
    //    List<MeshFilter> meshFilters = new List<MeshFilter>(FindObjectsOfType<MeshFilter>());
    //    List<UnityEngine.Material> uniqueMaterials = new();

    //    using (StreamWriter writer = new StreamWriter(exportPath))
    //    {
    //        // Write FBX header
    //        WriteFBXHeader(writer);

    //        // Write FBX nodes for each mesh with materials
    //        foreach (MeshFilter meshFilter in meshFilters)
    //        {
    //            UnityEngine.Material[] materials = meshFilter.GetComponent<Renderer>().sharedMaterials;

    //            foreach (UnityEngine.Material material in materials)
    //            {
    //                if (!uniqueMaterials.Contains(material))
    //                {
    //                    WriteMaterialNode(writer, material);
    //                    uniqueMaterials.Add(material);
    //                }
    //            }

    //            WriteMeshNode(writer, meshFilter, uniqueMaterials.IndexOf(materials[0])); // Assign the index of the first material
    //        }

    //        // Write FBX footer
    //        WriteFBXFooter(writer);
    //    }

    //    Debug.Log("Export to FBX successful: " + exportPath);
    //}

    //void WriteFBXHeader(StreamWriter writer)
    //{
    //    // Write FBX header information (you may need to adjust this based on FBX format)
    //    writer.WriteLine("; FBX 7.4.0 project file");
    //    writer.WriteLine("FBXHeaderExtension:  {");
    //    writer.WriteLine("\tFBXHeaderVersion: 1003");
    //    writer.WriteLine("\tFBXVersion: 7400");
    //    writer.WriteLine("}");
    //    writer.WriteLine("CreationTime: \"\"");
    //}

    //void WriteMaterialNode(StreamWriter writer, UnityEngine.Material material)
    //{
    //    // Write FBX node information for each material (you may need to adjust this based on FBX format)
    //    writer.WriteLine("Node: {");
    //    writer.WriteLine($"\tName: \"{material.name}\"");
    //    writer.WriteLine("\tType: \"Material\"");
    //    writer.WriteLine("\tVersion: 102");
    //    writer.WriteLine("\tProperties60: {");
    //    writer.WriteLine($"\t\tP: \"ShadingModel\", \"KString\", \"\", \"\", \"{material.shader.name}\"");
    //    writer.WriteLine($"\t\tP: \"MultiLayer\", \"Bool\", \"\", \"\",{material.shader.renderQueue == 2000}");
    //    writer.WriteLine("\t}");
    //    writer.WriteLine("\t}");
    //    writer.WriteLine("\tCulling: \"CullingOff\"");
    //    writer.WriteLine("\tVisibility: 1");
    //    writer.WriteLine("\tColor: 1, 1, 1");
    //    writer.WriteLine("}");
    //}

    //void WriteMeshNode(StreamWriter writer, MeshFilter meshFilter, int materialIndex)
    //{
    //    // Write FBX node information for each mesh (you may need to adjust this based on FBX format)
    //    writer.WriteLine("Node: {");
    //    writer.WriteLine($"\tName: \"Mesh_{meshFilter.name}\"");
    //    writer.WriteLine("\tType: \"Mesh\"");
    //    writer.WriteLine("\tVersion: 232");
    //    writer.WriteLine("\tProperties60: {");
    //    writer.WriteLine("\t\tP: \"Mesh\", \"KString\", \"\", \"\"");
    //    writer.WriteLine("\t\tP: \"Version\", \"int\", \"\", \"\",1");
    //    writer.WriteLine("\t\tP: \"Vertices\", \"Vector\", \"Vector\", \"\",{");

    //    Mesh mesh = meshFilter.sharedMesh;
    //    Vector3[] vertices = mesh.vertices;

    //    for (int i = 0; i < vertices.Length; i++)
    //    {
    //        writer.WriteLine($"\t\t\t{vertices[i].x},{vertices[i].y},{vertices[i].z},");
    //    }

    //    writer.WriteLine("\t\t}");
    //    writer.WriteLine("\t\tP: \"PolygonVertexIndex\", \"int\", \"\", \"\",{");

    //    int[] triangles = mesh.triangles;
    //    for (int i = 0; i < triangles.Length; i += 3)
    //    {
    //        writer.WriteLine($"\t\t\t{triangles[i]},{triangles[i + 1]},{triangles[i + 2]},");
    //    }

    //    writer.WriteLine("\t\t}");
    //    writer.WriteLine("\t\tP: \"Materials\", \"Compound\", \"\", \"\"");
    //    writer.WriteLine("\t\tP: \"ShadingModel\", \"KString\", \"\", \"\", \"\"");
    //    writer.WriteLine("\t\tP: \"MultiLayer\", \"Bool\", \"\", \"\", False");
    //    writer.WriteLine("\t}");
    //    writer.WriteLine($"\tMaterial: {materialIndex}");
    //    writer.WriteLine("\tCulling: \"CullingOff\"");
    //    writer.WriteLine("\tVisibility: 1");
    //    writer.WriteLine("\tColor: 1, 1, 1");
    //    writer.WriteLine("}");
    //}

    //void WriteFBXFooter(StreamWriter writer)
    //{
    //    // Write FBX footer information (you may need to adjust this based on FBX format)
    //    writer.WriteLine("Takes:  {");
    //    writer.WriteLine("}");
    //    writer.WriteLine("Comments: \"\"");
    //}


}
