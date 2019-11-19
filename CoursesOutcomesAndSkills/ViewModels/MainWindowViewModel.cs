using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ProgramPlanning.Shared.Models;
using ProgramPlanning.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace CoursesOutcomesAndSkills.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		public MainWindowViewModel(ICourseInfoRepository courseRepository, ISettingsManager<MySettings> settingsManager)
		{
			this.courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
			this.settingsManager = settingsManager ?? throw new ArgumentNullException(nameof(settingsManager));

			Connections = (settingsManager.LoadSettings() ?? new MySettings()).Connections;
			DataIsEnabled = Connections.Any();
			if(DataIsEnabled)
			{
				SelectedConnection = Connections.First();
			}
			else
			{
				MessengerInstance.Send(new MessageBoxMessage("No Databases Configured", "No database connections have been configured.\nPlease add some on the 'Config' tab."));
				SelectedTabIndex = 3;//config tab
			}
		}

		private readonly ICourseInfoRepository courseRepository;
		private readonly ISettingsManager<MySettings> settingsManager;

		private bool dataIsEnabled;
		public bool DataIsEnabled
		{
			get => dataIsEnabled;
			set { Set(ref dataIsEnabled, value); }
		}

		private int selectedTabIndex;
		public int SelectedTabIndex
		{
			get => selectedTabIndex;
			set { Set(ref selectedTabIndex, value); }
		}

		public IEnumerable<ConnectionInfo> Connections { get; set; }
		private ConnectionInfo selectedConnection;
		public ConnectionInfo SelectedConnection { get { return selectedConnection; }
			set
			{
				selectedConnection = value;
				courseRepository.SetConnection(selectedConnection);
			}
		}
	}
}
