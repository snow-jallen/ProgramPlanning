using Microsoft.Win32;
using ProgramPlanning.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPlanning.Wpf.ViewModels
{
    public class WpfFileService : FileService
    {
        public override string BrowseFolderPath()
        {
            var fileBrowser = new OpenFileDialog();
            fileBrowser.CheckFileExists = true;
            fileBrowser.DefaultExt = ".xlsx";
            fileBrowser.Filter = "Spreadsheets (*.xlsx)|*.xlsx";
            fileBrowser.Title = "Course Information Spreadsheet";
            if (fileBrowser.ShowDialog() ?? false)
            {
                return fileBrowser.FileName;
            }
            return null;
        }
    }
}
