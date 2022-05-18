// See https://aka.ms/new-console-template for more information
using Pythagoras;

var factory = new RightAngledTriangleFactory();
var cnt = 17;
var triangles = new RightAngledTriangle[cnt];
for(int i = 0; i < cnt; i++)
{
    triangles[i] = factory.Create();
}
var pdfCreator = new PdfCreator();
File.Delete(@"c:\temp\solution.pdf");
File.Delete(@"c:\temp\question.pdf");
pdfCreator.CreateSolutionFile(@"c:\temp\solution.pdf", triangles);
pdfCreator.CreateQuestionsFile(@"c:\temp\question.pdf", triangles);

Console.WriteLine("done");
Console.ReadLine();

