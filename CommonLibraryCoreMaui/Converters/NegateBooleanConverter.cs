using System;
using System.Linq;
using MvvmCross.Converters;

namespace CommonLibraryCoreMaui.Converters
{
	//negate boolean
	//if true comes return false
	//if false comes return true
	public class NegateBooleanConverter : MvxValueConverter
	{
		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return !(bool)value;
		}
		public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return !(bool)value;
		}
	}

	//return $40/month decorator for value 40
	public class CostTitleValueConverter : MvxValueConverter
	{
		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
				return null;

			return $"${value}/month";
		}
	}

	//if string value null or empty return None
	public class EmptyTextValueConverter : MvxValueConverter
	{
		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
				return "None";
			if (((string)value) == string.Empty)
				return "None";
			return value.ToString();
		}
	}

	public class SpaceEmptyTextValueConverter : MvxValueConverter
	{
		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
				return " ";
			if (((string)value) == string.Empty)
				return " ";
			return value.ToString();
		}
	}

	//Get the initial letter of gender. Also return Select for Select
	public class GenderValueConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;
			if (((string)value).Equals("Select"))
				return value;
            return Theme.Values.GenderOptions.First(g => g.StartsWith((string)value, StringComparison.Ordinal));
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;

            return ((string)value).ToCharArray()[0];
        }
    }

	//return Add, edit qualified text
	//for instance, for value Allergy it returns Add/Edit Allery 
    public class MedicalIssueActionTypeValueConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;

            return $"{((bool)parameter ? "Edit" : "Add")} {((Models.PrimaryIssue)value).IssueType.ToString()}";
        }
    }

	public class RemoveSlashesValueConverter : MvxValueConverter
	{
		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
				return null;

			return ((string)value).Replace("\\n", "\n");
		}
	}
}
