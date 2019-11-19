using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ProgramPlanning.Shared.Models;
using ProgramPlanning.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoursesOutcomesAndSkills.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		public MainWindowViewModel(ICourseInfoRepository courseRepository, ISettingsManager<MySettings> settingsManager)
		{
			this.courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
			this.settingsManager = settingsManager ?? throw new ArgumentNullException(nameof(settingsManager));

			Connections = settingsManager.LoadSettings().Connections;
		}

		private RelayCommand connectToDatabase;
		private readonly ICourseInfoRepository courseRepository;
		private readonly ISettingsManager<MySettings> settingsManager;

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
