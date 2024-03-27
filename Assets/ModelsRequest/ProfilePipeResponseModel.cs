using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.ModelsRequest
{
    public class ProfilePipeResponseModel
    {
        public string Name { get; set; }
        public float Height { get; set; }
        public float Length { get; set; }
        public float Thickness { get; set; }
        public float Radius { get; set; }
        public float Area { get; set; } // u.m. = sm2 
        public float MomentInertia { get; set; } // u.m. = sm4 
        public float MomentResistance { get; set; } // u.m. = sm3
        public float WeightMeter { get; set; } // u.m. = kg
        public string Gost { get; set; }
        public float PricePerM { get; set; } // u.m. = $ / m
    }
}
