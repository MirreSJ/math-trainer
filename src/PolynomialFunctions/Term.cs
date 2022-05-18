
namespace PolynomialFunctions
{
    internal class Term
    {
        public int Exponent { get; }
        public int Coefficient { get; }
        public char Variable => 'x';

        public Term(int exponent, int coefficient)
        {
            Exponent = exponent;
            Coefficient = coefficient;
        }
    }
}
