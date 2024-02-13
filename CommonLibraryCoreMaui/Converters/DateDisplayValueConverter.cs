using System;
using System.Globalization;
using MvvmCross.Converters;

namespace CommonLibraryCoreMaui.Converters
{
    public class DateDisplayValueConverter : IMvxValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return DateTime.MinValue.ToDisplayDate();

            return ((DateTime)value).ToDisplayDate();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
