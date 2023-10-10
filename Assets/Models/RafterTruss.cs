using Assets.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Models
{
    public class RafterTruss
    {
        private PlanColumn PlanColumn { get; set; }
        public Truss Truss { get; set; }
        public float Step { get; set; }
        public float LengthTop { get; private set; }
        public float LengthBottom { get; private set; }
        public float Tail { get; private set; } 
        public float PlaceOneCrateStandart { get => Mathf.Cos(Truss.AngleCrate) * Truss.LengthCrate; }
        public int CountCratesStandart { get => (int)Math.Floor((LengthTop - 2 * Tail - (PlaceOneCrateStandart + Truss.Gap) / 2) / (PlaceOneCrateStandart + Truss.Gap)); }
        public float PieceMidToExter { get => (Truss.ProfileCrate.Height / 2 - (Truss.ProfileBelt.Height / 2) * Mathf.Cos(Truss.AngleCrate)) / Mathf.Sin(Truss.AngleCrate); }
        public float PlaceAllStandartCrates { get  => CountCratesStandart * (PlaceOneCrateStandart + Truss.Gap) - Truss.Gap + 2 * PieceMidToExter; }
        public float PlaceAllNonStandartCrates { get => LengthTop - PlaceAllStandartCrates - 2 * Tail; }
        public bool HasTwoNonStandartCrate { get => CountCratesStandart % 2 == 0; }
        public float DimenOneCrateNonStandart { get => HasTwoNonStandartCrate ? (PlaceAllNonStandartCrates - 2 * Truss.GapExter) / 2 : PlaceAllNonStandartCrates - Truss.GapExter; }             
        public float AngleDiagonalNonStandartCrate { get => (Mathf.Atan((Truss.Height - 2 * Truss.ProfileBelt.Height) / DimenOneCrateNonStandart) * Mathf.Rad2Deg); } // degrees
        public float LengthDiagonalNonStandartCrate { get => DimenOneCrateNonStandart / Mathf.Cos(Mathf.Deg2Rad * AngleDiagonalNonStandartCrate); }
        public float AngleNonStandartCrate { get => AngleDiagonalNonStandartCrate + Mathf.Asin(Truss.ProfileCrate.Height / LengthDiagonalNonStandartCrate) * Mathf.Rad2Deg; } // degrees
        public float LengthNonStandartCrate { get => (Truss.Height - Truss.ProfileBelt.Height) / Mathf.Sin(AngleNonStandartCrate * Mathf.Deg2Rad); }
        public float PerspectWidthHalfNonStandartCrate { get => (Truss.ProfileCrate.Height / Mathf.Sin(AngleNonStandartCrate * Mathf.Deg2Rad) - Truss.ProfileBelt.Height / Mathf.Tan(AngleNonStandartCrate * Mathf.Deg2Rad)) / 2; }

        public RafterTruss(string nameTruss, string path, PlanColumn planColumn, float dimensionColumn)
        {
            PlanColumn = planColumn;
            Truss = FileAction<Truss>.ReadAndDeserialyze(path).Find(e => e.Name == nameTruss);
            Tail = (PlanColumn.OutputRafter + dimensionColumn / 2) / Mathf.Cos(PlanColumn.Slope);
            LengthTop = (PlanColumn.SizeByX + PlanColumn.OutputRafter * 2) / Mathf.Cos(PlanColumn.Slope);
            LengthBottom = LengthTop - PlanColumn.OutputRafter * 2 / Mathf.Cos(PlanColumn.Slope);
        }
    }
}
