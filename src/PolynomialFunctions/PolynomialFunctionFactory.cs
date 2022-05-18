using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolynomialFunctions
{
    internal class PolynomialFunctionFactory
    {
        private static Random random = new Random();

        internal IPolynomialFunction Create(int degree)
        {
            if(degree != 2)
            {
                throw new NotImplementedException($"Functions with dergree {degree} are currently not supported!");
            }
            IPolynomialFunction result = new QuadraticFunction();
            for(int i = degree; i >= 0; i--)
            {
                result.AddTerm(CreateTerm(i));
            }
            return result;
        }

        private Term CreateTerm(int exponent)
        {
            return new Term(exponent, random.Next(-20, 20));
        }
    }
}
