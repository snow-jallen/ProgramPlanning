using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramPlanning.Shared.Models
{
    public class ConnectionInfo : IConnectionInfo
    {
        public string Nickname { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
