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
        public float GapHalf { get { return Gap / 2; } }
        public float LengthCrate { get; set; }
        public float AngleCrateInDegree { get; set; } // u.m. = degree
        public float AngleCrate { get { return Mathf.Deg2Rad * AngleCrateInDegree; } } // u.m. = rad
        public ProfilePipe ProfileBelt { get; set; } = new ProfilePipe();
        public ProfilePipe ProfileCrate { get; set; } = new ProfilePipe();
    }
}
