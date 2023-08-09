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
    }
}
