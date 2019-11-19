using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ProgramPlanning.Shared.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesOutcomesAndSkills.ViewModels
{
    public class ConfigViewModel : ViewModelBase
    {
        private readonly ISettingsManager<MySettings> settingsManager;
        private MySettings mySettings;

        public ConfigViewModel(ISettingsManager<MySettings> settingsManager)
        {
            this.settingsManager = settingsManager;
            mySettings = settingsManager.LoadSettings() ?? new MySettings();

            Connections2 = mySettings.Connections;
        }

        public List<ConnectionInfoViewModel> Connections2 { get; set; }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand => saveCommand ?? (saveCommand = new RelayCommand(() =>
        {
            settingsManager.SaveSettings(mySettings);
        }));
    }
}
