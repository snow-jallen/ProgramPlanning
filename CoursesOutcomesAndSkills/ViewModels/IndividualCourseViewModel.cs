using GalaSoft.MvvmLight;
using ProgramPlanning.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using ProgramPlanning.Shared.Services;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Media;
using AsyncAwaitBestPractices.MVVM;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;

namespace CoursesOutcomesAndSkills.ViewModels
{
    public class IndividualCourseViewModel : ViewModelBase
    {
        public IndividualCourseViewModel(Course course, ICourseInfoRepository courseRepository)
        {
            Course = course ?? throw new ArgumentNullException(nameof(course));

            this.courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
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
            set
            {
                Set(ref isPrerequisiteToCurrentCourse, value);
                //Debug.WriteLine($"{Course.CourseNumber} {(value ? "is" : "is not")} a pre-req to current course.");
            }
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

        private string newLearningOutcome;
        public string NewLearningOutcomeName
        {
            get => newLearningOutcome;
            set
            {
                Set(ref newLearningOutcome, value);
                AddNewLearningOutcome.RaiseCanExecuteChanged();
            }
        }

        private string newLearningOutcomeDescription;
        public string NewLearningOutcomeDescription
        {
            get => newLearningOutcomeDescription;
            set
            {
                Set(ref newLearningOutcomeDescription, value);
                AddNewLearningOutcome.RaiseCanExecuteChanged();
            }
        }

        private bool busyAddingLearningOutcome = false;
        public bool BusyAddingLearningOutcome
        {
            get => busyAddingLearningOutcome;
            set
            {
                Set(ref busyAddingLearningOutcome, value);
                AddNewLearningOutcome.RaiseCanExecuteChanged();
            }
        }
        private AsyncCommand addNewLearningOutcome;
        private readonly ICourseInfoRepository courseRepository;

        public AsyncCommand AddNewLearningOutcome => addNewLearningOutcome ?? (addNewLearningOutcome = new AsyncCommand(addLearningOutcome, addLearningOutcome_CanExecute));

        private bool addLearningOutcome_CanExecute(object arg)
        {
            return !string.IsNullOrWhiteSpace(NewLearningOutcomeName) &&
                !string.IsNullOrWhiteSpace(NewLearningOutcomeDescription) &&
                !BusyAddingLearningOutcome;
        }

        private async Task addLearningOutcome()
        {
            BusyAddingLearningOutcome = true;

            await courseRepository.AddLearningOutcomeAsync(Course, NewLearningOutcomeName, NewLearningOutcomeDescription);
            NewLearningOutcomeName = null;
            NewLearningOutcomeDescription = null;

            BusyAddingLearningOutcome = false;
            AddNewLearningOutcomeVisibility = Visibility.Collapsed;
        }

        private RelayCommand showAddNewLearningCommand;
        public RelayCommand ShowAddNewLearningOutcome => showAddNewLearningCommand ?? (showAddNewLearningCommand = new RelayCommand(() => AddNewLearningOutcomeVisibility = Visibility.Visible));

        private Visibility addNewLearningOutcomeVisibility = Visibility.Collapsed;
        public Visibility AddNewLearningOutcomeVisibility
        {
            get => addNewLearningOutcomeVisibility;
            set { Set(ref addNewLearningOutcomeVisibility, value); }
        }

        private bool canSave = true;
        public bool CanSave
        {
            get => canSave;
            set
            {
                Set(ref canSave, value);
                SaveOutcomesAndSkills.RaiseCanExecuteChanged();
            }
        }

        private const string defaultSaveButtonContent = "💾 Save Changes";
        private string saveButtonContent = defaultSaveButtonContent;
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
            () =>
            {
                return CanSave;
            }));
    }
}
