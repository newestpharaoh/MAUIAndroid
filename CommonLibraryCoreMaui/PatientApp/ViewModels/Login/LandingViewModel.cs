using CommonLibraryCoreMaui.ViewModels;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class LandingViewModel : BaseViewModel
    {
        public IMvxAsyncCommand GoLoginCommand { get; private set; }
        public IMvxAsyncCommand GoRegistrationCommand { get; private set; }

        public LandingViewModel()
        {
            GoLoginCommand = new MvxAsyncCommand(() => _navigationService.Navigate<PatientLoginViewModel>());
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }
    }
}