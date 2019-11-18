using GalaSoft.MvvmLight;
using ProgramPlanning.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections.ObjectModel;

namespace CoursesOutcomesAndSkills.ViewModels
{
    public class CourseViewModel : ViewModelBase
    {
        private readonly IEnumerable<IndividualCourseViewModel> courses;
        private readonly IEnumerable<ObservableCollection<IndividualCourseViewModel>> semesters;

        public CourseViewModel(IEnumerable<Course> courses)
        {
            if (courses == null)
                throw new ArgumentNullException(nameof(courses), "Must have a list of courses");
            this.courses = courses.Select(c => new IndividualCourseViewModel(c));

            semesters = new[]
            {
                PreReqCourses,
                Year1Fall,
                Year2Spring,
                Year2Fall,
                Year3Spring,
                Year3Fall,
                Year3Spring,
                Year4Fall,
                Year4Spring
            };

            PreReqCourses = new ObservableCollection<IndividualCourseViewModel>(this.courses.Where(c => c.Semester == Semester.PreReq));
            Year1Fall = new ObservableCollection<IndividualCourseViewModel>(this.courses.Where(c => c.Semester == Semester.Year1Fall));
            Year1Spring = new ObservableCollection<IndividualCourseViewModel>(this.courses.Where(c => c.Semester == Semester.Year1Spring));
            Year2Fall = new ObservableCollection<IndividualCourseViewModel>(this.courses.Where(c => c.Semester == Semester.Year2Fall));
            Year2Spring = new ObservableCollection<IndividualCourseViewModel>(this.courses.Where(c => c.Semester == Semester.Year2Spring));
            Year3Fall = new ObservableCollection<IndividualCourseViewModel>(this.courses.Where(c => c.Semester == Semester.Year3Fall));
            Year3Spring = new ObservableCollection<IndividualCourseViewModel>(this.courses.Where(c => c.Semester == Semester.Year3Spring));
            Year4Fall = new ObservableCollection<IndividualCourseViewModel>(this.courses.Where(c => c.Semester == Semester.Year4Fall));
            Year4Spring = new ObservableCollection<IndividualCourseViewModel>(this.courses.Where(c => c.Semester == Semester.Year4Spring));
        }

        public ObservableCollection<IndividualCourseViewModel> PreReqCourses { get; private set; }
        public ObservableCollection<IndividualCourseViewModel> Year1Fall { get; private set; }
        public ObservableCollection<IndividualCourseViewModel> Year1Spring { get; private set; }
        public ObservableCollection<IndividualCourseViewModel> Year2Fall { get; private set; }
        public ObservableCollection<IndividualCourseViewModel> Year2Spring { get; private set; }
        public ObservableCollection<IndividualCourseViewModel> Year3Fall { get; private set; }
        public ObservableCollection<IndividualCourseViewModel> Year3Spring { get; private set; }
        public ObservableCollection<IndividualCourseViewModel> Year4Fall { get; private set; }
        public ObservableCollection<IndividualCourseViewModel> Year4Spring { get; private set; }
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
            foreach (var preReq in selectedCourse.Course.Prerequisites)
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
            foreach (var semester in semesters)
            {

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
