using Assets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Services
{
    public static class CalculationDeflection
    {
        public static float GetDeflectionPermissible(float length)
        {
            float deflectionPermissible;
            if (length <= 3)
                deflectionPermissible = (length / 150) * 100;
            else deflectionPermissible = length > 3 && length <= 12 ? (length / 200) * 100 : (length / 250) * 100;
            return deflectionPermissible;
        }

        public static float GetDeflectionFact(float length, float forceLinear, float elasticModulus, float momentInertia) =>
            5 * (forceLinear / 100) * MathF.Pow(length * 100, 4) / (384 * elasticModulus * momentInertia);
    }
}
