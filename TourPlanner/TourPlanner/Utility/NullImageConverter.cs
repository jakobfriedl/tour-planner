using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TourPlanner.Utility
{
    internal class NullImageConverter : IValueConverter
    {
	    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
		    return value ?? DependencyProperty.UnsetValue; 
	    }

	    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
		    return Binding.DoNothing; 
	    }
    }
}
