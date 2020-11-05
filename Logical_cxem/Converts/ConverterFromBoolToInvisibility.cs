using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Logical_cxem.Converts
{
    internal class ConverterFromBoolToInvisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !((bool) value) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Visibility.Collapsed == (Visibility) value ? true : false;
        }
    }
}