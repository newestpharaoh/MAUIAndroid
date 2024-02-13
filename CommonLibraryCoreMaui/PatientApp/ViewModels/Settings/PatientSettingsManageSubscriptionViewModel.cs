using System.Threading.Tasks;
using CommonLibraryCoreMaui.ViewModels;
using MvvmCross.Commands;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
	public class PatientSettingsManageSubscriptionViewModel : BaseViewModel
	{
		public IMvxCommand UpdateCardInformationCommand => new MvxAsyncCommand(UpdateCardInformation);
		public IMvxCommand ManageMemberCommand => new MvxAsyncCommand(ManageMember);



		public async override Task Initialize()
		{
			Title = "Manage Subscription";
			await base.Initialize();
		}



		private async Task UpdateCardInformation()
		{
			await _navigationService.Navigate<PatientSettingsManageCardInfoViewModel>();
		}



		private async Task ManageMember()
		{
			await _navigationService.Navigate<PatientSettingsManageSubscriptionMembersViewModel, bool>(false);
		}
	}
}
