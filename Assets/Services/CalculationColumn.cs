using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Services
{
    public static class CalculationColumn
    {
        public static float Calculate(int segmentBySlope, int segmentByLength, int countStep)
        {
            float lengthStep = countStep > 1 ? (float)segmentByLength / countStep : (float)segmentByLength / 2;
            float areaCargo = ((float)segmentBySlope / 2) * lengthStep;
            return areaCargo / MathF.Pow(10,6); // u.m. = m
        }
    }
}
