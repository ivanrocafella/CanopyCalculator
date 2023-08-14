using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public class Truss
    {
        public float Height { get; set; }
        public Material ProfileBelt { get; set; } = new Material();
        public Material ProfileCrate { get; set; } = new Material();
        public Truss()
        {
            ProfileBelt.Length = 20;
            ProfileBelt.Width = 20;
            ProfileBelt.Thickness = 1.2f;
            ProfileBelt.Radius = 2 * ProfileBelt.Thickness;
            ProfileCrate.Length = 15;
            ProfileCrate.Width = 15;
            ProfileCrate.Thickness = 1.2f;
            ProfileCrate.Radius = 2 * ProfileBelt.Thickness;
        }
    }
}
