using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.ModelsRequest
{
    public class ProfileUpdateModel
    {
        public string Name { get; set; }
        public float PricePerM { get; set; } // u.m. = $ / m
    }
}
