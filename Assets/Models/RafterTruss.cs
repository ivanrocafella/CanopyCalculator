using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Models
{
    public class RafterTruss : Truss
    {
        private PlanColumn PlanColumn { get; set; } = new();
        public float LengthTop { get => (float)Math.Round(PlanColumn.SizeByX / Mathf.Cos(PlanColumn.Slope) + 200, 0, MidpointRounding.AwayFromZero); }
        public float LengthBottom { get => LengthTop - 400; }
        public float Tail { get; set; } = 200;
        public float PlaceOneCrateStandart { get => Mathf.Cos(AngleCrate) * LengthCrate; }
        public int CountCratesStandart { get => (int)Math.Floor((LengthTop - 2 * Tail - (PlaceOneCrateStandart + Gap)) / (PlaceOneCrateStandart + Gap)) + 1; }
        public float PieceMidToExter { get => (ProfileCrate.Width / 2 - (ProfileBelt.Width / 2) * Mathf.Cos(AngleCrate)) / Mathf.Sin(AngleCrate); }
        public float PlaceAllStandartCrates { get  => CountCratesStandart * (PlaceOneCrateStandart + Gap) - Gap + PieceMidToExter; }
        public float PlaceAllNonStandartCrates { get => LengthTop - PlaceAllStandartCrates - 2 * Tail; }
        public bool HasTwoNonStandartCrate { get => CountCratesStandart % 2 == 0; }
        public float DimenOneCrateNonStandart { get => HasTwoNonStandartCrate ? (PlaceAllNonStandartCrates - 2 * GapExter) / 2 : PlaceAllNonStandartCrates - GapExter; }             
        public float AngleDiagonalNonStandartCrate { get => (Mathf.Atan((Height - 2 * ProfileBelt.Width) / DimenOneCrateNonStandart) * Mathf.Rad2Deg); } // degrees
        public float LengthDiagonalNonStandartCrate { get => DimenOneCrateNonStandart / Mathf.Cos(Mathf.Deg2Rad * AngleDiagonalNonStandartCrate); }
        public float AngleNonStandartCrate { get => AngleDiagonalNonStandartCrate + Mathf.Asin(ProfileCrate.Width / LengthDiagonalNonStandartCrate) * Mathf.Rad2Deg; } // degrees
        public float LengthNonStandartCrate { get => (Height - ProfileBelt.Width) / Mathf.Sin(AngleNonStandartCrate * Mathf.Deg2Rad); }
        public float PerspectWidthHalfNonStandartCrate { get => (ProfileCrate.Width / Mathf.Sin(AngleNonStandartCrate * Mathf.Deg2Rad) - ProfileBelt.Width / Mathf.Tan(AngleNonStandartCrate * Mathf.Deg2Rad)) / 2; }

    }
}
