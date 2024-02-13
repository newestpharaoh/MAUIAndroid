using System;
using System.Threading.Tasks;
//using Acr.UserDialogs;
//using CommonLibraryCoreMaui.PatientApp.ViewModels;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.DependencyInjection;
using MvvmCross.Navigation;
//using MvvmCross.Core.Navigation;
using MvvmCross;
using Acr.UserDialogs;
//using MvvmCross.Navigation;

namespace CommonLibraryCoreMaui.Helper
{
	public class TooManyAttempts
	{
		public static TooManyAttempts Instance { get; } = new TooManyAttempts();
		IUserDialogs _userDialogs;

		static TooManyAttempts() { }
		private TooManyAttempts()
		{
		}

		public void Add(IUserDialogs userDialogs)
		{
			_userDialogs = userDialogs;
			ApiUtility.TooManyFailedAttemptsEvent += ApiUtility_TooManyFailedAttemptsEvent;
		}

		public void Delete()
		{
			ApiUtility.TooManyFailedAttemptsEvent -= ApiUtility_TooManyFailedAttemptsEvent;
		}

		private void ApiUtility_TooManyFailedAttemptsEvent(object sender, EventArgs e)
		{
			try
			{
				double? minutes = null;
				if (e is LockoutTimeEventArgs)
				{
					minutes = ((LockoutTimeEventArgs)e).Minutes;
				}

				Task.Run(async () =>
				{
					await FailedAttemptsHelper.TooManyFailedAttempts(SettingsValues.ApiURLValue, "0", minutes);
					if (FailedAttemptsHelper.IsShowTooManyFailedAttempts() ?? false)
					{
						//_userDialogs?.HideLoading();
      //                  await Mvx.IoCProvider.Resolve<IMvxNavigationService>().Navigate<PatientTooManyFailedAttemptsViewModel>();
                     //  await Ioc.Resolve<IMvxNavigationService>().Navigate<PatientTooManyFailedAttemptsViewModel>();
					}
				});
			}
			catch (Exception)
			{
			}
		}
	}
}
