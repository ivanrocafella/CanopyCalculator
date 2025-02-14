﻿using Assets.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Assets.Models
{
    public class ColumnBody
    {
        public KindLength KindLength { get; set; }
        public float Height { get; set; }
        public ProfilePipe Profile { get; set; }
        public PlanCanopy PlanColumn { get; set; } = new();
        public ColumnBody(ProfilePipe profilePipe, PlanCanopy planColumn)
        {        
            Profile = profilePipe;
            PlanColumn = planColumn;
        }

        public void SetHeight (KindLength kindLength) 
        {
            KindLength = kindLength;
            Height = KindLength switch
            {
                KindLength.Short => PlanColumn.SizeByYLow,
                _ => PlanColumn.SizeByY
            };
        }   
    }
}
