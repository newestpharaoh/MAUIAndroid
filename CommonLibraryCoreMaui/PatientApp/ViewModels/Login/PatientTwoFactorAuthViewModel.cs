using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.Services;
using CommonLibraryCoreMaui.ViewModels;
using MvvmCross.Commands;
using System.Threading.Tasks;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class PatientTwoFactorAuthViewModel : BaseViewModel
    {
        IUserAccountService _userAccountService;

        public IMvxCommand SendCodeCommand => new MvxAsyncCommand(SendCode);

        public PatientTwoFactorAuthViewModel(IUserAccountService userAccountService, IProviderService providerService)
        {
            _userAccountService = userAccountService;

        }

        private NotificationPreferencesViewModel _notificationPreferences;
        public NotificationPreferencesViewModel NotificationPreferences
        {
            get { return _notificationPreferences; }
            set { SetProperty(ref _notificationPreferences, value); }
        }

        public async override Task Initialize()
        {
            IsBusy = true;
            try
            {
                UserContactPreference ucp = await _userAccountService.GetUserContactPreference((int)Globals.Instance.UserInfo.UserID).ConfigureAwait(false);
                var model = new NotificationPreferencesViewModel()
                {
                    Email = ucp.Email,
                    Phone = ucp.Phone,
                };
                NotificationPreferences = model;
            }
            catch { }
            IsBusy = false;
            await base.Initialize();
        }

        private async Task SendCode()
        {
            IsBusy = true;
            StatusResponse resp = await _userAccountService.UserAccountSendCode(Globals.Instance.UserInfo.UserID, NotificationPreferences.NotificationPreference.ToLower()).ConfigureAwait(false);
            IsBusy = false;
            if (resp != null)
            {
                if (string.IsNullOrEmpty(resp.ErrorMessage))
                {
                    if (resp.StatusCode == StatusCode.Success)
                    {
                        await _navigationService.Navigate<PatientTwoFactorAuthStep2ViewModel>();
                    }
                    else if (!string.IsNullOrEmpty(resp.Message))
                    {
                        await _userDialogs.AlertAsync($"Verification code request failed. Reason: {resp.Message}");
                    }
                }
                else
                {
                    await _userDialogs.AlertAsync(resp.ErrorMessage);
                }
            }
            else
            {
                await _userDialogs.AlertAsync("Verification failed. No response from server.");
            }
        }
    }
}
