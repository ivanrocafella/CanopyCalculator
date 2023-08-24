using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public class Material
    {
        public string Name { get; set; } = "Труба 20x20x1.2";
        public float Height { get; set; } = 20;
        public float Length { get; set; } = 20;
        public float Thickness { get; set; } = 1.2f;
        public float Radius { get; set; } = 2.4f;
    }
}
