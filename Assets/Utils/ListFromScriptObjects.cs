using Assets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Profiling;
using static UnityEditor.Progress;

namespace Assets.Utils
{
    public static class ListFromScriptObjects
    {
        public static List<ProfilePipe> GetListProfilePipes(ProfilePipeDataList profilePipeDataList)
        {
            List<ProfilePipe> profilePipes = profilePipeDataList.profilePipesData.Select( e =>
            new ProfilePipe()
            {
                Name = e.Name,
                Height = e.Height,
                Length = e.Length,
                Thickness = e.Thickness,
                Radius = e.Radius,
                Area = e.Area,
                MomentInertia = e.MomentInertia,
                MomentResistance = e.MomentResistance,
                WeightMeter = e.WeightMeter,
                Gost = e.Gost
            }
            ).ToList();
            return profilePipes;
        }
    }
}
