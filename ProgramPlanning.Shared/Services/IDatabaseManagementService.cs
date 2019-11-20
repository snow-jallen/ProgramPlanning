using ProgramPlanning.Shared.Models;

namespace ProgramPlanning.Shared.Services
{
    public interface IDatabaseManagementService
    {
        bool HasDockerInstalled { get; }
        bool HasPostgresInstalled { get; }

        void LoadFromBackupUsingDocker(ConnectionInfo info, string backupFile);
        void LoadFromBackupWithoutDocker(ConnectionInfo info, string backupFile);
        void DumpDatabaseUsingDocker(ConnectionInfo info, string exportFile);
        void DumpDatabaseWithoutDocker(ConnectionInfo info, string exportFile);
    }
}