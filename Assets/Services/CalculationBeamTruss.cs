using Assets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Material = Assets.Models.Material;

namespace Assets.Services
{
    public static class CalculationBeamTruss
    {
        public static float DeflectionPermissible { get; set; } // u.m. = sm
        public static float DeflectionFact { get; set; } // u.m. = sm
        public static Truss CalculateBeamTruss(float segmentBySlope, float segmentByLength, int countStep, float cargo,  Material material, List<Truss> trusses)
        {
            float length = segmentByLength / (countStep * MathF.Pow(10, 3)); // u.m. = m
            float forceLinear = CalculateForceLinear(segmentBySlope, cargo); // u.m. = kg/m
            float momentBendMax = forceLinear * MathF.Pow(length, 2) / 8; // u.m. = kg*m
            float momentResistReq = (momentBendMax * 100) / material.YieldStrength; // u.m. = sm3
            Truss truss = trusses.FirstOrDefault(e => e.MomentResistance > momentResistReq);
            if (truss == null)
                return null;
            float comparer;
            int i = trusses.IndexOf(truss);
            do
            {
                if (i == trusses.Count)
                    return null;
                truss = trusses[i];
                comparer = (momentBendMax * 100) / (truss.MomentResistance * material.YieldStrength);
                i++;
            } while (comparer > 1);
            DeflectionPermissible = CalculationDeflection.GetDeflectionPermissible(length);
            DeflectionFact = CalculationDeflection.GetDeflectionFact(length, forceLinear, material.ElastiModulus, truss.MomentInertia);
            return truss;
        }

        private static float CalculateForceLinear(float segmentBySlope, float cargo) => cargo * segmentBySlope / ( 2 * MathF.Pow(10, 3)); // u.m. = kg/m
    }
}
