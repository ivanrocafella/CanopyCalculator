using Assets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Services
{
    public static class CalculationRafterTruss
    {
        public static float DeflectionPermissible { get; set; } // u.m. = sm
        public static float DeflectionFact { get; set; } // u.m. = sm
        public static Truss CalculateRafterTruss(float segmentBySlope, float stepRafter, float outputRafter, float cargo, Material material, List<Truss> trusses)
        {
            float length = segmentBySlope / MathF.Pow(10, 3); // u.m. = m
            float console = outputRafter / MathF.Pow(10, 3); // u.m. = m
            float forceLinear = CalculateForceLinear(stepRafter, cargo); // u.m. = kg/m
            float momentBendMaxSlope = forceLinear * MathF.Pow(length, 2) / 8; // u.m. = kg*m
            float momentBendMaxConsole = forceLinear * MathF.Pow(console, 2) / 2; // u.m. = kg*m
            float momentResistReqSlope = (momentBendMaxSlope * 100) / material.YieldStrength; // u.m. = sm3
            float momentResistReqConsole = (momentBendMaxConsole * 100) / material.YieldStrength; // u.m. = sm3
            Truss truss = trusses.FirstOrDefault(e => e.MomentResistance > momentResistReqSlope && e.ProfileBelt.MomentResistance > momentResistReqConsole);
            if (truss == null)
                return null;
            float comparerSlope;
            float comparerConsole;
            int i = trusses.IndexOf(truss);
            do
            {
                if (i == trusses.Count)
                    return null;
                truss = trusses[i];
                comparerSlope = (momentBendMaxSlope * 100) / (truss.MomentResistance * material.YieldStrength);
                comparerConsole = (momentBendMaxConsole * 100) / (truss.ProfileBelt.MomentResistance * material.YieldStrength);
                i++;
            } while (comparerSlope > 1 && comparerConsole > 1);
            DeflectionPermissible = CalculationDeflection.GetDeflectionPermissible(length);
            DeflectionFact = CalculationDeflection.GetDeflectionFact(length, forceLinear, material.ElastiModulus, truss.MomentInertia);
            return truss;
        }
        private static float CalculateForceLinear(float stepRafter, float cargo) => cargo * stepRafter / (MathF.Pow(10, 3)); // u.m. = kg/m
    }
}
