using Assets.Models;
using Assets.Models.Enums;
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

public class ProfileListController : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown dropdown;
    [SerializeField]
    private TMP_InputField inputFieldCost;
    [SerializeField]
    private TMP_InputField inputFieldRateDollar;
    [SerializeField]
    private TrussDataList TrussDataList;
    [SerializeField]
    private ProfilePipeDataList ProfilePipeDataList;
    [SerializeField]
    private DollarRateData DollarRateData;
    [SerializeField]
    private MountUnitColumnBeamTrussDataList MountUnitColumnBeamTrussDataList;
    [SerializeField]
    private MountUnitBeamRafterTrussDataList MountUnitBeamRafterTrussDataList;
    [SerializeField]
    private KindDropDown KindDropDown;
    private List<string> options;
    public List<ProfilePipe> profilePipes;
    public List<Truss> trusses;
    public List<Flange> flanges;
    public List<Fixing> fixings;
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
        switch (KindDropDown)
        {
            case KindDropDown.DropdownProfiles:
                 // Make list options from profile and truss SO objects 
                 yield return StartCoroutine(GetProfiles());
                 // Filling options
                 options = trusses.Select(e => e.Name).ToList();
                 options.AddRange(profilePipes.Select(e => e.Name));
                break;
            case KindDropDown.DropdownPlates:
                // Make list options from profile and truss SO objects 
                yield return StartCoroutine(GetFlanges());
                // Filling options
                options = flanges.Select(e => e.Name).ToList();
                break;
            case KindDropDown.DropdownFixings:
                // Make list options from profile and truss SO objects 
                yield return StartCoroutine(GetFixings());
                // Filling options
                options = fixings.Select(e => e.Name).ToList();
                break;
            default:
                print("Undefined dropdown");
                break;
        }
        dropdown.ClearOptions();
        // Add new options from the dataList
        dropdown.AddOptions(options);
        SetValueInputCostPr(0);
    }

    public void SetValueInputCostPr(int value)
    {
        string name = dropdown.options[value].text;
        float price;
#if UNITY_WEBGL
        switch (KindDropDown)
        {
            case KindDropDown.DropdownProfiles:
                Truss truss = trusses.Find(e => e.Name == name);
                ProfilePipe profilePipe = profilePipes.Find(e => e.Name == name);
                price = truss != null ? truss.PricePerM : profilePipe.PricePerM;
                break;
            case KindDropDown.DropdownPlates:
                price = flanges.Find(e => e.Name == name).Price;
                break;
            case KindDropDown.DropdownFixings:
                price = fixings.Find(e => e.Name == name).PricePerKg;
                break;
            default:
                price = 0;
                print("Undefined dropdown");
                break;
        }
       
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
        price = ValAction.GetPricePlayerPrefs(name);
#endif
        inputFieldCost.text = price.ToString();
        print("Choosen value:" + price);      
    }

    IEnumerator SetValueInputRateDollar()
    {
#if UNITY_WEBGL
        yield return DatabaseAction<DollarRate>.GetData("/api/DollarRate", (returnedDollarRate) => dollarRate = returnedDollarRate);
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
        yield return DatabaseAction<List<ProfilePipe>>.GetData("/api/ProfilePipe/ProfilePipes", (returnedProfiles) => profilePipes = returnedProfiles);
        yield return DatabaseAction<List<Truss>>.GetData("/api/Truss/Trusses", (returnedProfiles) => trusses = returnedProfiles);
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
        print("UNITY_STANDALONE_WIN || UNITY_EDITOR");
        profilePipes = ScriptObjectsAction.GetListProfilePipes(ProfilePipeDataList);
        trusses = ScriptObjectsAction.GetListTrusses(TrussDataList);
#endif
        yield return null;
    }

    IEnumerator GetFlanges()
    {
#if UNITY_WEBGL
        yield return DatabaseAction<List<Flange>>.GetData("/api/Flange/Flanges", (returnedFlanges) => flanges = returnedFlanges);
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
        print("UNITY_STANDALONE_WIN || UNITY_EDITOR");
        flanges = ScriptObjectsAction.GetListFlange(MountUnitColumnBeamTrussDataList, MountUnitBeamRafterTrussDataList);
#endif
        yield return null;
    }

    IEnumerator GetFixings()
    {
#if UNITY_WEBGL
        yield return DatabaseAction<List<Fixing>>.GetData("/api/Fixing/Fixings", (returnedFixings) => fixings = returnedFixings);
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
        print("UNITY_STANDALONE_WIN || UNITY_EDITOR");
        fixings = ScriptObjectsAction.GetListFixing(MountUnitColumnBeamTrussDataList, MountUnitBeamRafterTrussDataList);
#endif
        yield return null;
    }
}
