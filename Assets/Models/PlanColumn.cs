using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public class PlanColumn
    {
        public int SizeByX { get; set; } = 6000; // u.m. = mm
        public int SizeByZ { get; set; } = 6000; // u.m. = mm
        public int Slope { get; set; } = 7; // u.m. = degree
        public int CountStep { get; set; } = 2; // u.m. = 1
        public float Step { get { return (float)SizeByZ / CountStep; } } // u.m. = mm
    }
}
