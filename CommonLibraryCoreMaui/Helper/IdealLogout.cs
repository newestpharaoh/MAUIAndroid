using System;
using System.Reactive.Linq;
using Acr.UserDialogs;
using CommonLibraryCoreMaui.DependencyServices;
using MvvmCross;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CommonLibraryCoreMaui.Helper
{
	public class IdealLogout
	{
		public static IdealLogout Instance { get; } = new IdealLogout();
		IDisposable subscription;

		static IdealLogout() { }
		private IdealLogout() { }

		public void StartTimer()
		{
			if (CommonAuthSession.IsLoggedIn)
				subscription = Observable
							.Interval(TimeSpan.FromMilliseconds(SettingsValues.IdealLogoutPeriod))
							.Subscribe(_ =>
							{
								Instance.StopTimer();
								Mvx.IoCProvider.Resolve<IUserDialogs>().AlertAsync("You have been logged out for your security.", "Idle Logout")
								.ContinueWith((x) =>
								{
									DependencyService.Get<IPlatformFeatures>().GoToRootView(() =>
									{
										CommonAuthSession.ClearSession();
									});
								});
							});
		}

		public void StopTimer()
		{
			if (CommonAuthSession.IsLoggedIn)
				subscription?.Dispose();
		}
	}
}
