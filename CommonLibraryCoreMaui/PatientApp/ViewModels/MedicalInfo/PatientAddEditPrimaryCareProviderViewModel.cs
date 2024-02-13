using System.Threading.Tasks;
using MvvmCross.Commands;
using CommonLibraryCoreMaui.ViewModels;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class PatientAddEditPrimaryCareProviderViewModel : BaseViewModel
    {
        public IMvxCommand ContinueCommand => new MvxAsyncCommand(Continue);
        public IMvxCommand CancelCommand => new MvxAsyncCommand(Cancel);

        public override Task Initialize()
        {
            return base.Initialize();
        }

        private async Task Continue()
        {
            await _navigationService.Close(this);
        }

        private async Task Cancel()
        {
            await _navigationService.Close(this);
        }
    }
}