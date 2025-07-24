using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Shortcut.Converters
{
    public class WindowStateMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            WindowState state = (WindowState)value;
            if (state == WindowState.Normal)
            {
                return new Thickness(0);
            }
            else
            {
                var param = (string)parameter;
                var right = param == "Exit" ? 7 : 0;
                return new Thickness(0, 7, right, 0);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
