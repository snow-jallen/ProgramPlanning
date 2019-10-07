#region Copyright Syncfusion Inc. 2001-2019.
// Copyright Syncfusion Inc. 2001-2019. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws.
#endregion
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Prism.Regions;
using ProgramPlanning.Wpf.ViewModels;
using Syncfusion.UI.Xaml.Diagram;
using Syncfusion.UI.Xaml.Diagram.Controls;
using Syncfusion.UI.Xaml.Diagram.Layout;
using Syncfusion.Windows.Shared;

namespace ProgramPlanning.Wpf.Views
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        private readonly IRegionManager regionManager;

        public MainWindowView(IRegionManager regionManager)
        {
            this.InitializeComponent();

            this.regionManager = regionManager;
            Loaded += MainWindowView_Loaded;

        }

        private void MainWindowView_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}