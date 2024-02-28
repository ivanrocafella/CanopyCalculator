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
    private TMP_InputField inputField;
    [SerializeField]
    private Button Button;
    [SerializeField]
    private TrussDataList TrussDataList;
    [SerializeField]
    private ProfilePipeDataList ProfilePipeDataList;
    private List<string> options;

    // Start is called before the first frame update
    void Start()
    {
        // Populate the Dropdown with data
        PopulateDropdown();
        SetValueInput(dropdown.value);
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

    public void SetValueInput(int value)
    {
        string name = dropdown.options[value].text;
        //ProfilePipe profilePipe = ScriptObjectsAction.GetProfilePipeByName(name, ProfilePipeDataList);
        //Truss truss = ScriptObjectsAction.GetTrussByName(name, TrussDataList);
        ProfilePipeData profilePipeData = ProfilePipeDataList.profilePipesData.FirstOrDefault(s => s.Name == name);
        TrussData trussData = TrussDataList.trussesData.FirstOrDefault(s => s.Name == name);
        float pricePerM = profilePipeData != null ? profilePipeData.pricePerM : trussData.pricePerM;
        inputField.text = pricePerM.ToString();
        print("Choosen value:" + pricePerM);          
    }
}
