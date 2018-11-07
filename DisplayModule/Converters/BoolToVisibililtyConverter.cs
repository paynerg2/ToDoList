using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DisplayModule.Converters
{
    public class BoolToVisibililtyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Visibility.Collapsed;
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
