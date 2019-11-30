using GalaSoft.MvvmLight;
using ProgramPlanning.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ProgramPlanning.Shared.Services;
using System.Diagnostics;

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
            var previousSelectedCourseNumber = SelectedCourse?.CourseNumber;
            courseViewModels = new List<IndividualCourseViewModel>(from c in courseRepository.GetCourses()
                                                                   select new IndividualCourseViewModel(c, courseRepository));
            PreReqCourses = new List<IndividualCourseViewModel>(courseViewModels.Where(c => c.Semester == Semester.Year0PreReq));
            Year1Fall = new List<IndividualCourseViewModel>(courseViewModels.Where(c => c.Semester == Semester.Year1Fall));
            Year1Spring = new List<IndividualCourseViewModel>(courseViewModels.Where(c => c.Semester == Semester.Year1Spring));
            Year2Fall = new List<IndividualCourseViewModel>(courseViewModels.Where(c => c.Semester == Semester.Year2Fall));
            Year2Spring = new List<IndividualCourseViewModel>(courseViewModels.Where(c => c.Semester == Semester.Year2Spring));
            Year3Fall = new List<IndividualCourseViewModel>(courseViewModels.Where(c => c.Semester == Semester.Year3Fall));
            Year3Spring = new List<IndividualCourseViewModel>(courseViewModels.Where(c => c.Semester == Semester.Year3Spring));
            Year4Fall = new List<IndividualCourseViewModel>(courseViewModels.Where(c => c.Semester == Semester.Year4Fall));
            Year4Spring = new List<IndividualCourseViewModel>(courseViewModels.Where(c => c.Semester == Semester.Year4Spring));

            if (previousSelectedCourseNumber != null)
                SelectedCourse = courseViewModels.FirstOrDefault(c => c.CourseNumber == previousSelectedCourseNumber);
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

        private bool isACourseSelected;
        public bool IsACourseSelected
        {
            get => isACourseSelected;
            set { Set(ref isACourseSelected, value); }
        }

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
                {
                    selectedCourse.IsSelected = true;
                    IsACourseSelected = true;
                }
                else
                    IsACourseSelected = false;
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
}
