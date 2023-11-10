using Assets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Services
{
    public static class CalculationGirder
    {
        public static ProfilePipe CalculateGirder(float stepRafter, float stepGirder, float outputGirder, float cargo, Material material, List<ProfilePipe> profilePipes)
        {
            float length = stepRafter / MathF.Pow(10, 3); // u.m. = m
            float console = outputGirder / MathF.Pow(10, 3); // u.m. = m
            float forceLinear = CalculateForceLinear(stepGirder, cargo); // u.m. = kg/m
            float momentBendMaxSlope = forceLinear * MathF.Pow(length, 2) / 8; // u.m. = kg*m
            float momentBendMaxConsole = forceLinear * MathF.Pow(console, 2) / 2; // u.m. = kg*m
            float momentResistReqSlope = (momentBendMaxSlope * 100) / material.YieldStrength; // u.m. = sm3
            float momentResistReqConsole = (momentBendMaxConsole * 100) / material.YieldStrength; // u.m. = sm3
            ProfilePipe profilePipe = profilePipes.First(e => e.MomentResistance > momentResistReqSlope && e.MomentResistance > momentResistReqConsole);
            if (profilePipe == null)
                return null;
            float comparerSlope;
            float comparerConsole;
            int i = profilePipes.IndexOf(profilePipe);
            do
            {
                if (i == profilePipes.Count)
                    return null;
                profilePipe = profilePipes[i];
                comparerSlope = (momentBendMaxSlope * 100) / (profilePipe.MomentResistance * material.YieldStrength);
                comparerConsole = (momentBendMaxConsole * 100) / (profilePipe.MomentResistance * material.YieldStrength);
                i++;
            } while (comparerSlope > 1 && comparerConsole > 1);
            return profilePipe;
        }
        private static float CalculateForceLinear(float stepGirder, float cargo) => cargo * stepGirder / (MathF.Pow(10, 3)); // u.m. = kg/m
    }
}
