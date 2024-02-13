using System.Threading.Tasks;
using MvvmCross.Commands;
using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.ViewModels;
using CommonLibraryCoreMaui.Models.NavigationParameters;
using System;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class PatientRegistrationVerificationViewModel : BaseNavigationViewModel<bool>
	{
		int verificationCode;
		public bool IsBackBarNeeded;
		private NotificationPreferencesViewModel _notificationPreferences;
		public NotificationPreferencesViewModel NotificationPreferences
		{
			get { return _notificationPreferences; }
			set { SetProperty(ref _notificationPreferences, value); }
		}

		private string _enteredVerificationCode;
		public string EnteredVerificationCode
		{
			get { return _enteredVerificationCode; }
			set { SetProperty(ref _enteredVerificationCode, value); }
		}

		private bool _isEnabled = true;
		public bool IsEnabled
		{
			get { return _isEnabled; }
			set { SetProperty(ref _isEnabled, value); }
		}

		public bool IsSelfPay
		{
			get { return Registration.Instance.IsSelfPay; }
		}

		public IMvxCommand SendCodeCommand => new MvxAsyncCommand(SendCode);
		public IMvxCommand VerifyCodeCommand => new MvxAsyncCommand(VerifyCode);
		public IMvxCommand GoToPreviousPageCommand => new MvxAsyncCommand(GoToPreviousPage);

		public PatientRegistrationVerificationViewModel()
		{
			NotificationPreferences = new NotificationPreferencesViewModel()
			{
				Email = MaskHelper.MaskEmail(Registration.Instance.Email),
				Phone = MaskHelper.MaskPhoneNumber(Registration.Instance.Phone),
			};
		}

		public override void Prepare(bool parameter)
		{
			IsBackBarNeeded = parameter;
			base.Prepare();
		}

		private async Task GoToPreviousPage()
		{
			await _navigationService.Close(this);
		}

		private async Task SendCode()
		{
			IsBusy = true;
			try
			{
				StatusResponse resp = await DataUtility.SendRegistrationCodeAsync(SettingsValues.ApiURLValue, 0,
					Registration.Instance.FirstName,
					Registration.Instance.LastName,
					NotificationPreferences.NotificationPreference.ToLower() == "email" ? SendMethod.Email : SendMethod.Phone,
					Registration.Instance.Phone,
					Registration.Instance.Email).ConfigureAwait(false);

				IsBusy = false;
				if (resp != null)
				{
					if (resp.StatusCode == StatusCode.Success)
					{
						if (!int.TryParse(resp.Payload, out verificationCode))
						{
							await _userDialogs.AlertAsync("An error occurred!");
						}
						else
						{
							await _userDialogs.AlertAsync("The verification code has been sent.");
						}
					}
					else if (resp.StatusCode == StatusCode.NotFound || resp.StatusCode == StatusCode.Error)
					{
						await _userDialogs.AlertAsync("An error occurred!");
					}
				}
				else
				{
				}
			}
			catch { IsBusy = false; }
		}

		private async Task VerifyCode()
        {
			try
			{
                if (!string.IsNullOrEmpty(EnteredVerificationCode))
				{
					IsEnabled = false;
					IsBusy = true;
					StatusResponse resp = await DataUtility.AuthenticateRegistrationCodeAsync(SettingsValues.ApiURLValue, verificationCode, EnteredVerificationCode).ConfigureAwait(false);

					if (resp != null)
					{
                        if (string.IsNullOrEmpty(resp.ErrorMessage))
						{
                            if (resp.StatusCode == StatusCode.Success)
							{
                                if (Registration.Instance.IsSelfPay)
								{
                                    StatusResponse step3resp = await DataUtility.RegistrationStep3SelfPayAsync(SettingsValues.ApiURLValue, Registration.Instance).ConfigureAwait(false);
									IsBusy = false;
									if (step3resp != null)
									{
                                        switch (step3resp.StatusCode)
										{
											case StatusCode.Success:
                                                int patientId;
												if (int.TryParse(step3resp.Payload, out patientId))
												{
													IsEnabled = true;
													await _navigationService.Navigate<PatientMedicalnfoViewModel, MedicalHistoryNavigationParam>(
														new MedicalHistoryNavigationParam() { PatientId = patientId, NavigationType = MedicalInfoNavigationType.Registration });
												}
												else
												{
													IsEnabled = true;
													await _userDialogs.AlertAsync("Verification failed. No response from server.");
												}
												break;
											default:
												IsEnabled = true;
												await _userDialogs.AlertAsync("There was an error please try again.");
												break;
										}
									}
									else
									{
										IsEnabled = true;
										IsBusy = false;
										await _userDialogs.AlertAsync("There was an error please try again.");
									}
								}
								else
								{
									IsEnabled = true;
									IsBusy = false;
									await _navigationService.Navigate<PatientRegistrationStepOneViewModel>();
                                }
							}
							else if (resp.StatusCode == StatusCode.CodeInvalid)
							{
								IsEnabled = true;
								IsBusy = false;
								await _userDialogs.AlertAsync("Verification failed. Please retry or press Send Code to have a new code sent to you.");
							}
							else if (resp.StatusCode == StatusCode.CodeExpired)
							{
								IsEnabled = true;
								IsBusy = false;
								await _userDialogs.AlertAsync("Code expired. Please retry or press Send Code to have a new code sent to you.");
							}
							else if (resp.StatusCode == StatusCode.Lockout) // lockout
							{
								IsEnabled = true;
								IsBusy = false;
							}
							else if (!string.IsNullOrEmpty(resp.Message))
							{
								IsEnabled = true;
								IsBusy = false;
								await _userDialogs.AlertAsync($"Verification failed. {resp.Message}");
							}
						}
						else
						{
							IsEnabled = true;
							IsBusy = false;
							await _userDialogs.AlertAsync($"Verification failed. Server response: {resp.ErrorMessage}");
						}
					}
					else
					{
						IsEnabled = true;
						IsBusy = false;
						await _userDialogs.AlertAsync("Verification failed. No response from server.");
					}
				}
				else
				{
					IsEnabled = true;
					IsBusy = false;
					await _userDialogs.AlertAsync("Please enter the verification code.");
				}
			}
			catch 
            {
				IsEnabled = true;
				IsBusy = false;
			}
        }
    }
}