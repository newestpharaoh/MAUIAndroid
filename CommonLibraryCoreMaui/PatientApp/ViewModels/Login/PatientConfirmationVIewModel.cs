using System.Threading.Tasks;

using CommonLibraryCoreMaui.ViewModels;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class PatientConfirmationViewModel : BaseViewModel
    {
        private string _viewTitle;
        public string ViewTitle
        {
            get { return _viewTitle; }
            set { SetProperty(ref _viewTitle, value); }
        }

        private string _subtitle;
        public string Subtitle
        {
            get { return _subtitle; }
            set { SetProperty(ref _subtitle, value); }
        }

        public IMvxCommand OkayCommand => new MvxAsyncCommand(Okay);

        public override Task Initialize()
        {
            return base.Initialize();
        }

        private async Task Okay()
        {
            await _navigationService.Navigate<PatientRegistrationStepOneViewModel>();
        }
    }
}