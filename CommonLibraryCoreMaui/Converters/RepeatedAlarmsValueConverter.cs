using MvvmCross.Converters;
using System;
using System.Globalization;

namespace CommonLibraryCoreMaui.Converters
{
	//return no of time wrt coming value
    public class RepeatedAlarmsValueConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (((int)value) == 0) return "Never";
            if (((int)value) == 1) return "Once";
            if (((int)value) == 2) return "Twice";
            if (((int)value) == 3) return "3 Times";
            if (((int)value) == 4) return "4 Times";
            if (((int)value) == 5) return "5 Times";
            return null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int index;
            switch ((string)value)
            {
                case "Never":
                    index = 0;
                    break;
                case "Once":
                    index = 1;
                    break;
                case "Twice":
                    index = 2;
                    break;
                case "3 Times":
                    index = 3;
                    break;
                case "4 Times":
                    index = 4;
                    break;
                case "5 Times":
                    index = 5;
                    break;
                default:
                    index = 0;
                    break;
            }
            return index;
        }
    }
}
