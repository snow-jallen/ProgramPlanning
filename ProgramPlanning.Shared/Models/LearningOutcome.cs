﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ProgramPlanning.Shared;

namespace ProgramPlanning.Shared.Models
{
    public class LearningOutcome : INotifyPropertyChanged
    {
        private ObservableCollection<Skill> skills;
        private List<LearningOutcome> preOutcomes;
        private List<LearningOutcome> postOutcomes;
        private List<Course> courses;

        public LearningOutcome(int id, string name, string description, IEnumerable<Skill> skills=null, IEnumerable<LearningOutcome> preOutcomes = null, IEnumerable<LearningOutcome> postOutcomes = null, IEnumerable<Course> courses=null)
        {
            this.Id = id;
            this.Name = name;
            Description = description;
            this.skills = new ObservableCollection<Skill>();
            if (skills != null)
                skills.ToList().ForEach(s => this.skills.Add(s));
            this.skills.CollectionChanged += (s, e) => IsDirty = true;

            this.preOutcomes = new List<LearningOutcome>();
            if (preOutcomes != null)
                this.preOutcomes.AddRange(preOutcomes);

            this.postOutcomes = new List<LearningOutcome>();
            if (postOutcomes != null)
                this.postOutcomes.AddRange(postOutcomes);

            this.courses = new List<Course>();
            if (courses != null)
                this.courses.AddRange(courses);
            refreshHeading();

            IsDirty = false;
        }

        private void refreshHeading()
        {
            Heading = $"{Name ?? Description.Truncate(90)}";
        }

        public int Id { get; set; }
        private string name;
        public string Name
        {
            get => name;
            set
            {
                SetField(ref name, value);
                refreshHeading();
                IsDirty = true;
            }
        }
        private string description;
        public string Description
        {
            get => description;
            set
            {
                SetField(ref description, value);
                IsDirty = true;
            }
        }

        public ObservableCollection<Skill> Skills { get => skills; }
        public IEnumerable<LearningOutcome> PreOutcomes { get => preOutcomes; }
        public IEnumerable<LearningOutcome> PostOutcomes { get => postOutcomes; }
        public List<Course> Courses
        {
            get => courses;
            set
            {
                courses = value;
                CoursesText = String.Join(", ", Courses.Select(c => c.CourseNumber));
            }
        }
        private string heading;
        public string Heading
        {
            get => heading;
            set { SetField(ref heading, value); }
        }

        public string CoursesText { get; private set; }

        private bool isExpanded;
        public bool IsExpanded
        {
            get => isExpanded;
            set { SetField(ref isExpanded, value); }
        }

        private bool isDirty;
        public bool IsDirty
        {
            get => isDirty;
            set { SetField(ref isDirty, value); }
        }


        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion

    }
}
