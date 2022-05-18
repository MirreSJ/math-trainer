namespace PolynomialFunctions
{
    internal interface IPolynomialFunction
    {
        internal int Degree { get; }
        internal void AddTerm(Term term);

        internal IReadOnlyCollection<Term> Terms { get; }
    }
}