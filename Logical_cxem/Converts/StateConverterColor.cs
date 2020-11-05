using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Logical_cxem
{
    public class StateConverterColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((EState) value == EState.OK)
            {
                var color = (byte) App.random.Next(0, 100);
                return Color.FromRgb(color, color, color).ToString();
                //return Color.FromRgb((byte)ran.Next(255), 0, 0).ToString();
            }

            return EState.False == (EState) value
                ? Color.FromRgb((byte) App.random.Next(150, 255), 0, 0).ToString()
                : Color.FromRgb(0, (byte) App.random.Next(150, 255), 0).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "1" == (string) value ? EState.True : EState.False;
        }
    }
}