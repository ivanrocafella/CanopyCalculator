using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Models
{
    public class PlanColumn
    {
        public int SizeByX { get; set; } = 4350; // u.m. = mm
        public int SizeByZ { get; set; } = 9000; // u.m. = mm
        public int SizeByY { get; set; } = 3000; // u.m. = mm
        public int SlopeInDegree { get; set; } = 6; // u.m. = degree
        public float Slope { get { return Mathf.Deg2Rad * SlopeInDegree; } } // u.m. = rad
        public int CountStep { get; set; } = 5; // u.m. = 1
        public float Step { get { return SizeByZ / CountStep; } } // u.m. = mm
    }
}
