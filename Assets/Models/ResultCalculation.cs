namespace Assets.Models
{
    public class ResultCalculation
    {
        public string NameColumn { get; set; }
        public int LengthHighColumn { get; set; } // u.m = mm
        public int QuantityInRowColumn { get; set; } // u.m = 1
        public int LengthLowColumn { get; set; } // u.m = mm
        public float QuantityMaterialColumn { get; set; } // u.m = m
        public float CostColumns { get; set; } // u.m = som
        public string NameBeamTruss { get; set; }
        public int LengthBeamTruss { get; set; } // u.m = mm
        public int QuantityBeamTruss { get; set; } // u.m = 1
        public float QuantityMaterialBeamTruss { get; set; } // u.m = m
        public float MomentResistReqBeamTruss { get; set; } // u.m = sm3
        public float DeflectionFactBeamTruss { get; set; } // u.m = sm
        public float DeflectionPermissibleBeamTruss { get; set; } // u.m = sm
        public float CostBeamTrusses { get; set; } // u.m = som
        public string NameRafterTruss { get; set; }
        public int LengthRafterTruss { get; set; } // u.m = mm
        public int QuantityRafterTruss { get; set; } // u.m = 1
        public float QuantityMaterialRafterTruss { get; set; } // u.m = m
        public int StepRafterTruss { get; set; } // u.m = sm
        public float MomentResistReqRafterTruss { get; set; } // u.m = sm3
        public float DeflectionFactRafterTruss { get; set; } // u.m = sm
        public float DeflectionPermissibleRafterTruss { get; set; } // u.m = sm
        public float CostRafterTrusses { get; set; } // u.m = som
        public string NameGirder { get; set; }
        public int LengthGirder { get; set; } // u.m = mm
        public int QuantityGirder { get; set; } // u.m = 1
        public float QuantityMaterialGirder { get; set; } // u.m = m
        public int StepGirder { get; set; } // u.m = sm
        public float DeflectionFactGirder { get; set; } // u.m = sm
        public float DeflectionPermissibleGirder { get; set; } // u.m = sm
        public float CostGirders { get; set; } // u.m = som
        public string NameFlangeColumnMucbt { get; set; }
        public string NameFlangeBeamTrussMucbt { get; set; } 
        public string NameFlangeBeamTrussMubrt { get; set; } 
        public string NameFlangeRafterTrussMubrt { get; set; } 
        public int QuantityFlangeColumnMucbt { get; set; } // u.m = 1
        public int QuantityFlangeBeamTrussMucbt { get; set; } // u.m = 1
        public int QuantityFlangeBeamTrussMubrt { get; set; } // u.m = 1
        public int QuantityFlangeRafterTrussMubrt { get; set; } // u.m = 1
        public float CountMaterialFlangeColumnMucbt { get; set; } // u.m = m2
        public float CountMaterialFlangeBeamTrussMucbt { get; set; } // u.m = m2
        public float CountMaterialFlangeBeamTrussMubrt { get; set; } // u.m = m2
        public float CountMaterialFlangeRafterTrussMubrt { get; set; } // u.m = m2
        public float CostFlangeColumnMucbt { get; set; } // u.m = som
        public float CostFlangeBeamTrussMucbt { get; set; } // u.m = som
        public float CostFlangeBeamTrussMubrt { get; set; } // u.m = som
        public float CostFlangeRafterTrussMubrt { get; set; } // u.m = som
        public string NameScrew { get; set; } 
        public string NameNut { get; set; }
        public string NameWasher { get; set; }
        public int QuantityScrew { get; set; } // u.m = 1
        public int QuantityNut { get; set; } // u.m = 1
        public int QuantityWasher { get; set; } // u.m = 1
        public float CostScrews { get; set; } // u.m = som
        public float CostNuts { get; set; } // u.m = som
        public float CostWashers { get; set; } // u.m = som
        public float CostTotal { get; set; } // u.m = som
    }
}
    