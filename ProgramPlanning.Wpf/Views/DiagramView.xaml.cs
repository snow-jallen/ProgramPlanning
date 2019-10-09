using Prism.Regions;
using Syncfusion.UI.Xaml.Diagram;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProgramPlanning.Wpf.Views
{
    /// <summary>
    /// Interaction logic for DiagramView.xaml
    /// </summary>
    public partial class DiagramView : UserControl
    {

        public DiagramView()
        {
            InitializeComponent();
        }


        private void Sfdiagram_Loaded(object sender, RoutedEventArgs e)
        {
            var dataContext = sfdiagram.DataContext as ProgramPlanning.Wpf.ViewModels.DiagramViewModel;
            if(dataContext != null)
                dataContext.prevbutton = this.ToptoBottomOrientation;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void export_Click(object sender, RoutedEventArgs e)
        {
            var exportFileName = "Program Planning.png";
            sfdiagram.ExportSettings.FileName = exportFileName;
            sfdiagram.ExportSettings.ExportMode = ExportMode.Content;
            sfdiagram.Export();
            Process.Start(exportFileName);
        }
    }
}
