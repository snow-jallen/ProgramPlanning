using ProgramPlanning.Shared.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ProgramPlanning.Shared.Services
{
    public class DatabaseManagementService : IDatabaseManagementService
    {
        public DatabaseManagementService()
        {
            try
            {
                HasDockerInstalled = Directory.GetFiles(
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Docker"),
                    "docker.exe", SearchOption.AllDirectories).Length > 0;
            }
            catch { HasDockerInstalled = false; }
            try
            {
                HasPostgresInstalled = Directory.GetFiles(
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "PostgreSQL"),
                    "psql.exe", SearchOption.AllDirectories).Length > 0;
            }
            catch { HasPostgresInstalled = false; }
        }

        public bool HasDockerInstalled { get; private set; }
        public bool HasPostgresInstalled { get; private set; }

        public void LoadFromBackupUsingDocker(ConnectionInfo info, string backupFile) => loadFromBackup(info, backupFile, runInDocker: true);
        public void LoadFromBackupWithoutDocker(ConnectionInfo info, string backupFile) => loadFromBackup(info, backupFile, runInDocker: false);

        private void loadFromBackup(ConnectionInfo info, string importFile, bool runInDocker)
        {
            var folder = Path.GetDirectoryName(importFile);
            if (!Directory.Exists(folder))
                throw new DirectoryNotFoundException(importFile);

            var file = Path.GetFileName(importFile);

            string dockerRun = $"docker run --rm -e PGPASSWORD={info.Password} -v \"{folder}:/usr/backupoutput\" -it postgres ";
            var backupScript = runInDocker ?
                $"/usr/backupoutput/{file}" :
                importFile;

            var cmd = $"{(runInDocker ? dockerRun : String.Empty)} dropdb -h {info.Host} -p {info.Port} -U {info.User} {info.Database};";
            cmd += $"{(runInDocker ? dockerRun : String.Empty)} createdb -h {info.Host} -p {info.Port} -U {info.User} {info.Database};";
            cmd += $"{(runInDocker ? dockerRun : String.Empty)} psql -h {info.Host} -p {info.Port} -U {info.User} -d {info.Database} -f {backupScript};";

            string arguments = $"-NoExit -c {{{cmd}}}";
            Process.Start("powershell.exe", arguments);
        }

        public void DumpDatabaseUsingDocker(ConnectionInfo info, string exportFile) => dumpDatabase(info, exportFile, runInDocker: true);
        public void DumpDatabaseWithoutDocker(ConnectionInfo info, string exportFile) => dumpDatabase(info, exportFile, runInDocker: false);
        private void dumpDatabase(ConnectionInfo info, string exportFile, bool runInDocker)
        {

        }

    }
}
