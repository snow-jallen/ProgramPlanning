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
        private readonly IEnumerable<IndividualCourseViewModel> courses;

        public CourseViewModel(IEnumerable<Course> courses)
        {
            if (courses == null)
                throw new ArgumentNullException(nameof(courses), "Must have a list of courses");
            this.courses = courses.Select(c => new IndividualCourseViewModel(c));
        }

        public IEnumerable<IndividualCourseViewModel> PreReqCourses => courses.Where(c => c.Semester == Semester.PreReq);
        public IEnumerable<IndividualCourseViewModel> Year1Fall => courses.Where(c => c.Semester == Semester.Year1Fall);
        public IEnumerable<IndividualCourseViewModel> Year1Spring => courses.Where(c => c.Semester == Semester.Year1Spring);
        public IEnumerable<IndividualCourseViewModel> Year2Fall => courses.Where(c => c.Semester == Semester.Year2Fall);
        public IEnumerable<IndividualCourseViewModel> Year2Spring => courses.Where(c => c.Semester == Semester.Year2Spring);
        public IEnumerable<IndividualCourseViewModel> Year3Fall => courses.Where(c => c.Semester == Semester.Year3Fall);
        public IEnumerable<IndividualCourseViewModel> Year3Spring => courses.Where(c => c.Semester == Semester.Year3Spring);
        public IEnumerable<IndividualCourseViewModel> Year4Fall => courses.Where(c => c.Semester == Semester.Year4Fall);
        public IEnumerable<IndividualCourseViewModel> Year4Spring => courses.Where(c => c.Semester == Semester.Year4Spring);
        private IndividualCourseViewModel selectedCourse;
        public IndividualCourseViewModel SelectedCourse
        {
            get { return selectedCourse; }
            set
            {
                clearPrerequisites(selectedCourse);
                Set(ref selectedCourse, null);//un-select the prior one

                Set(ref selectedCourse, value);//select the new one
                highlightPrerequisites(selectedCourse);
            }
        }

        private void clearPrerequisites(IndividualCourseViewModel selectedCourse)
        {
            if (selectedCourse?.Course?.Prerequisites == null)
                return;
            foreach(var preReq in selectedCourse.Course.Prerequisites)
            {
                var courseVM = courses.Single(c => c.Course == preReq);
                courseVM.IsPrerequisiteToCurrentCourse = false;
            }
        }

        private void highlightPrerequisites(IndividualCourseViewModel selectedCourse)
        {
            if (selectedCourse?.Course?.Prerequisites == null)
                return;
            foreach (var preReq in selectedCourse.Course.Prerequisites)
            {
                var courseVM = courses.Single(c => c.Course == preReq);
                courseVM.IsPrerequisiteToCurrentCourse = true;
            }
        }
    }

    public class IndividualCourseViewModel : ViewModelBase
    {
        public IndividualCourseViewModel(Course course)
        {
            Course = course ?? throw new ArgumentNullException(nameof(course));
        }

        public Course Course { get; }
        public string CourseNumber => Course.CourseNumber;
        public string Title => Course.Title;
        public Semester Semester => Course.Semester;
        public string Summary => Course.Summary;
        public IEnumerable<LearningOutcome> Outcomes => Course.Outcomes;

        private bool isPrerequisiteToCurrentCourse;
        public bool IsPrerequisiteToCurrentCourse
        {
            get => isPrerequisiteToCurrentCourse;
            set { Set(ref isPrerequisiteToCurrentCourse, value); }
        }

    }
}
