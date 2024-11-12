using Assets.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Models
{
    public class PlanCanopy
    {
        public float SizeByX { get; set; } // u.m. = mm
        public float SizeByZ { get; set; } // u.m. = mm
        public float SizeByY { get; set; } // u.m. = mm
        public float SlopeInDegree { get; set; } // u.m. = degree
        public float Slope { get { return Mathf.Deg2Rad * SlopeInDegree; } } // u.m. = rad
        public float SizeByYLow { get { return SizeByY - (float)Math.Tan(Slope) * SizeByX; } } // u.m. = mm
        public int CountStep { get; set; } // u.m. = 1
        public float Step { get { return SizeByZ / CountStep; } } // u.m. = mm
        public float OutputRafter { get; set; }
        public float OutputGirder { get; set; }
        public KindProfilePipe KindProfileColumn { get; set; }
        public KindTruss KindTrussBeam { get; set; }
        public KindTruss KindTrussRafter { get; set; }
        public float StepRafter { get; set; }
        public KindProfilePipe KindProfileGirder { get; set; }
        public float StepGirder { get; set; }
        public KindMaterial KindMaterial { get; set; }
        public KindMountUnitBeamRafterTruss KindMountUnitBeamRafterTruss { get; set; }
        public KindMountUnitColumnBeamTruss KindMountUnitColumnBeamTruss { get; set; }
        public bool IsDemountable { get; set; }
    }
}
