using CoursesOutcomesAndSkills.ViewModels;
using ProgramPlanning.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesOutcomesAndSkills
{
    public class MySettings
    {
        public MySettings()
        {
            Connections = new List<ConnectionInfo>();
        }

        public List<ConnectionInfo> Connections { get; set; }
    }
}
