using ProgramPlanning.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesOutcomesAndSkills.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator(ISettingsManager<MySettings> settingsManager, IDatabaseManagementService databaseManagementService)
        {
            if (settingsManager is null)
            {
                throw new ArgumentNullException(nameof(settingsManager));
            }

            if (databaseManagementService is null)
            {
                throw new ArgumentNullException(nameof(databaseManagementService));
            }

            var courseRepository = new BogusCourseInfoRepository();
            var courses = courseRepository.GetCourses();
            MainWindowViewModel = new MainWindowViewModel();
            ProgramViewModel = new ProgramViewModel(courses);
            ReportViewModel = new ReportViewModel();
            CourseViewModel = new CourseViewModel(courses);
            ConfigViewModel = new ConfigViewModel(databaseManagementService, settingsManager);
        }

        public MainWindowViewModel MainWindowViewModel { get; private set; }
        public ProgramViewModel ProgramViewModel { get; private set; }
        public ReportViewModel ReportViewModel { get; private set; }
        public CourseViewModel CourseViewModel { get; private set; }
        public ConfigViewModel ConfigViewModel { get; private set; }
    }
}
