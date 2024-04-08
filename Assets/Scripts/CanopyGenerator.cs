using Assets.Models;
using Assets.ModelsRequest;
using Assets.Services;
using Assets.Utils;
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
    public PlanCanopy planColumn;
    private GameObject[] columnsHigh;
    private GameObject[] columnsLow;
    private GameObject[] beamTrussesOnHigh;
    private GameObject[] beamTrussesOnLow;
    private GameObject[] rafterTrusses;
    private GameObject[] girders;
    private GameObject canopy;
    private ColumnBody columnBodyHigh;
    private BeamTruss beamTruss;
    private RafterTruss rafterTruss;
    private Girder girder;
    private ColumnPlug columnPlug = new();
    private int countStepRafterTruss;
    private int countStepGirder;
    public GameObject CanopyDescription;
    public GameObject LoadingTextBox;
    public const float factorTolerance = 1.2f;
    public List<ProfilePipe> profilePipes = new();
    public List<Truss> trusses = new();
    public DollarRate dollarRate = new();

    void Awake()
    {
        print("CanopyGenerator");
    }

    void Start()
    {
        StartCoroutine(MakeCanopy());
        StartCoroutine(Calculate());  
    }

    // Update is called once per frame
    void Update()
    {       
    }

    IEnumerator MakeCanopy()
    {
        planColumn = GameObject.FindGameObjectWithTag("PlanCanopy").GetComponent<PlanCanopyGenerator>().MakePlanCanopy();
        canopy = GameObject.FindGameObjectWithTag("Canopy");
        columnsHigh = new GameObject[planColumn.CountStep + 1];
        columnsLow = new GameObject[planColumn.CountStep + 1];
        beamTrussesOnHigh = new GameObject[planColumn.CountStep];
        beamTrussesOnLow = new GameObject[beamTrussesOnHigh.Length];
        columnBodyHigh = GameObject.FindGameObjectsWithTag("ColumnHigh")[0].GetComponent<ColumnGenerator>().ColumnBody;
        beamTruss = GameObject.FindGameObjectsWithTag("BeamTruss")[0].GetComponent<BeamTrussGenerator>().beamTrussForRead;
        rafterTruss = GameObject.FindGameObjectsWithTag("RafterTruss")[0].GetComponent<RafterTrussGenerator>().rafterTrussForRead;
        girder = GameObject.FindGameObjectsWithTag("Girder")[0].GetComponent<GirderGenerator>().girder;
        countStepRafterTruss = planColumn.SizeByZ / Mathf.FloorToInt(planColumn.SizeByZ / rafterTruss.Step) <= rafterTruss.Step ?
            Mathf.FloorToInt(planColumn.SizeByZ / rafterTruss.Step) : Mathf.FloorToInt(planColumn.SizeByZ / rafterTruss.Step) + 1;
        rafterTruss.Step = planColumn.SizeByZ / countStepRafterTruss;
        countStepGirder = (rafterTruss.LengthTop - girder.Profile.Length) / (Mathf.FloorToInt((rafterTruss.LengthTop - girder.Profile.Length) / girder.Step)) <= girder.Step ?
            Mathf.FloorToInt((rafterTruss.LengthTop - girder.Profile.Length) / girder.Step) : Mathf.FloorToInt((rafterTruss.LengthTop - girder.Profile.Length) / girder.Step) + 1;
        girder.Step = rafterTruss.LengthTop / countStepGirder;
        rafterTrusses = new GameObject[countStepRafterTruss + 1];
        girders = new GameObject[countStepGirder];
        float partAdditFromAngle = Mathf.Tan(planColumn.Slope)
            * (beamTruss.Truss.ProfileBelt.Length / 2 - beamTruss.Truss.ProfileBelt.Radius + planColumn.OutputRafter);
        float partAdditHalfBeltAngle = rafterTruss.Truss.ProfileBelt.Height / 2 / Mathf.Cos(planColumn.Slope);

        // Make columns
        for (int i = 0; i < planColumn.CountStep; i++)
        {
            columnsHigh[i] = Object.Instantiate(GameObject.FindGameObjectsWithTag("ColumnHigh")[0]);
            columnsHigh[i].transform.SetParent(canopy.transform);
            Destroy(columnsHigh[i].GetComponent<TransformColumnHigh>());
            columnsHigh[i].transform.localPosition = new Vector3(0, 0, planColumn.Step + planColumn.Step * i);
            columnsLow[i] = Object.Instantiate(GameObject.FindGameObjectsWithTag("ColumnLow")[0]);
            columnsLow[i].transform.SetParent(canopy.transform);
            Destroy(columnsLow[i].GetComponent<TransformColumnLow>());
            columnsLow[i].transform.localPosition = new Vector3(planColumn.SizeByX, 0, planColumn.Step + planColumn.Step * i);
        }
        // Make beam trusses on high columns
        for (int i = 0; i < beamTrussesOnHigh.Length - 1; i++)
        {
            beamTrussesOnHigh[i] = Object.Instantiate(GameObject.FindGameObjectsWithTag("BeamTruss")[0]);
            beamTrussesOnHigh[i].transform.SetParent(canopy.transform);
            Destroy(beamTrussesOnHigh[i].GetComponent<BeamTrussTransform>());
            beamTrussesOnHigh[i].transform.localPosition = new Vector3(0
           , planColumn.SizeByY + columnPlug.Thickness + beamTruss.Truss.ProfileBelt.Height / 2
           , planColumn.Step + planColumn.Step * i);
            beamTrussesOnHigh[i].transform.localRotation = Quaternion.Euler(0f, -90f, -90f);
        }
        // Make beam trusses on low columns
        for (int i = 0; i < beamTrussesOnLow.Length; i++)
        {
            beamTrussesOnLow[i] = Object.Instantiate(GameObject.FindGameObjectsWithTag("BeamTruss")[0]);
            beamTrussesOnLow[i].transform.SetParent(canopy.transform);
            Destroy(beamTrussesOnLow[i].GetComponent<BeamTrussTransform>());
            beamTrussesOnLow[i].transform.localPosition = new Vector3(planColumn.SizeByX
           , planColumn.SizeByYLow + columnPlug.Thickness + beamTruss.Truss.ProfileBelt.Height / 2
           , planColumn.Step * i);
            beamTrussesOnLow[i].transform.localRotation = Quaternion.Euler(0f, -90f, -90f);
        }
        // Make rafter trusses
        for (int i = 0; i < rafterTrusses.Length; i++)
        {
            rafterTrusses[i] = Object.Instantiate(GameObject.FindGameObjectsWithTag("RafterTruss")[0]);
            rafterTrusses[i].transform.SetParent(canopy.transform);
            Destroy(rafterTrusses[i].GetComponent<RafterTrussTransform>());
            if (i == rafterTrusses.Length - 1)
            {
                rafterTrusses[i].transform.localPosition = new Vector3(-planColumn.OutputRafter
                             , planColumn.SizeByY + columnPlug.Thickness + partAdditFromAngle
                             + partAdditHalfBeltAngle + beamTruss.Truss.ProfileBelt.Height
                             , planColumn.SizeByZ);
                rafterTrusses[i].transform.localRotation = Quaternion.Euler(0, 0, -(90 + planColumn.SlopeInDegree));
            }
            else
            {
                rafterTrusses[i].transform.localPosition = new Vector3(-planColumn.OutputRafter
                             , planColumn.SizeByY + columnPlug.Thickness + partAdditFromAngle
                             + partAdditHalfBeltAngle + beamTruss.Truss.ProfileBelt.Height
                             , rafterTruss.Step + rafterTruss.Step * i);
                rafterTrusses[i].transform.localRotation = Quaternion.Euler(0, 0, -(90 + planColumn.SlopeInDegree));
            }
        }
        // Make girders
        float stepGirder;
        float projectionHorStepGirder;
        float projectionVertStepGirder;
        Vector3 elemenGirderPosition = GameObject.FindGameObjectWithTag("Girder").transform.position;
        for (int i = 0; i < girders.Length; i++)
        {
            girders[i] = Object.Instantiate(GameObject.FindGameObjectsWithTag("Girder")[0]);
            girders[i].transform.SetParent(canopy.transform);
            Destroy(girders[i].GetComponent<GirderTransform>());
            if (i == girders.Length - 1)
            {
                stepGirder = rafterTruss.LengthTop - girder.Profile.Length;
                projectionHorStepGirder = Mathf.Cos(planColumn.Slope) * stepGirder;
                projectionVertStepGirder = Mathf.Sin(planColumn.Slope) * stepGirder;
                girders[i].transform.localPosition = new Vector3(elemenGirderPosition.x + projectionHorStepGirder
                             , elemenGirderPosition.y - projectionVertStepGirder
                             , elemenGirderPosition.z);
                girders[i].transform.localRotation = Quaternion.Euler(-planColumn.SlopeInDegree, -90, -90);
            }
            else
            {
                stepGirder = girder.Step * (1 + i);
                projectionHorStepGirder = Mathf.Cos(planColumn.Slope) * stepGirder;
                projectionVertStepGirder = Mathf.Sin(planColumn.Slope) * stepGirder;
                girders[i].transform.localPosition = new Vector3(elemenGirderPosition.x + projectionHorStepGirder
                             , elemenGirderPosition.y - projectionVertStepGirder
                             , elemenGirderPosition.z);
                girders[i].transform.localRotation = Quaternion.Euler(-planColumn.SlopeInDegree, -90, -90);
            }
        }
        yield return new WaitForSeconds(0.001f);
        LoadingTextBox.GetComponent<TMP_Text>().text = string.Empty;
    }

    IEnumerator Calculate()
    {
        LoadPrefab loadPrefab = GameObject.FindGameObjectWithTag("LoadPrefab").GetComponent<LoadPrefab>();
        profilePipes = loadPrefab.profilePipes;
        trusses = loadPrefab.trusses;
        dollarRate = loadPrefab.dollarRate;

        CanopyDescription = GameObject.FindGameObjectWithTag("CanopyDescription");
        LoadingTextBox = GameObject.FindGameObjectWithTag("LoadingTextBox");

        float columnMaterialLength = (float)(Mathf.RoundToInt(planColumn.SizeByY) * (planColumn.CountStep + 1) + Mathf.RoundToInt(planColumn.SizeByYLow) * (planColumn.CountStep + 1)) / 1000;
        float girderMaterialLength = (girders.Length + 1) * girder.Length / 1000;
        float pricePerMcolumn;
        float pricePerMbeamTruss;
        float pricePerMrafterTruss;
        float pricePerMgirder;
        float dollarRateValue;
#if UNITY_WEBGL
        pricePerMcolumn = ValAction.GetPricePmOfProfilePipe(columnBodyHigh.Profile.Name, profilePipes);
        pricePerMbeamTruss = ValAction.GetPricePmOfTruss(beamTruss.Truss.Name, trusses);
        pricePerMrafterTruss = ValAction.GetPricePmOfTruss(rafterTruss.Truss.Name, trusses);
        pricePerMgirder = ValAction.GetPricePmOfProfilePipe(girder.Profile.Name, profilePipes);
        dollarRateValue = dollarRate.Rate;
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
        pricePerMcolumn = ValAction.GetPricePmPlayerPrefs(columnBodyHigh.Profile.Name);
        pricePerMbeamTruss = ValAction.GetPricePmPlayerPrefs(beamTruss.Truss.Name);
        pricePerMrafterTruss = ValAction.GetPricePmPlayerPrefs(rafterTruss.Truss.Name);
        pricePerMgirder = ValAction.GetPricePmPlayerPrefs(girder.Profile.Name);
        dollarRateValue = ValAction.GetDollarRatePlayerPrefs();
#endif
        print("pricePerMcolumn:" + pricePerMcolumn);
        print("pricePerMbeamTruss:" + pricePerMbeamTruss);
        print("pricePerMrafterTruss:" + pricePerMrafterTruss);
        print("pricePerMgirder:" + pricePerMgirder);
        print("dollarRate:" + dollarRate);

        float costColumns = Mathf.RoundToInt(pricePerMcolumn * columnMaterialLength * dollarRateValue);
        float costBeamTrusses = Mathf.RoundToInt(pricePerMbeamTruss * beamTruss.LengthTop / 1000 * planColumn.CountStep * 2 * dollarRateValue);
        float costRafterTrusses = Mathf.RoundToInt(pricePerMrafterTruss * rafterTruss.LengthTop / 1000 * rafterTrusses.Length * dollarRateValue);
        float costGirders = Mathf.RoundToInt(pricePerMgirder * girderMaterialLength * dollarRateValue);

        CanopyDescription.GetComponent<TMP_Text>().text = $"������� ������� ������:\n\t������� - {columnBodyHigh.Profile.Name}" +
            $"\n\t����� - {Mathf.RoundToInt(planColumn.SizeByY)} ��" +
            $"\n\t���-�� - {planColumn.CountStep + 1} ��" +
            $"\n������� ����� ������:\n\t������� - {columnBodyHigh.Profile.Name}" +
            $"\n\t����� - {Mathf.RoundToInt(planColumn.SizeByYLow)} ��" +
            $"\n\t���-�� - {planColumn.CountStep + 1} ��" +
            $"\n���-�� ���-�� �� �������: {Math.Round(columnMaterialLength, 1)} �" +
            $"\n��-�� ���-�� �� �������: {costColumns} ���" +
            $"\n" +
            $"�������� �����:\n\t��� - {beamTruss.Truss.Name}" +
            $"\n\t����� - {Mathf.RoundToInt(beamTruss.LengthTop)} ��" +
            $"\n\t���-�� - {planColumn.CountStep * 2} ��" +
            $"\n����. ������ ����. - {MathF.Round(CalculationBeamTruss.MomentResistReq, 1)} ��3" +
            $"\n����. ������ - {MathF.Round(CalculationBeamTruss.DeflectionFact, 1)} ��" +
            $" (���. ������ - {MathF.Round(CalculationBeamTruss.DeflectionPermissible, 1)} ��)" +
            $"\n��-�� �������� ����: {costBeamTrusses} ���" +
            $"\n" +
            $"����������� �����:\n\t��� - {rafterTruss.Truss.Name}" +
            $"\n\t����� - {Mathf.RoundToInt(rafterTruss.LengthTop)} ��" +
            $"\n\t���-�� - {rafterTrusses.Length} ��" +
            $"\n\t����������� ��� - {Mathf.RoundToInt(rafterTruss.Step / 10)} ��" +
            $"\n����. ������ ����. - {MathF.Round(CalculationRafterTruss.MomentResistReqSlope, 1)} ��3" +
            $"\n����. ������ - {MathF.Round(CalculationRafterTruss.DeflectionFact, 1)} ��" +
            $" (���. ������ - {MathF.Round(CalculationRafterTruss.DeflectionPermissible, 1)} ��)" +
            $"\n��-�� ����������� ����: {costRafterTrusses} ���" +
            $"\n" +
            $"������:\n\t������� - {girder.Profile.Name}" +
            $"\n\t����� - {Mathf.RoundToInt(girder.Length)} ��" +
            $"\n\t���-�� - {girders.Length + 1} ��" +
            $"\n\t����������� ��� - {Mathf.RoundToInt(girder.Step / 10)} ��" +
            $"\n����. ������ - {MathF.Round(CalculationGirder.DeflectionFact, 1)} ��" +
            $" (���. ������ - {MathF.Round(CalculationGirder.DeflectionPermissible, 1)} ��)" +
            $"\n���-�� ���-�� �� �������: {Math.Round(girderMaterialLength, 1)} �" +
            $"\n��-�� ���-�� �� �������: {costGirders} ���" +
            $"\n�����: {costColumns + costBeamTrusses + costRafterTrusses + costGirders} ���";
        yield return null;
    }
}
    