using System;
using System.Collections.Generic;

namespace ProgramPlanning.Models
{
    public partial class CourseOutcome
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Outcome { get; set; }

        public virtual Course Course { get; set; }
    }
}
