using Assets.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public class Girder
    {
        public float Length { get; }
        public Material Material { get; set; }
        public float Step { get; set; }
        public PlanColumn PlanColumn { get; set; } = new();
        public Girder(string nameMaterial, string path, PlanColumn planColumn) 
        {
            Material = FileAction<Material>.ReadAndDeserialyze(path).Find(e => e.Name == nameMaterial);
            PlanColumn = planColumn;
            Length = planColumn.SizeByZ + 2 * PlanColumn.OutputGirder;
        }
    }
}
