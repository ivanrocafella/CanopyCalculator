using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Models
{
    public class Fixing : ObjectWithName
    {
        public float Weight { get; set; }
        public float PricePerKg { get; set; } // u.m. = $ / kg
    }
}
