using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolynomialFunctions
{
    internal class QuadraticFunction : IPolynomialFunction
    {
        private readonly List<Term> terms = new List<Term>();
        public IReadOnlyCollection<Term> Terms => new List<Term>(terms);

        int IPolynomialFunction.Degree => 2;

        public void AddTerm(Term term)
        {
            terms.Add(term);
            terms.Sort((a, b) => a.Exponent > b.Exponent ? -1 : 1);
        }
    }
}
