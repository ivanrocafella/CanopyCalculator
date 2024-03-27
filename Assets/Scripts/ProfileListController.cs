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

    // Start is called before the first frame update
    void Start()
    {
        // Populate the Dropdown with data
        StartCoroutine(PopulateDropdown());       
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator PopulateDropdown()
    {
        // Make list options from profile and truss SO objects 
        UnityWebRequest unityWebRequestProfilePipes = UnityWebRequest.Get("http://localhost:5004/api/ProfilePipe/ProfilePipes");
        UnityWebRequest unityWebRequestTrusses = UnityWebRequest.Get("http://localhost:5004/api/Truss/Trusses");
        yield return unityWebRequestProfilePipes.SendWebRequest();
        yield return unityWebRequestTrusses.SendWebRequest();
        print("Response:" + unityWebRequestProfilePipes.result);
        switch (unityWebRequestProfilePipes.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError("Error: " + unityWebRequestProfilePipes.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError("HTTP Error: " + unityWebRequestProfilePipes.error);
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log("Received: " + unityWebRequestProfilePipes.downloadHandler.text);
                break;
        }
        ApiResult<List<ProfilePipe>> apiResultProfilePipe = JsonConvert.DeserializeObject<ApiResult<List<ProfilePipe>>>(unityWebRequestProfilePipes.downloadHandler.text);
        ApiResult<List<Truss>> apiResultTruss = JsonConvert.DeserializeObject<ApiResult<List<Truss>>>(unityWebRequestTrusses.downloadHandler.text);
        profilePipes = apiResultProfilePipe.Result;
        trusses = apiResultTruss.Result;
        // Filling options
        options = trusses.Select(e => e.Name).ToList();
        options.AddRange(profilePipes.Select(e => e.Name));
        dropdown.ClearOptions();
        // Add new options from the dataList
        dropdown.AddOptions(options);
        SetValueInputCostPr(0);
        SetValueInputRateDollar();
    }

    public void SetValueInputCostPr(int value)
    {
        string name = dropdown.options[value].text;
        Truss truss = trusses.Find(e => e.Name == name);
        ProfilePipe profilePipe = profilePipes.Find(e => e.Name == name);
        float pricePerM = truss != null ? truss.PricePerM : profilePipe.PricePerM;
        inputFieldCostPr.text = pricePerM.ToString();
        print("Choosen value:" + pricePerM);          
    }

    public void SetValueInputRateDollar()
    {
        inputFieldRateDollar.text = PlayerPrefs.GetFloat("DollarRate").ToString();
    }
}
