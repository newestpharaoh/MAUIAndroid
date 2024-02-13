using System;
using Acr.UserDialogs;
using CommonLibraryCoreMaui.Models;
using MvvmCross;
using MvvmCross.ViewModels;
using CommonLibraryCoreMaui.DependencyServices;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CommonLibraryCoreMaui.Helper
{
	public class RestartVisitAlert : IDisposable
	{
		IDisposable subscription;

		public RestartVisitAlert() { }

		public void StartTimer()
		{
			if (CommonAuthSession.IsLoggedIn)
				subscription = Observable
							.Interval(TimeSpan.FromMilliseconds(SettingsValues.WaitingPatientPeriod))
							.Select(l => Observable.FromAsync(Monitor))
							.Concat()
							.Subscribe();
		}

		public void StopTimer()
		{
			if (CommonAuthSession.IsLoggedIn)
				subscription?.Dispose();
		}

		public async Task Monitor()
		{
			Console.WriteLine($"React timer tick {nameof(RestartVisitAlert)}");
			var patientVisitValue = await DataUtility.RestartVisitAsync(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.ProviderID.Value, CommonAuthSession.Token);
			if (patientVisitValue > 0)
			{
				var numberOfPatientTitle = patientVisitValue == 1 ? $"There is now {patientVisitValue} patient who has resumed visits."
					: $"There are now {patientVisitValue} patients who have resumed visits.";
				StopTimer();
				DependencyService.Resolve<IAudioService>().Play("returning-patient.wav");

				await Mvx.IoCProvider.Resolve<IUserDialogs>().AlertAsync(
					$"A patient has rejoined chat." + Environment.NewLine +
					$"{numberOfPatientTitle}",
					"Restarted Patient Visit", "OK").ContinueWith(async (t) =>
					{
						DependencyService.Resolve<IAudioService>().Stop();
						await DataUtility.SetRestartedVisitFlagAsync(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.ProviderID.Value, CommonAuthSession.Token);
						StartTimer();
					});
			}

		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				subscription?.Dispose();
			}
		}
	}
}
