using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEngine.InputManagerEntry;

namespace Assets.Models
{
    public class ColumnBody
    {
        public KindLength KindLength { get; set; }
        public float Height { get; set; }
        public Material Material { get; set; } = new();
        public PlanColumn PlanColumn { get; set; } = new();
        public ColumnBody(KindLength kindLength) {
            KindLength = kindLength;
            Height = KindLength switch {
                KindLength.Short => (float)Math.Floor(PlanColumn.SizeByY - Math.Tan(PlanColumn.Slope) * PlanColumn.SizeByX),
                _ => PlanColumn.SizeByY
            };

            Material.Length = 80;
            Material.Width = 80;
            Material.Thickness = 4;
            Material.Radius = 8;
        }
    }
}
