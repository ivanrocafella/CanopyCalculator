using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    [Serializable]
    public class MountUnitColumnBeamTruss
    {
        public string MountUnitName { get; set; }
        public string BeamTrussName { get; set; }
        public int SizeProfileBeamTruss { get; set; }
        public int WidthFlangeColumn { get; set; }
        public int LengthFlangeColumn { get; set; }
        public int ThicknessFlangeColumn { get; set; }
        public int WidthFlangeBeamTruss { get; set; }
        public int LengthFlangeBeamTruss { get; set; }
        public int ThicknessFlangeBeamTruss { get; set; }
        public float ThicknessHeadScrew { get; set; }
        public float ThicknessHeadNut { get; set; }
        public float ThicknessWasher { get; set; }
        public float CenterCenterDistance { get; set; }
        public string NameFlangeColumn { get; set; }
        public float PriceFlangeColumn { get; set; }
        public string NameFlangeBeam { get; set; }
        public float PriceFlangeBeam { get; set; }
        public string NameScrew { get; set; }
        public float WeightUnitScrew { get; set; } 
        public float PriceKgScrew { get; set; }
        public float PriceUnitScrew => WeightUnitScrew * PriceKgScrew;
        public string NameNut { get; set; }
        public float WeightUnitNut { get; set; }
        public float PriceKgNut { get; set; }
        public float PriceUnitNut => WeightUnitNut * PriceKgNut;
        public string NameWasher { get; set; }
        public float WeightUnitWasher { get; set; }
        public float PriceKgWasher { get; set; }
        public float PriceUnitWasher => WeightUnitWasher * PriceKgWasher;
    }
}
