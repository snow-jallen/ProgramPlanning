using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using OfficeOpenXml;
using ProgramPlanning.Shared.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace CoursesOutcomesAndSkills.ViewModels
{
    public class ReportViewModel : ViewModelBase
    {
        private readonly ICourseInfoRepository courseRepository;

        public ReportViewModel(ProgramPlanning.Shared.Services.ICourseInfoRepository courseRepository)
        {
            this.courseRepository = courseRepository;
            TemplatePath = @"c:\git\programplanning\data\LearningOutcomeSurvey_Template.xlsx";
        }

        private string templatePath;
        public string TemplatePath
        {
            get => templatePath;
            set { Set(ref templatePath, value); }
        }


        private RelayCommand makeReport;
        public RelayCommand MakeReport => makeReport ?? (makeReport = new RelayCommand(() =>
        {
            var report = Path.Combine(Path.GetDirectoryName(TemplatePath), "Learning Outcomes Survey.xlsx");
            var templateSheet = "CS 1234";
            using (var package = new ExcelPackage(new FileInfo(report), new FileInfo(TemplatePath)))
            {
                foreach (var course in from c in courseRepository.GetCourses()
                                       where c.Prefix == "CS" || c.Prefix == "SE"
                                       select c)
                {
                    var sheet = package.Workbook.Worksheets.Copy(templateSheet, course.CourseNumber);
                    sheet.Cells["A1"].Value = course.CourseNumber;
                    sheet.Cells["D1"].Value = $"{course.NonProgramPrereqs} {String.Join(" ", course.Prerequisites.Select(p => p.CourseNumber))}";
                    sheet.Cells["A2"].Value = course.Title;
                    sheet.Cells["A4"].Value = course.Summary;

                    var row = 10;
                    var col = 3;
                    foreach (var outcome in course.Outcomes)
                    {
                        var outcomeText = outcome.Name ?? String.Empty;
                        if (outcomeText.Length > 0)
                            outcomeText += "\n";
                        outcomeText += outcome.Description;
                        sheet.Cells[row, col].Value = outcomeText;
                        row += 5;
                    }
                }
                package.Workbook.Worksheets.Delete(templateSheet);
                package.Save();
            }
        }));
    }
}
