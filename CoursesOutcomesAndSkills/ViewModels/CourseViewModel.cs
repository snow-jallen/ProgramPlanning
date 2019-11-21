using GalaSoft.MvvmLight;
using ProgramPlanning.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ProgramPlanning.Shared.Services;
using GalaSoft.MvvmLight.CommandWpf;
using System.Diagnostics;
using System.Windows.Media;

namespace CoursesOutcomesAndSkills.ViewModels
{
    public class CourseViewModel : ViewModelBase
    {
        private List<IndividualCourseViewModel> courseViewModels;
        private readonly ICourseInfoRepository courseRepository;

        public CourseViewModel(ICourseInfoRepository courseRepository)
        {
            this.courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            refreshCourses();
            MessengerInstance.Register<RefreshDatabaseMessage>(this, (m) => refreshCourses());
        }

        private void refreshCourses()
        {
            courseViewModels = new List<IndividualCourseViewModel>(from c in courseRepository.GetCourses()
                                                                   select new IndividualCourseViewModel(c));
            PreReqCourses = new List<IndividualCourseViewModel>(courseViewModels.Where(c => c.Semester == Semester.Year0PreReq));
            Year1Fall = new List<IndividualCourseViewModel>(courseViewModels.Where(c => c.Semester == Semester.Year1Fall));
            Year1Spring = new List<IndividualCourseViewModel>(courseViewModels.Where(c => c.Semester == Semester.Year1Spring));
            Year2Fall = new List<IndividualCourseViewModel>(courseViewModels.Where(c => c.Semester == Semester.Year2Fall));
            Year2Spring = new List<IndividualCourseViewModel>(courseViewModels.Where(c => c.Semester == Semester.Year2Spring));
            Year3Fall = new List<IndividualCourseViewModel>(courseViewModels.Where(c => c.Semester == Semester.Year3Fall));
            Year3Spring = new List<IndividualCourseViewModel>(courseViewModels.Where(c => c.Semester == Semester.Year3Spring));
            Year4Fall = new List<IndividualCourseViewModel>(courseViewModels.Where(c => c.Semester == Semester.Year4Fall));
            Year4Spring = new List<IndividualCourseViewModel>(courseViewModels.Where(c => c.Semester == Semester.Year4Spring));
        }

        public List<IndividualCourseViewModel> PreReqCourses { get; private set; }
        public List<IndividualCourseViewModel> Year1Fall { get; private set; }
        public List<IndividualCourseViewModel> Year1Spring { get; private set; }
        public List<IndividualCourseViewModel> Year2Fall { get; private set; }
        public List<IndividualCourseViewModel> Year2Spring { get; private set; }
        public List<IndividualCourseViewModel> Year3Fall { get; private set; }
        public List<IndividualCourseViewModel> Year3Spring { get; private set; }
        public List<IndividualCourseViewModel> Year4Fall { get; private set; }
        public List<IndividualCourseViewModel> Year4Spring { get; private set; }

        private IndividualCourseViewModel selectedCourse;
        public IndividualCourseViewModel SelectedCourse
        {
            get { return selectedCourse; }
            set
            {
                if (selectedCourse != null)
                    selectedCourse.IsSelected = false;
                clearPrerequisites(selectedCourse);
                Set(ref selectedCourse, null);//un-select the prior one

                Set(ref selectedCourse, value);//select the new one
                highlightPrerequisites(selectedCourse);
                if (selectedCourse != null)
                    selectedCourse.IsSelected = true;
            }
        }

        private void clearPrerequisites(IndividualCourseViewModel selectedCourse)
        {
            if (selectedCourse?.Course?.Prerequisites == null)
                return;
            foreach (var preReq in selectedCourse.Course.Prerequisites)
            {
                var courseVM = courseViewModels.Single(c => c.Course.Id == preReq.Id);
                courseVM.IsPrerequisiteToCurrentCourse = false;
            }
        }

        private void highlightPrerequisites(IndividualCourseViewModel selectedCourse)
        {
            if (selectedCourse?.Course?.Prerequisites == null)
                return;
            foreach (var preReq in selectedCourse.Course.Prerequisites)
            {
                var courseVM = courseViewModels.Single(c => c.Course.CourseNumber == preReq.CourseNumber);
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
        public string Prereqs => $"{Course.NonProgramPrereqs} {String.Join(" ", Course.Prerequisites?.Select(p => p.CourseNumber))}";

        private bool isPrerequisiteToCurrentCourse;
        public bool IsPrerequisiteToCurrentCourse
        {
            get => isPrerequisiteToCurrentCourse;
            set { Set(ref isPrerequisiteToCurrentCourse, value); }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                Set(ref isSelected, value);
                if (isSelected)
                {
                    Background = Brushes.Turquoise;
                }
                else
                {
                    Background = Brushes.White;
                }
                RaisePropertyChanged(nameof(Background));
            }
        }

        public SolidColorBrush Background { get; set; }

        public override string ToString() => $"{CourseNumber} | IsPrereq={IsPrerequisiteToCurrentCourse}; IsSelected: {IsSelected}";
    }
}
