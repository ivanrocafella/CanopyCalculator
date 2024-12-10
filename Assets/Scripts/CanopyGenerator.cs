using Assets.Models;
using Assets.Models.Enums;
using Assets.Services;
using Assets.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

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
    public List<Flange> flanges = new();
    public List<Fixing> fixings = new();
    public List<MountUnitColumnBeamTruss> mountUnitColumnBeamTrusses = new();
    public List<MountUnitBeamRafterTruss> mountUnitBeamRafterTrusses = new();
    private int countStepRafterTrussSection;
    private float stepRafterTrussSection;
    private float lengthSection;
    private float stepRafterTrussSectionWide;
    private LoadPrefab LoadPrefab;
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
        LoadPrefab = GameObject.FindGameObjectWithTag("LoadPrefab").GetComponent<LoadPrefab>();
        GameObject planCanopy = GameObject.FindGameObjectWithTag("PlanCanopy");
        Canopy.PlanColumn = planCanopy.GetComponent<PlanCanopyGenerator>().MakePlanCanopy();
        CanopyObject = GameObject.FindGameObjectWithTag("Canopy");
        Canopy.ColumnsHigh = new GameObject[Canopy.PlanColumn.CountStep + 1];
        Canopy.ColumnsLow = new GameObject[Canopy.ColumnsHigh.Length];
        Canopy.BeamTrussesOnHigh = new GameObject[Canopy.PlanColumn.CountStep];
        Canopy.BeamTrussesOnLow = new GameObject[Canopy.BeamTrussesOnHigh.Length];
        Canopy.ColumnBodyHigh = GameObject.FindGameObjectWithTag("ColumnHigh").GetComponent<ColumnGenerator>().ColumnBody;
        Canopy.BeamTruss = GameObject.FindGameObjectWithTag("BeamTruss").GetComponent<BeamTrussGenerator>().beamTrussForRead;
        Canopy.RafterTruss = GameObject.FindGameObjectWithTag("RafterTruss").GetComponent<RafterTrussGenerator>().rafterTrussForRead;
        Canopy.Girder = GameObject.FindGameObjectWithTag("Girder").GetComponent<GirderGenerator>().girder;
        Canopy.MountUnitBeamRafterTruss = ScriptObjectsAction.GetMountUnitBeamRafterTrussByName(Canopy.RafterTruss.Truss.Name, MountUnitBeamRafterTrussDataList);
        Canopy.MountUnitColumnBeamTruss = ScriptObjectsAction.GetMountUnitColumnBeamTrussByName(Canopy.BeamTruss.Truss.Name, MountUnitColumnBeamTrussDataList);

        Canopy.CountStepRafterTruss = Canopy.PlanColumn.SizeByZ / Mathf.FloorToInt(Canopy.PlanColumn.SizeByZ / Canopy.RafterTruss.Step) <= Canopy.RafterTruss.Step ?
            Mathf.FloorToInt(Canopy.PlanColumn.SizeByZ / Canopy.RafterTruss.Step) : Mathf.FloorToInt(Canopy.PlanColumn.SizeByZ / Canopy.RafterTruss.Step) + 1;
        Canopy.RafterTruss.Step = Canopy.PlanColumn.SizeByZ / Canopy.CountStepRafterTruss;

        Canopy.CountStepGirder = (Canopy.RafterTruss.LengthTop - Canopy.Girder.Profile.Length) / (Mathf.FloorToInt((Canopy.RafterTruss.LengthTop - Canopy.Girder.Profile.Length) / Canopy.Girder.Step)) <= Canopy.Girder.Step ?
            Mathf.FloorToInt((Canopy.RafterTruss.LengthTop - Canopy.Girder.Profile.Length) / Canopy.Girder.Step) : Mathf.FloorToInt((Canopy.RafterTruss.LengthTop - Canopy.Girder.Profile.Length) / Canopy.Girder.Step) + 1;
        Canopy.Girder.Step = Canopy.RafterTruss.LengthTop / Canopy.CountStepGirder;
        Canopy.Girders = new GameObject[Canopy.CountStepGirder + 1];

        if (Canopy.PlanColumn.IsDemountable)
        {
            lengthSection = Canopy.PlanColumn.Step - Canopy.ColumnBodyHigh.Profile.Length - Canopy.MountUnitColumnBeamTruss.WidthFlangeColumn * 2 - Canopy.MountUnitBeamRafterTruss.LengthFlangeBeamTruss;
            countStepRafterTrussSection = lengthSection / Mathf.FloorToInt(lengthSection / Canopy.RafterTruss.Step) <= Canopy.RafterTruss.Step ? Mathf.FloorToInt(lengthSection / Canopy.RafterTruss.Step) : Mathf.FloorToInt(lengthSection / Canopy.RafterTruss.Step) + 1;
            stepRafterTrussSection = lengthSection / countStepRafterTrussSection;
            // Recalculation stepRafterTrussSection
            stepRafterTrussSectionWide = stepRafterTrussSection + Canopy.ColumnBodyHigh.Profile.Length + Canopy.MountUnitColumnBeamTruss.WidthFlangeColumn * 2 + Canopy.MountUnitBeamRafterTruss.LengthFlangeBeamTruss;
            countStepRafterTrussSection = stepRafterTrussSectionWide <= Canopy.RafterTruss.Step ? countStepRafterTrussSection : countStepRafterTrussSection + 1;
            stepRafterTrussSection = lengthSection / countStepRafterTrussSection;;
            // Recalculation RafterTruss
            Truss trussRafter = CalculationRafterTruss.CalculateRafterTruss(Canopy.PlanColumn.SizeByX
             , (stepRafterTrussSectionWide + stepRafterTrussSection) / 2
             , Canopy.PlanColumn.OutputRafter
             , LoadPrefab.cargo, LoadPrefab.material, LoadPrefab.trusses);
            Canopy.PlanColumn.KindTrussRafter = (KindTruss)LoadPrefab.trusses.IndexOf(trussRafter);
            DestroyImmediate(GameObject.FindGameObjectWithTag("RafterTruss"));
            var newRafterTruss = LoadPrefab.canopyPrefab.transform.GetChild(2).gameObject;
            planCanopy.GetComponent<PlanCanopyGenerator>().KindTrussRafter = Canopy.PlanColumn.KindTrussRafter;
            Instantiate(newRafterTruss);
            GameObject rafterTrussGO = GameObject.FindGameObjectWithTag("RafterTruss");
            Canopy.RafterTruss = rafterTrussGO.GetComponent<RafterTrussGenerator>().rafterTrussForRead;
            rafterTrussGO.transform.SetParent(CanopyObject.transform);


            Canopy.MountUnitsColumnBeamTrussOnHC = new GameObject[Canopy.PlanColumn.CountStep * 2];
            Canopy.MountUnitsColumnBeamTrussOnLC = new GameObject[Canopy.MountUnitsColumnBeamTrussOnHC.Length];
            Canopy.MountUnitsBeamRafterTrussOnHC = new GameObject[Canopy.PlanColumn.CountStep * (countStepRafterTrussSection + 1)];
            Canopy.MountUnitsBeamRafterTrussOnLC = new GameObject[Canopy.MountUnitsBeamRafterTrussOnHC.Length];
            Canopy.RafterTruss.Step = stepRafterTrussSection;
        }

        int countStepRafterTruss = Canopy.PlanColumn.IsDemountable ? Canopy.MountUnitsBeamRafterTrussOnHC.Length : Canopy.CountStepRafterTruss + 1; 
        Canopy.RafterTrusses = new GameObject[countStepRafterTruss];

        float partAdditFromAngle = Mathf.Tan(Canopy.PlanColumn.Slope)
            * (Canopy.BeamTruss.Truss.ProfileBelt.Length / 2 - Canopy.BeamTruss.Truss.ProfileBelt.Radius + Canopy.PlanColumn.OutputRafter);
        float partAdditHalfBeltAngle = Canopy.RafterTruss.Truss.ProfileBelt.Height / 2 / Mathf.Cos(Canopy.PlanColumn.Slope);

        // Make columns
        for (int i = 0; i < Canopy.ColumnsHigh.Length; i++)
        {
            GameObject columnHigh = GameObject.FindGameObjectWithTag("ColumnHigh");
            GameObject columnLow = GameObject.FindGameObjectWithTag("ColumnLow");
            if (i == 0)
            {
                Canopy.ColumnsHigh[i] = columnHigh;
                Canopy.ColumnsLow[i] = columnLow;
            }
            else
            {
                InstantiateGO<TransformColumnHigh>(columnHigh, Canopy.ColumnsHigh, 0, 0, Canopy.PlanColumn.Step * i, 0f, 0f, 0f, i);
                InstantiateGO<TransformColumnLow>(columnLow, Canopy.ColumnsLow, Canopy.PlanColumn.SizeByX, 0, Canopy.PlanColumn.Step * i, 0f, 0f, 0f, i);
            }
        }
        if (Canopy.PlanColumn.IsDemountable)
        {
             // Make mountUnitsColumnBeamTruss on highColumns
             MountUnitsColumnBeamTruss(Canopy.MountUnitsColumnBeamTrussOnHC, true);
             // Make mountUnitsColumnBeamTruss on lowColumns
             MountUnitsColumnBeamTruss(Canopy.MountUnitsColumnBeamTrussOnLC, false);
             // Make mountUnitsBeamRufterTruss on highColumns
             MountUnitsBeamRafterTruss(Canopy.MountUnitsBeamRafterTrussOnHC, true);
             // Make mountUnitsBeamRufterTruss on lowColumns
             MountUnitsBeamRafterTruss(Canopy.MountUnitsBeamRafterTrussOnLC, false);
        }
        // Make beam trusses
        for (int i = 0; i < Canopy.BeamTrussesOnHigh.Length; i++)
        {
            GameObject beamTruss = GameObject.FindGameObjectWithTag("BeamTruss");
            if (i == 0)
                Canopy.BeamTrussesOnHigh[i] = beamTruss;
            else
                InstantiateGO<BeamTrussTransform>(beamTruss, Canopy.BeamTrussesOnHigh
                    , 0
                    , Canopy.PlanColumn.SizeByY + Canopy.ColumnPlug.Thickness + Canopy.BeamTruss.Truss.ProfileBelt.Height / 2
                    , Canopy.PlanColumn.Step * i
                    , 0f, -90f, -90f, i);
            InstantiateGO<BeamTrussTransform>(beamTruss, Canopy.BeamTrussesOnLow
                 , Canopy.PlanColumn.SizeByX
                 , Canopy.PlanColumn.SizeByYLow + Canopy.ColumnPlug.Thickness + Canopy.BeamTruss.Truss.ProfileBelt.Height / 2
                 , Canopy.PlanColumn.Step * i
                 , 0f, -90f, -90f, i);
        }
        yield return null;
        // Make rafter trusses
        MountRafterTrusses(Canopy.RafterTrusses, Canopy.PlanColumn.IsDemountable);
        // Make girders
        float stepGirder;
        float projectionHorStepGirder;
        float projectionVertStepGirder;
        Vector3 elemenGirderPosition = GameObject.FindGameObjectWithTag("Girder").transform.localPosition;
        for (int i = 0; i < Canopy.Girders.Length; i++)
        {
            GameObject girder = GameObject.FindGameObjectWithTag("Girder");
            if (i == 0)
            {
                Canopy.Girders[i] = girder;
            }
            else
            {
                stepGirder = i == Canopy.Girders.Length - 1 ? Canopy.RafterTruss.LengthTop - Canopy.Girder.Profile.Length : Canopy.Girder.Step * i; 
                projectionHorStepGirder = Mathf.Cos(Canopy.PlanColumn.Slope) * stepGirder;
                projectionVertStepGirder = Mathf.Sin(Canopy.PlanColumn.Slope) * stepGirder;
                InstantiateGO<GirderTransform>(girder, Canopy.Girders
                    , elemenGirderPosition.x + projectionHorStepGirder
                    , elemenGirderPosition.y - projectionVertStepGirder
                    , elemenGirderPosition.z
                    , -Canopy.PlanColumn.SlopeInDegree, -90, -90, i);
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
    // Methode that places RafterTrusses
    private void MountRafterTrusses(GameObject[] RafterTrusses, bool isDemountable)
    {
        GameObject rafterTruss = GameObject.FindGameObjectWithTag("RafterTruss");
        Vector3 elemenRafterTrussPosition = rafterTruss.transform.localPosition;
        if (isDemountable)
        {
            for (int i = 0, j = 0; i < Canopy.PlanColumn.CountStep; i++)
            {
                for (int k = 0; k <= countStepRafterTrussSection; k++, j++)
                {
                    if (j == RafterTrusses.Length)
                        break;
                    if (j == 0)
                        RafterTrusses[j] = rafterTruss;
                    else
                    {
                        if (j != i * (countStepRafterTrussSection + 1))                       
                            InstantiateGO<RafterTrussTransform>(rafterTruss, RafterTrusses
                                 , -Canopy.PlanColumn.OutputRafter
                                 , elemenRafterTrussPosition.y
                                 , elemenRafterTrussPosition.z + stepRafterTrussSection * k + Canopy.PlanColumn.Step * i
                                 , 0f, 0f, -(90f + Canopy.PlanColumn.SlopeInDegree), j);
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < RafterTrusses.Length; i++)
            {
                if (i != 0)
                    InstantiateGO<RafterTrussTransform>(rafterTruss, RafterTrusses
                        , -Canopy.PlanColumn.OutputRafter
                        , elemenRafterTrussPosition.y
                        , Canopy.RafterTruss.Step * i
                        , 0f, 0f, -(90f + Canopy.PlanColumn.SlopeInDegree), i);
                else
                    RafterTrusses[i] = rafterTruss;
            }
        }
    }
    // Methode for instantiating go
    void InstantiateGO<T>(GameObject gameObject, GameObject[] gameObjects, float x, float y, float z, float angleX, float angleY, float angleZ, int iter) where T : Component
    {
        gameObjects[iter] = Instantiate(gameObject);
        gameObjects[iter].transform.SetParent(CanopyObject.transform);
        Destroy(gameObjects[iter].GetComponent<T>());
        gameObjects[iter].transform.SetLocalPositionAndRotation(new Vector3(x, y, z), Quaternion.Euler(angleX, angleY, angleZ));
    }
    // Method for instantiating rafterTruss
    private void InstantiateRafterTruss(GameObject[] RafterTrusses, GameObject rafterTruss, Vector3 elemenRafterTrussPosition, int iter, float coordZ)
    {
        RafterTrusses[iter] = Instantiate(rafterTruss);
        RafterTrusses[iter].transform.SetParent(CanopyObject.transform);
        Destroy(RafterTrusses[iter].GetComponent<RafterTrussTransform>());
        RafterTrusses[iter].transform.SetLocalPositionAndRotation(new Vector3(-Canopy.PlanColumn.OutputRafter, elemenRafterTrussPosition.y, coordZ)
            , Quaternion.Euler(0, 0, -(90 + Canopy.PlanColumn.SlopeInDegree)));
    }
    public IEnumerator Calculate()
    {      
        mountUnitColumnBeamTrusses = LoadPrefab.mountUnitColumnBeamTrusses;
        mountUnitBeamRafterTrusses = LoadPrefab.mountUnitBeamRafterTrusses;

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
        int quantityFlangeColumnMucbt = Canopy.MountUnitsColumnBeamTrussOnHC.Length + Canopy.MountUnitsColumnBeamTrussOnLC.Length;
        int quantityFlangeBeamTrussMucbt = quantityFlangeColumnMucbt;
        int quantityFlangeBeamTrussMubrt = Canopy.MountUnitsBeamRafterTrussOnHC.Length + Canopy.MountUnitsBeamRafterTrussOnLC.Length;
        int quantityFlangeRafterTrussMubrt = quantityFlangeBeamTrussMubrt;
        int quantityScrew = (quantityFlangeColumnMucbt + quantityFlangeBeamTrussMubrt) * 2;
        int quantityNut = quantityScrew;
        int quantityWasher = quantityScrew * 2;
#if UNITY_WEBGL
        yield return DatabaseAction<List<ProfilePipe>>.GetData("/api/ProfilePipe/ProfilePipes", (p) => profilePipes = p);
        yield return DatabaseAction<List<Truss>>.GetData("/api/Truss/Trusses", (t) => trusses = t);
        yield return DatabaseAction<List<Flange>>.GetData("/api/Flange/Flanges", (f) => flanges = f);
        yield return DatabaseAction<List<Fixing>>.GetData("/api/Fixing/Fixings", (f) => fixings = f);
        yield return DatabaseAction<DollarRate>.GetData("/api/DollarRate", (d) => dollarRate = d);
        pricePerMcolumn = ValAction.GetPricePmOfProfilePipe(Canopy.ColumnBodyHigh.Profile.Name, profilePipes);
        pricePerMbeamTruss = ValAction.GetPricePmOfTruss(Canopy.BeamTruss.Truss.Name, trusses);
        pricePerMrafterTruss = ValAction.GetPricePmOfTruss(Canopy.RafterTruss.Truss.Name, trusses);
        pricePerMgirder = ValAction.GetPricePmOfProfilePipe(Canopy.Girder.Profile.Name, profilePipes);
        dollarRateValue = dollarRate.Rate;
        mountUnitColumnBeamTrusses.ForEach(e => e.PriceFlangeColumn = ValAction.GetObjectByName<Flange>(flanges, t => t.Name, e.NameFlangeColumn).Price);
        mountUnitColumnBeamTrusses.ForEach(e => e.PriceFlangeBeam = ValAction.GetObjectByName<Flange>(flanges, t => t.Name, e.NameFlangeBeam).Price);
        mountUnitColumnBeamTrusses.ForEach(e => e.PriceKgScrew = ValAction.GetObjectByName<Fixing>(fixings, t => t.Name, e.NameScrew).PricePerKg);
        mountUnitColumnBeamTrusses.ForEach(e => e.PriceKgNut = ValAction.GetObjectByName<Fixing>(fixings, t => t.Name, e.NameNut).PricePerKg);
        mountUnitColumnBeamTrusses.ForEach(e => e.PriceKgWasher = ValAction.GetObjectByName<Fixing>(fixings, t => t.Name, e.NameWasher).PricePerKg);
        mountUnitBeamRafterTrusses.ForEach(e => e.PriceFlangeBeam = ValAction.GetObjectByName<Flange>(flanges, t => t.Name, e.NameFlangeBeam).Price);
        mountUnitBeamRafterTrusses.ForEach(e => e.PriceFlangeRafter = ValAction.GetObjectByName<Flange>(flanges, t => t.Name, e.NameFlangeRafter).Price);
        mountUnitBeamRafterTrusses.ForEach(e => e.PriceKgScrew = ValAction.GetObjectByName<Fixing>(fixings, t => t.Name, e.NameScrew).PricePerKg);
        mountUnitBeamRafterTrusses.ForEach(e => e.PriceKgNut = ValAction.GetObjectByName<Fixing>(fixings, t => t.Name, e.NameNut).PricePerKg);
        mountUnitBeamRafterTrusses.ForEach(e => e.PriceKgWasher = ValAction.GetObjectByName<Fixing>(fixings, t => t.Name, e.NameWasher).PricePerKg);
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
        pricePerMcolumn = ValAction.GetPricePlayerPrefs(Canopy.ColumnBodyHigh.Profile.Name);
        pricePerMbeamTruss = ValAction.GetPricePlayerPrefs(Canopy.BeamTruss.Truss.Name);
        pricePerMrafterTruss = ValAction.GetPricePlayerPrefs(Canopy.RafterTruss.Truss.Name);
        pricePerMgirder = ValAction.GetPricePlayerPrefs(Canopy.Girder.Profile.Name);
        dollarRateValue = ValAction.GetDollarRatePlayerPrefs();

        mountUnitColumnBeamTrusses.ForEach(e => e.PriceFlangeColumn = ValAction.GetPricePlayerPrefs(e.NameFlangeColumn));
        mountUnitColumnBeamTrusses.ForEach(e => e.PriceFlangeBeam = ValAction.GetPricePlayerPrefs(e.NameFlangeBeam));
        mountUnitColumnBeamTrusses.ForEach(e => e.PriceKgScrew = ValAction.GetPricePlayerPrefs(e.NameScrew));
        mountUnitColumnBeamTrusses.ForEach(e => e.PriceKgNut = ValAction.GetPricePlayerPrefs(e.NameNut));
        mountUnitColumnBeamTrusses.ForEach(e => e.PriceKgWasher = ValAction.GetPricePlayerPrefs(e.NameWasher));
        mountUnitBeamRafterTrusses.ForEach(e => e.PriceFlangeBeam = ValAction.GetPricePlayerPrefs(e.NameFlangeBeam));
        mountUnitBeamRafterTrusses.ForEach(e => e.PriceFlangeRafter = ValAction.GetPricePlayerPrefs(e.NameFlangeRafter));
        mountUnitBeamRafterTrusses.ForEach(e => e.PriceKgScrew = ValAction.GetPricePlayerPrefs(e.NameScrew));
        mountUnitBeamRafterTrusses.ForEach(e => e.PriceKgNut = ValAction.GetPricePlayerPrefs(e.NameNut));
        mountUnitBeamRafterTrusses.ForEach(e => e.PriceKgWasher = ValAction.GetPricePlayerPrefs(e.NameWasher));

#endif
        float priceFlangeColumnMucbt = mountUnitColumnBeamTrusses.Find(e => e.MountUnitName == Canopy.MountUnitColumnBeamTruss.MountUnitName).PriceFlangeColumn;
        float priceFlangeBeamTrussMucbt = mountUnitColumnBeamTrusses.Find(e => e.MountUnitName == Canopy.MountUnitColumnBeamTruss.MountUnitName).PriceFlangeBeam;
        float priceFlangeBeamTrussMubrt = mountUnitBeamRafterTrusses.Find(e => e.MountUnitName == Canopy.MountUnitBeamRafterTruss.MountUnitName).PriceFlangeBeam;
        float priceFlangeRafterTrussMubrt = mountUnitBeamRafterTrusses.Find(e => e.MountUnitName == Canopy.MountUnitBeamRafterTruss.MountUnitName).PriceFlangeRafter;
        float priceScrew = quantityScrew * mountUnitColumnBeamTrusses.Find(e => e.MountUnitName == Canopy.MountUnitColumnBeamTruss.MountUnitName).PriceUnitScrew;
        float priceNut = quantityNut * mountUnitColumnBeamTrusses.Find(e => e.MountUnitName == Canopy.MountUnitColumnBeamTruss.MountUnitName).PriceUnitNut; ;
        float priceWasher = quantityWasher * mountUnitColumnBeamTrusses.Find(e => e.MountUnitName == Canopy.MountUnitColumnBeamTruss.MountUnitName).PriceUnitWasher; ;

        print("pricePerMcolumn:" + pricePerMcolumn);
        print("pricePerMbeamTruss:" + pricePerMbeamTruss);
        print("pricePerMrafterTruss:" + pricePerMrafterTruss);
        print("pricePerMgirder:" + pricePerMgirder);
        print("dollarRate:" + dollarRate);

        int costColumns = Mathf.RoundToInt(pricePerMcolumn * columnMaterialLength * dollarRateValue);
        int costBeamTrusses = Mathf.RoundToInt(pricePerMbeamTruss * beamTrussMaterialLength * dollarRateValue);
        int costRafterTrusses = Mathf.RoundToInt(pricePerMrafterTruss * rafterTrussMaterialLength * dollarRateValue);
        int costGirders = Mathf.RoundToInt(pricePerMgirder * girderMaterialLength * dollarRateValue);
        int costFlangeColumnMucbt = Mathf.RoundToInt(priceFlangeColumnMucbt * quantityFlangeColumnMucbt * dollarRateValue);
        int costFlangeBeamTrussMucbt = Mathf.RoundToInt(priceFlangeBeamTrussMucbt * quantityFlangeBeamTrussMucbt * dollarRateValue);
        int costFlangeBeamTrussMubrt = Mathf.RoundToInt(priceFlangeBeamTrussMubrt * quantityFlangeBeamTrussMubrt * dollarRateValue);
        int costFlangeRafterTrussMubrt = Mathf.RoundToInt(priceFlangeRafterTrussMubrt * quantityFlangeRafterTrussMubrt * dollarRateValue);
        int costScrews = Mathf.RoundToInt(priceScrew * quantityScrew * dollarRateValue);   
        int costNuts = Mathf.RoundToInt(priceNut * quantityNut * dollarRateValue);
        int costWashers = Mathf.RoundToInt(priceWasher * quantityWasher * dollarRateValue);
        float countMaterialUnitFlangeColumnMucbt = Canopy.MountUnitColumnBeamTruss.LengthFlangeColumn * Canopy.MountUnitColumnBeamTruss.WidthFlangeColumn / 1000000f;
        float countMaterialUnitFlangeBeamTrussMucbt = Canopy.MountUnitColumnBeamTruss.LengthFlangeBeamTruss * Canopy.MountUnitColumnBeamTruss.WidthFlangeBeamTruss / 1000000f;
        float countMaterialUnitFlangeBeamTrussMubrt = Canopy.MountUnitBeamRafterTruss.LengthFlangeBeamTruss * Canopy.MountUnitBeamRafterTruss.WidthFlangeBeamTruss / 1000000f;
        float countMaterialUnitFlangeRafterTrussMubrt = Canopy.MountUnitBeamRafterTruss.LengthFlangeRafterTruss * Canopy.MountUnitBeamRafterTruss.WidthFlangeRafterTruss / 1000000f;
        float countMaterialFlangeColumnMucbt = MathF.Round(countMaterialUnitFlangeColumnMucbt * quantityFlangeColumnMucbt, 3);
        float countMaterialFlangeBeamTrussMucbt = MathF.Round(countMaterialUnitFlangeBeamTrussMucbt * quantityFlangeBeamTrussMucbt, 3);
        float countMaterialFlangeBeamTrussMubrt = MathF.Round(countMaterialUnitFlangeBeamTrussMubrt * quantityFlangeBeamTrussMubrt, 3);
        float countMaterialFlangeRafterTrussMubrt = MathF.Round(countMaterialUnitFlangeRafterTrussMubrt * quantityFlangeRafterTrussMubrt, 3);

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
            NameFlangeColumnMucbt = Canopy.MountUnitColumnBeamTruss.NameFlangeColumn,
            NameFlangeBeamTrussMucbt = Canopy.MountUnitColumnBeamTruss.NameFlangeBeam,
            NameFlangeBeamTrussMubrt = Canopy.MountUnitBeamRafterTruss.NameFlangeBeam,
            NameFlangeRafterTrussMubrt = Canopy.MountUnitBeamRafterTruss.NameFlangeRafter,
            QuantityFlangeColumnMucbt = quantityFlangeColumnMucbt,
            QuantityFlangeBeamTrussMucbt = quantityFlangeBeamTrussMucbt,
            QuantityFlangeBeamTrussMubrt = quantityFlangeBeamTrussMubrt,
            QuantityFlangeRafterTrussMubrt = quantityFlangeRafterTrussMubrt,
            CountMaterialFlangeColumnMucbt = countMaterialFlangeColumnMucbt,
            CountMaterialFlangeBeamTrussMucbt = countMaterialFlangeBeamTrussMucbt,
            CountMaterialFlangeBeamTrussMubrt = countMaterialFlangeBeamTrussMubrt,
            CountMaterialFlangeRafterTrussMubrt = countMaterialFlangeRafterTrussMubrt, 
            CostFlangeColumnMucbt = costFlangeColumnMucbt,
            CostFlangeBeamTrussMucbt = costFlangeBeamTrussMucbt,
            CostFlangeBeamTrussMubrt = costFlangeBeamTrussMubrt,
            CostFlangeRafterTrussMubrt = costFlangeRafterTrussMubrt,
            NameScrew = Canopy.MountUnitColumnBeamTruss.NameScrew,
            NameNut = Canopy.MountUnitColumnBeamTruss.NameNut,
            NameWasher = Canopy.MountUnitColumnBeamTruss.NameWasher,
            QuantityScrew = quantityScrew,
            QuantityNut = quantityNut,
            QuantityWasher = quantityWasher,
            CostScrews = costScrews,
            CostNuts = costNuts,
            CostWashers = costWashers,
            CostTotal = costColumns + costBeamTrusses + costRafterTrusses + costGirders
            + costFlangeColumnMucbt + costFlangeBeamTrussMucbt + costFlangeBeamTrussMubrt + costFlangeRafterTrussMubrt
            + costScrews + costNuts + costWashers
        };
        string description = Canopy.PlanColumn.IsDemountable ?
            $"\n" +
            $"Узел колонна-балка:\n\t{Canopy.ResultCalculation.NameFlangeColumnMucbt} - {Canopy.ResultCalculation.QuantityFlangeColumnMucbt} шт" +
            $"\n\t{Canopy.ResultCalculation.NameFlangeBeamTrussMucbt} - {Canopy.ResultCalculation.QuantityFlangeBeamTrussMucbt} шт" +
            $"\nУзел балка-стропило:\n\t{Canopy.ResultCalculation.NameFlangeBeamTrussMubrt} - {Canopy.ResultCalculation.QuantityFlangeBeamTrussMubrt} шт" +
            $"\n\t{Canopy.ResultCalculation.NameFlangeRafterTrussMubrt} - {Canopy.ResultCalculation.QuantityFlangeRafterTrussMubrt} шт" +
            $"\nСт-ть листового металла: {Canopy.ResultCalculation.CostFlangeColumnMucbt + Canopy.ResultCalculation.CostFlangeBeamTrussMucbt + Canopy.ResultCalculation.CostFlangeBeamTrussMubrt + Canopy.ResultCalculation.CostFlangeRafterTrussMubrt} сом" +
            $"\n" +
            $"Метизы:\n\t{Canopy.ResultCalculation.NameScrew} - {Canopy.ResultCalculation.QuantityScrew} шт" +
            $"\n\t{Canopy.ResultCalculation.NameNut} - {Canopy.ResultCalculation.QuantityNut} шт" +
            $"\n\t{Canopy.ResultCalculation.NameWasher} - {Canopy.ResultCalculation.QuantityWasher} шт" +
            $"\nСт-ть метизов: {Canopy.ResultCalculation.CostScrews + Canopy.ResultCalculation.CostNuts + Canopy.ResultCalculation.CostWashers} сом"
            : string.Empty;

        CanopyDescription.GetComponent<TMP_Text>().text = $"Колонна большей высоты:\n\t{Canopy.ResultCalculation.NameColumn}, L={Canopy.ResultCalculation.LengthHighColumn} - {Canopy.ResultCalculation.QuantityInRowColumn} шт" +
            $"\nКолонна малой высоты:\n\t{Canopy.ColumnBodyHigh.Profile.Name}, L={Canopy.ResultCalculation.LengthLowColumn} - {Canopy.ResultCalculation.QuantityInRowColumn} шт" +
            $"\nКол-во мат-ла на колонны: {Canopy.ResultCalculation.QuantityMaterialColumn} м" +
            $"\nСт-ть мат-ла на колонны: {Canopy.ResultCalculation.CostColumns} сом" +
            $"\n" +
            $"Балочная ферма:\n\t{Canopy.ResultCalculation.NameBeamTruss}, L={Canopy.ResultCalculation.LengthBeamTruss} - {Canopy.ResultCalculation.QuantityBeamTruss} шт" +
            $"\nТреб. момент сопр. - {Canopy.ResultCalculation.MomentResistReqBeamTruss} см3" +
            $"\nФакт. прогиб - {Canopy.ResultCalculation.DeflectionFactBeamTruss} см" +
            $" (Доп. прогиб - {Canopy.ResultCalculation.DeflectionPermissibleBeamTruss} см)" +
            $"\nСт-ть балочных ферм: {Canopy.ResultCalculation.CostBeamTrusses} сом" +
            $"\n" +
            $"Стропильная ферма:\n\t{Canopy.ResultCalculation.NameRafterTruss}, L={Canopy.ResultCalculation.LengthRafterTruss} - {Canopy.ResultCalculation.QuantityRafterTruss} шт" +
            $"\n\tПодобранный шаг - {Canopy.ResultCalculation.StepRafterTruss} см" +
            $"\nТреб. момент сопр. - {Canopy.ResultCalculation.MomentResistReqRafterTruss} см3" +
            $"\nФакт. прогиб - {Canopy.ResultCalculation.DeflectionFactRafterTruss} см" +
            $" (Доп. прогиб - {Canopy.ResultCalculation.DeflectionPermissibleRafterTruss} см)" +
            $"\nСт-ть стропильных ферм: {Canopy.ResultCalculation.CostRafterTrusses} сом" +
            $"\n" +
            $"Прогон:\n\t{Canopy.ResultCalculation.NameGirder}, L={Canopy.ResultCalculation.LengthGirder} - {Canopy.ResultCalculation.QuantityGirder} шт" +
            $"\n\tПодобранный шаг - {Canopy.ResultCalculation.StepGirder} см" +
            $"\nФакт. прогиб - {Canopy.ResultCalculation.DeflectionFactGirder} см" +
            $" (Доп. прогиб - {Canopy.ResultCalculation.DeflectionPermissibleGirder} см)" +
            $"\nКол-во мат-ла на прогоны: {Canopy.ResultCalculation.QuantityMaterialGirder} м" +
            $"\nСт-ть мат-ла на прогоны: {Canopy.ResultCalculation.CostGirders} сом" + description +
            $"\nИтого: {Canopy.ResultCalculation.CostTotal} сом";
        Canopy.CanopyDescription = CanopyDescription.GetComponent<TMP_Text>().text;
 
        yield return null;
    }
}
    