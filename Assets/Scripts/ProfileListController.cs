using Assets.Models;
using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    // Start is called before the first frame update
    void Start()
    {
        // Populate the Dropdown with data
        PopulateDropdown();
        SetValueInputCostPr(dropdown.value);
        SetValueInputRateDollar();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void PopulateDropdown()
    {
        // Make list options from profile and truss SO objects 
        options = ScriptObjectsAction.GetListTrusses(TrussDataList).Select(e => e.Name).ToList();
        options.AddRange(ScriptObjectsAction.GetListProfilePipes(ProfilePipeDataList).Select(e => e.Name).ToList());
        // Clear existing options
        dropdown.ClearOptions();
        // Add new options from the dataList
        dropdown.AddOptions(options);
    }

    public void SetValueInputCostPr(int value)
    {
        string name = dropdown.options[value].text;
        float pricePerM = ValAction.GetPricePmPlayerPrefs(name);
        inputFieldCostPr.text = pricePerM.ToString();
        print("Choosen value:" + pricePerM);          
    }

    public void SetValueInputRateDollar()
    {
        inputFieldRateDollar.text = PlayerPrefs.GetFloat("DollarRate").ToString();
    }
}
