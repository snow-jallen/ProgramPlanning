using System.Collections.Generic;

namespace ProgramPlanning.Wpf.Services
{
    public interface IFileService
    {
        IEnumerable<Course> ReadCourses(string excelFile);
        string BrowseFolderPath();
    }
}
