using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesOutcomesAndSkills.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		public MainWindowViewModel()
		{
			Host = "postgres";
			Database = "courses1";
		}

		private string host;
		public string Host
		{
			get { return host; }
			set { Set(ref host, value); }
		}

		private string database;
		public string Database
		{
			get { return database; }
			set { Set(ref database, value); }
		}
	}
}
