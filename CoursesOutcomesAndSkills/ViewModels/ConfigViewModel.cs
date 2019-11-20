using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ProgramPlanning.Shared.Models;
using ProgramPlanning.Shared.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace CoursesOutcomesAndSkills.ViewModels
{
    public class ConfigViewModel : ViewModelBase
    {
        private readonly IDatabaseManagementService databaseManagement;
        private readonly ISettingsManager<MySettings> settingsManager;
        private MySettings mySettings;

        public ConfigViewModel(IDatabaseManagementService databaseManagement, ISettingsManager<MySettings> settingsManager)
        {
            this.databaseManagement = databaseManagement ?? throw new ArgumentNullException(nameof(databaseManagement));
            this.settingsManager = settingsManager ?? throw new ArgumentNullException(nameof(settingsManager));
            mySettings = settingsManager.LoadSettings() ?? new MySettings();

            Connections = new List<ConnectionInfoViewModel>();
            foreach(var conn in mySettings.Connections)
            {
                Connections.Add(new ConnectionInfoViewModel(conn, databaseManagement));
            }

            RunInDocker = true;
            if(databaseManagement.HasPostgresInstalled)
                RunInDocker = false;
            else if (!databaseManagement.HasDockerInstalled)
                NeitherDockerNorPostgresInstalled = "You have neither Docker nor Postgres installed. :(";
        }

        public List<ConnectionInfoViewModel> Connections { get; private set; }

        private bool runInDocker;
        public bool RunInDocker
        {
            get => runInDocker;
            set
            {
                Set(ref runInDocker, value);
                RaisePropertyChanged(nameof(RunWithoutDocker));
                foreach (var c in Connections)
                    c.RunInDocker = value;
            }
        }
        public bool RunWithoutDocker
        {
            get { return !RunInDocker; }
            set { RunInDocker = !value; }
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
            Task.Run(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(5));
                SaveCommandOutput = null;
            });

            foreach (var c in Connections)
            {
                //new items don't get databaseManagement via dependency injection, so we have to use property injection after the fact.
                c.DatabaseManagement = databaseManagement;
            }
        }));

        private string saveCommandOutput;
        public string SaveCommandOutput
        {
            get => saveCommandOutput;
            set { Set(ref saveCommandOutput, value); }
        }

        public string NeitherDockerNorPostgresInstalled { get; }
    }
}
