using System.Threading.Tasks;
using CommonLibraryCoreMaui.PatientApp.ViewModels;
using MvvmCross.Commands;

namespace CommonLibraryCoreMaui.ViewModels
{
    public class PatientSettingsFinePrintViewModel : BaseViewModel
	{
		public IMvxCommand GoTermOfUseCommand => new MvxAsyncCommand(GoTermOfUse);
		public IMvxCommand GoBillingPoliciesCommand => new MvxAsyncCommand(GoBillingPolicies);

		public async override Task Initialize()
		{
			await base.Initialize();
		}

		private async Task GoTermOfUse()
		{
			await _navigationService.Navigate<PatientSettingsFinePrintTermsOfUseViewModel>();
		}

		private async Task GoBillingPolicies()
		{
			await _navigationService.Navigate<PatientSettingsBillingPollicesViewModel>();
		}
	}

	public class PatientSettingsBillingPollicesViewModel : BaseViewModel
	{
		public async override Task Initialize()
		{
			await base.Initialize();
		}
	}
}
