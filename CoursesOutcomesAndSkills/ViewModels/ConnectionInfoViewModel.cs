using GalaSoft.MvvmLight.Command;
using ProgramPlanning.Shared.Models;
using ProgramPlanning.Shared.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;

namespace CoursesOutcomesAndSkills.ViewModels
{
    public class ConnectionInfoViewModel : IConnectionInfo
    {
        public ConnectionInfoViewModel()
        {
            conn = new ConnectionInfo();
        }

        public ConnectionInfoViewModel(ConnectionInfo conn, IDatabaseManagementService databaseManagement)
        {
            if (conn is null)
            {
                throw new ArgumentNullException(nameof(conn));
            }

            this.databaseManagement = databaseManagement ?? throw new ArgumentNullException(nameof(databaseManagement));
            this.conn = conn;
        }

        public string Nickname { get { return conn.Nickname; } set { conn.Nickname = value; } }
        public string Host { get { return conn.Host; } set { conn.Host = value; } }
        public int Port { get { return conn.Port; } set { conn.Port = value; } }
        public string Database { get { return conn.Database; } set { conn.Database = value; } }
        public string User { get { return conn.User; } set { conn.User = value; } }
        public string Password { get { return conn.Password; } set { conn.Password = value; } }
        public string ImportFile { get; set; }
        public string ExportFile { get; set; }
        public bool RunInDocker { get; set; }
        private RelayCommand importCommand;
        public RelayCommand ImportCommand => importCommand ?? (importCommand = new RelayCommand(() =>
            {
                if (RunInDocker)
                    DatabaseManagement.LoadFromBackupUsingDocker(conn, ImportFile);
                else
                    DatabaseManagement.LoadFromBackupWithoutDocker(conn, ImportFile);
            },
            () =>
            {
                return File.Exists(ImportFile);
            }));
        private RelayCommand exportCommand;
        private IDatabaseManagementService databaseManagement;
        private readonly ConnectionInfo conn;

        public RelayCommand ExportCommand => exportCommand ?? (exportCommand = new RelayCommand(()=>
            {
                if (RunInDocker)
                    DatabaseManagement.DumpDatabaseUsingDocker(conn, ExportFile);
                else
                    DatabaseManagement.DumpDatabaseWithoutDocker(conn, ExportFile);
            },
            () =>
            {
                return !String.IsNullOrWhiteSpace(ExportFile);
            }));

        public IDatabaseManagementService DatabaseManagement
        {
            get => databaseManagement;
            set => databaseManagement = value;
        }
    }
}
