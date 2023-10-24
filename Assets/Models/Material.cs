using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public class Material
    {
        public string Name { get; set; }
        public float YieldStrength { get; set; } // u.m. = kg/sm2
        public float TensileStrength { get; set; } // u.m. = kg/sm2
        public float ElastiModulus { get; set; } // u.m. = kg/sm2
    }
}
