using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Models
{
    public class Truss
    {
        public string Name { get; set; }
        public float Height { get; set; }
        public float Gap { get; set; }
        public float GapExter { get; set; }
        public float GapHalf { get => Gap / 2; }
        public float LengthCrate { get; set; }
        public float AngleCrateInDegree { get; set; } // u.m. = degree
        public float AngleCrate { get => Mathf.Deg2Rad * AngleCrateInDegree; } // u.m. = rad        
        public ProfilePipe ProfileBelt { get; set; } = new ProfilePipe();
        public ProfilePipe ProfileCrate { get; set; } = new ProfilePipe();
        public float MomentInertia { get => (float)Math.Round(2 * ProfileBelt.Area * Mathf.Pow((Height - ProfileBelt.Height) / 20, 2), 4); } // u.m. = sm4 
        public float MomentResistance { get => (float)Math.Round(MomentInertia * 2 / ((Height - ProfileBelt.Height) / 10), 4); } // u.m. = sm3
        public float PricePerM { get; set; } // u.m. = $ / m
    }
}
