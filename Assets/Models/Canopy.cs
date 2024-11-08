using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Models
{
    [Serializable]
    public class Canopy
    {
        public PlanCanopy PlanColumn;
        public GameObject[] ColumnsHigh;
        public GameObject[] ColumnsLow;
        public GameObject[] BeamTrussesOnHigh;
        public GameObject[] BeamTrussesOnLow;
        public GameObject[] RafterTrusses;
        public GameObject[] Girders;
        public GameObject[] MountUnitsColumnBeamTrussOnHC;
        public GameObject[] MountUnitsColumnBeamTrussOnLC;
        public GameObject[] MountUnitsBeamRafterTrussOnHC;
        public GameObject[] MountUnitsBeamRafterTrussOnLC;
        public ColumnBody ColumnBodyHigh;
        public BeamTruss BeamTruss;
        public RafterTruss RafterTruss;
        public Girder Girder;
        public ColumnPlug ColumnPlug = new();
        public int CountStepRafterTruss;
        public int CountStepGirder;
        public string CanopyDescription;
        public ResultCalculation ResultCalculation;
        public MountUnitBeamRafterTruss MountUnitBeamRafterTruss;
        public MountUnitColumnBeamTruss MountUnitColumnBeamTruss;
    }
}
