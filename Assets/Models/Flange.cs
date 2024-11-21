using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public class Flange : ObjectWithName
    {
        public int Width { get; set; }
        public int Length { get; set; }
        public float CenterCenterDistance { get; set; }
        public float Price { get; set; } // u.m. = $
    }
}
