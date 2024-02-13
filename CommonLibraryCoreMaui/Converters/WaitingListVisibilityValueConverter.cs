using CommonLibraryCoreMaui.Models;
using MvvmCross.Converters;
using System;

namespace CommonLibraryCoreMaui.Converters
{
	//return false if value is greater then zero
    public class WaitingListVisibilityValueConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;

            if ((int)value > 0)
            {
                return false;
            }

            return true;
        }
    }

	//return true if record id is null
    public class NullableToBooleanValueConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return false;

            if (value is Models.GenericRecord)
            {
                return ((Models.GenericRecord)value).ID is null;
            }
            else if (value is Models.Patient)
            {
                return ((Models.Patient)value).PatientID is null;
            }

            return false;
        }
    }

	//return false if record id is null (negate above)
	public class NegateNullableToBooleanValueConverter : MvxValueConverter
	{
		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
				return true;

			if (value is Models.GenericRecord)
			{
				return !(((Models.GenericRecord)value).ID is null);
			}
			else if (value is Models.Patient)
			{
				return !(((Models.Patient)value).PatientID is null);
			}

			return true;
		}
	}

	public class AuthCodeToBooleanValueConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return false;

            return ((string)value).Length == 6;
        }
    }

	//return true if value is not null
	public class NullableToBooleanMedicalHistoryValueConverter : MvxValueConverter
	{
		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return value != null;
		}
	}

	//return true if value is null (negate above)
	public class NegateNullableToBooleanMedicalHistoryValueConverter : MvxValueConverter
	{
		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return value == null;
		}
	}

	//return false if string is null or empty
	public class EmptyToBooleanValueConverter : MvxValueConverter
	{
		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
				return false;
			if ((string)value == string.Empty)
				return false;
			return true;
		}
	}

	//return true if string is null or empty (negate of above)
	public class NegateEmptyToBooleanValueConverter : MvxValueConverter
	{
		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
				return true;
			if ((string)value == string.Empty)
				return true;
			return false;
		}
	}

	//return false if status is complete. Determine whether status is incomplete
	public class VisitStatusIsIncompleteValueConverter : MvxValueConverter
	{
		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
				return false;
			if ((string)value == "complete")
				return false;
			return true;
		}
	}

	//return true if status is complete. (negate of above)
	public class NegateVisitStatusIsIncompleteValueConverter : MvxValueConverter
	{
		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
				return true;
			if ((string)value == "complete")
				return true;
			return false;
		}
	}

	//return false if subscription is not applicable or "Payment Plans" string
	public class NASubscriptionVisibilityValueConverter : MvxValueConverter
	{
		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
				return false;
			if ((string)value == "N/A" || (string)value == "Payment Plans")
				return false;
			return true;
		}
	}

	//return Nobe if coming string is null or empty
    public class EmptyStringToNoneValueConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
			try
			{
				return string.IsNullOrEmpty((string)value) ? "None" : value;
			}
			catch (Exception)
			{
				return "None";
			}
        }
    }

	//return Nobe if coming string is null or empty for diagnosis
	public class EmptyStringToNoneDiagnosisValueConverter : MvxValueConverter
	{
		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			try
			{
				return (string.IsNullOrEmpty(((ICDCode)value).Value) || ((ICDCode)value).ID == 0) ? "None" : value;
			}
			catch (Exception)
			{
				return "None";
			}
		}
	}

	//return Nobe if coming string is null or empty
	public class BooleanToNoneValueConverter : MvxValueConverter
	{
		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
				return "None";
			return (bool)value ? "Yes" : "None";
		}
	}
}
