// See https://aka.ms/new-console-template for more information

using PolynomialFunctions;

var factory = new PolynomialFunctionFactory();
var functions = new List<IPolynomialFunction>();
var cnt = 10;
for (int i = 0; i < cnt; i++)
{
    functions.Add(factory.Create(2));
}
var pdfCreator = new PdfCreator();
File.Delete(@"c:\temp\solution.pdf");
File.Delete(@"c:\temp\question.pdf");
pdfCreator.CreateSolutionFile(@"c:\temp\solution.pdf", functions);
pdfCreator.CreateQuestionsFile(@"c:\temp\question.pdf", functions);

Console.WriteLine();
Console.WriteLine("done");
Console.ReadLine();
