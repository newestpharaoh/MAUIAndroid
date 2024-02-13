using System.Text.RegularExpressions;

namespace CommonLibraryCoreMaui
{
    public class MaskHelper
	{
		public static string MaskEmail(string email)
		{
			try
			{
				return Regex.Replace(email, @"(?<=[\w]{0})[\w-\._\+%]*(?=[\w]{1}@)", m => new string('*', m.Length));
			}
			catch
			{
			}
			return string.Empty;
		}

		public static string MaskPhoneNumber(string phone)
		{
			try
			{
				return Regex.Replace(phone, @"\d(?!\d{0,3}$)", m => new string('*', m.Length));
			}
			catch { }
			return string.Empty;
		}
	}
}
