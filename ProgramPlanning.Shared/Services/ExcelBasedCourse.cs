﻿using System.Collections.Generic;

namespace ProgramPlanning.Shared.Services
{
    public class ExcelBasedCourse
    {
        public string Area { get; set; }
        public int Number { get; set; }
        public string Name => $"{Area} {Number}";
        public string Title { get; set; }
        public string Content { get; set; }
        public IEnumerable<string> Outcomes { get; set; }
        public IEnumerable<string> Prerequisites { get; set; }
        public IEnumerable<string> Corequisites { get; set; }
        public string Semester { get; set; }
    }
}
