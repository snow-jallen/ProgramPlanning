﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPlanning.Shared.Models
{
    public class Course
    {
        private List<Course> prerequisites;
        private ObservableCollection<LearningOutcome> outcomes;

        public Course(int id, string prefix, int number, Semester semester, string title, string summary = null, string nonProgramPrereqs=null, IEnumerable<Course> prerequisites = null, IEnumerable<LearningOutcome> outcomes = null)
        {
            this.Id = id;
            this.Prefix = prefix;
            this.Number = number;
            this.Title = title;
            this.Semester = semester;
            this.Summary = summary;
            this.NonProgramPrereqs = NonProgramPrereqs;
            this.prerequisites = new List<Course>();
            if (prerequisites != null)
                this.prerequisites.AddRange(prerequisites);

            this.outcomes = new ObservableCollection<LearningOutcome>();
            if (outcomes != null)
            {
                foreach (var o in outcomes)
                    this.outcomes.Add(o);
            }
        }

        public int Id { get; set; }
        public string Prefix { get; set; }
        public int Number { get; set; }
        public string CourseNumber => $"{Prefix} {Number}";
        public string Title { get; set; }
        public Semester Semester { get; set; }
        public string Summary { get; set; }
        public string NonProgramPrereqs { get; set; }
        public IEnumerable<Course> Prerequisites { get => prerequisites; }
        public ObservableCollection<LearningOutcome> Outcomes { get => outcomes; }
    }

    public enum Semester
    {
        Year0PreReq,
        Year1Fall,
        Year1Spring,
        Year2Fall,
        Year2Spring,
        Year3Fall,
        Year3Spring,
        Year4Fall,
        Year4Spring,
    }
}
