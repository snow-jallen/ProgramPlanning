using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ProgramPlanning.Shared.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesOutcomesAndSkills.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		public MainWindowViewModel()
		{
			ConnectionString = "Server=localhost; Port=5432; Database=postgres; User ID=postgres; Password=postgres;";
		}

		private string connectionString;
		public string ConnectionString
		{
			get { return connectionString; }
			set { Set(ref connectionString, value); }
		}

		private RelayCommand connectToDatabase;
		public RelayCommand ConnectToDatabase => connectToDatabase ?? (connectToDatabase = new RelayCommand(() =>
		{
			var repo = new PostgresCourseRepository(ConnectionString);
			var courses = repo.TestConnection();
		}));
	}
}
