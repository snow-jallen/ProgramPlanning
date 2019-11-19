using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ProgramPlanning.Shared.Models;
using ProgramPlanning.Shared.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CoursesOutcomesAndSkills.ViewModels
{
    public class ConfigViewModel : ViewModelBase
    {
        private readonly ISettingsManager<MySettings> settingsManager;
        private MySettings mySettings;

        public ConfigViewModel(ISettingsManager<MySettings> settingsManager)
        {
            this.settingsManager = settingsManager ?? throw new ArgumentNullException(nameof(settingsManager));
            mySettings = settingsManager.LoadSettings() ?? new MySettings();

            Connections = new List<ConnectionInfoViewModel>();
            foreach(var conn in mySettings.Connections)
            {
                Connections.Add(new ConnectionInfoViewModel(conn));
            }

            RunInDocker = true;
        }

        public List<ConnectionInfoViewModel> Connections { get; private set; }

        private bool runInDocker;
        public bool RunInDocker
        {
            get => runInDocker;
            set
            {
                Set(ref runInDocker, value);
                foreach (var c in Connections)
                    c.RunInDocker = value;
            }
        }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand => saveCommand ?? (saveCommand = new RelayCommand(() =>
        {
            SaveCommandOutput = null;
            mySettings.Connections.Clear();
            mySettings.Connections.AddRange(from c in Connections
                                            select new ConnectionInfo
                                            {
                                                Host = c.Host,
                                                Port = c.Port,
                                                Database = c.Database,
                                                User = c.User,
                                                Password = c.Password,
                                                Nickname = c.Nickname
                                            });
            settingsManager.SaveSettings(mySettings);
            SaveCommandOutput = "Saved OK";
        }));

        private string saveCommandOutput;
        public string SaveCommandOutput
        {
            get => saveCommandOutput;
            set { Set(ref saveCommandOutput, value); }
        }

    }
}
