using Assets.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using Material = Assets.Models.Material;

namespace Assets.Services
{
    public static class CalculationColumn
    {       
        public static ProfilePipe CalculateColumn(float segmentBySlope, float segmentByLength, float segmentByHeight, int countStep
                                            , float cargo, Material material, List<ProfilePipe> profilePipes)
        {
            float force = CalculateForce(segmentBySlope, segmentByLength, countStep, cargo); // u.m. = kg
            float areaRequiredCrossSec = force / material.YieldStrength; // u.m. = sm2
            ProfilePipe profilePipe = profilePipes.First(e => e.Area > areaRequiredCrossSec);
            float elasticity;
            float elasticityReduced;
            float coefficientFi;
            float forceCritical;
            float comparer;
            int i = profilePipes.IndexOf(profilePipe);
            do
            {
                if (i == profilePipes.Count)
                    return null;
                profilePipe = profilePipes[i];
                elasticity = (segmentByHeight / 10) / profilePipe.RadiusInertia;
                elasticityReduced = elasticity * (Mathf.Pow(material.YieldStrength / material.ElastiModulus, 0.5f));
                if (elasticityReduced <= 2.5f)
                    coefficientFi = 1 - (0.073f - 5.53f * (material.YieldStrength / material.ElastiModulus)) * elasticityReduced * MathF.Pow(elasticityReduced, 0.5f);
                else if (elasticityReduced > 2.5f && elasticityReduced <= 4.5f)
                    coefficientFi = 1.47f - 13 * (material.YieldStrength / material.ElastiModulus) - (0.371f - 27.3f * (material.YieldStrength / material.ElastiModulus)) * elasticityReduced + (0.0275f - 5.53f * (material.YieldStrength / material.ElastiModulus)) * MathF.Pow(elasticityReduced, 2);
                else
                    coefficientFi = 332 / (MathF.Pow(elasticityReduced, 2) * (51 - elasticityReduced));
                forceCritical = coefficientFi * profilePipe.Area * material.YieldStrength; // u.m. = kg
                comparer = force / (coefficientFi * profilePipe.Area * material.YieldStrength);
                i++;
            } while (comparer > 1 || elasticity > 150);
            return profilePipe; 
        }

        private static float CalculateForce(float segmentBySlope, float segmentByLength, int countStep, float cargo)
        {
            float lengthStep = countStep > 1 ? (float)segmentByLength / countStep : (float)segmentByLength / 2;
            float areaCargo = ((float)segmentBySlope / 2) * lengthStep;
            return areaCargo * cargo / MathF.Pow(10, 6); // u.m. = kg
        }
    }
}
