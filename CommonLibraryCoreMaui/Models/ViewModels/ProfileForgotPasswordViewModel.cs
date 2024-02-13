using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using CommonLibraryCoreMaui.Models;

namespace CommonLibraryCoreMaui.ViewModels
{
	public class ProfileForgotPasswordViewModel : BaseViewModel
	{
        private bool _isError = false;
        public bool IsError
        {
            get { return _isError; }
            set { SetProperty(ref _isError, value); }
        }

        private string _errorText;
        public string ErrorText
        {
            get { return _errorText; }
            set { SetProperty(ref _errorText, value); }
        }

        private string _confirmPasswordText;
		public string ConfirmPasswordText
		{
			get { return _confirmPasswordText; }
			set { SetProperty(ref _confirmPasswordText, value); }
		}

		private string _newPasswordText;
		public string NewPasswordText
		{
			get { return _newPasswordText; }
			set { SetProperty(ref _newPasswordText, value); }
		}

		private string _currentPasswordText;
		public string CurrentPasswordText
		{
			get { return _currentPasswordText; }
			set { SetProperty(ref _currentPasswordText, value); }
		}

		public ProfileForgotPasswordViewModel(IUserDialogs userDialogs)
		{
			_userDialogs = userDialogs;
		}

		public virtual async Task UpdatePassword(Action successAction)
		{
			try
			{
				if (string.IsNullOrEmpty(ConfirmPasswordText) && string.IsNullOrEmpty(NewPasswordText))
				{
					await _userDialogs.AlertAsync("Passwords cannot be empty. Try again.");
					return;
				}

				if (ConfirmPasswordText != NewPasswordText)
				{
					await _userDialogs.AlertAsync("These passwords do not match. Try again.");
					return;
				}

				IsBusy = true;

				StatusResponse resp = await DataUtility.UpdateProviderPasswordAsync(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.ProviderID.ToString(), CurrentPasswordText, NewPasswordText, CommonAuthSession.Token);
				if (resp != null)
				{
					IsBusy = false;
					switch (resp.StatusCode)
					{
						case StatusCode.Success:
							successAction.Invoke();
							break;
						case StatusCode.PasswordRequirementNotMet:
                        case StatusCode.PsswdReqNotMet:
                        case StatusCode.PsswdAtLeast8Chars:
                        case StatusCode.PsswdAtLeastOneOfThese:
                        case StatusCode.PsswdAtLeastOneLowerAndOneUpper:
                            await _userDialogs.AlertAsync("Password must be between 8-10 characters and contain at least 1 capital letter, 1 number, and 1 symbol (e.g. !, ?,.)");
							break;
						case StatusCode.IncorrectPassword:
							if (!string.IsNullOrEmpty(resp.Message)) _userDialogs.Toast(resp.Message);
							break;
					}
				}
			}
			catch (Exception ex)
			{
                ReportCrash(ex, Title);
                IsBusy = false;
				await _userDialogs.AlertAsync(ex.Message);
			}
			IsBusy = false;
		}

	}

    public class ProviderChangePasswordViewModel : BaseViewModel
    {
        private bool _isError = false;
        public bool IsError
        {
            get { return _isError; }
            set { SetProperty(ref _isError, value); }
        }

        private string _errorText;
        public string ErrorText
        {
            get { return _errorText; }
            set { SetProperty(ref _errorText, value); }
        }

        private string _confirmPasswordText;
        public string ConfirmPasswordText
        {
            get { return _confirmPasswordText; }
            set { SetProperty(ref _confirmPasswordText, value); ErrorText = string.Empty; }
        }

        private string _newPasswordText;
        public string NewPasswordText
        {
            get { return _newPasswordText; }
            set { SetProperty(ref _newPasswordText, value); ErrorText = string.Empty; }
        }

        private string _currentPasswordText;
        public string CurrentPasswordText
        {
            get { return _currentPasswordText; }
            set { SetProperty(ref _currentPasswordText, value); }
        }

        public ProviderChangePasswordViewModel(IUserDialogs userDialogs)
        {
            _userDialogs = userDialogs;
        }

        public virtual async Task UpdatePassword(Action successAction)
        {
            try
            {
                IsError = false;
                if (string.IsNullOrEmpty(ConfirmPasswordText) && string.IsNullOrEmpty(NewPasswordText))
                {
                    IsError = true;
                    ErrorText = "Please write new password and / or confirmed password.";
                    return;
                }

                if (ConfirmPasswordText != NewPasswordText)
                {
                    IsError = true;
                    ErrorText = "New Password does not match Confirm Password.";
                    return;
                }

				
				IsBusy = true;

                StatusResponse resp = await DataUtility.UpdateProviderPasswordAsync(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.ProviderID.ToString(), CurrentPasswordText, NewPasswordText, CommonAuthSession.Token);
                if (resp != null)
                {
                    IsBusy = false;
                    switch (resp.StatusCode)
                    {
                        case StatusCode.Success:
                            successAction.Invoke();
                            break;
                        case StatusCode.PasswordRequirementNotMet:
                        case StatusCode.IncorrectPassword:
                            if (!string.IsNullOrEmpty(resp.Message))
                            {
                                IsError = true;
                                ErrorText = "Current password is incorrect.";
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ReportCrash(ex, Title);
                IsBusy = false;
                await _userDialogs.AlertAsync(ex.Message);
            }
            IsBusy = false;
        }

    }

    public class PatientProfileForgotPasswordViewModel : BaseViewModel
    {
        private bool _isError = false;
        public bool IsError
        {
            get { return _isError; }
            set { SetProperty(ref _isError, value); }
        }

        private string _errorText;
        public string ErrorText
        {
            get { return _errorText; }
            set { SetProperty(ref _errorText, value); }
        }

        private string _confirmPasswordText;
        public string ConfirmPasswordText
        {
            get { return _confirmPasswordText; }
            set { SetProperty(ref _confirmPasswordText, value); }
        }

        private string _newPasswordText;
        public string NewPasswordText
        {
            get { return _newPasswordText; }
            set { SetProperty(ref _newPasswordText, value); }
        }

        private string _currentPasswordText;
        public string CurrentPasswordText
        {
            get { return _currentPasswordText; }
            set { SetProperty(ref _currentPasswordText, value); }
        }

        public PatientProfileForgotPasswordViewModel(IUserDialogs userDialogs)
		{

		}

		public async Task UpdatePassword(Action successAction)
		{
			try
			{
                IsError = false;
				if (string.IsNullOrEmpty(ConfirmPasswordText) && string.IsNullOrEmpty(NewPasswordText))
				{
                    IsError = true;
                    ErrorText = "Please provide new password and / or confirmed password.";
					return;
				}

				if (ConfirmPasswordText != NewPasswordText)
				{
                    IsError = true;
                    ErrorText = "New Password does not match Confirm Password.";
					return;
				}

				IsBusy = true;
				
				Password pwd = new Password() { CurrentPassword = CurrentPasswordText, ID = Globals.Instance.UserInfo.PatientID, NewPassword = NewPasswordText };
				StatusResponse resp = await DataUtility.PatientUpdatePasswordAsync(SettingsValues.ApiURLValue, CommonAuthSession.Token, pwd).ConfigureAwait(false);
				if (resp != null)
				{
					IsBusy = false;
					switch (resp.StatusCode)
					{
						case StatusCode.Success:
							successAction.Invoke();
							break;
						case StatusCode.PasswordRequirementNotMet:
						case StatusCode.IncorrectPassword:
							if (!string.IsNullOrEmpty(resp.Message))
                            {
                                IsError = true;
                                ErrorText = resp.Message;
                            }
							break;
                    }
                }
			}
			catch (Exception ex)
			{
                ReportCrash(ex, Title);
                IsBusy = false;
				await _userDialogs.AlertAsync(ex.Message);
			}
			IsBusy = false;
		}
	}
}
