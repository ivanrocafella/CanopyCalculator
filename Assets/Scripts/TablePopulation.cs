using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TablePopulation : MonoBehaviour
{
    [SerializeField]
    private GameObject table;
    //private GameObject gameObject;
    private Canopy canopy;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Populate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Populate()
    { 
        canopy = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(e => e.CompareTag("Canopy")).GetComponent<CanopyGenerator>().Canopy;
        yield return null;
        table.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.NameColumn;
        table.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.LengthHighColumn.ToString();
        table.transform.GetChild(1).GetChild(3).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.QuantityInRowColumn.ToString();
        table.transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.NameColumn;
        table.transform.GetChild(2).GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.LengthLowColumn.ToString();
        table.transform.GetChild(2).GetChild(3).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.QuantityInRowColumn.ToString();
        table.transform.GetChild(2).GetChild(4).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.QuantityMaterialColumn.ToString() + " (на все колонны)";
        table.transform.GetChild(2).GetChild(5).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.CostColumns.ToString() + " (на все колонны)";
    }
}
