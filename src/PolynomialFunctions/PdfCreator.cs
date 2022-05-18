// See https://aka.ms/new-console-template for more information

using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;

namespace PolynomialFunctions
{
    internal class PdfCreator
    {
        public PdfCreator()
        {
        }

        internal bool CreateSolutionFile(string filePath, List<IPolynomialFunction> functions)
        {
            try
            {
                System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("de-DE");
                var solutionDoc = new Document();
                PageSetup pageSetup = solutionDoc.DefaultPageSetup.Clone();
                Section sec = solutionDoc.Sections.AddSection();
                sec.PageSetup = pageSetup;
                sec.AddParagraph("Bestimme die Lösungsmengen.");
                PrintPolynomialFunctionsWithSolutiuons(functions, sec);

                PdfDocumentRenderer printer = new PdfDocumentRenderer()
                {
                    Document = solutionDoc,
                };
                printer.RenderDocument();
                printer.PdfDocument.Save(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        private void PrintPolynomialFunctionsWithSolutiuons(List<IPolynomialFunction> functions, Section sec)
        {
            var alphabet = Enumerable.Range(0, 26).Select(i => Convert.ToChar('a' + i).ToString()).ToArray();

            for (int i = 0; i < functions.Count; i++)
            {
                var paragraph = sec.AddParagraph($"{alphabet[i]}) ");
                paragraph.AddText(" ");
                PrintPolynomialFunction(functions[i], paragraph);
                PrintSolution(functions[i], sec.AddParagraph());
                sec.AddParagraph();
            }
        }

        private void PrintSolution(IPolynomialFunction polynomialFunction, Paragraph paragraph)
        {
            //throw new NotImplementedException();
        }

        internal bool CreateQuestionsFile(string filePath, List<IPolynomialFunction> functions)
        {
            try
            {
                System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("de-DE");
                var solutionDoc = new Document();
                PageSetup pageSetup = solutionDoc.DefaultPageSetup.Clone();
                Section sec = solutionDoc.Sections.AddSection();
                sec.PageSetup = pageSetup;
                sec.AddParagraph("Bestimme die Lösungsmengen.");
                PrintPolynomialFunctions(functions, sec);

                PdfDocumentRenderer printer = new PdfDocumentRenderer()
                {
                    Document = solutionDoc,
                };
                printer.RenderDocument();
                printer.PdfDocument.Save(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        private void PrintPolynomialFunctions(List<IPolynomialFunction> functions, Section sec)
        {
            var alphabet = Enumerable.Range(0, 26).Select(i => Convert.ToChar('a' + i).ToString()).ToArray();
           
            for (int i = 0; i < functions.Count; i++)
            {                
                var paragraph = sec.AddParagraph($"{alphabet[i]}) ");
                paragraph.AddText(" ");
                PrintPolynomialFunction(functions[i], paragraph);
                sec.AddParagraph();
            }
        }

        private void PrintPolynomialFunction(IPolynomialFunction function, Paragraph paragraph)
        {
            var first = true;
            foreach(Term term in function.Terms)
            {
                PrintTerm(term, paragraph, first);
                first = false;
            }
            paragraph.AddText("= 0");
        }

        private void PrintTerm(Term term, Paragraph paragraph, bool first)
        {
            if(first)
            {
                paragraph.AddText($"{term.Coefficient}");
            }
            else
            {
                paragraph.AddText($"{term.Coefficient:+ 0;- #}");
            }
            if (term.Exponent > 0)
            {
                paragraph.AddText($"{term.Variable}");
            }
            if (term.Exponent > 1)
            {
                var exponentText = paragraph.AddFormattedText($"{term.Exponent}");
                exponentText.Superscript = true;
            }
            paragraph.AddText(" ");
        }
    }
}