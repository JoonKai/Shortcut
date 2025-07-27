using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SJSControls.UIConverters
{
    public class ValidatingBorderThicknessConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool? isValidation = values[0] as bool?;
            if (isValidation != null && isValidation == true)
            {
                return new Thickness(1.5);
            }
            else
            {
                return values[1];
            }

        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
