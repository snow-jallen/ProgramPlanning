using System.Collections.Generic;

namespace ProgramPlanning.Wpf.Services
{
    public class Course
    {
        public string Area { get; set; }
        public int Number { get; set; }
        public string Content { get; set; }
        public IEnumerable<string> Outcomes { get; set; }
    }
}
