using GalaSoft.MvvmLight;
using ProgramPlanning.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ProgramPlanning.Shared.Services;
using GalaSoft.MvvmLight.CommandWpf;

namespace CoursesOutcomesAndSkills.ViewModels
{
    public class CourseViewModel : ViewModelBase
    {
        private IEnumerable<IndividualCourseViewModel> courseViewModels;
        private readonly ICourseInfoRepository courseRepository;

        public CourseViewModel(ICourseInfoRepository courseRepository)
        {
            this.courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            refreshCourses();
            MessengerInstance.Register<RefreshDatabaseMessage>(this, (m) => refreshCourses());
        }

        private void refreshCourses()
        {
            this.courseViewModels = courseRepository
                                        .GetCourses()
                                        .Select(c => new IndividualCourseViewModel(c));
            RaisePropertyChanged(nameof(PreReqCourses));
            RaisePropertyChanged(nameof(Year1Fall));
            RaisePropertyChanged(nameof(Year1Spring));
            RaisePropertyChanged(nameof(Year2Fall));
            RaisePropertyChanged(nameof(Year2Spring));
            RaisePropertyChanged(nameof(Year3Fall));
            RaisePropertyChanged(nameof(Year3Spring));
            RaisePropertyChanged(nameof(Year4Fall));
            RaisePropertyChanged(nameof(Year4Spring));
        }

        public IEnumerable<IndividualCourseViewModel> PreReqCourses => courseViewModels.Where(c => c.Semester == Semester.Year0PreReq);
        public IEnumerable<IndividualCourseViewModel> Year1Fall => courseViewModels.Where(c => c.Semester == Semester.Year1Fall);
        public IEnumerable<IndividualCourseViewModel> Year1Spring => courseViewModels.Where(c => c.Semester == Semester.Year1Spring);
        public IEnumerable<IndividualCourseViewModel> Year2Fall => courseViewModels.Where(c => c.Semester == Semester.Year2Fall);
        public IEnumerable<IndividualCourseViewModel> Year2Spring => courseViewModels.Where(c => c.Semester == Semester.Year2Spring);
        public IEnumerable<IndividualCourseViewModel> Year3Fall => courseViewModels.Where(c => c.Semester == Semester.Year3Fall);
        public IEnumerable<IndividualCourseViewModel> Year3Spring => courseViewModels.Where(c => c.Semester == Semester.Year3Spring);
        public IEnumerable<IndividualCourseViewModel> Year4Fall => courseViewModels.Where(c => c.Semester == Semester.Year4Fall);
        public IEnumerable<IndividualCourseViewModel> Year4Spring => courseViewModels.Where(c => c.Semester == Semester.Year4Spring);
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
                var courseVM = courseViewModels.Single(c => c.Course == preReq);
                courseVM.IsPrerequisiteToCurrentCourse = false;
            }
        }

        private void highlightPrerequisites(IndividualCourseViewModel selectedCourse)
        {
            if (selectedCourse?.Course?.Prerequisites == null)
                return;
            foreach (var preReq in selectedCourse.Course.Prerequisites)
            {
                var courseVM = courseViewModels.Single(c => c.Course == preReq);
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
