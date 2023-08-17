using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public class PlanColumn
    {
        public int SizeByX { get; set; } = 4500; // u.m. = mm
        public int SizeByZ { get; set; } = 9000; // u.m. = mm
        public int SizeByY { get; set; } = 3000; // u.m. = mm
        public int SlopeInDegree { get; set; } = 6; // u.m. = degree
        public double Slope { get { return (Math.PI / 180) * SlopeInDegree; } } // u.m. = rad
        public int CountStep { get; set; } = 5; // u.m. = 1
        public float Step { get { return (float)SizeByZ / CountStep; } } // u.m. = mm
    }
}
