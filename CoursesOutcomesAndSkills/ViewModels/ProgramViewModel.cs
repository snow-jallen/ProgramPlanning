using GalaSoft.MvvmLight;
using ProgramPlanning.Shared.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using ProgramPlanning.Shared.Services;

namespace CoursesOutcomesAndSkills.ViewModels
{
    public class ProgramViewModel : ViewModelBase
    {
        public ProgramViewModel(ICourseInfoRepository courseRepository)
        {
            this.courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            refreshCourses();
            SelectedOutcome = Outcomes.FirstOrDefault();
            FilterText = string.Empty;
            MessengerInstance.Register<RefreshDatabaseMessage>(this, (m) => refreshCourses());
        }

        private void refreshCourses()
        {
            Courses = courseRepository.GetCourses();
            Outcomes = from c in Courses
                       from o in c.Outcomes
                       select o;
            RaisePropertyChanged(nameof(Courses));
            RaisePropertyChanged(nameof(Outcomes));
            RaisePropertyChanged(nameof(FilteredOutcomes));
        }

        private string filterText;
        public string FilterText
        {
            get => filterText;
            set
            {
                Set(ref filterText, value);
                RaisePropertyChanged(nameof(FilteredOutcomes));
            }
        }


        public IEnumerable<Course> Courses { get; private set; }
        public IEnumerable<LearningOutcome> Outcomes { get; private set; }
        public IEnumerable<LearningOutcome> FilteredOutcomes => from o in Outcomes
                                                                where (o.Name?.Contains(FilterText, StringComparison.InvariantCultureIgnoreCase) ?? false) ||
                                                                      (o.Description?.Contains(FilterText, StringComparison.InvariantCultureIgnoreCase) ?? false) ||
                                                                      o.Skills.Any(s => s.Name.Contains(FilterText, StringComparison.InvariantCultureIgnoreCase))
                                                                select o;
        private LearningOutcome selectedOutcome;
        private readonly ICourseInfoRepository courseRepository;

        public LearningOutcome SelectedOutcome
        {
            get => selectedOutcome;
            set { Set(ref selectedOutcome, value); }
        }

    }
}
