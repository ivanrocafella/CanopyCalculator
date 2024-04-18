using Assets.Models;
using Assets.ModelsRequest;
using Assets.Utils;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.UI;

public class ButtonChangeController : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown dropdown;
    [SerializeField]
    private TMP_InputField inputFieldCostPr;
    [SerializeField]
    private TMP_InputField inputFieldRateDollar;
    [SerializeField]
    private Button ButtonChange;
    private DollarRate dollarRate;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!inputFieldCostPr.GetComponent<InputFieldValidator>().isValid || !inputFieldRateDollar.GetComponent<InputFieldValidator>().isValid)
            ButtonChange.interactable = false;
        else
            ButtonChange.interactable = true;
    }

    public void ClickButtonChange()
    {
        string name = dropdown.options[dropdown.value].text;
#if UNITY_WEBGL
        ProfileListController profileListController = dropdown.GetComponent<ProfileListController>();
        Truss truss = profileListController.trusses.Find(e => e.Name == name);
        ProfilePipe profilePipe = profileListController.profilePipes.Find(e => e.Name == name);
        dollarRate = profileListController.dollarRate;
        float newPrice = ValAction.ToFloat(inputFieldCostPr.GetComponent<TMP_InputField>().text);
        string kindAction;
        if (truss != null)
        {
            profileListController.trusses.Find(e => e.Name == name).PricePerM = newPrice;
            kindAction = "TrussUpdate";
        }
        else
        { 
            profileListController.profilePipes.Find(e => e.Name == name).PricePerM = newPrice;
            kindAction = "ProfileUpdate";
        }
        StartCoroutine(UpdateProfile(name, newPrice, kindAction));
        StartCoroutine(UpdateDollarRate());
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
        ValAction.SetPricePmPlayerPrefs(name, ValAction.ToFloat(inputFieldCostPr.GetComponent<TMP_InputField>().text));
        PlayerPrefs.SetFloat("DollarRate", ValAction.ToFloat(inputFieldRateDollar.GetComponent<TMP_InputField>().text));
        PlayerPrefs.Save();
#endif
        print(name);
    }

    IEnumerator UpdateDollarRate()
    {
        dollarRate.Rate = ValAction.ToFloat(inputFieldRateDollar.GetComponent<TMP_InputField>().text);
        string dollarRateJson = JsonConvert.SerializeObject(dollarRate);
        print("dollarRateJson: " + dollarRateJson);
        UnityWebRequest unityWebRequestUpdateDollarRate = UnityWebRequest.Put(Config.baseUrl + "/api/DollarRate/Update", dollarRateJson);
        unityWebRequestUpdateDollarRate.SetRequestHeader("Content-Type", "application/json");
        yield return unityWebRequestUpdateDollarRate.SendWebRequest();
        print("unityWebRequestUpdateDollarRate.result: " + unityWebRequestUpdateDollarRate.result);
        if (unityWebRequestUpdateDollarRate.result != UnityWebRequest.Result.Success)
            Debug.Log(unityWebRequestUpdateDollarRate.error);
        else
            Debug.Log("UpdateDollarRate complete!");
    }

    IEnumerator UpdateProfile(string name, float newPrice, string kindAction)
    {
        ProfileUpdateModel profileUpdateModel = new()
        {
            Name = name,
            PricePerM = newPrice
        };
        string profileUpdateModelJson = JsonConvert.SerializeObject(profileUpdateModel);
        UnityWebRequest unityWebRequest = new();        
        switch (kindAction)
        {
            case "TrussUpdate":
                unityWebRequest = UnityWebRequest.Put(Config.baseUrl + "/api/Truss/Update", profileUpdateModelJson);
                break;
            case "ProfileUpdate":
                unityWebRequest = UnityWebRequest.Put(Config.baseUrl + "/api/ProfilePipe/Update", profileUpdateModelJson);
                break;
        }
        unityWebRequest.SetRequestHeader("Content-Type", "application/json");
        yield return unityWebRequest.SendWebRequest();
        print("unityWebRequest.result: " + unityWebRequest.result);
        if (unityWebRequest.result != UnityWebRequest.Result.Success)
            Debug.Log(unityWebRequest.error);
        else
            Debug.Log("UpdateProfile complete!");
    }
}
