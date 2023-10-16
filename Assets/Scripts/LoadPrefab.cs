using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadPrefab : MonoBehaviour
{
    private GameObject canopyPrefab;
    private GameObject planCanopy;
    private int MultipleForMeter = 1000;
    private int MultipleForSentimeter = 10;

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

        GameObject spanInput = GameObject.FindGameObjectWithTag("SpanInput");
        GameObject lengthInput = GameObject.FindGameObjectWithTag("LengthInput");
        GameObject heightInput = GameObject.FindGameObjectWithTag("HeightInput");
        GameObject slopeInput = GameObject.FindGameObjectWithTag("SlopeInput");
        GameObject ñountStepColumnInput = GameObject.FindGameObjectWithTag("CountStepColumnInput");
        GameObject outputRafterInput = GameObject.FindGameObjectWithTag("OutputRafterInput");
        GameObject outputGirderInput = GameObject.FindGameObjectWithTag("OutputGirderInput");


        GameObject canopy = GameObject.FindGameObjectWithTag("Canopy");
        DestroyImmediate(canopy);

        planCanopy.GetComponent<PlanCanopyGenerator>().SizeByX = int.Parse(spanInput.GetComponent<TMP_InputField>().text) * MultipleForMeter;
        planCanopy.GetComponent<PlanCanopyGenerator>().SizeByZ = int.Parse(lengthInput.GetComponent<TMP_InputField>().text) * MultipleForMeter;
        planCanopy.GetComponent<PlanCanopyGenerator>().SizeByY = int.Parse(heightInput.GetComponent<TMP_InputField>().text) * MultipleForMeter;
        planCanopy.GetComponent<PlanCanopyGenerator>().SlopeInDegree = int.Parse(slopeInput.GetComponent<TMP_InputField>().text);
        planCanopy.GetComponent<PlanCanopyGenerator>().CountStep = int.Parse(ñountStepColumnInput.GetComponent<TMP_InputField>().text);
        planCanopy.GetComponent<PlanCanopyGenerator>().OutputRafter = int.Parse(outputRafterInput.GetComponent<TMP_InputField>().text) * MultipleForSentimeter;
        planCanopy.GetComponent<PlanCanopyGenerator>().OutputGirder = int.Parse(outputGirderInput.GetComponent<TMP_InputField>().text) * MultipleForSentimeter;


        Instantiate(canopyPrefab);
    }
}
