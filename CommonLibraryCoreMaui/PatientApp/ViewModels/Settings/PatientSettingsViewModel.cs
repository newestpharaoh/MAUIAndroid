using System;
using System.Threading.Tasks;
using CommonLibraryCoreMaui.Models.NavigationParameters;
using CommonLibraryCoreMaui.ViewModels;
using MvvmCross.Commands;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
	public class PatientSettingsViewModel : LogoutFunctionalityViewModel
	{
		public IMvxCommand GoProfileCommand => new MvxAsyncCommand(GoProfile);
		public IMvxCommand GoManageSubscriptionCommand => new MvxAsyncCommand(GoManageSubscription);
		public IMvxCommand GoFinePrintCommand => new MvxAsyncCommand(GoFinePrint);

		public override Task Initialize()
		{
			Title = "Settings";
			return base.Initialize();
		}



		private async Task GoProfile()
		{
			if (Globals.Instance.UserInfo.IsPrivate)
			{
				await _navigationService.Navigate<PatientProfileViewModel, ProfileNavigationParam>(new ProfileNavigationParam()
				{
					IsProfile = true,
					PatientId = 0,
					IsEmailEnabled = false
				});
			}
			else
			{
				await _navigationService.Navigate<PatientAccountProfilesViewModel, bool>(false);
			}
		}



		private async Task GoManageSubscription()
		{
			await _navigationService.Navigate<PatientSettingsManageSubscriptionMembersViewModel, bool>(false);
		}



		private async Task GoFinePrint()
		{
			await _navigationService.Navigate<PatientSettingsFinePrintViewModel>();
		}
	}
}
