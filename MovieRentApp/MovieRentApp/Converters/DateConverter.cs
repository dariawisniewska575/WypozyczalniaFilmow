using System;
using System.Globalization;
using System.Windows.Data;

namespace MovieRentApp.Converters
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null!;

            var date = (DateTime)value;
            return date.ToString("dd/MM/yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (string)value;
            return DateTime.Parse(date);
        }
    }
}
