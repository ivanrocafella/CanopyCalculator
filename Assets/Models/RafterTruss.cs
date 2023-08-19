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
        public float LengthTop { get { return (float)Math.Round(PlanColumn.SizeByX / Mathf.Cos(PlanColumn.Slope) + 200, 0, MidpointRounding.AwayFromZero); } }
        public float LengthBottom { get { return LengthTop - 400; } }
        public float Tail { get; set; } = 200;
        public float PlaceOneCrateStandart { get { return Mathf.Cos(AngleCrate) * LengthCrate; } }
        public int CountCratesStandart { get { return (int)Math.Floor((LengthTop - 2 * Tail - (PlaceOneCrateStandart + Gap)) / (PlaceOneCrateStandart + Gap)); } }
        public float PieceMidToExter
        {
            get
            {
                return (ProfileCrate.Width / 2 - (ProfileBelt.Width / 2) * Mathf.Cos(AngleCrate)) / Mathf.Sin(AngleCrate);
            }
        }
        public float PlaceAllStandartCrates
        {
            get
            {
                return CountCratesStandart * (PlaceOneCrateStandart + Gap) + PlaceOneCrateStandart + PieceMidToExter;
            }
        }
        public float PlaceAllNonStandartCrates
        {
            get
            {
                return LengthTop - PlaceAllStandartCrates - 2 * Tail;
            }
        }
        public float DimenOneCrateNonStandart { get {
                if (CountCratesStandart % 2 != 0) return (PlaceAllNonStandartCrates - 2 * GapExter) / 2 ;
                else
                    return PlaceAllNonStandartCrates - GapExter;
            } }
        public float AngleDiagonalNonStandartCrate { get { return (Mathf.Atan((Height - 2 * ProfileBelt.Width) / DimenOneCrateNonStandart) * Mathf.Rad2Deg); } } // degrees
        public float LengthDiagonalNonStandartCrate { get { return DimenOneCrateNonStandart / Mathf.Cos(Mathf.Deg2Rad * AngleDiagonalNonStandartCrate); } }
        public float AngleNonStandartCrate { get { return AngleDiagonalNonStandartCrate + Mathf.Asin(ProfileCrate.Width/LengthDiagonalNonStandartCrate) * Mathf.Rad2Deg; } } // degrees
        public float LengthNonStandartCrate { get { return (Height - ProfileBelt.Width) / Mathf.Sin(AngleNonStandartCrate * Mathf.Deg2Rad); } }
        public float PerspectWidthHalfNonStandartCrate { get { return (ProfileBelt.Width / Mathf.Sin(AngleNonStandartCrate * Mathf.Deg2Rad) - ProfileBelt.Width / Mathf.Tan(AngleNonStandartCrate * Mathf.Deg2Rad)) / 2; } }

    }
}
