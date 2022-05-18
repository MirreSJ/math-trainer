using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pythagoras
{
    public class RightAngledTriangleFactory
    {
        private static Random random = new Random();

        public RightAngledTriangle Create()
        {
            var result = new RightAngledTriangle();
            result.AddProperty(Properties.a, GetRandomCathetusLength(0.5, 20));
            result.AddProperty(Properties.b, GetRandomCathetusLength(0.5, 20));
            result.AddProperty(Properties.c, CalculateHypotenuse(result.Properties[Properties.a], result.Properties[Properties.b]));
            result.AddProperty(Properties.A, CalculateArea(result.Properties[Properties.a], result.Properties[Properties.b]));
            result.AddProperty(Properties.h, CalculateHeight(result.Properties[Properties.A], result.Properties[Properties.c]));
            result.AddProperty(Properties.p, CalculateCathetus(result.Properties[Properties.a], result.Properties[Properties.h]));
            result.AddProperty(Properties.q, CalculateCathetus(result.Properties[Properties.b], result.Properties[Properties.h]));
            return result;
        }

        private float CalculateCathetus(float hypotenuse, float cathetus)
        {
            return (float)Math.Sqrt(Math.Pow(hypotenuse, 2) - Math.Pow(cathetus, 2));
        }

        private float CalculateHeight(float A, float c)
        {
            return (float)(2*A/c);
        }

        private float CalculateArea(float a, float b)
        {
            return (float)(a*b/2.0);
        }

        private float CalculateHypotenuse(float a, float b)
        {
            return (float)Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
        }

        private float GetRandomCathetusLength(double minimum, double maximum)
        {
            return (float)(random.NextDouble() * (maximum - minimum) + minimum);
        }
    }
}
