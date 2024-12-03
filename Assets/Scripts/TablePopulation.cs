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
        table.transform.GetChild(3).GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.NameBeamTruss;
        table.transform.GetChild(3).GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.LengthBeamTruss.ToString();
        table.transform.GetChild(3).GetChild(3).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.QuantityBeamTruss.ToString();
        table.transform.GetChild(3).GetChild(4).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.QuantityMaterialBeamTruss.ToString();
        table.transform.GetChild(3).GetChild(5).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.CostBeamTrusses.ToString();
        table.transform.GetChild(4).GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.NameRafterTruss;
        table.transform.GetChild(4).GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.LengthRafterTruss.ToString();
        table.transform.GetChild(4).GetChild(3).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.QuantityRafterTruss.ToString();
        table.transform.GetChild(4).GetChild(4).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.QuantityMaterialRafterTruss.ToString();
        table.transform.GetChild(4).GetChild(5).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.CostRafterTrusses.ToString();
        table.transform.GetChild(5).GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.NameGirder;
        table.transform.GetChild(5).GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.LengthGirder.ToString();
        table.transform.GetChild(5).GetChild(3).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.QuantityGirder.ToString();
        table.transform.GetChild(5).GetChild(4).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.QuantityMaterialGirder.ToString();
        table.transform.GetChild(5).GetChild(5).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.CostGirders.ToString();
        int iter = 6;
        if (canopy.PlanColumn.IsDemountable)
        {
            table.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 900);
            //table.transform.localScale = new Vector3(0.8f, 0.8f, 1);
            //DestroyImmediate(table.transform.GetChild(6).transform.gameObject);
            for (int i = 0; i < 7; i++) 
            {
                iter++;
                Instantiate(table.transform.GetChild(6)).SetParent(table.transform);
            }
            table.transform.GetChild(6).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Фланец колонны (колонна-балка)";
            table.transform.GetChild(6).GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.NameFlangeColumnMucbt;
            table.transform.GetChild(6).GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = "_";
            table.transform.GetChild(6).GetChild(3).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.QuantityFlangeColumnMucbt.ToString();
            table.transform.GetChild(6).GetChild(4).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.CountMaterialFlangeColumnMucbt.ToString();
            table.transform.GetChild(6).GetChild(5).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.CostFlangeColumnMucbt.ToString();
            table.transform.GetChild(7).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Фланец балки (колонна-балка)";
            table.transform.GetChild(7).GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.NameFlangeBeamTrussMucbt;
            table.transform.GetChild(7).GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = "_";
            table.transform.GetChild(7).GetChild(3).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.QuantityFlangeBeamTrussMucbt.ToString();
            table.transform.GetChild(7).GetChild(4).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.CountMaterialFlangeBeamTrussMucbt.ToString();
            table.transform.GetChild(7).GetChild(5).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.CostFlangeBeamTrussMucbt.ToString();
            table.transform.GetChild(8).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Фланец балки (балка-стропило)";
            table.transform.GetChild(8).GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.NameFlangeBeamTrussMubrt;
            table.transform.GetChild(8).GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = "_";
            table.transform.GetChild(8).GetChild(3).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.QuantityFlangeBeamTrussMubrt.ToString();
            table.transform.GetChild(8).GetChild(4).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.CountMaterialFlangeBeamTrussMubrt.ToString();
            table.transform.GetChild(8).GetChild(5).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.CostFlangeBeamTrussMubrt.ToString();
            table.transform.GetChild(9).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Фланец стропила (балка-стропило)";
            table.transform.GetChild(9).GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.NameFlangeRafterTrussMubrt;
            table.transform.GetChild(9).GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = "_";
            table.transform.GetChild(9).GetChild(3).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.QuantityFlangeRafterTrussMubrt.ToString();
            table.transform.GetChild(9).GetChild(4).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.CountMaterialFlangeRafterTrussMubrt.ToString();
            table.transform.GetChild(9).GetChild(5).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.CostFlangeRafterTrussMubrt.ToString();
            table.transform.GetChild(10).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Болт";
            table.transform.GetChild(10).GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.NameScrew;
            table.transform.GetChild(10).GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = "_";
            table.transform.GetChild(10).GetChild(3).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.QuantityScrew.ToString();
            table.transform.GetChild(10).GetChild(4).GetChild(0).GetComponent<TMP_Text>().text = "_";
            table.transform.GetChild(10).GetChild(5).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.CostScrews.ToString();
            table.transform.GetChild(11).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Гайка";
            table.transform.GetChild(11).GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.NameNut;
            table.transform.GetChild(11).GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = "_";
            table.transform.GetChild(11).GetChild(3).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.QuantityNut.ToString();
            table.transform.GetChild(11).GetChild(4).GetChild(0).GetComponent<TMP_Text>().text = "_";
            table.transform.GetChild(11).GetChild(5).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.CostNuts.ToString();
            table.transform.GetChild(12).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Шайба";
            table.transform.GetChild(12).GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.NameWasher;
            table.transform.GetChild(12).GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = "_";
            table.transform.GetChild(12).GetChild(3).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.QuantityWasher.ToString();
            table.transform.GetChild(12).GetChild(4).GetChild(0).GetComponent<TMP_Text>().text = "_";
            table.transform.GetChild(12).GetChild(5).GetChild(0).GetComponent<TMP_Text>().text = canopy.ResultCalculation.CostWashers.ToString();
        }
        table.transform.GetChild(iter).GetChild(5).GetChild(0).GetComponent<TMP_Text>().text = "Итого: " + canopy.ResultCalculation.CostTotal.ToString();
    }
}
