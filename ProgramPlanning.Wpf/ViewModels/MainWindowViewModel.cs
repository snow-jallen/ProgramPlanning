using OfficeOpenXml;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using ProgramPlanning.Shared.Services;
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

            BrowseCommand = new DelegateCommand(browseCommand_Execute);
            ReadFileCommand = new DelegateCommand(readFileCommand_Execute);
            FilePath = @"C:\git\ProgramPlanning\ProgramPlanning.Web\Learning Outcomes.xlsx";
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
            FilePath = fileService.BrowseFolderPath();
        }

        public DelegateCommand ReadFileCommand { get; private set; }
        private void readFileCommand_Execute()
        {
            var courses = fileService.ReadCourses(FilePath);
            var navParams = new NavigationParameters();
            navParams.Add("courses", courses);
            //regionManager.RequestNavigate(Constants.ContentRegion, "DiagramKey", (nr)=>
            //{
            //});
            regionManager.RequestNavigate(Constants.ContentRegion, "DiagramView", navParams);
        }
    }
}
