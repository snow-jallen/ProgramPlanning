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
            using (var connection = new NpgsqlConnection(connectionString))
            {
                var rows = connection.Query<QueryRow>("select [columns] from [tables] [where]", new { ParamName = "Bogus" });
                foreach(var row in rows)
                {
                    //map row columns to course, learning outcome and skill
                }
            }

            return courses;
        }

        public object TestConnection()
        {
            string create = "create table Customers (CustomerName text)";
            string sql = "INSERT INTO Customers (CustomerName) Values (@CustomerName);";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Execute(create);

                var affectedRows = connection.Execute(sql, new { CustomerName = "Mark" });

                Console.WriteLine(affectedRows);

                var customer = connection.Query<Customer>("Select * FROM CUSTOMERS WHERE CustomerName = 'Mark'").ToList();

            }
            return null;
        }

        private class Customer
        {
            string CustomerName { get; set; }
        }
    }

    public class QueryRow
    {

    }
}
