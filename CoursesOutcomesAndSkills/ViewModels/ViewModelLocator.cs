using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesOutcomesAndSkills.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            MainWindowViewModel = new MainWindowViewModel();
        }

        public static MainWindowViewModel MainWindowViewModel { get; private set; }
    }
}
