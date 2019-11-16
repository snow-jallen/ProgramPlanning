using ProgramPlanning.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesOutcomesAndSkills.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            var courseRepository = new BogusCourseInfoRepository();
            var courses = courseRepository.GetCourses();
            MainWindowViewModel = new MainWindowViewModel();
            ProgramViewModel = new ProgramViewModel(courses);
            ReportViewModel = new ReportViewModel();
            CourseViewModel = new CourseViewModel();
        }

        public MainWindowViewModel MainWindowViewModel { get; private set; }
        public ProgramViewModel ProgramViewModel { get; private set; }
        public ReportViewModel ReportViewModel { get; private set; }
        public CourseViewModel CourseViewModel { get; private set; }
    }
}
