using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public class MountUnitBeamRafterTruss
    {
        public string MountUnitName { get; set; }
        public string RafterTrussName { get; set; }
        public int SizeProfileRafterTruss { get; set; }
        public int WidthFlangeBeamTruss { get; set; }
        public int LengthFlangeBeamTruss { get; set; }
        public int ThicknessFlangeBeamTruss { get; set  ; }
        public float ThicknessHeadScrew { get; set; }
        public float ThicknessHeadNut { get; set; }
        public float ThicknessWasher { get; set; }
        public int WidthFlangeRafterTruss { get; set; }
        public int LengthFlangeRafterTruss { get; set; }
        public int ThicknessFlangeRafterTruss { get; set; }
        public float CenterCenterDistance { get; set; }
        public float ThicknessTable { get; set; }
    }
}
