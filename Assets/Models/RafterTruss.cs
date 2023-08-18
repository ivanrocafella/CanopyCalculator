using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public class RafterTruss : Truss
    {
        private PlanColumn PlanColumn { get; set; } = new();
        public float LengthTop { get { return (float)Math.Round(PlanColumn.SizeByX / (float)Math.Cos(PlanColumn.Slope) + 200, 0, MidpointRounding.AwayFromZero); } }
        public float LengthBottom { get { return LengthTop - 400; } }
        public float Tail { get; set; } = 200;
        public float PlaceOneCrateStandart { get { return (float)Math.Cos(AngleCrate) * LengthCrate; } }
        public int CountCratesStandart { get { return (int)Math.Floor((LengthTop - 2 * Tail - (PlaceOneCrateStandart + Gap)) / (PlaceOneCrateStandart + Gap)); } }
        public float PieceMidToExter
        {
            get
            {
                return (ProfileCrate.Width / 2 - (ProfileBelt.Width / 2) * (float)Math.Cos(AngleCrate))/ (float)Math.Sin(AngleCrate);
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
        public float PlaceOneCrateNonStandart { get {
                if (CountCratesStandart % 2 != 0) return (PlaceAllNonStandartCrates - 2 * GapExter) / 2 ;
                else
                    return PlaceAllNonStandartCrates - GapExter;
            } }
        public float AngleNonStandartCrate { get { return (float)(Math.Atan((Height - 2 * ProfileBelt.Width) / PlaceOneCrateNonStandart) * 180 / Math.PI); } } // degrees
        public float LengthNonStandartCrate { get { return PlaceOneCrateNonStandart / (float)Math.Cos(Math.PI / 180 * AngleNonStandartCrate); } } 

    }
}
