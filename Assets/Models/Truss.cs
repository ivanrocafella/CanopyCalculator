using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public class Truss
    {
        public float Height { get; set; } = 300;
        public float GapHalf { get; set; } = 8.69262222f;
        public float LengthCrate { get; set; } = 381.75f;
        public float AngleCrate { get; set; } = 45.06955012f;
        public Material ProfileBelt { get; set; } = new Material();
        public Material ProfileCrate { get; set; } = new Material();
        public Truss()
        {
            ProfileBelt.Length = 30;
            ProfileBelt.Width = 30;
            ProfileBelt.Thickness = 1.8f;
            ProfileBelt.Radius = 2 * ProfileBelt.Thickness;
            ProfileCrate.Length = 25;
            ProfileCrate.Width = 25;
            ProfileCrate.Thickness = 1.2f;
            ProfileCrate.Radius = 2 * ProfileBelt.Thickness;
        }
    }
}
