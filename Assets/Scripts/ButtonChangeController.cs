using Assets.Models;
using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
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
        ProfileListController profileListController = dropdown.GetComponent<ProfileListController>();
        Truss truss = profileListController.trusses.Find(e => e.Name == name);
        ProfilePipe profilePipe = profileListController.profilePipes.Find(e => e.Name == name);
        float newPrice = ValAction.ToFloat(inputFieldCostPr.GetComponent<TMP_InputField>().text);
        if (truss != null)
            profileListController.trusses.Find(e => e.Name == name).PricePerM = newPrice;
        else
            profileListController.profilePipes.Find(e => e.Name == name).PricePerM = newPrice;
        PlayerPrefs.SetFloat("DollarRate", ValAction.ToFloat(inputFieldRateDollar.GetComponent<TMP_InputField>().text));
        PlayerPrefs.Save();
        print(name);
    }
}
