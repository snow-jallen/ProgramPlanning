using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPlanning.Shared.Models
{
    public class Course
    {
        private List<Course> prerequisites;
        private List<LearningOutcome> outcomes;

        public Course(string prefix, int number, string semester, string title, string summary = null, IEnumerable<Course> prerequisites = null, IEnumerable<LearningOutcome> outcomes = null)
        {
            this.Prefix = prefix;
            this.Number = number;
            this.Title = title;
            this.Semester = semester;
            this.Summary = summary;
            this.prerequisites = new List<Course>();
            if (prerequisites != null)
                this.prerequisites.AddRange(prerequisites);

            this.outcomes = new List<LearningOutcome>();
            if (outcomes != null)
                this.outcomes.AddRange(outcomes);
        }

        public int Id { get; set; }
        public string Prefix { get; set; }
        public int Number { get; set; }
        public string CourseNumber => $"{Prefix} {Number}";
        public string Title { get; set; }
        public string Semester { get; set; }
        public string Summary { get; set; }
        public IEnumerable<Course> Prerequisites { get => prerequisites; }
        public IEnumerable<LearningOutcome> Outcomes { get => outcomes; }
    }
}
