using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPlanning.Shared.Models
{
    public class LearningOutcome
    {
        private List<Skill> skills;
        private List<LearningOutcome> preOutcomes;
        private List<LearningOutcome> postOutcomes;
        private List<Course> courses;

        public LearningOutcome(string name, IEnumerable<Skill> skills, IEnumerable<LearningOutcome> preOutcomes = null, IEnumerable<LearningOutcome> postOutcomes = null, IEnumerable<Course> courses=null)
        {
            this.Name = name;
            this.skills = new List<Skill>(skills);

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
        public IEnumerable<Skill> Skills { get => skills; }
        public IEnumerable<LearningOutcome> PreOutcomes { get => preOutcomes; }
        public IEnumerable<LearningOutcome> PostOutcomes { get => postOutcomes; }
        public ICollection<Course> Courses { get => courses; }
    }
}
