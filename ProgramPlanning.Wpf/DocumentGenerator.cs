using MigraDoc.DocumentObjectModel;
using OfficeOpenXml;
using ProgramPlanning.Shared.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPlanning.Wpf
{
    public static class DocumentGenerator
    {
        public static void MakeDocument(IEnumerable<Course> courses)
        {
            if (courses == null)
                throw new ArgumentNullException(nameof(courses));
            
            string fileName = "report.xlsx";
            if (File.Exists(fileName))
                File.Delete(fileName);
            using (var package = new OfficeOpenXml.ExcelPackage(new System.IO.FileInfo(fileName)))
            {
                var sheet = package.Workbook.Worksheets.Add("Exported Courses");
                var row = 1;
                int col = 1;

                sheet.Cells[row, col++].Value = "Area";
                sheet.Cells[row, col++].Value = "Number";
                sheet.Cells[row, col++].Value = "Name";
                sheet.Cells[row, col++].Value = "Title";
                sheet.Cells[row, col++].Value = "Content";
                sheet.Cells[row, col++].Value = "Semester";
                sheet.Cells[row, col++].Value = "Prereqs";
                for(int i = 1; i <= 12; i++)
                    sheet.Cells[row, col++].Value = $"Outcome {i}";

                foreach (var course in courses)
                {
                    col = 1;
                    row++;
                    sheet.Cells[row, col++].Value = course.Area;
                    sheet.Cells[row, col++].Value = course.Number;
                    sheet.Cells[row, col++].Value = course.Name;
                    sheet.Cells[row, col++].Value = course.Title;
                    sheet.Cells[row, col++].Value = course.Content;
                    sheet.Cells[row, col++].Value = course.Semester;
                    sheet.Cells[row, col++].Value = String.Join(", ", course.Prerequisites);
                    foreach(var outcome in course.Outcomes)
                    {
                        sheet.Cells[row, col++].Value = outcome;
                    }
                }
                package.Save();
            }
            Process.Start(fileName);
        }

        private static void defineStyles(Document document)
        {
            // Get the predefined style Normal.
            Style style = document.Styles["Normal"];
            // Because all styles are derived from Normal, the next line changes the 
            // font of the whole document. Or, more exactly, it changes the font of
            // all styles and paragraphs that do not redefine the font.
            style.Font.Name = "Times New Roman";

            // Heading1 to Heading9 are predefined styles with an outline level. An outline level
            // other than OutlineLevel.BodyText automatically creates the outline (or bookmarks) 
            // in PDF.

            style = document.Styles["Heading1"];
            style.Font.Name = "Tahoma";
            style.Font.Size = 14;
            style.Font.Bold = true;
            style.Font.Color = Colors.DarkBlue;
            style.ParagraphFormat.PageBreakBefore = true;
            style.ParagraphFormat.SpaceAfter = 6;

            style = document.Styles["Heading2"];
            style.Font.Size = 12;
            style.Font.Bold = true;
            style.ParagraphFormat.PageBreakBefore = false;
            style.ParagraphFormat.SpaceBefore = 6;
            style.ParagraphFormat.SpaceAfter = 6;

            style = document.Styles["Heading3"];
            style.Font.Size = 10;
            style.Font.Bold = true;
            style.Font.Italic = true;
            style.ParagraphFormat.SpaceBefore = 6;
            style.ParagraphFormat.SpaceAfter = 3;

            style = document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);

            style = document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

            // Create a new style called TextBox based on style Normal
            style = document.Styles.AddStyle("TextBox", "Normal");
            style.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
            style.ParagraphFormat.Borders.Width = 2.5;
            style.ParagraphFormat.Borders.Distance = "3pt";
            style.ParagraphFormat.Shading.Color = Colors.SkyBlue;

            // Create a new style called TOC based on style Normal
            style = document.Styles.AddStyle("TOC", "Normal");
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right, TabLeader.Dots);
            style.ParagraphFormat.Font.Color = Colors.Blue;
        }

        private static void defineContentSection(Document document)
        {
            Section section = document.AddSection();
            section.PageSetup.OddAndEvenPagesHeaderFooter = true;
            section.PageSetup.StartingNumber = 1;

            HeaderFooter header = section.Headers.Primary;
            header.AddParagraph("\tOdd Page Header");

            header = section.Headers.EvenPage;
            header.AddParagraph("Even Page Header");

            // Create a paragraph with centered page number. See definition of style "Footer".
            Paragraph paragraph = new Paragraph();
            paragraph.AddTab();
            paragraph.AddPageField();

            // Add paragraph to footer for odd pages.
            section.Footers.Primary.Add(paragraph);
            // Add clone of paragraph to footer for odd pages. Cloning is necessary because an object must
            // not belong to more than one other object. If you forget cloning an exception is thrown.
            section.Footers.EvenPage.Add(paragraph.Clone());
        }
    }
}
