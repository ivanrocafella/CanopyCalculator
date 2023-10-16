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

        GameObject inputSpan = GameObject.FindGameObjectWithTag("SpanInput");
        GameObject lengthInput = GameObject.FindGameObjectWithTag("LengthInput");
        GameObject heightInput = GameObject.FindGameObjectWithTag("HeightInput");
        GameObject slopeInput = GameObject.FindGameObjectWithTag("SlopeInput");
      
        

        GameObject canopy = GameObject.FindGameObjectWithTag("Canopy");
        Destroy(GameObject.FindGameObjectWithTag("ColumnHigh"));
        Destroy(canopy);
        planCanopy.GetComponent<PlanCanopyGenerator>().SizeByX = int.Parse(inputSpan.GetComponent<TMP_InputField>().text);
        planCanopy.GetComponent<PlanCanopyGenerator>().SizeByZ = int.Parse(lengthInput.GetComponent<TMP_InputField>().text);
        planCanopy.GetComponent<PlanCanopyGenerator>().SizeByY = int.Parse(heightInput.GetComponent<TMP_InputField>().text);
        planCanopy.GetComponent<PlanCanopyGenerator>().SlopeInDegree = int.Parse(slopeInput.GetComponent<TMP_InputField>().text);
   


        Instantiate(canopyPrefab);
    }
}
