﻿using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPlanning.Shared.Services
{

    public class FileService : IFileService
    {
        public FileService()
        {

        }

        public virtual string BrowseFolderPath()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExcelBasedCourse> ReadCourses(string excelFile)
        {
            var file = new FileInfo(excelFile);
            if (file.Exists == false)
                throw new FileNotFoundException("Unable to locate excel file", excelFile);

            var courses = new List<ExcelBasedCourse>();
            using(var package = new ExcelPackage(file))
            {
                var sheet = package.Workbook.Worksheets.First();
                var course = new ExcelBasedCourse();
                var outcomes = new List<string>();
                var preRequisites = new List<string>();
                var coRequisites = new List<string>();
                var content = string.Empty;
                var title = string.Empty;
                var semester = string.Empty;
                for (var row = 2; row < sheet.Dimension.End.Row; row++)
                {
                    var area = sheet.Cells[row, 1].Value.ToString();
                    var num = (int)(double)sheet.Cells[row, 2].Value;
                    var linetype = (string)sheet.Cells[row, 3].Value;
                    if (linetype == "Content")
                    {
                        content = (string)sheet.Cells[row, 4].Value;
                    }
                    else if (linetype == "Title")
                    {
                        title = (string)sheet.Cells[row, 4].Value;
                    }
                    else if (linetype == "Semester")
                    {
                        semester = (string)sheet.Cells[row, 4].Value;
                    }
                    else if (linetype == "Prereq")
                    {
                        preRequisites.Add((string)sheet.Cells[row, 4].Value);
                    }
                    else if (linetype == "Coreq")
                    {
                        coRequisites.Add((string)sheet.Cells[row, 4].Value);
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
                        course.Title = title;
                        course.Semester = semester;
                        course.Outcomes = outcomes;
                        course.Prerequisites = preRequisites;
                        course.Corequisites = coRequisites;
                        courses.Add(course);
                        course = new ExcelBasedCourse();
                        outcomes = new List<string>();
                        preRequisites = new List<string>();
                        coRequisites = new List<string>();
                        content = string.Empty;
                        title = string.Empty;
                        semester = string.Empty;
                    }
                }
            }
            return courses;
        }

    }
}
