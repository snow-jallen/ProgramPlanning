using System.Collections.Generic;

namespace ProgramPlanning.Shared.Services
{
    public interface IFileService
    {
        IEnumerable<ExcelBasedCourse> ReadCourses(string excelFile);
        string BrowseFolderPath();
    }
}
