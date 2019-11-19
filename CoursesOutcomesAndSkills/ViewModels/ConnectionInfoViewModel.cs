using GalaSoft.MvvmLight.Command;
using ProgramPlanning.Shared.Models;
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

        }

        public ConnectionInfoViewModel(ConnectionInfo conn)
            : this()
        {
            if (conn is null)
            {
                throw new ArgumentNullException(nameof(conn));
            }

            Nickname = conn.Nickname;
            Host = conn.Host;
            Port = conn.Port;
            Database = conn.Database;
            User = conn.User;
            Password = conn.Password;
        }

        public string Nickname { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string ImportFile { get; set; }
        public string ExportFile { get; set; }
        public bool RunInDocker { get; set; }
        private RelayCommand importCommand;
        public RelayCommand ImportCommand => importCommand ?? (importCommand = new RelayCommand(() =>
            {
                var folder = Path.GetDirectoryName(ImportFile);
                if (!Directory.Exists(folder))
                    throw new DirectoryNotFoundException(ImportFile);

                var file = Path.GetFileName(ImportFile);

                string dockerRun = $"docker run --rm -e PGPASSWORD={Password} -v \"{folder}:/usr/backupoutput\" -it postgres ";
                var cmd = $"{(RunInDocker?dockerRun:String.Empty)} dropdb -h {Host} -p {Port} -U {User} {Database};";
                cmd += $"{(RunInDocker ? dockerRun : String.Empty)} createdb -h {Host} -p {Port} -U {User} {Database};";

                var backupScript = RunInDocker ?
                    $"/usr/backupoutput/{file}" :
                    ImportFile;
                cmd += $"{(RunInDocker ? dockerRun : String.Empty)} psql -h {Host} -p {Port} -U {User} -d {Database} -f {backupScript};";
                Clipboard.SetText(cmd);
                Process.Start("powershell.exe", $"-NoExit -c {{{cmd}}}");
            },
            () =>
            {
                return File.Exists(ImportFile);
            }));
        private RelayCommand exportCommand;
        public RelayCommand ExportCommand => exportCommand ?? (exportCommand = new RelayCommand(()=>
            {

            },
            () =>
            {
                return !String.IsNullOrWhiteSpace(ExportFile);
            }));
    }
}
