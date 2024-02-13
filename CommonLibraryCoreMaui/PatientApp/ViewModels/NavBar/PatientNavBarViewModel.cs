using System.Threading.Tasks;
using CommonLibraryCoreMaui.Models.NavigationParameters;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class PatientNavBarViewModel  : MvxViewModel
    {
        IMvxNavigationService _navigationService;

		public IMvxCommand GoFamilyCommand => new MvxAsyncCommand(GoToFamily);
		public IMvxCommand GoMyAccountCommand => new MvxAsyncCommand(GoToMyAccount);
        public IMvxCommand GoHomeCommand => new MvxAsyncCommand(GoToHome);
        public IMvxCommand GoVisitsCommand => new MvxAsyncCommand(GoToVisits);
        public IMvxCommand GoMedicalInfoCommand => new MvxAsyncCommand(GoToMedicalInfo);
        public bool _isPatientTermed;
        public bool IsPatientTermed
        {
            get { return _isPatientTermed; }
            set { SetProperty(ref _isPatientTermed, value); }
        }
        public PatientNavBarViewModel()
        {
           
            _navigationService = MvvmCross.Mvx.IoCProvider.Resolve<IMvxNavigationService>();
            IsPatientTermed = Globals.Instance.IsTermed;
        }

        private async Task GoToFamily()
        {
            
            await _navigationService.Navigate<PatientAccountProfilesViewModel>();

        }

        private async Task GoToHome()
        {
            await _navigationService.Navigate<HomeViewModel>();
        }

        private async Task GoToMyAccount()
        {
            await _navigationService.Navigate<PatientSettingsViewModel>();
        }

        private async Task GoToVisits()
        {
			await _navigationService.Navigate<VisitsScreenViewModel>();
		}

        private async Task GoToMedicalInfo()
        {
			await _navigationService.Navigate<PatientMedicalInfoMedicalHistoryDetailViewModel, MedicalHistoryNavigationParam>(
			new MedicalHistoryNavigationParam()
			{
				PatientId = Globals.Instance.UserInfo.PatientID,
				Name = $"{Globals.Instance.UserInfo.FirstName} {Globals.Instance.UserInfo.LastName}"
			});
		}
    }
}