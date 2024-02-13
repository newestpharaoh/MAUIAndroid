using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using CommonLibraryCoreMaui.DependencyServices;
using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.Services;
//using CommonLibraryCoreMaui.ViewModels;
using MvvmCross;
using MvvmCross.ViewModels;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using Microsoft.Maui.Storage;

namespace CommonLibraryCoreMaui.Helper
{
	public class NewPatientAlert : IDisposable
	{
		IWaitListPatientService waitListPatientService = Mvx.IoCProvider.Resolve<IWaitListPatientService>();
		IDisposable subscription;

		public NewPatientAlert() { }

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
			Console.WriteLine($"React timer tick {nameof(NewPatientAlert)}");
			if (Globals.Instance.UserInfo != null && Globals.Instance.UserInfo.ProviderID != null)
			{
				ProviderWaitListResponse resp = await waitListPatientService.GetWaitList((int)Globals.Instance.UserInfo.ProviderID);
				if (resp != null)
				{
					var WaitListCount = resp?.PatientWaitListCount ?? 0;
					await SetPatientWaitCount(WaitListCount);
				}
			}
		}

		public static async Task SetPatientWaitCount(int WaitListCount)
		{
			var previousWaitListCount = Preferences.Get(SettingsValues.WaitPatientCount, -1);
			if (previousWaitListCount > 0)
			{
				if (previousWaitListCount < WaitListCount)
				{
					Preferences.Set(SettingsValues.WaitPatientCount, WaitListCount);
					DependencyService.Resolve<IAudioService>().Play("new-patient.caf");
					await Mvx.IoCProvider.Resolve<IUserDialogs>().AlertAsync($"A new patient has entered the wait list. There are now " + WaitListCount + " patient(s) in the wait list.",
					"New Patient Waiting", "OK").ContinueWith((t) =>
					{
						//MessagingCenter.Send<object>(MvxViewModelRequest.GetDefaultRequest(typeof(ActiveVisitsViewModel)), "PatientWaitCount");
						//MessagingCenter.Send<object>(MvxViewModelRequest.GetDefaultRequest(typeof(WaitListViewModel)), "GetPatientWaitList");
						DependencyService.Resolve<IAudioService>().Stop();
					});
				}
			}
			else
			{
				if (WaitListCount > 0)
				{
					Preferences.Set(SettingsValues.WaitPatientCount, WaitListCount);
					DependencyService.Resolve<IAudioService>().Play("new-patient.caf");
					await Mvx.IoCProvider.Resolve<IUserDialogs>().AlertAsync($"A new patient has entered the wait list. There are now " + WaitListCount + " patient(s) in the wait list.",
					"New Patient Waiting", "OK").ContinueWith((t) =>
					{
						//MessagingCenter.Send<object>(MvxViewModelRequest.GetDefaultRequest(typeof(ActiveVisitsViewModel)), "PatientWaitCount");
						//MessagingCenter.Send<object>(MvxViewModelRequest.GetDefaultRequest(typeof(WaitListViewModel)), "GetPatientWaitList");
						DependencyService.Resolve<IAudioService>().Stop();
					});
				}
			}
			Preferences.Set(SettingsValues.WaitPatientCount, WaitListCount);
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
