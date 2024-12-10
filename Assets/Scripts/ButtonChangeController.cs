using Assets.Models;
using Assets.ModelsRequest;
using Assets.Utils;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ButtonChangeController : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown dropdownProfiles;
    [SerializeField]
    private TMP_Dropdown dropdownPlates;
    [SerializeField]
    private TMP_Dropdown dropdownFixings;
    [SerializeField]
    private TMP_InputField inputFieldCostPr;
    [SerializeField]
    private TMP_InputField inputFieldCostPl;
    [SerializeField]
    private TMP_InputField inputFieldCostFixing;
    [SerializeField]
    private TMP_InputField inputFieldRateDollar;
    [SerializeField]
    private Button ButtonChange;
    public GameObject PopUp;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!inputFieldCostPr.GetComponent<InputFieldValidator>().isValid ||
            !inputFieldRateDollar.GetComponent<InputFieldValidator>().isValid ||
            !inputFieldCostPl.GetComponent<InputFieldValidator>().isValid ||
            !inputFieldCostFixing.GetComponent<InputFieldValidator>().isValid)
            ButtonChange.interactable = false;
        else
            ButtonChange.interactable = true;
    }

    public void ClickButtonChange()
    {
        string nameProfile = dropdownProfiles.options[dropdownProfiles.value].text;
        string namePlate = dropdownPlates.options[dropdownPlates.value].text;
        string nameFixing = dropdownFixings.options[dropdownFixings.value].text;
#if UNITY_WEBGL
        ProfileListController profileListControllerProfile = dropdownProfiles.GetComponent<ProfileListController>();
        ProfileListController profileListControllerFlange = dropdownPlates.GetComponent<ProfileListController>();
        ProfileListController profileListControllerFixing = dropdownFixings.GetComponent<ProfileListController>();
        Truss truss = profileListControllerProfile.trusses.Find(e => e.Name == nameProfile);
        ProfilePipe profilePipe = profileListControllerProfile.profilePipes.Find(e => e.Name == nameProfile);
        DollarRate dollarRate = profileListControllerProfile.dollarRate;
        float newPriceProfile = ValAction.ToFloat(inputFieldCostPr.GetComponent<TMP_InputField>().text);
        float newPricePlate = ValAction.ToFloat(inputFieldCostPl.GetComponent<TMP_InputField>().text);
        float newPriceFixing = ValAction.ToFloat(inputFieldCostFixing.GetComponent<TMP_InputField>().text);
        float newRateDollar = ValAction.ToFloat(inputFieldRateDollar.GetComponent<TMP_InputField>().text);
        if (truss != null)
        {
            profileListControllerProfile.trusses.Find(e => e.Name == nameProfile).PricePerM = newPriceProfile;
            StartCoroutine(UpdateEntity(nameProfile, newPriceProfile, "TrussUpdate"));
        }
        else
        {
            profileListControllerProfile.profilePipes.Find(e => e.Name == nameProfile).PricePerM = newPriceProfile;
            StartCoroutine(UpdateEntity(nameProfile, newPriceProfile, "ProfileUpdate"));
        }
        profileListControllerFlange.flanges.Find(e => e.Name == namePlate).Price = newPricePlate;
        StartCoroutine(UpdateEntity(namePlate, newPricePlate, "FlangeUpdate"));
        profileListControllerFixing.fixings.Find(e => e.Name == nameFixing).PricePerKg = newPriceFixing;
        StartCoroutine(UpdateEntity(nameFixing, newPriceFixing, "FixingUpdate"));
        dollarRate.Rate = newRateDollar;
        StartCoroutine(UpdateEntity(default, newRateDollar, "DollarRateUpdate"));
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
        ValAction.SetPricePlayerPrefs(nameProfile, ValAction.ToFloat(inputFieldCostPr.GetComponent<TMP_InputField>().text));
        ValAction.SetPricePlayerPrefs(namePlate, ValAction.ToFloat(inputFieldCostPl.GetComponent<TMP_InputField>().text));
        ValAction.SetPricePlayerPrefs(nameFixing, ValAction.ToFloat(inputFieldCostFixing.GetComponent<TMP_InputField>().text));
        PlayerPrefs.SetFloat("DollarRate", ValAction.ToFloat(inputFieldRateDollar.GetComponent<TMP_InputField>().text));
        PlayerPrefs.Save();
#endif
        print(name);
    }

    IEnumerator UpdateEntity(string name, float newPrice, string kindAction)
    {
        EntityUpdateModel entityUpdateModel = new()
        {
            Name = name,
            Price = newPrice
        };
        string entityUpdateModelJson = JsonConvert.SerializeObject(entityUpdateModel);
        UnityWebRequest unityWebRequest = new();        
        switch (kindAction)
        {
            case "TrussUpdate":
                unityWebRequest = UnityWebRequest.Put(Config.baseUrl + "/api/Truss/Update", entityUpdateModelJson);
                break;
            case "ProfileUpdate":
                unityWebRequest = UnityWebRequest.Put(Config.baseUrl + "/api/ProfilePipe/Update", entityUpdateModelJson);
                break;
            case "FlangeUpdate":
                unityWebRequest = UnityWebRequest.Put(Config.baseUrl + "/api/Flange/Update", entityUpdateModelJson);
                break;
            case "FixingUpdate":
                unityWebRequest = UnityWebRequest.Put(Config.baseUrl + "/api/Fixing/Update", entityUpdateModelJson);
                break;
            case "DollarRateUpdate":
                unityWebRequest = UnityWebRequest.Put(Config.baseUrl + "/api/DollarRate/Update", entityUpdateModelJson);
                break;
        }
        unityWebRequest.SetRequestHeader("Content-Type", "application/json");
        yield return unityWebRequest.SendWebRequest();
        print("unityWebRequest.result: " + unityWebRequest.result);
        if (unityWebRequest.result != UnityWebRequest.Result.Success)
            Debug.Log(unityWebRequest.error);
        else
        { 
            Debug.Log("UpdateEntity complete!");
            PopUp.GetComponent<PopUpController>().StartPopUpFadeOut();
        }
    }
}
