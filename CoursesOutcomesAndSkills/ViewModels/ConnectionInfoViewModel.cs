using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesOutcomesAndSkills.ViewModels
{
    public class ConnectionInfoViewModel
    {
        public ConnectionInfoViewModel()
        {

        }

        public string Host { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string ImportFile { get; set; }
        public string ExportFile { get; set; }
        public RelayCommand ImportCommand { get; private set; }
        public RelayCommand ExportCommand { get; private set; }
    }
}
