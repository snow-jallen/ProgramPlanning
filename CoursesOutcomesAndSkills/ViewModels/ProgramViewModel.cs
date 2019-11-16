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
        }

        public IEnumerable<Course> Courses { get; }
        public IEnumerable<LearningOutcome> Outcomes { get; }
        private LearningOutcome selectedOutcome;
        public LearningOutcome SelectedOutcome
        {
            get => selectedOutcome;
            set { Set(ref selectedOutcome, value); }
        }

    }
}
