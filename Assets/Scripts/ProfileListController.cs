using Assets.Models;
using Assets.ModelsRequest;
using Assets.Utils;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class ProfileListController : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown dropdown;
    [SerializeField]
    private TMP_InputField inputFieldCostPr;
    [SerializeField]
    private TMP_InputField inputFieldRateDollar;
    [SerializeField]
    private TrussDataList TrussDataList;
    [SerializeField]
    private ProfilePipeDataList ProfilePipeDataList;
    [SerializeField]
    private DollarRateData DollarRateData;
    private List<string> options;
    public List<ProfilePipe> profilePipes;
    public List<Truss> trusses;
    public DollarRate dollarRate;

    // Start is called before the first frame update
    void Start()
    {
        // Populate the Dropdown with data
        StartCoroutine(PopulateDropdown());
        StartCoroutine(SetValueInputRateDollar());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator PopulateDropdown()
    {
        // Make list options from profile and truss SO objects 
        yield return StartCoroutine(GetProfiles());

        // Filling options
        options = trusses.Select(e => e.Name).ToList();
        options.AddRange(profilePipes.Select(e => e.Name));
        dropdown.ClearOptions();
        // Add new options from the dataList
        dropdown.AddOptions(options);
        SetValueInputCostPr(0);
    }

    public void SetValueInputCostPr(int value)
    {
        string name = dropdown.options[value].text;
        float pricePerM;
#if UNITY_WEBGL
        Truss truss = trusses.Find(e => e.Name == name);
        ProfilePipe profilePipe = profilePipes.Find(e => e.Name == name);
        pricePerM = truss != null ? truss.PricePerM : profilePipe.PricePerM;
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
        pricePerM = ValAction.GetPricePmPlayerPrefs(name);
#endif
        inputFieldCostPr.text = pricePerM.ToString();
        print("Choosen value:" + pricePerM);      
    }

    IEnumerator SetValueInputRateDollar()
    {
#if UNITY_WEBGL
        yield return DatabaseAction<DollarRate>.GetData("http://localhost:5004/api/DollarRate", (returnedDollarRate) => dollarRate = returnedDollarRate);
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
        dollarRate = new()
        {
            Rate = PlayerPrefs.GetFloat("DollarRate")
        };     
#endif
        inputFieldRateDollar.text = dollarRate.Rate.ToString();
        yield return null;
    }

    IEnumerator GetProfiles()
    {
#if UNITY_WEBGL
        yield return DatabaseAction<List<ProfilePipe>>.GetData("http://localhost:5004/api/ProfilePipe/ProfilePipes", (returnedProfiles) => profilePipes = returnedProfiles);
        yield return DatabaseAction<List<Truss>>.GetData("http://localhost:5004/api/Truss/Trusses", (returnedProfiles) => trusses = returnedProfiles);
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
        print("UNITY_STANDALONE_WIN || UNITY_EDITOR");
        profilePipes = ScriptObjectsAction.GetListProfilePipes(ProfilePipeDataList);
        trusses = ScriptObjectsAction.GetListTrusses(TrussDataList);
#endif
        yield return null;
    }
}
