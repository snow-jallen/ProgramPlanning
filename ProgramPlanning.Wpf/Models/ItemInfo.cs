#region Copyright Syncfusion Inc. 2001-2019.
// Copyright Syncfusion Inc. 2001-2019. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws.
#endregion
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPlanning.Wpf.Models
{
    public class ItemInfo
    {
        public ItemInfo(string name, string color)
        {
            this.Name = name;
            this.RatingColor = color;
        }

        public string RatingColor { get; set; }

        public string Name { get; set; }

        public List<string> ReportingPerson { get; set; }
        public string CourseTitle { get; set; } = "Course Title";
        public string CourseSummary { get; set; } = "This is the course summary.  It's a great course, definitely one of the best we have. ;-)";
    }

    public class DataItems : ObservableCollection<ItemInfo>
    {

    }
}
