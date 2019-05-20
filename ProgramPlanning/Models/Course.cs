using System;
using System.Collections.Generic;

namespace ProgramPlanning.Models
{
    public partial class Course
    {
        public Course()
        {
            CourseOutcomes = new HashSet<CourseOutcome>();
        }

        public int Id { get; set; }
        public string Prefix { get; set; }
        public int Num { get; set; }
        public string Title { get; set; }

        public virtual ICollection<CourseOutcome> CourseOutcomes { get; set; }
    }
}
