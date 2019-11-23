using ProgramPlanning.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPlanning.Shared.Services
{
    public interface ICourseInfoRepository
    {
        IEnumerable<Course> GetCourses();
        void SetConnection(ConnectionInfo selectedConnection);
        Task SaveOutcomesAndSkillsAsync(IEnumerable<LearningOutcome> outcomes);
    }
}
