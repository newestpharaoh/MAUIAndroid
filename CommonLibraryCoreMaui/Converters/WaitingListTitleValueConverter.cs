using CommonLibraryCoreMaui.Models;
using MvvmCross.Converters;
using System;

namespace CommonLibraryCoreMaui.Converters
{
	//qualify value with some text
    public class WaitingListTitleValueConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;
            
            return $"{value} Patient(s) Currently Waiting";
        }
    }

	//visit is established or not wrt coming bool value
	public class ActiveVisitEstablishTitleValueConverter : MvxValueConverter
	{
		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
				return string.Empty;
			return (bool)value ? "Established" : "NOT Established";
		}
	}

	//return time for coming string in a specific format
	public class ActiveVisitStartTimeTitleValueConverter : MvxValueConverter
	{
		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
				return string.Empty;
			DateTime startTime = DateTime.ParseExact((string)value, "M/d/yyyy - h:mm tt", null);
			return startTime.ToString("h:mm tt");
		}
	}

	//full name of patient
	public class ActiveVisitPatientTitleValueConverter : MvxValueConverter
	{
		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
				return string.Empty;
			var resp = (VisitDetailsResponse)value;
			return $"{resp.PatientFirstName} {resp.PatientLastName} {(!string.IsNullOrEmpty(resp.PatientPreferredName) ? $"({resp.PatientPreferredName})" : "")}";
		}
	}

	//date of birth and age combine string
	public class ActiveVisitDOBTitleValueConverter : MvxValueConverter
	{
		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
				return string.Empty;
			var resp = (VisitDetailsResponse)value;
			return $"{resp.DOB} ({resp.Age})";
		}
	}

	//guardian and relation combine string
	public class ActiveVisitGuardianTitleValueConverter : MvxValueConverter
	{
		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
				return string.Empty;
			var resp = (VisitDetailsResponse)value;
			string relationship = string.IsNullOrEmpty(resp.GuardianRelationship) ? "" : $"({resp.GuardianRelationship})";
			return $"{resp.Guardian} {relationship}";
		}
	}
}
