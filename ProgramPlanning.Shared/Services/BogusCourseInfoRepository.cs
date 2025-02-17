﻿using ProgramPlanning.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPlanning.Shared.Services
{
    public class BogusCourseInfoRepository : ICourseInfoRepository
    {
        public BogusCourseInfoRepository()
        {

            var cs1400_lo1 = new LearningOutcome(1, "Need Name", "Know basic data types, control structures, and programming approaches for a current programming language.", new[] {
                new Skill("Basic Data Types"),
                new Skill("Control Structures"),
                new Skill("Programming Approaches")
            });
            var cs1400_lo3 = new LearningOutcome(2, "Need Name", "Understand the social responsibilities of the computing professional and the impact of computing on society.", new[]
            {
                new Skill("Understand social responsibilities"),
            });
            var cs1400_lo2 = new LearningOutcome(3, "Need Name", "Solve problems by developing algorithms and implementing those algorithms using a current programming language.", new[]
            {
                new Skill("Develop algorithms"),
                new Skill("Implement algorithms")
            }, preOutcomes: new[] { cs1400_lo1 }, postOutcomes: new[] { cs1400_lo3 });

            var cs1400 = new Course(1, "CS", 1400, Semester.Year0PreReq, "Intro to Programming - Lecture", prerequisites: null, outcomes: new[] { cs1400_lo1, cs1400_lo2, cs1400_lo3 });
            var cs1405 = new Course(2, "CS", 1405, Semester.Year0PreReq, "Intro to Programming - Lab");
            var cs1410 = new Course(3, "CS", 1410, Semester.Year1Fall, "OO Programming - Lecture", prerequisites: new[] { cs1400, cs1405 });
            var cs1415 = new Course(4, "CS", 1415, Semester.Year1Fall, "00 Programming - Lab");
            var cs2700 = new Course(5, "CS", 2700, Semester.Year1Spring, "Digital Circuits", summary: "This class is great because it has such an awesome summary.  The summary though, really, is definitely what makes this course.  In this course we review the summary, and other summarized articles.  In summary, the summary of this course is summarily great.  That about sums it up.");
            var cs1420 = new Course(6, "CS", 1420, Semester.Year1Spring, "Craziness");

            cs1400_lo1.Courses.Add(cs1400);
            cs1400_lo2.Courses.Add(cs1400);
            cs1400_lo3.Courses.Add(cs1400);

            courses = new List<Course>(new []{
                cs1400, cs1405, cs1410, cs1415, cs2700, cs1420
            });
        }

        List<Course> courses;

        public IEnumerable<Course> GetCourses() => courses.ToArray();

        public void SetConnection(ConnectionInfo selectedConnection)
        {

        }

        public async Task SaveOutcomesAndSkillsAsync(IEnumerable<LearningOutcome> outcomes)
        {
            throw new NotImplementedException();
        }

        public async Task AddLearningOutcomeAsync(Course course, string newLearningOutcomeName, string newLearningOutcomeDescription)
        {
            throw new NotImplementedException();
        }
    }
}
