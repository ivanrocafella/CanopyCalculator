using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public class ColumnBody
    {
        public float Height { get; set; } = 3000f;
        public Material Material { get; set; } = new Material();
        public ColumnBody() {
            Material.Length = 80;
            Material.Width = 80;
            Material.Thickness = 5;
            Material.Radius = 10;
        }
    }
}
