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
        public Material Material { get; set; } = new Material();
        public ColumnBody() {
            Material.Length = 100;
            Material.Width = 100;
            Material.Thickness = 5;
            Material.Radius = 10;
        }
    }
}
