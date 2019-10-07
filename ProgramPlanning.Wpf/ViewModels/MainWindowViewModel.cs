using OfficeOpenXml;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using ProgramPlanning.Wpf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPlanning.Wpf.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;
        private readonly IFileService fileService;

        public MainWindowViewModel(IRegionManager regionManager, IFileService fileService)
        {
            this.regionManager = regionManager;
            this.fileService = fileService;
            //regionManager.RegisterViewWithRegion(Constants.ContentRegion, typeof(ProgramPlanning.Wpf.Views.DiagramView));

            BrowseCommand = new DelegateCommand(browseCommand_Execute);
            FilePath = @"C:\git\ProgramPlanning\ProgramPlanning.Wpf\Learning Outcomes.xlsx";
        }

        private string filePath;
        public string FilePath
        {
            get { return filePath; }
            set { SetProperty(ref filePath, value); }
        }

        public DelegateCommand BrowseCommand { get; private set; }
        private void browseCommand_Execute()
        {
            var courses = fileService.ReadCourses(FilePath);
        }
    }
}
