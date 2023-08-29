using Assets.Utils;
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
        public Material Material { get; set; }
        public PlanColumn PlanColumn { get; set; } = new();
        public ColumnBody(string nameMaterial, string path, PlanColumn planColumn)
        {        
            Material = FileAction<Material>.ReadAndDeserialyze(path).Find(e => e.Name == nameMaterial);
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
