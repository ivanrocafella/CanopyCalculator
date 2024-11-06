using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
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
    }
}
