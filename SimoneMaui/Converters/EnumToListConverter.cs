using SimoneMaui.Models;

namespace SimoneMaui.Converters
{
    public class EnumToListConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        {
            if (value is MauiJobRoleEnum)
            {
                return Enum.GetName(typeof(MauiJobRoleEnum), value);
            }

            throw new ArgumentException("Invalid argument type", nameof(value));
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string s)
            {
                return Enum.Parse<MauiJobRoleEnum>(s);
            }

            throw new ArgumentException("Invalid argument type", nameof(value));
        }
    }
}

