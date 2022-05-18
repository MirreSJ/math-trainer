using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;

namespace Pythagoras
{
    public class PdfCreator
    {
        private static Random random = new Random();
        public bool CreateSolutionFile(string filePath, RightAngledTriangle[] triangles)
        {
            try
            {
                System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("de-DE");
                var solutionDoc = new Document();
                PageSetup pageSetup = solutionDoc.DefaultPageSetup.Clone();
                pageSetup.Orientation = Orientation.Landscape;
                Section sec = solutionDoc.Sections.AddSection();
                sec.PageSetup = pageSetup;

                Table table = sec.AddTable();
                table.Borders.Visible = true;

                CreateColumns(triangles, table);
                CreateHeaderRow(triangles, table);
                CreateTriangleRows(triangles, table);

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

        public bool CreateQuestionsFile(string filePath, RightAngledTriangle[] triangles)
        {
            try
            {
                System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("de-DE");
                var solutionDoc = new Document();
                PageSetup pageSetup = solutionDoc.DefaultPageSetup.Clone();
                pageSetup.Orientation = Orientation.Landscape;
                Section sec = solutionDoc.Sections.AddSection();
                sec.PageSetup = pageSetup;

                Table table = sec.AddTable();
                table.Borders.Visible = true;

                CreateColumns(triangles, table);
                CreateHeaderRow(triangles, table);
                List<Tuple<RightAngledTriangle, Properties, Properties>> triangleRandomPropCombo = GetRandomTrianglePropertyCombo(triangles);

                CreateRandomTriangleRows(triangleRandomPropCombo, table);

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

        private static List<Tuple<RightAngledTriangle, Properties, Properties>> GetRandomTrianglePropertyCombo(RightAngledTriangle[] triangles)
        {
            var properties = Enum.GetNames(typeof(Properties));
            List<Tuple<RightAngledTriangle, Properties, Properties>> triangleRandomPropCombo = new List<Tuple<RightAngledTriangle, Properties, Properties>>();
            foreach (var triangle in triangles)
            {
                Properties firstRandomProperty, secondRandomProperty;
                firstRandomProperty = secondRandomProperty = Enum.Parse<Properties>(properties[random.Next(0, properties.Length)]);
                while (firstRandomProperty == secondRandomProperty)
                {
                    secondRandomProperty = Enum.Parse<Properties>(properties[random.Next(0, properties.Length)]);
                }
                triangleRandomPropCombo.Add(new Tuple<RightAngledTriangle, Properties, Properties>(triangle, firstRandomProperty, secondRandomProperty));
            }

            return triangleRandomPropCombo;
        }

        private static void CreateTriangleRows(RightAngledTriangle[] triangles, Table table)
        {
            foreach (var value in Enum.GetValues(typeof(Properties)))
            {
                var row = table.AddRow();
                row.Height = 20;
                WritePropertyCell(value, row);
                WriteTriangleValues(triangles, (Properties)value, row);
            }
        }

        private static void CreateRandomTriangleRows(List<Tuple<RightAngledTriangle, Properties, Properties>> triangleRandomPropCombo, Table table)
        {
            foreach (var value in Enum.GetValues(typeof(Properties)))
            {
                var row = table.AddRow();
                row.Height = 20;
                WritePropertyCell(value, row);
                WriteSomeTriangleValues(triangleRandomPropCombo, (Properties)value, row);
            }
        }

        private static void WritePropertyCell(object value, Row row)
        {
            row.Cells[0].AddParagraph($"{(Properties)value}");
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            row.Cells[0].Format.LineSpacingRule = LineSpacingRule.Single;
        }

        private static void WriteTriangleValues(RightAngledTriangle[] triangles, Properties property, Row row)
        {
            for (int triangleCnt = 0; triangleCnt < triangles.Length; triangleCnt++)
            {
                var valueCell = row.Cells[triangleCnt + 1];
                valueCell.Format.Alignment = ParagraphAlignment.Right;
                valueCell.VerticalAlignment = VerticalAlignment.Center;
                valueCell.AddParagraph($"{Math.Round(triangles[triangleCnt].Properties[property], 2):0.00}");
            }
        }

        private static void WriteSomeTriangleValues(List<Tuple<RightAngledTriangle, Properties, Properties>> triangleRandomPropCombo, Properties property, Row row)
        {
            for (int triangleCnt = 0; triangleCnt < triangleRandomPropCombo.Count; triangleCnt++)
            {
                var valueCell = row.Cells[triangleCnt + 1];
                valueCell.Format.Alignment = ParagraphAlignment.Right;
                valueCell.VerticalAlignment = VerticalAlignment.Center;                
                if (property == triangleRandomPropCombo[triangleCnt].Item2 || property == triangleRandomPropCombo[triangleCnt].Item3)
                {
                    valueCell.AddParagraph($"{Math.Round(triangleRandomPropCombo[triangleCnt].Item1.Properties[property], 2):0.00}");
                }
                else
                {
                    valueCell.AddParagraph("");
                }
            }
        }

        private static void CreateColumns(RightAngledTriangle[] triangles, Table table)
        {
            for (int triangleCnt = 0; triangleCnt <= triangles.Length; triangleCnt++)
            {
                var col = table.AddColumn();
                col.Width = triangleCnt == 0 ? 20 : 40;
                col.Borders.Visible = true;
            }
        }

        private static void CreateHeaderRow(RightAngledTriangle[] triangles, Table table)
        {
            var alphabet = Enumerable.Range(0, 26).Select(i => Convert.ToChar('a' + i).ToString()).ToArray();
            Row row = table.AddRow();
            row.Height = 20;
            row.Cells[0].Column.Width = 20;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            for (int triangleCnt = 1; triangleCnt <= triangles.Length; triangleCnt++)
            {
                Cell headerCell = row.Cells[triangleCnt];
                headerCell.Format.LineSpacing = 2;
                headerCell.Format.Alignment = ParagraphAlignment.Right;
                headerCell.Format.Font.Bold = true;
                headerCell.VerticalAlignment = VerticalAlignment.Center;
                headerCell.AddParagraph(alphabet[triangleCnt - 1]);
            }
        }
    }
}
