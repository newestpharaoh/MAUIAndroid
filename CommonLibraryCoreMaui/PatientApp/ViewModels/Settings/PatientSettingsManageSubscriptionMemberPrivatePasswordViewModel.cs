using System;
using System.Threading.Tasks;
using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.ViewModels;
using MvvmCross.Commands;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
	public class PatientSettingsManageSubscriptionMemberPrivatePasswordViewModel : BaseNavigationViewModel<string>
	{
		public string PasswordId { get; set; }
		PrivateLinkInfo resp;

		private string _patientNameText;
		public string PatientNameText
		{
			get { return _patientNameText; }
			set { SetProperty(ref _patientNameText, value); }
		}

		private string _patientNameDescription;
		public string PatientNameDescription
		{
			get { return _patientNameDescription; }
			set { SetProperty(ref _patientNameDescription, value); }
		}

		private string _confirmPasswordText;
		public string ConfirmPasswordText
		{
			get { return _confirmPasswordText; }
			set { SetProperty(ref _confirmPasswordText, value); }
		}

		private string _passwordText;
		public string PasswordText
		{
			get { return _passwordText; }
			set { SetProperty(ref _passwordText, value); }
		}

		public IMvxCommand SubmitCommand => new MvxAsyncCommand(SubmitAsync);

		public PatientSettingsManageSubscriptionMemberPrivatePasswordViewModel() { }

		public override void Prepare(string parameter)
		{
			PasswordId = parameter;
			Task.Run(async () =>
			{
				resp = await DataUtility.GetMakePrivateLinkInfoAsync(SettingsValues.ApiURLValue, CommonAuthSession.Token, PasswordId).ConfigureAwait(false);
				if (resp != null)
				{
					PatientNameText = $"Please set a new password for {resp.PatientName}";
					PatientNameDescription = $"Though still a member of the {resp.ParentAccountName} family, this profile will now be private.";
				}
			});
			base.Prepare();
		}

		private async Task SubmitAsync()
		{
			if (string.IsNullOrEmpty(ConfirmPasswordText) && string.IsNullOrEmpty(PasswordText))
			{
				await _userDialogs.AlertAsync("Passwords cannot be empty. Try again.");
				return;
			}

			if (ConfirmPasswordText != PasswordText)
			{
				await _userDialogs.AlertAsync("These passwords do not match. Try again.");
				return;
			}

			IsBusy = true;

			StatusResponse createPrivateMemberResp = await DataUtility.CreatePrivateMemberPasswordAsync(SettingsValues.ApiURLValue, new PrivateMemberPassword() { Password = PasswordText, PasswordID = PasswordId }, CommonAuthSession.Token).ConfigureAwait(false);

			if (createPrivateMemberResp.StatusCode == StatusCode.Success)
			{
				TokenResponse token = await DataUtility.GetTokenResponseAsync(SettingsValues.ApiURLValue, resp.PatientEmail, PasswordText, "0").ConfigureAwait(false);
				if (token != null)
				{
					if (!string.IsNullOrEmpty(token.access_token) && token.expires_in != null)
					{
						CommonAuthSession.Token = token.access_token;
						CommonAuthSession.SetTokenExpirationDate(DateTime.Now.AddSeconds(Convert.ToInt32(token.expires_in)));

						UserInfo userInfo = await DataUtility.GetUserInfo(SettingsValues.ApiURLValue, token.userid, true, CommonAuthSession.Token).ConfigureAwait(false);
						IsBusy = false;
						Globals.Instance.UserInfo = userInfo;
						CommonAuthSession.IsAutheticated = true;

						await _navigationService.Navigate<HomeViewModel>();
					}
				}
				else
				{
					await _userDialogs.AlertAsync("There was an error please try again.");
				}
			}
			else if (createPrivateMemberResp.StatusCode == StatusCode.PasswordRequirementNotMet || createPrivateMemberResp.StatusCode == StatusCode.PsswdReqNotMet ||
                    createPrivateMemberResp.StatusCode == StatusCode.PsswdAtLeast8Chars || createPrivateMemberResp.StatusCode == StatusCode.PsswdAtLeastOneOfThese ||
                    createPrivateMemberResp.StatusCode == StatusCode.PsswdAtLeastOneLowerAndOneUpper)
			{
				IsBusy = false;
				await _userDialogs.AlertAsync("Password must be between 8-10 characters and contain at least 1 capital letter, 1 number, and 1 symbol (e.g. !, ?,.)");
			}
			else if (createPrivateMemberResp.StatusCode == StatusCode.NotFound)
			{
				IsBusy = false;
				await _userDialogs.AlertAsync("Record not found. Please try again.");
			}
			else
			{
				IsBusy = false;
				await _userDialogs.AlertAsync("There was an error please try again.");
			}

			IsBusy = false;
		}
	}
}
