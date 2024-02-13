using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using CommonLibraryCoreMaui.ViewModels;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class PatientAddEditPharmacyViewModel : BaseViewModel
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set { SetProperty(ref _comment, value); }
        }

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
            if (!String.IsNullOrEmpty(Name) || !String.IsNullOrEmpty(Comment))
            {
                var response = await _userDialogs.ConfirmAsync("Are you sure?");
                if (response)
                    return;
            }

            await _navigationService.Close(this);
        }
    }
}
