﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SJSControls.UIConverters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
        {
            return (value is bool && (bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
        {
            return value is Visibility && (Visibility)value == Visibility.Visible;
        }
    }
}
