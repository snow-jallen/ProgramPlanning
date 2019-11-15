#region Copyright Syncfusion Inc. 2001-2019.
// Copyright Syncfusion Inc. 2001-2019. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws.
#endregion
using Prism.Commands;
using Prism.Regions;
using ProgramPlanning.Shared.Services;
using ProgramPlanning.Wpf.Models;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Layout;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ProgramPlanning.Wpf.ViewModels
{
    public class DiagramViewModel : Syncfusion.UI.Xaml.Diagram.DiagramViewModel, INavigationAware
    {
        public Button prevbutton = null;
        public DiagramViewModel()
        {
            //Initialize Diagram Properties
            Connectors = new ObservableCollection<ConnectorVM>();
            Constraints = Constraints.Remove(GraphConstraints.PageEditing , GraphConstraints.PanRails);
            Menu = null;
            Tool = Tool.ZoomPan;
            //HorizontalRuler = new Ruler { Orientation = Orientation.Horizontal };
            //VerticalRuler = new Ruler { Orientation = Orientation.Vertical };
            DefaultConnectorType = ConnectorType.Orthogonal;

            // Initialize Command for sample changes

            Orientation_Command = new DelegateCommand<Object>(OnOrientation_Command);
            ExportReport = new DelegateCommand(() => DocumentGenerator.MakeDocument(courses));
        }

        #region Private Variables

        private ICommand _Orientation_Command;

        #endregion

        #region Commands

        public ICommand Orientation_Command
        {
            get { return _Orientation_Command; }
            set
            {
                if (_Orientation_Command != value)
                {
                    _Orientation_Command = value;
                    onPropertyChanged("Orientation_Command");
                }
            }
        }

        public DelegateCommand ExportReport { get; private set; }
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;

        private void onPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        /// <summary>
        /// Method to change Orientation of the Layout
        /// </summary>
        /// <param name="obj"></param>
        private void OnOrientation_Command(object obj)
        {
            Button button = obj as Button;
            if (prevbutton != null)
            {
                prevbutton.Style = App.Current.MainWindow.Resources["ButtonStyle"] as Style;
            }
            button.Style = App.Current.MainWindow.Resources["SelectedButtonStyle"] as Style;

            switch (button.Name.ToString())
            {
                case "ToptoBottomOrientation":
                    (LayoutManager.Layout as DirectedTreeLayout).Orientation = TreeOrientation.TopToBottom;
                    break;
                case "BottomtoTopOrientation":
                    (LayoutManager.Layout as DirectedTreeLayout).Orientation = TreeOrientation.BottomToTop;
                    break;
                case "LefttoRightOrientation":
                    (LayoutManager.Layout as DirectedTreeLayout).Orientation = TreeOrientation.LeftToRight;
                    break;
                case "RighttoLeftOrientation":
                    (LayoutManager.Layout as DirectedTreeLayout).Orientation = TreeOrientation.RightToLeft;
                    break;
            }
            prevbutton = obj as Button;
        }

        /// <summary>
        /// Method to Get Data for DataSource
        /// </summary>
        /// <param name="data"></param>
        private DataItems GetData(IEnumerable<ExcelBasedCourse> courses)
        {
            DataItems data = new DataItems();
            int i = 1;
            foreach(var course in courses)
            {
                data.Add(new ItemInfo($"{course.Area} {course.Number}", "#ff6329")
                {
                    CourseSummary = course.Content,
                    CourseTitle = course.Title,
                    ReportingPerson = new List<string>(course.Prerequisites),
                    Course = course,
                    XTransform = (i += 50)
                });
            }

            //data.Add(new ItemInfo("n11", "#ff6329"));
            //data.Add(new ItemInfo("n12", "#ff6329"));
            //data.Add(new ItemInfo("n13", "#ff6329"));
            //data.Add(new ItemInfo("n21", "#941100") { ReportingPerson = new List<string> { "n11", "n12", "n13" } });
            //data.Add(new ItemInfo("n31", "#669be5") { ReportingPerson = new List<string> { "n21" } });
            //data.Add(new ItemInfo("n32", "#669be5") { ReportingPerson = new List<string> { "n21" } });
            //data.Add(new ItemInfo("n41", "#30ab5c") { ReportingPerson = new List<string> { "n31" } });
            //data.Add(new ItemInfo("n42", "#30ab5c") { ReportingPerson = new List<string> { "n31" } });
            //data.Add(new ItemInfo("n43", "#30ab5c") { ReportingPerson = new List<string> { "n31" } });
            //data.Add(new ItemInfo("n44", "#30ab5c") { ReportingPerson = new List<string> { "n31", "n32" } });
            //data.Add(new ItemInfo("n45", "#30ab5c") { ReportingPerson = new List<string> { "n32" } });
            //data.Add(new ItemInfo("n46", "#30ab5c") { ReportingPerson = new List<string> { "n32" } });
            //data.Add(new ItemInfo("n47", "#30ab5c") { ReportingPerson = new List<string> { "n32" } });
            //data.Add(new ItemInfo("n51", "#ff9400") { ReportingPerson = new List<string> { "n41", "n42", "n43" } });
            //data.Add(new ItemInfo("n52", "#ff9400") { ReportingPerson = new List<string> { "n45", "n46", "n47" } });
            //data.Add(new ItemInfo("n61", "#99bb55") { ReportingPerson = new List<string> { "n51" } });
            //data.Add(new ItemInfo("n62", "#99bb55") { ReportingPerson = new List<string> { "n51" } });
            //data.Add(new ItemInfo("n63", "#99bb55") { ReportingPerson = new List<string> { "n51", "n44" } });
            //data.Add(new ItemInfo("n64", "#99bb55") { ReportingPerson = new List<string> { "n44", "n52" } });
            //data.Add(new ItemInfo("n65", "#99bb55") { ReportingPerson = new List<string> { "n52" } });
            //data.Add(new ItemInfo("n66", "#99bb55") { ReportingPerson = new List<string> { "n52" } });

            return data;
        }

        IEnumerable<ExcelBasedCourse> courses;

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext is null)
            {
                throw new ArgumentNullException(nameof(navigationContext));
            }

            courses = navigationContext.Parameters["courses"] as IEnumerable<ExcelBasedCourse>;

            // Initialize DataSourceSettings for SfDiagram
            var dataItems = GetData(courses);
            DataSourceSettings = new DataSourceSettings()
            {
                ParentId = "ReportingPerson",
                Id = "Name",
                DataSource = dataItems,
            };

            var groups = new ObservableCollection<GroupViewModel>();
            foreach(var group in from c in dataItems
                                 orderby c.Course.Semester
                                 group c by c.Course.Semester into g
                                 select new {Semester = g.Key, DataItems=g})
            {
                //groups.Add(new GroupViewModel
                //{
                //    Nodes = group.DataItems.ToArray(),
                //    ID=group.Semester,
                //    Key=group.Semester,
                //    ZIndex=1
                //});
            }

            //Groups = groups;


            // Initialize LayoutSettings for SfDiagram
            LayoutManager = new LayoutManager()
            {
                Layout = new DirectedTreeLayout()
                {
                    Type = LayoutType.Hierarchical,
                    Orientation = TreeOrientation.LeftToRight,
                    AvoidSegmentOverlapping = true,
                    HorizontalSpacing = 40,
                    VerticalSpacing = 40,
                },
            };
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => false;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
