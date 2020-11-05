using System;
using System.Globalization;
using System.Windows.Data;
using Logical_cxem.Enumerations;

namespace Logical_cxem.Converts
{
    internal class ConverterStateAppToText : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((EStateApp) value)
            {
                case EStateApp.Editor:
                    return "Редактирование";
                    break;
                case EStateApp.Test:
                    return "Тестирование";
                    break;
                default: return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "1" == (string) value ? EState.True : EState.False;
        }
    }
}