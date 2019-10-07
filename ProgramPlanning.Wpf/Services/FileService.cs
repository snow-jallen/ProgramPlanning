using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPlanning.Wpf.Services
{

    public class FileService : IFileService
    {
        public FileService()
        {

        }

        public IEnumerable<Course> ReadCourses(string excelFile)
        {
            var file = new FileInfo(excelFile);
            var courses = new List<Course>();
            using(var package = new ExcelPackage(file))
            {
                var sheet = package.Workbook.Worksheets.First();
                var course = new Course();
                var outcomes = new List<string>();
                for(var row = 2; row < sheet.Dimension.End.Row; row++)
                {
                    var area = sheet.Cells[row, 1].Value.ToString();
                    var num = (int)(double)sheet.Cells[row, 2].Value;
                    var linetype = (string)sheet.Cells[row, 3].Value;
                    var content = String.Empty;
                    if (linetype == "Content")
                    {
                        content = (string)sheet.Cells[row, 4].Value;

                    }
                    else
                    {
                        outcomes.Add((string)sheet.Cells[row, 4].Value);
                    }
                    var nextNum = (int)(double)(sheet.Cells[row + 1, 2].Value ?? -1);
                    if (nextNum != num)
                    {
                        course.Area = area;
                        course.Number = num;
                        course.Content = content;
                        course.Outcomes = outcomes;
                        courses.Add(course);
                        course = new Course();
                        outcomes = new List<string>();
                    }


                }

            }
            return null;
        }

    }
}
