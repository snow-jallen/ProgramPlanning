using System.Collections.Generic;

namespace ProgramPlanning.Shared.Services
{
    public interface IFileService
    {
        IEnumerable<Course> ReadCourses(string excelFile);
        string BrowseFolderPath();
    }
}
