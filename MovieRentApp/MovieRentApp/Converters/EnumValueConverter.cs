using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MovieRentApp.Converters;

public class EnumValueConverter : MarkupExtension, IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || !value.GetType().IsEnum)
            return null;

        var enumValue = (Enum)value;
        return enumValue.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || !targetType.IsEnum)
            return null;

        return Enum.Parse(targetType, value.ToString());
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }
}
