using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public class Truss
    {
        public float LengthTop { get; set; }
        public float LengthBottom { get; set; }
        public float Height { get; set; }
        public Material ProfileBelt { get; set; }
        public Material ProfileCrate { get; set; }
        public Truss()
        {
            ProfileBelt.Length = 20;
            ProfileBelt.Width = 20;
            ProfileCrate.Thickness = 1.5f;
            ProfileCrate.Radius = 3;
            ProfileCrate.Length = 15;
            ProfileCrate.Width = 15;
            ProfileCrate.Thickness = 1.2f;
            ProfileCrate.Radius = 2.4f;
        }
    }
}
