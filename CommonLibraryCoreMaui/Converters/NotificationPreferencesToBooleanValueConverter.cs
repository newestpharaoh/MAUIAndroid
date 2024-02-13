using MvvmCross.Converters;
using System;
using System.Globalization;

namespace CommonLibraryCoreMaui.Converters
{
    public class NotificationPreferencesToBooleanValueConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                return null;

            return (((string)value).ToLower()).Equals(((string)parameter).ToLower());
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool val = (bool)value;

            if (((string)(parameter)).ToLower().Equals("email") && val)
            {
                return "Email";
            }
            else if (((string)(parameter)).ToLower().Equals("email") && !val)
            {
                return "Text";
            }
            else if (((string)(parameter)).ToLower().Equals("text") && val)
            {
                return "Text";
            }
            else
            {
                return "Email";
            }
        }
    }

	//return Yes for true
	//return No for false
	public class EstablishedToBooleanValueConverter : MvxValueConverter
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool val = (bool)value;
			return val ? "Yes" : "No";
		}
	}

	//return Established for true
	//return empty for false
	public class BooleanToEstablishedValueConverter : MvxValueConverter
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool val = (bool)value;
			return val ? "Established" : string.Empty;
		}
	}

	//If user is 
	//primary return navigation image
	//not primary return lock image
	public class PrimaryIconBooleanValueConverter : MvxValueConverter
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool val = (bool)value;
			return val ? "lock-selected.png" : "chevron-right.png";
		}
	}
}
