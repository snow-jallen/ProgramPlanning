using GalaSoft.MvvmLight;
using ProgramPlanning.Shared.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using ProgramPlanning.Shared.Services;
using GalaSoft.MvvmLight.CommandWpf;
using System.Threading.Tasks;

namespace CoursesOutcomesAndSkills.ViewModels
{
    public class ProgramViewModel : ViewModelBase
    {
        private const string defaultSaveButtonContent = "💾 Save Changes";

        public ProgramViewModel(ICourseInfoRepository courseRepository)
        {
            this.courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            refreshCourses();
            SelectedOutcome = Outcomes.FirstOrDefault();
            FilterText = string.Empty;
            MessengerInstance.Register<RefreshDatabaseMessage>(this, (m) => refreshCourses());
            SaveButtonContent = defaultSaveButtonContent;
            CanSave = true;
        }

        private void refreshCourses()
        {
            Courses = courseRepository.GetCourses();
            Outcomes = new List<LearningOutcome>(from c in Courses
                                                 from o in c.Outcomes
                                                 select o);
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
        public List<LearningOutcome> Outcomes { get; private set; }
        public IEnumerable<LearningOutcome> FilteredOutcomes => from o in Outcomes
                                                                where (o.Name?.Contains(FilterText, StringComparison.InvariantCultureIgnoreCase) ?? false) ||
                                                                      (o.Description?.Contains(FilterText, StringComparison.InvariantCultureIgnoreCase) ?? false) ||
                                                                      ((o.Skills?.Any(s => s.Name.Contains(FilterText, StringComparison.InvariantCultureIgnoreCase)) ?? false) ||
                                                                      (o.CoursesText?.Contains(FilterText, StringComparison.InvariantCultureIgnoreCase) ?? false))
                                                                select o;
        private LearningOutcome selectedOutcome;
        private readonly ICourseInfoRepository courseRepository;

        public LearningOutcome SelectedOutcome
        {
            get => selectedOutcome;
            set { Set(ref selectedOutcome, value); }
        }

        private bool canSave;
        public bool CanSave
        {
            get => canSave;
            set
            {
                Set(ref canSave, value);
                SaveOutcomesAndSkills.RaiseCanExecuteChanged();
            }
        }

        private string saveButtonContent;
        public string SaveButtonContent
        {
            get => saveButtonContent;
            set { Set(ref saveButtonContent, value); }
        }

        private RelayCommand saveOutcomesAndSkills;
        public RelayCommand SaveOutcomesAndSkills => saveOutcomesAndSkills ?? (saveOutcomesAndSkills = new RelayCommand(() =>
            {
                CanSave = false;
                SaveButtonContent = "Saving...";
                Task.Run(() => courseRepository.SaveOutcomesAndSkillsAsync(Outcomes))
                .ContinueWith(t =>
                {
                    if (t.Exception != null)
                        throw t.Exception;
                    CanSave = true;
                    SaveButtonContent = defaultSaveButtonContent;
                }, TaskScheduler.FromCurrentSynchronizationContext());
            },
            ()=>
            {
                return CanSave;
            }));

        private RelayCommand collapseAll;
        public RelayCommand CollapseAll => collapseAll ?? (collapseAll = new RelayCommand(() =>
        {
            foreach (var o in Outcomes)
                o.IsExpanded = false;
        }));

        private RelayCommand expandAll;

        public RelayCommand ExpandAll => expandAll ?? (expandAll = new RelayCommand(() =>
        {
            foreach (var o in Outcomes)
                o.IsExpanded = true;
        }));
    }
}
