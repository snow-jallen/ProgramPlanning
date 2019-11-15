using Dapper;
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
        private readonly string connectionString;

        public PostgresCourseRepository(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("message", nameof(connectionString));
            }

            this.connectionString = connectionString;
        }

        public IEnumerable<Course> GetCourses()
        {
            var courses = new List<Course>();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                var rows = conn.Query<QueryRow>("select [columns] from [tables] [where]", new { ParamName = "Bogus" });
                foreach(var row in rows)
                {
                    //map row columns to course, learning outcome and skill
                }
            }

            return courses;
        }
    }

    public class QueryRow
    {

    }
}
