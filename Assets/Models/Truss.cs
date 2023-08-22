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
        public string Name { get; set; } = "ФМ 200";
        public float Height { get; set; } = 200;
        public float Gap { get; set; } = 10.31963498f;
        public float GapExter { get; set; } = 9f;
        public float GapHalf { get { return Gap / 2; } }
        public float LengthCrate { get; set; } = 253.125f;
        public float AngleCrateInDegree { get; set; } = 45.32539044f; // u.m. = degree
        public float AngleCrate { get { return Mathf.Deg2Rad * AngleCrateInDegree; } } // u.m. = rad
        public Material ProfileBelt { get; set; } = new Material();
        public Material ProfileCrate { get; set; } = new Material();
        public Truss()
        {
            ProfileBelt.Name = "Труба 20x20x1.5";
            ProfileBelt.Length = 20;
            ProfileBelt.Height = 20;
            ProfileBelt.Thickness = 1.5f;
            ProfileBelt.Radius = 2 * ProfileBelt.Thickness;

            ProfileCrate.Name = "Труба 15x15x1.2";
            ProfileCrate.Length = 15;
            ProfileCrate.Height = 15;
            ProfileCrate.Thickness = 1.2f;
            ProfileCrate.Radius = 2 * ProfileCrate.Thickness;
        }
    }
}
