using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.Services;
using CommonLibraryCoreMaui.ViewModels;
using MvvmCross.Commands;
using System.Threading.Tasks;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class PatientTwoFactorAuthStep2ViewModel : BaseViewModel
    {
        IUserAccountService _userAccountService;
        public IMvxCommand SubmitCommand => new MvxAsyncCommand(Submit);

        private string _code;
        public string Code
        {
            get { return _code; }
            set
            {
                SetProperty(ref _code, value);
            }
        }

        public PatientTwoFactorAuthStep2ViewModel(IUserAccountService service)
        {
            _userAccountService = service;
        }

        public async override Task Initialize()
        {
            await base.Initialize();
        }

        private async Task Submit()
        {
			IsBusy = true;
            StatusResponse resp = await _userAccountService.UserAccountAuthenticateCode(Globals.Instance.UserInfo.UserID, Code).ConfigureAwait(false);
            IsBusy = false;

            if (resp != null)
            {
                if (string.IsNullOrEmpty(resp.ErrorMessage))
                {
                    if (resp.StatusCode == StatusCode.Success)
                    {
						if (Globals.Instance.UserInfo.ProviderID is null)
						{
							UserInfo userInfo = await DataUtility.GetUserInfo(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.UserID, true, CommonAuthSession.Token).ConfigureAwait(false);
							Globals.Instance.UserInfo = userInfo;
						}
						CommonAuthSession.IsAutheticated = true;
						await _navigationService.Navigate<HomeViewModel>();
                    }
                    else if (!string.IsNullOrEmpty(resp.Message))
                    {
                        await _userDialogs.AlertAsync($"Verification failed. {resp.Message}");
                    }
                }
                else
                {
                    await _userDialogs.AlertAsync($"Verification failed. Server response: {resp.ErrorMessage}");
                }
            }
            else
            {
                await _userDialogs.AlertAsync("Verification failed. No response from server.");
            }
        }
    }
}
