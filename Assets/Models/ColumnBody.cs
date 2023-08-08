using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public class ColumnBody
    {
        public float Height { get; set; } = 4000;
        public Material Material { get; set; } = new();
        public ColumnBody() {
            Material.Length = 80;
            Material.Width = 80;
            Material.Thickness = 4;
            Material.Radius = 8;
        }
    }
}
