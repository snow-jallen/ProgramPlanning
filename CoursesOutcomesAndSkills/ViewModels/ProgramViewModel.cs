using GalaSoft.MvvmLight;
using ProgramPlanning.Shared.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace CoursesOutcomesAndSkills.ViewModels
{
    public class ProgramViewModel : ViewModelBase
    {
        public ProgramViewModel(IEnumerable<Course> courses)
        {
            Courses = courses;
            Outcomes = from c in courses
                       from o in c.Outcomes
                       select o;
            SelectedOutcome = Outcomes.FirstOrDefault();
            FilterText = string.Empty;
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


        public IEnumerable<Course> Courses { get; }
        public IEnumerable<LearningOutcome> Outcomes { get; }
        public IEnumerable<LearningOutcome> FilteredOutcomes => from o in Outcomes
                                                                where o.Name.Contains(FilterText, StringComparison.InvariantCultureIgnoreCase) ||
                                                                      o.Skills.Any(s => s.Name.Contains(FilterText, StringComparison.InvariantCultureIgnoreCase))
                                                                select o;
        private LearningOutcome selectedOutcome;
        public LearningOutcome SelectedOutcome
        {
            get => selectedOutcome;
            set { Set(ref selectedOutcome, value); }
        }

    }
}
