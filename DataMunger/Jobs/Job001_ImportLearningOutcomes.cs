using OfficeOpenXml;
using ProgramPlanning.Shared.Models;
using ProgramPlanning.Shared.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataMunger.Jobs
{
    public static class Job001_ImportLearningOutcomes
    {
        public static void Run()
        {
            var repo = new PostgresCourseRepository();
            repo.SetConnection(new ConnectionInfo
            {
                Host = "127.0.0.1",
                Database = "pp3",
                Port = 5432,
                User = "postgres",
                Password = "password"
            });

            var path = @"C:\git\ProgramPlanning\data\RawSpreadsheet.xlsx";
            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                var sheet = package.Workbook.Worksheets[0];

                var prefixCol = 2;
                var numCol = 3;
                var outcomeColStart = 10;
                for(int row = 4; row <= 48; row++)
                {
                    var prefix = (string)sheet.Cells[row, prefixCol].Value;
                    var num = (int)(Double)sheet.Cells[row, numCol].Value;

                    Console.WriteLine($"Doing {prefix} {num}");

                    DbCourse dbCourse = repo.GetDbCourse(prefix, num);

                    for(int outcomeCol = outcomeColStart; outcomeCol < outcomeColStart+12; outcomeCol++)
                    {
                        var outcomeText = (string)sheet.Cells[row, outcomeCol].Value;
                        if (outcomeText == null)
                            break;

                        var learningOutcome = new DbLearningOutcome
                        {
                            Description = outcomeText
                        };
                        learningOutcome = repo.AddOutcome(learningOutcome);

                        var outcomeCourseLink = new DbCourseLearningOutcomeXRef
                        {
                            LearningOutcomeId = learningOutcome.Id,
                            CourseId = dbCourse.Id
                        };
                        repo.AddCourseOutcomeLink(outcomeCourseLink);

                        Console.WriteLine($"Added learning outcome {outcomeText.Substring(0, Math.Min(outcomeText.Length, 25))}...");
                    }
                }

                Console.WriteLine("All done.");
            }
            Console.ReadLine();
        }
    }
}
