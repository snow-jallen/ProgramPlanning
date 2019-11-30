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
using AsyncAwaitBestPractices.MVVM;

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
            IsDirty = false;
        }

        private void refreshCourses()
        {
            Courses = courseRepository.GetCourses();
            Outcomes = new List<LearningOutcome>(from c in Courses
                                                 from o in c.Outcomes
                                                 select o);
            Outcomes.ForEach(p => p.PropertyChanged += (s, e) =>  IsDirty = Outcomes.Any(q => q.IsDirty));

            RaisePropertyChanged(nameof(Courses));
            RaisePropertyChanged(nameof(Outcomes));
            RaisePropertyChanged(nameof(FilteredOutcomes));
        }

        private bool isDirty;
        public bool IsDirty
        {
            get => isDirty;
            set
            {
                Set(ref isDirty, value);
                RaisePropertyChanged(nameof(OutcomesAndSkillsHeadingWithIsDirtyFlag));
            }
        }

        public string OutcomesAndSkillsHeadingWithIsDirtyFlag => "Outcomes & Skills " + (IsDirty ? "(Changed!)" : String.Empty);

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
                                                                      ((o.Skills?.Any(s => s.Name?.Contains(FilterText, StringComparison.InvariantCultureIgnoreCase) ?? false) ?? false) ||
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

        private AsyncCommand saveOutcomesAndSkills;
        public AsyncCommand SaveOutcomesAndSkills => saveOutcomesAndSkills ?? (saveOutcomesAndSkills = new AsyncCommand(async () =>
            {
                CanSave = false;
                SaveButtonContent = "Saving...";
                await courseRepository.SaveOutcomesAndSkillsAsync(Outcomes);
                CanSave = true;
                SaveButtonContent = defaultSaveButtonContent;
            },
            (o)=>
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

        private RelayCommand moveSelectedPostOutcomeToSelectedOutcome;
        public RelayCommand MoveSelectedPostOutcomeToSelectedOutcome => moveSelectedPostOutcomeToSelectedOutcome ?? (moveSelectedPostOutcomeToSelectedOutcome = new RelayCommand(() =>
        {
            SelectedOutcome = SelectedPostOutcome;
        }));

        private LearningOutcome selectedPostOutcome;
        public LearningOutcome SelectedPostOutcome
        {
            get => selectedPostOutcome;
            set { Set(ref selectedPostOutcome, value); }
        }

        private RelayCommand moveSelectedPreOutcomeToSelectedOutcome;
        public RelayCommand MoveSelectedPreOutcomeToSelectedOutcome => moveSelectedPreOutcomeToSelectedOutcome ?? (moveSelectedPreOutcomeToSelectedOutcome = new RelayCommand(() =>
        {
            SelectedOutcome = SelectedPreOutcome;
        }));

        private LearningOutcome selectedPreOutcome;
        public LearningOutcome SelectedPreOutcome
        {
            get => selectedPreOutcome;
            set { Set(ref selectedPreOutcome, value); }
        }

    }
}
