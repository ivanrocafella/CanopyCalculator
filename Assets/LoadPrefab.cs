using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;

public class LoadPrefab : MonoBehaviour
{
    private void Awake()
    {
        GameObject canopyPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(Path.Combine("Assets", "Prefabs", "Canopy.prefab"), typeof(GameObject));
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
        GameObject inputSpan = GameObject.FindGameObjectWithTag("SpanInput");
        GameObject lengthInput = GameObject.FindGameObjectWithTag("LengthInput");
        Debug.Log(inputSpan.GetComponent<TMP_InputField>().text);
        GameObject canopy = GameObject.FindGameObjectWithTag("Canopy");
        Destroy(canopy);
        GameObject canopyPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(Path.Combine("Assets", "Prefabs", "Canopy.prefab"), typeof(GameObject));
        canopyPrefab.GetComponent<CanopyGenerator>().SizeByX = int.Parse(inputSpan.GetComponent<TMP_InputField>().text);
        canopyPrefab.GetComponent<CanopyGenerator>().SizeByZ = int.Parse(lengthInput.GetComponent<TMP_InputField>().text);
        
       // Instantiate(canopyPrefab);
    }
}
