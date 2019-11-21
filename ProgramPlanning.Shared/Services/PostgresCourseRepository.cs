using Dapper;
using GalaSoft.MvvmLight.Messaging;
using Npgsql;
using ProgramPlanning.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPlanning.Shared.Services
{
    public class PostgresCourseRepository : ICourseInfoRepository
    {
        private string connectionString;

        public PostgresCourseRepository()
        {
        }

        public IEnumerable<Course> GetCourses()
        {
            if (courses == null)
                refreshCourses();

            return courses;
        }

        private void refreshCourses()
        {
            courses = new List<Course>();
            if (!String.IsNullOrEmpty(connectionString))
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    var sql = @"select id, num, title, summary, semester, prefix, nonprogramprereq from public.course;
                                select course_id, prereq_id from ""coursePreReq"";
                                select id, name, description from learningoutcome;
                                select course_id, learningoutcome_id from course_learningoutcome;";
                    using(var multi = connection.QueryMultiple(sql))
                    {
                        var dbCourses = multi.Read<DbCourse>();
                        var coursePreReqs = multi.Read<DbCoursePreReq>();
                        var dbOutcomes = multi.Read<DbLearningOutcome>();
                        var dbCourseOutcomes = multi.Read<DbCourseLearningOutcome>();

                        courses.AddRange(from c in dbCourses
                                         let prereqs = from p in coursePreReqs
                                                       where p.Course_Id == c.Id
                                                       let prereq = dbCourses.Single(prereq => prereq.Id == p.Prereq_Id)
                                                       select CourseFromDbCourse(prereq)
                                         let outcomes = from co in dbCourseOutcomes
                                                        where co.Course_Id == c.Id
                                                        let outcome = dbOutcomes.Single(o => o.Id == co.LearningOutcome_Id)
                                                        select new LearningOutcome(outcome.Name, outcome.Description)
                                         select CourseFromDbCourse(c, prereqs, outcomes)); ;
                    }
                }
                Messenger.Default.Send(new RefreshDatabaseMessage());
            }
        }

        private List<Course> courses;

        public void SetConnection(ConnectionInfo selectedConnection)
        {
            var c = selectedConnection;
            connectionString = $"Server={c.Host}; Port={c.Port}; Database={c.Database}; User ID={c.User}; Password={c.Password};";

            refreshCourses();
        }

        public DbCourse GetDbCourse(string prefix, int num)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                var sql = "select id, num, title, summary, semester, prefix, nonprogramprereq from public.course where prefix = @prefix and num = @num";
                var course = connection.QuerySingle<DbCourse>(sql, new { prefix = prefix, num = num });
                return course;
            }
        }

        public DbLearningOutcome AddOutcome(DbLearningOutcome learningOutcome)
        {
            using(var conn = new NpgsqlConnection(connectionString))
            {
                var sql = "insert into learningoutcome (name, description) values (@name, @description)";
                conn.Execute(sql, new { name = learningOutcome.Name, description = learningOutcome.Description });
                var newOutcome = conn.QuerySingle<DbLearningOutcome>("select id, name, description from learningoutcome where description = @description order by id desc fetch first 1 row only",
                    new { description = learningOutcome.Description });
                return newOutcome;
            }
        }

        public void AddCourseOutcomeLink(DbCourseLearningOutcome outcomeCourseLink)
        {
            using(var conn = new NpgsqlConnection(connectionString))
            {
                var sql = "insert into course_learningoutcome (course_id, learningoutcome_id) values (@CourseId, @LearningOutcomeId)";
                conn.Execute(sql, new { CourseId = outcomeCourseLink.Course_Id, LearningOutcomeId = outcomeCourseLink.LearningOutcome_Id });
            }
        }

        public static Course CourseFromDbCourse(DbCourse dbCourse, IEnumerable<Course> prereqs=null, IEnumerable<LearningOutcome> outcomes=null)
        {
            var c = new Course(
                dbCourse.Id,
                dbCourse.Prefix,
                dbCourse.Num,
                translateSemester(dbCourse.Semester),
                dbCourse.Title,
                dbCourse.Summary,
                dbCourse.NonProgramPreReq,
                prereqs,
                outcomes);
            return c;
        }

        private static Semester translateSemester(string semester)
        {
            switch (semester)
            {
                case "Yr0-PreReq":
                    return Semester.Year0PreReq;
                case "Yr1 - Fall":
                    return Semester.Year1Fall;
                case "Yr1 - Spring":
                    return Semester.Year1Spring;
                case "Yr2 - Fall":
                    return Semester.Year2Fall;
                case "Yr2 - Spring":
                    return Semester.Year2Spring;
                case "Yr3 - Fall":
                    return Semester.Year3Fall;
                case "Yr3 - Spring":
                    return Semester.Year3Spring;
                case "Yr4 - Fall":
                    return Semester.Year4Fall;
                case "Yr4 - Spring":
                    return Semester.Year4Spring;
                default:
                    return Semester.Year0PreReq;
            }
        }
    }

    public class DbCourse
    {
        public DbCourse()
        {
            PreRequisites = new List<DbCourse>();
        }
        public int Id { get; set; }
        public int Num { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Semester { get; set; }
        public string Prefix { get; set; }
        public string NonProgramPreReq { get; set; }
        public List<DbCourse> PreRequisites { get; set; }
    }

    public class DbCoursePreReq
    {
        public int Course_Id { get; set; }
        public int Prereq_Id { get; set; }
    }

    public class DbCourseLearningOutcome
    {
        public int Course_Id { get; set; }
        public int LearningOutcome_Id { get; set; }
    }

    public class DbLearningOutcome
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class DbLearningOutcomePost
    {
        public int Here { get; set; }
        public int Post { get; set; }
    }

    public class DbLearingOutcomePre
    {
        public int Here { get; set; }
        public int Pre { get; set; }
    }

    public class DbSkill
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
