using System;
using System.Globalization;
using System.Windows.Data;

namespace Logical_cxem
{
    public class StateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return EState.True == (EState) value ? "1" : "0";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "1" == (string) value ? EState.True : EState.False;
        }
    }
}