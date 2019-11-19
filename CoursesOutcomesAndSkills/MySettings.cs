using CoursesOutcomesAndSkills.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesOutcomesAndSkills
{
    public class MySettings
    {
        public MySettings()
        {
            Connections = new List<ConnectionInfoViewModel>();
        }

        public List<ConnectionInfoViewModel> Connections { get; set; }
    }
}
