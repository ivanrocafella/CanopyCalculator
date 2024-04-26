using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public class ResultCalculation
    {
        public string NameColumn { get; set; }
        public int LengthHighColumn { get; set; } // u.m = mm
        public int QuantityInRowColumn { get; set; } // u.m = 1
        public int LengthLowColumn { get; set; } // u.m = mm
        public int QuantityLowColumn { get; set; } // u.m = 1
        public double QuantityMaterialColumn { get; set; } // u.m = m
        public int CostColumns { get; set; } // u.m = som
    }
}
