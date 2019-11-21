using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPlanning.Shared.Models
{
    public class LearningOutcome : INotifyPropertyChanged
    {
        private List<Skill> skills;
        private List<LearningOutcome> preOutcomes;
        private List<LearningOutcome> postOutcomes;
        private List<Course> courses;

        public LearningOutcome(int id, string name, string description, IEnumerable<Skill> skills=null, IEnumerable<LearningOutcome> preOutcomes = null, IEnumerable<LearningOutcome> postOutcomes = null, IEnumerable<Course> courses=null)
        {
            this.Id = id;
            this.Name = name;
            Description = description;
            this.skills = new List<Skill>();
            if (skills != null)
                this.skills.AddRange(skills);

            this.preOutcomes = new List<LearningOutcome>();
            if (preOutcomes != null)
                this.preOutcomes.AddRange(preOutcomes);

            this.postOutcomes = new List<LearningOutcome>();
            if (postOutcomes != null)
                this.postOutcomes.AddRange(postOutcomes);

            this.courses = new List<Course>();
            if (courses != null)
                this.courses.AddRange(courses);
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Skill> Skills { get => skills; }
        public IEnumerable<LearningOutcome> PreOutcomes { get => preOutcomes; }
        public IEnumerable<LearningOutcome> PostOutcomes { get => postOutcomes; }
        public List<Course> Courses { get => courses; set { courses = value; } }

        private bool isExpanded;
        public bool IsExpanded
        {
            get => isExpanded;
            set { SetField(ref isExpanded, value); }
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
