using System;
using System.Globalization;
using System.Windows.Data;

namespace MovieRentApp.Converters;

public class BoolToYesNoConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? "TAK" : "NIE";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (string)value == "TAK";
    }
}
