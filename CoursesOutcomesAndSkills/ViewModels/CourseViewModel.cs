using GalaSoft.MvvmLight;
using ProgramPlanning.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CoursesOutcomesAndSkills.ViewModels
{
    public class CourseViewModel : ViewModelBase
    {
        private readonly IEnumerable<Course> courses;

        public CourseViewModel(IEnumerable<Course> courses)
        {
            this.courses = courses ?? throw new ArgumentNullException(nameof(courses));
        }

        public IEnumerable<Course> PreReqCourses => courses.Where(c => c.Semester == Semester.PreReq);
        public IEnumerable<Course> Year1Fall => courses.Where(c => c.Semester == Semester.Year1Fall);
        public IEnumerable<Course> Year1Spring => courses.Where(c => c.Semester == Semester.Year1Spring);
        public IEnumerable<Course> Year2Fall => courses.Where(c => c.Semester == Semester.Year2Fall);
        public IEnumerable<Course> Year2Spring => courses.Where(c => c.Semester == Semester.Year2Spring);
        public IEnumerable<Course> Year3Fall => courses.Where(c => c.Semester == Semester.Year3Fall);
        public IEnumerable<Course> Year3Spring => courses.Where(c => c.Semester == Semester.Year3Spring);
        public IEnumerable<Course> Year4Fall => courses.Where(c => c.Semester == Semester.Year4Fall);
        public IEnumerable<Course> Year4Spring => courses.Where(c => c.Semester == Semester.Year4Spring);
        private Course selectedCourse;
        public Course SelectedCourse
        {
            get { return selectedCourse; }
            set
            {
                selectedCourse = null;
                RaisePropertyChanged();
                Set(ref selectedCourse, value);
            }
        }

    }
}
