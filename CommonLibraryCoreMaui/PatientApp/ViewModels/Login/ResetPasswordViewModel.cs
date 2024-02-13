using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.ViewModels;
using MvvmCross.Commands;
using System;
using System.Threading.Tasks;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class ResetPasswordViewModel : BaseNavigationViewModel<int>
	{
		private int UserId;
		private string _newPasswordText;
		public string NewPasswordText
		{
			get { return _newPasswordText; }
			set { SetProperty(ref _newPasswordText, value); }
		}

		private string _retypePasswordText;
		public string RetypePasswordText
		{
			get { return _retypePasswordText; }
			set { SetProperty(ref _retypePasswordText, value); }
		}

		private bool _isErorr = false;
		public bool IsErorr
		{
			get { return _isErorr; }
			set { SetProperty(ref _isErorr, value); }
		}

		private string _errorText;
		public string ErrorText
		{
			get { return _errorText; }
			set { SetProperty(ref _errorText, value); }
		}

		public IMvxCommand SubmitCommand => new MvxAsyncCommand<Action>(SubmitPassword);

		public override Task Initialize()
        {
            return base.Initialize();
        }

		public override void Prepare(int parameter)
		{
			UserId = parameter;
			base.Prepare();
		}

		public virtual async Task SubmitPassword(Action successAction)
		{
			try
			{
				IsErorr = false;
				ErrorText = string.Empty;
				if (string.IsNullOrEmpty(NewPasswordText) && string.IsNullOrEmpty(RetypePasswordText))
				{
					ErrorText = "Passwords cannot be empty. Try again.";
					IsErorr = true;
					return;
				}

				if (NewPasswordText != RetypePasswordText)
				{
					ErrorText = "These passwords do not match. Try again.";
					IsErorr = true;
					return;
				}

				IsBusy = true;
				StatusResponse resp = await DataUtility.ForgotPasswordResetPassword(SettingsValues.ApiURLValue, UserId, NewPasswordText).ConfigureAwait(false);
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
                            ErrorText = "Password must be between 8-10 characters and contain at least 1 capital letter, 1 number, and 1 symbol (e.g. !, ?,.)";
							IsErorr = true;
							break;
						case StatusCode.IncorrectPassword:
							if (!string.IsNullOrEmpty(resp.Message))
							{
								ErrorText = resp.Message;
								IsErorr = true;
							}
							break;
					}
				}
			}
			catch (Exception ex)
			{
				ReportCrash(ex, Title);
				IsBusy = false;
				ErrorText = ex.Message;
				IsErorr = true;
			}
			IsBusy = false;
		}
	}
}
