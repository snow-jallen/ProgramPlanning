namespace ProgramPlanning.Shared.Models
{
    public interface IConnectionInfo
    {
        string Database { get; set; }
        string Host { get; set; }
        string Password { get; set; }
        int Port { get; set; }
        string User { get; set; }
    }
}