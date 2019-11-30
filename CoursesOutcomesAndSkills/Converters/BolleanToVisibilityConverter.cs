using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace CoursesOutcomesAndSkills.Converters
{
    public sealed class BooleanToVisibilityConverter : BooleanConverter<Visibility>
    {
        public BooleanToVisibilityConverter() :
            base(Visibility.Visible, Visibility.Collapsed)
        { }
    }
}
