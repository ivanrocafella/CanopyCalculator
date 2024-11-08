using Assets.Models;
using Assets.ModelsRequest;
using Assets.Services;
using Assets.Utils;
using Assimp.Configs;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class CanopyGenerator : MonoBehaviour
{
    public Canopy Canopy = new();
    public GameObject CanopyObject;
    public GameObject CanopyDescription;
    public GameObject LoadingTextBox;
    public const float factorTolerance = 1.2f;
    public DollarRate dollarRate = new();
    [SerializeField]
    private MountUnitColumnBeamTrussDataList MountUnitColumnBeamTrussDataList;
    [SerializeField]
    private MountUnitBeamRafterTrussDataList MountUnitBeamRafterTrussDataList;
    public List<ProfilePipe> profilePipes = new();
    public List<Truss> trusses = new();
    private int countStepRafterTrussSection;
    private float stepRafterTrussSection;
    private float lengthSection; 
    void Awake()
    {
        print("CanopyGenerator");
    }

    void Start()
    {  
        StartCoroutine(MakeCanopy());
        StartCoroutine(Calculate());
        print("Canopy");
    }

    // Update is called once per frame
    void Update()
    {       
    }

    IEnumerator MakeCanopy()
    {
        Canopy.PlanColumn = GameObject.FindGameObjectWithTag("PlanCanopy").GetComponent<PlanCanopyGenerator>().MakePlanCanopy();
        CanopyObject = GameObject.FindGameObjectWithTag("Canopy");
        Canopy.ColumnsHigh = new GameObject[Canopy.PlanColumn.CountStep + 1];
        Canopy.ColumnsLow = new GameObject[Canopy.ColumnsHigh.Length];
        Canopy.BeamTrussesOnHigh = new GameObject[Canopy.PlanColumn.CountStep];
        Canopy.BeamTrussesOnLow = new GameObject[Canopy.BeamTrussesOnHigh.Length];
        Canopy.ColumnBodyHigh = GameObject.FindGameObjectsWithTag("ColumnHigh")[0].GetComponent<ColumnGenerator>().ColumnBody;
        Canopy.BeamTruss = GameObject.FindGameObjectsWithTag("BeamTruss")[0].GetComponent<BeamTrussGenerator>().beamTrussForRead;
        Canopy.RafterTruss = GameObject.FindGameObjectsWithTag("RafterTruss")[0].GetComponent<RafterTrussGenerator>().rafterTrussForRead;
        Canopy.Girder = GameObject.FindGameObjectsWithTag("Girder")[0].GetComponent<GirderGenerator>().girder;
        Canopy.MountUnitBeamRafterTruss = ScriptObjectsAction.GetMountUnitBeamRafterTrussByName(Canopy.RafterTruss.Truss.Name, MountUnitBeamRafterTrussDataList);
        Canopy.MountUnitColumnBeamTruss = ScriptObjectsAction.GetMountUnitColumnBeamTrussByName(Canopy.BeamTruss.Truss.Name, MountUnitColumnBeamTrussDataList);

        Canopy.CountStepRafterTruss = Canopy.PlanColumn.SizeByZ / Mathf.FloorToInt(Canopy.PlanColumn.SizeByZ / Canopy.RafterTruss.Step) <= Canopy.RafterTruss.Step ?
            Mathf.FloorToInt(Canopy.PlanColumn.SizeByZ / Canopy.RafterTruss.Step) : Mathf.FloorToInt(Canopy.PlanColumn.SizeByZ / Canopy.RafterTruss.Step) + 1;
        Canopy.RafterTruss.Step = Canopy.PlanColumn.SizeByZ / Canopy.CountStepRafterTruss;

        lengthSection = Canopy.PlanColumn.Step - Canopy.ColumnBodyHigh.Profile.Length - Canopy.MountUnitColumnBeamTruss.WidthFlangeColumn * 2 - Canopy.MountUnitBeamRafterTruss.LengthFlangeBeamTruss;
        countStepRafterTrussSection = lengthSection / Mathf.FloorToInt( lengthSection / Canopy.RafterTruss.Step) <= Canopy.RafterTruss.Step ? Mathf.FloorToInt(lengthSection / Canopy.RafterTruss.Step) : Mathf.FloorToInt(lengthSection / Canopy.RafterTruss.Step) + 1;
        stepRafterTrussSection = lengthSection / countStepRafterTrussSection;

        Canopy.CountStepGirder = (Canopy.RafterTruss.LengthTop - Canopy.Girder.Profile.Length) / (Mathf.FloorToInt((Canopy.RafterTruss.LengthTop - Canopy.Girder.Profile.Length) / Canopy.Girder.Step)) <= Canopy.Girder.Step ?
            Mathf.FloorToInt((Canopy.RafterTruss.LengthTop - Canopy.Girder.Profile.Length) / Canopy.Girder.Step) : Mathf.FloorToInt((Canopy.RafterTruss.LengthTop - Canopy.Girder.Profile.Length) / Canopy.Girder.Step) + 1;
        Canopy.Girder.Step = Canopy.RafterTruss.LengthTop / Canopy.CountStepGirder;
        Canopy.RafterTrusses = new GameObject[Canopy.CountStepRafterTruss + 1];
        Canopy.Girders = new GameObject[Canopy.CountStepGirder];
        Canopy.MountUnitsColumnBeamTrussOnHC = new GameObject[Canopy.PlanColumn.CountStep * 2];
        Canopy.MountUnitsColumnBeamTrussOnLC = new GameObject[Canopy.MountUnitsColumnBeamTrussOnHC.Length];
        Canopy.MountUnitsBeamRafterTrussOnHC = new GameObject[Canopy.PlanColumn.CountStep * (countStepRafterTrussSection + 1)];
        Canopy.MountUnitsBeamRafterTrussOnLC = new GameObject[Canopy.MountUnitsBeamRafterTrussOnHC.Length];

        float partAdditFromAngle = Mathf.Tan(Canopy.PlanColumn.Slope)
            * (Canopy.BeamTruss.Truss.ProfileBelt.Length / 2 - Canopy.BeamTruss.Truss.ProfileBelt.Radius + Canopy.PlanColumn.OutputRafter);
        float partAdditHalfBeltAngle = Canopy.RafterTruss.Truss.ProfileBelt.Height / 2 / Mathf.Cos(Canopy.PlanColumn.Slope);

        // Make columns
        for (int i = 0; i < Canopy.PlanColumn.CountStep; i++)
        {
            Canopy.ColumnsHigh[i] = Instantiate(GameObject.FindGameObjectsWithTag("ColumnHigh")[0]);
            Canopy.ColumnsHigh[i].transform.SetParent(CanopyObject.transform);
            Destroy(Canopy.ColumnsHigh[i].GetComponent<TransformColumnHigh>());
            Canopy.ColumnsHigh[i].transform.localPosition = new Vector3(0, 0, Canopy.PlanColumn.Step + Canopy.PlanColumn.Step * i);
            Canopy.ColumnsLow[i] = Instantiate(GameObject.FindGameObjectsWithTag("ColumnLow")[0]);
            Canopy.ColumnsLow[i].transform.SetParent(CanopyObject.transform);
            Destroy(Canopy.ColumnsLow[i].GetComponent<TransformColumnLow>());
            Canopy.ColumnsLow[i].transform.localPosition = new Vector3(Canopy.PlanColumn.SizeByX, 0, Canopy.PlanColumn.Step + Canopy.PlanColumn.Step * i);
        }
        // Make mountUnitsColumnBeamTruss on highColumns
        MountUnitsColumnBeamTruss(Canopy.MountUnitsColumnBeamTrussOnHC, true);
        // Make mountUnitsColumnBeamTruss on lowColumns
        MountUnitsColumnBeamTruss(Canopy.MountUnitsColumnBeamTrussOnLC, false);
        // Make mountUnitsBeamRufterTruss on highColumns
        MountUnitsBeamRafterTruss(Canopy.MountUnitsBeamRafterTrussOnHC, true);
        // Make mountUnitsBeamRufterTruss on lowColumns
        MountUnitsBeamRafterTruss(Canopy.MountUnitsBeamRafterTrussOnLC, false);
        // Make beam trusses on high columns
        for (int i = 0; i < Canopy.BeamTrussesOnHigh.Length - 1; i++)
        {
            Canopy.BeamTrussesOnHigh[i] = Object.Instantiate(GameObject.FindGameObjectsWithTag("BeamTruss")[0]);
            Canopy.BeamTrussesOnHigh[i].transform.SetParent(CanopyObject.transform);
            Destroy(Canopy.BeamTrussesOnHigh[i].GetComponent<BeamTrussTransform>());
            Canopy.BeamTrussesOnHigh[i].transform.SetLocalPositionAndRotation(new Vector3(0
           , Canopy.PlanColumn.SizeByY + Canopy.ColumnPlug.Thickness + Canopy.BeamTruss.Truss.ProfileBelt.Height / 2
           , Canopy.PlanColumn.Step + Canopy.PlanColumn.Step * i), Quaternion.Euler(0f, -90f, -90f));
        }
        // Make beam trusses on low columns
        for (int i = 0; i < Canopy.BeamTrussesOnLow.Length; i++)
        {
            Canopy.BeamTrussesOnLow[i] = Object.Instantiate(GameObject.FindGameObjectsWithTag("BeamTruss")[0]);
            Canopy.BeamTrussesOnLow[i].transform.SetParent(CanopyObject.transform);
            Destroy(Canopy.BeamTrussesOnLow[i].GetComponent<BeamTrussTransform>());
            Canopy.BeamTrussesOnLow[i].transform.SetLocalPositionAndRotation(new Vector3(Canopy.PlanColumn.SizeByX
           , Canopy.PlanColumn.SizeByYLow + Canopy.ColumnPlug.Thickness + Canopy.BeamTruss.Truss.ProfileBelt.Height / 2
           , Canopy.PlanColumn.Step * i), Quaternion.Euler(0f, -90f, -90f));
        }
        // Make rafter trusses
        yield return null;
        Vector3 elemenRafterTrussPosition = GameObject.FindGameObjectWithTag("RafterTruss").transform.position;
        for (int i = 0; i < Canopy.RafterTrusses.Length; i++)
        {
            Canopy.RafterTrusses[i] = Object.Instantiate(GameObject.FindGameObjectWithTag("RafterTruss"));
            Canopy.RafterTrusses[i].transform.SetParent(CanopyObject.transform);
            Destroy(Canopy.RafterTrusses[i].GetComponent<RafterTrussTransform>());
            if (i == Canopy.RafterTrusses.Length - 1)
            {
                Canopy.RafterTrusses[i].transform.SetLocalPositionAndRotation(new Vector3(-Canopy.PlanColumn.OutputRafter
                             , elemenRafterTrussPosition.y
                             , Canopy.PlanColumn.SizeByZ), Quaternion.Euler(0, 0, -(90 + Canopy.PlanColumn.SlopeInDegree)));
            }
            else
            {
                Canopy.RafterTrusses[i].transform.SetLocalPositionAndRotation(new Vector3(-Canopy.PlanColumn.OutputRafter
                             , elemenRafterTrussPosition.y
                             , Canopy.RafterTruss.Step + Canopy.RafterTruss.Step * i), Quaternion.Euler(0, 0, -(90 + Canopy.PlanColumn.SlopeInDegree)));
            }
        }
        // Make girders
        float stepGirder;
        float projectionHorStepGirder;
        float projectionVertStepGirder;
        Vector3 elemenGirderPosition = GameObject.FindGameObjectWithTag("Girder").transform.localPosition;
        for (int i = 0; i < Canopy.Girders.Length; i++)
        {
            Canopy.Girders[i] = Object.Instantiate(GameObject.FindGameObjectsWithTag("Girder")[0]);
            Canopy.Girders[i].transform.SetParent(CanopyObject.transform);
            Destroy(Canopy.Girders[i].GetComponent<GirderTransform>());
            if (i == Canopy.Girders.Length - 1)
            {
                stepGirder = Canopy.RafterTruss.LengthTop - Canopy.Girder.Profile.Length;
                projectionHorStepGirder = Mathf.Cos(Canopy.PlanColumn.Slope) * stepGirder;
                projectionVertStepGirder = Mathf.Sin(Canopy.PlanColumn.Slope) * stepGirder;
                Canopy.Girders[i].transform.SetLocalPositionAndRotation(new Vector3(elemenGirderPosition.x + projectionHorStepGirder
                             , elemenGirderPosition.y - projectionVertStepGirder
                             , elemenGirderPosition.z), Quaternion.Euler(-Canopy.PlanColumn.SlopeInDegree, -90, -90));
            }
            else
            {
                stepGirder = Canopy.Girder.Step * (1 + i);
                projectionHorStepGirder = Mathf.Cos(Canopy.PlanColumn.Slope) * stepGirder;
                projectionVertStepGirder = Mathf.Sin(Canopy.PlanColumn.Slope) * stepGirder;
                Canopy.Girders[i].transform.SetLocalPositionAndRotation(new Vector3(elemenGirderPosition.x + projectionHorStepGirder
                             , elemenGirderPosition.y - projectionVertStepGirder
                             , elemenGirderPosition.z), Quaternion.Euler(-Canopy.PlanColumn.SlopeInDegree, -90, -90));
            }
        }
        yield return new WaitForSeconds(0.001f);
        LoadingTextBox.GetComponent<TMP_Text>().text = string.Empty;
    }

    // Methode that make mountUnitsColumnBeamTruss
    private void MountUnitsColumnBeamTruss(GameObject[] MountUnitsColumnBeamTruss, bool isOnHighColumns)
    {
        for (int i = 0, j = 0; i < Canopy.PlanColumn.CountStep; i++)
        {
            for (int k = 0; k < 2; k++, j++)
            {
                if (j == Canopy.PlanColumn.CountStep * 2 - 1)
                    break;
                MountUnitsColumnBeamTruss[j] = Instantiate(GameObject.FindGameObjectWithTag("MountUnitColumnBeamTruss"));
                MountUnitsColumnBeamTruss[j].transform.SetParent(CanopyObject.transform);
                MountUnitsColumnBeamTruss[j].SetActive(false);
                MountUnitColumnBeamTrussGenerator mountUnitColumnBeamTrussGenerator = MountUnitsColumnBeamTruss[j].GetComponent<MountUnitColumnBeamTrussGenerator>();
                if (j % 2 == 0)
                    mountUnitColumnBeamTrussGenerator.backSideLocation = true;
                mountUnitColumnBeamTrussGenerator.onHighColumns = isOnHighColumns;
                mountUnitColumnBeamTrussGenerator.zCoord = Canopy.PlanColumn.Step + Canopy.PlanColumn.Step * i;
                MountUnitsColumnBeamTruss[j].SetActive(true);
            }
        }
    }

    // Methode that make mountUnitsBeamRafterTruss
    private void MountUnitsBeamRafterTruss(GameObject[] MountUnitsBeamRafterTruss, bool isOnHighColumns)
    {
        int index = isOnHighColumns ? 0 : 1;
        GameObject mountUnitBeamRafterTruss = GameObject.FindGameObjectsWithTag("MountUnitBeamRafterTruss")[index];
        for (int i = 0, j = 0; i < Canopy.PlanColumn.CountStep; i++)
        {
            for (int k = 0; k <= countStepRafterTrussSection; k++, j++)
            {
                if (j == MountUnitsBeamRafterTruss.Length)
                    break;
                if (j == 0)
                    MountUnitsBeamRafterTruss[j] = mountUnitBeamRafterTruss;
                else
                {
                    if (j != i * (countStepRafterTrussSection + 1))
                    { 
                        MountUnitsBeamRafterTruss[j] = Instantiate(mountUnitBeamRafterTruss);
                        MountUnitsBeamRafterTruss[j].transform.SetParent(CanopyObject.transform);
                        MountUnitsBeamRafterTruss[j].SetActive(false);
                        MountUnitBeamRafterTrussGenerator mountUnitBeamRafterTrussGenerator = MountUnitsBeamRafterTruss[j].GetComponent<MountUnitBeamRafterTrussGenerator>();
                        mountUnitBeamRafterTrussGenerator.zCoord = stepRafterTrussSection * k + Canopy.PlanColumn.Step * i;
                        MountUnitsBeamRafterTruss[j].SetActive(true); 
                    }
                }
            }
        }
    }
    public IEnumerator Calculate()
    {
        LoadPrefab loadPrefab = GameObject.FindGameObjectWithTag("LoadPrefab").GetComponent<LoadPrefab>();
        profilePipes = loadPrefab.profilePipes;
        trusses = loadPrefab.trusses;
        dollarRate = loadPrefab.dollarRate;

        CanopyDescription = GameObject.FindGameObjectWithTag("CanopyDescription");
        LoadingTextBox = GameObject.FindGameObjectWithTag("LoadingTextBox");

        float columnMaterialLength = (float)(Mathf.RoundToInt(Canopy.PlanColumn.SizeByY) * (Canopy.PlanColumn.CountStep + 1) + Mathf.RoundToInt(Canopy.PlanColumn.SizeByYLow) * (Canopy.PlanColumn.CountStep + 1)) / 1000;
        float girderMaterialLength = (Canopy.Girders.Length + 1) * Canopy.Girder.Length / 1000;
        float beamTrussMaterialLength = Canopy.PlanColumn.CountStep * 2 * Canopy.BeamTruss.LengthTop / 1000;
        float rafterTrussMaterialLength = Canopy.RafterTrusses.Length * Canopy.RafterTruss.LengthTop / 1000;
        float pricePerMcolumn;
        float pricePerMbeamTruss;
        float pricePerMrafterTruss;
        float pricePerMgirder;
        float dollarRateValue;
#if UNITY_WEBGL
        pricePerMcolumn = ValAction.GetPricePmOfProfilePipe(Canopy.ColumnBodyHigh.Profile.Name, profilePipes);
        pricePerMbeamTruss = ValAction.GetPricePmOfTruss(Canopy.BeamTruss.Truss.Name, trusses);
        pricePerMrafterTruss = ValAction.GetPricePmOfTruss(Canopy.RafterTruss.Truss.Name, trusses);
        pricePerMgirder = ValAction.GetPricePmOfProfilePipe(Canopy.Girder.Profile.Name, profilePipes);
        dollarRateValue = dollarRate.Rate;
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
        pricePerMcolumn = ValAction.GetPricePmPlayerPrefs(Canopy.ColumnBodyHigh.Profile.Name);
        pricePerMbeamTruss = ValAction.GetPricePmPlayerPrefs(Canopy.BeamTruss.Truss.Name);
        pricePerMrafterTruss = ValAction.GetPricePmPlayerPrefs(Canopy.RafterTruss.Truss.Name);
        pricePerMgirder = ValAction.GetPricePmPlayerPrefs(Canopy.Girder.Profile.Name);
        dollarRateValue = ValAction.GetDollarRatePlayerPrefs();
#endif
        print("pricePerMcolumn:" + pricePerMcolumn);
        print("pricePerMbeamTruss:" + pricePerMbeamTruss);
        print("pricePerMrafterTruss:" + pricePerMrafterTruss);
        print("pricePerMgirder:" + pricePerMgirder);
        print("dollarRate:" + dollarRate);

        int costColumns = Mathf.RoundToInt(pricePerMcolumn * columnMaterialLength * dollarRateValue);
        float costBeamTrusses = Mathf.RoundToInt(pricePerMbeamTruss * beamTrussMaterialLength * dollarRateValue);
        float costRafterTrusses = Mathf.RoundToInt(pricePerMrafterTruss * rafterTrussMaterialLength * dollarRateValue);
        float costGirders = Mathf.RoundToInt(pricePerMgirder * girderMaterialLength * dollarRateValue);

        Canopy.ResultCalculation = new()
        {
            NameColumn = Canopy.ColumnBodyHigh.Profile.Name,
            LengthHighColumn = Mathf.RoundToInt(Canopy.PlanColumn.SizeByY),
            QuantityInRowColumn = Canopy.PlanColumn.CountStep + 1,
            LengthLowColumn = Mathf.RoundToInt(Canopy.PlanColumn.SizeByYLow),
            QuantityMaterialColumn = MathF.Round(columnMaterialLength, 1),
            CostColumns = costColumns,
            NameBeamTruss = Canopy.BeamTruss.Truss.Name,
            LengthBeamTruss = Mathf.RoundToInt(Canopy.BeamTruss.LengthTop),
            QuantityBeamTruss = Canopy.PlanColumn.CountStep * 2,
            QuantityMaterialBeamTruss = MathF.Round(beamTrussMaterialLength, 1),
            MomentResistReqBeamTruss = MathF.Round(CalculationBeamTruss.MomentResistReq, 1),
            DeflectionFactBeamTruss = MathF.Round(CalculationBeamTruss.DeflectionFact, 1),
            DeflectionPermissibleBeamTruss = MathF.Round(CalculationBeamTruss.DeflectionPermissible, 1),
            CostBeamTrusses = costBeamTrusses,
            NameRafterTruss = Canopy.RafterTruss.Truss.Name,
            LengthRafterTruss = Mathf.RoundToInt(Canopy.RafterTruss.LengthTop),
            QuantityRafterTruss = Canopy.RafterTrusses.Length,
            QuantityMaterialRafterTruss = MathF.Round(rafterTrussMaterialLength, 1),
            StepRafterTruss = Mathf.RoundToInt(Canopy.RafterTruss.Step / 10),
            MomentResistReqRafterTruss = MathF.Round(CalculationRafterTruss.MomentResistReqSlope, 1),
            DeflectionFactRafterTruss = MathF.Round(CalculationRafterTruss.DeflectionFact, 1),
            DeflectionPermissibleRafterTruss = MathF.Round(CalculationRafterTruss.DeflectionPermissible, 1),
            CostRafterTrusses = costRafterTrusses,
            NameGirder = Canopy.Girder.Profile.Name,
            LengthGirder = Mathf.RoundToInt(Canopy.Girder.Length),
            QuantityGirder = Canopy.Girders.Length + 1,
            QuantityMaterialGirder = girderMaterialLength,
            StepGirder = Mathf.RoundToInt(Canopy.Girder.Step / 10),
            DeflectionFactGirder = MathF.Round(CalculationGirder.DeflectionFact, 1),
            DeflectionPermissibleGirder = MathF.Round(CalculationGirder.DeflectionPermissible, 1),
            CostGirders = costGirders,
            CostTotal = costColumns + costBeamTrusses + costRafterTrusses + costGirders
        };

        CanopyDescription.GetComponent<TMP_Text>().text = $"Колонна большей высоты:\n\tПрофиль - {Canopy.ResultCalculation.NameColumn}" +
            $"\n\tДлина - {Canopy.ResultCalculation.LengthHighColumn} мм" +
            $"\n\tКол-во - {Canopy.ResultCalculation.QuantityInRowColumn} шт" +
            $"\nКолонна малой высоты:\n\tПрофиль - {Canopy.ColumnBodyHigh.Profile.Name}" +
            $"\n\tДлина - {Canopy.ResultCalculation.LengthLowColumn} мм" +
            $"\n\tКол-во - {Canopy.ResultCalculation.QuantityInRowColumn} шт" +
            $"\nКол-во мат-ла на колонны: {Canopy.ResultCalculation.QuantityMaterialColumn} м" +
            $"\nСт-ть мат-ла на колонны: {Canopy.ResultCalculation.CostColumns} сом" +
            $"\n" +
            $"Балочная ферма:\n\tТип - {Canopy.ResultCalculation.NameBeamTruss}" +
            $"\n\tДлина - {Canopy.ResultCalculation.LengthBeamTruss} мм" +
            $"\n\tКол-во - {Canopy.ResultCalculation.QuantityBeamTruss} шт" +
            $"\nТреб. момент сопр. - {Canopy.ResultCalculation.MomentResistReqBeamTruss} см3" +
            $"\nФакт. прогиб - {Canopy.ResultCalculation.DeflectionFactBeamTruss} см" +
            $" (Доп. прогиб - {Canopy.ResultCalculation.DeflectionPermissibleBeamTruss} см)" +
            $"\nСт-ть балочных ферм: {Canopy.ResultCalculation.CostBeamTrusses} сом" +
            $"\n" +
            $"Стропильная ферма:\n\tТип - {Canopy.ResultCalculation.NameRafterTruss}" +
            $"\n\tДлина - {Canopy.ResultCalculation.LengthRafterTruss} мм" +
            $"\n\tКол-во - {Canopy.ResultCalculation.QuantityRafterTruss} шт" +
            $"\n\tПодобранный шаг - {Canopy.ResultCalculation.StepRafterTruss} см" +
            $"\nТреб. момент сопр. - {Canopy.ResultCalculation.MomentResistReqRafterTruss} см3" +
            $"\nФакт. прогиб - {Canopy.ResultCalculation.DeflectionFactRafterTruss} см" +
            $" (Доп. прогиб - {Canopy.ResultCalculation.DeflectionPermissibleRafterTruss} см)" +
            $"\nСт-ть стропильных ферм: {Canopy.ResultCalculation.CostRafterTrusses} сом" +
            $"\n" +
            $"Прогон:\n\tПрофиль - {Canopy.ResultCalculation.NameGirder}" +
            $"\n\tДлина - {Canopy.ResultCalculation.LengthGirder} мм" +
            $"\n\tКол-во - {Canopy.ResultCalculation.QuantityGirder} шт" +
            $"\n\tПодобранный шаг - {Canopy.ResultCalculation.StepGirder} см" +
            $"\nФакт. прогиб - {Canopy.ResultCalculation.DeflectionFactGirder} см" +
            $" (Доп. прогиб - {Canopy.ResultCalculation.DeflectionPermissibleGirder} см)" +
            $"\nКол-во мат-ла на прогоны: {Canopy.ResultCalculation.QuantityMaterialGirder} м" +
            $"\nСт-ть мат-ла на прогоны: {Canopy.ResultCalculation.CostGirders} сом" +
            $"\nИтого: {Canopy.ResultCalculation.CostTotal} сом";
        Canopy.CanopyDescription = CanopyDescription.GetComponent<TMP_Text>().text;
 
        yield return null;
    }
}
    