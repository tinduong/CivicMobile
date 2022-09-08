using System.Globalization;
namespace CivicMobile.Converters;

public class IsUserAnswerCorrectToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var isValid = (bool)value;
        return isValid ? new Color().Green : new Color().Red;

    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}
