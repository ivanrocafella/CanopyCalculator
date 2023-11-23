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
        public ProfilePipe Profile { get; set; }
        public ProfilePipe ProfileScr { get; set; }
        public ProfilePipeData ProfileData { get; set; }    
        public ProfilePipeData ProfilePipeData { get; set; }
        public PlanCanopy PlanColumn { get; set; } = new();
        public ColumnBody(string nameMaterial, string path, PlanCanopy planColumn, ProfilePipeDataList profilePipeDataList)
        {        
            Profile = FileAction<ProfilePipe>.ReadAndDeserialyze(path).Find(e => e.Name == nameMaterial);
            PlanColumn = planColumn;
            ProfilePipeData = profilePipeDataList.profilePipesData.ToList().Find(e => e.Name == nameMaterial);
            ProfileScr = new()
            { 
                Name = ProfilePipeData.Name,
                Height = ProfilePipeData.Height,
                Length = ProfilePipeData.Length,
                Thickness = ProfilePipeData.Thickness,
                Radius = ProfilePipeData.Radius,
                Area = ProfilePipeData.Area,
                MomentInertia = ProfilePipeData.MomentInertia,
                MomentResistance = ProfilePipeData.MomentResistance,
                WeightMeter = ProfilePipeData.WeightMeter,
                Gost = ProfilePipeData.Gost
            };
            Console.WriteLine(ProfileScr.ToString());
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
