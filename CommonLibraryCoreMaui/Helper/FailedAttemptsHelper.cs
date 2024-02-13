using Microsoft.Maui.Storage;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommonLibraryCoreMaui
{
	public class FailedAttemptsHelper
	{
		public static async Task TooManyFailedAttempts(string apiUrl, string ip, double? minutes = null)
		{
			double _minutes = 0;
			if (minutes is null)
			{
				string lockoutTime = await DataUtility.GetLockoutTimeAsync(apiUrl, ip).ConfigureAwait(false);
				if (!string.IsNullOrEmpty(lockoutTime))
				{
					string result = Regex.Replace(lockoutTime, "minutes", "", RegexOptions.IgnoreCase);

					if (!double.TryParse(result, out _minutes))
					{
						_minutes = 30;
					}
				}
			}
			else
			{
				_minutes = (double)minutes;
			}
			Preferences.Set(SettingsValues.FailedAttemptsTimer, DateTime.UtcNow.AddMinutes(_minutes).ToString());
			
		}

		public static string ShowWaitingTime()
		{
			string dt = Preferences.Get(SettingsValues.FailedAttemptsTimer, string.Empty);
			if (!string.IsNullOrEmpty(dt))
			{
				return WaitTimeUtility.DisplayWaitTime(TimeSpan.FromSeconds(DateTime.Parse(dt).Subtract(DateTime.UtcNow).TotalSeconds));
			}
			return string.Empty;
		}

		public static bool? IsShowTooManyFailedAttempts()
		{
			string dt = Preferences.Get(SettingsValues.FailedAttemptsTimer, string.Empty);
			if (!string.IsNullOrEmpty(dt))
			{
				if (DateTime.Parse(dt) < DateTime.UtcNow)
				{
					Preferences.Remove(SettingsValues.FailedAttemptsTimer);
					return false;
				}
				return true;
			}
			return null;
		}
	}
}
