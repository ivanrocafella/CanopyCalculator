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
        public ProfilePipe Profile { get; set; }
        public float Step { get; set; }
        public PlanCanopy PlanColumn { get; set; } = new();
        public Girder(ProfilePipe profilePipe, PlanCanopy planColumn) 
        {
            Profile = profilePipe;
            PlanColumn = planColumn;
            Length = planColumn.SizeByZ + 2 * PlanColumn.OutputGirder;
        }
    }
}
