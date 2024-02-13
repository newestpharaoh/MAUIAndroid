using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using CommonLibraryCoreMaui.Models;
using MvvmCross.Navigation;
using Acr.UserDialogs;
using System.Linq;

namespace CommonLibraryCoreMaui.ViewModels
{
    public class PatientRegistrationStepOneViewModel : BaseViewModel
    {
		private RegistrationUserInfo _userProfile;
		public RegistrationUserInfo UserProfile
		{
			get { return _userProfile; }
			set { SetProperty(ref _userProfile, value); }
		}

		private string _paymentPlan;
		public string PaymentPlan
		{
			get { return _paymentPlan; }
			set { SetProperty(ref _paymentPlan, value); }
		}

		private string _isEstablished;
		public string IsEstablished
		{
			get { return _isEstablished; }
			set { SetProperty(ref _isEstablished, value); }
		}

		private string _userPassword;
		public string UserPassword
		{
			get { return _userPassword; }
			set { SetProperty(ref _userPassword, value); }
		}

		private string _userConfirmPassword;
		public string UserConfirmPassword
		{
			get { return _userConfirmPassword; }
			set { SetProperty(ref _userConfirmPassword, value); }
		}

		private string _paymentPlanHeading;
		public string PaymentPlanHeading
		{
			get { return _paymentPlanHeading; }
			set { SetProperty(ref _paymentPlanHeading, value); }
		}

		private bool _isTermsAndConditionChecked;
		public bool IsTermsAndConditionChecked
		{
			get { return _isTermsAndConditionChecked; }
			set { SetProperty(ref _isTermsAndConditionChecked, value); }
		}

		public bool IsSelfPay
		{
			get { return Registration.Instance.IsSelfPay; }
		}

		public IMvxCommand ContinueCommand => new MvxAsyncCommand(Continue);
		public IMvxCommand GoToPreviousCommand => new MvxAsyncCommand(GoToPreviousAsync);

		public PatientRegistrationStepOneViewModel(IMvxNavigationService mvxNavigationService, IUserDialogs userDialogs)
		{
			_navigationService = mvxNavigationService;
			_userDialogs = userDialogs;
		}

        public async override Task Initialize()
        {
			SetUserInfo();
			await GetUIText();
			await base.Initialize();
		}

		private void SetUserInfo()
		{
			UserProfile = new RegistrationUserInfo();
			UserProfile.FirstName = Registration.Instance.FirstName;
			UserProfile.LastName = Registration.Instance.LastName;
			UserProfile.Email = Registration.Instance.Email;
			UserProfile.Phone = Registration.Instance.Phone;
			UserProfile.DOB = Registration.Instance.DOB;

			UserProfile.Street1 = Registration.Instance.Street1;
			UserProfile.Street2 = Registration.Instance.Street2;
			UserProfile.City = Registration.Instance.City;
			UserProfile.Zip = Registration.Instance.Zip;
			UserProfile.State = string.IsNullOrEmpty(Registration.Instance.State)
												? "TX" : Registration.Instance.State;

			UserProfile.Domain = Registration.Instance.Domain;
			UserPassword = Registration.Instance.Password;
			UserConfirmPassword = Registration.Instance.Password;
			UserProfile.PreferredName = Registration.Instance.PreferredName;

			UserProfile.Gender = string.IsNullOrEmpty(Registration.Instance.Gender)
												? "Select": Registration.Instance.Gender;
			if (Registration.Instance.Established != null)
				UserProfile.Established = Registration.Instance.Established.Value;
			UserProfile.Language = Registration.Instance.Language;

            
            if (!string.IsNullOrEmpty(Registration.Instance.PatientID))
                UserProfile.PatientID = Convert.ToInt64(Registration.Instance.PatientID);
            
            PaymentPlanHeading = "Payment Plan";
			if (Registration.Instance.Subscription != null && Registration.Instance.IsSelfPay)
			{
				PaymentPlan = Registration.Instance.Subscription.Name;
			}

			if (!Registration.Instance.IsSelfPay)
			{
				PaymentPlanHeading = "Insurance/Employer*";
				PaymentPlan = UserProfile.Domain;
			}
			
		}

        private async Task Continue()
        {
			if (
				string.IsNullOrEmpty(UserProfile.FirstName) ||
				string.IsNullOrEmpty(UserProfile.LastName) ||
				string.IsNullOrEmpty(UserProfile.Email) ||
				string.IsNullOrEmpty(UserProfile.Language) ||
				string.IsNullOrEmpty(UserProfile.Phone) ||
				string.IsNullOrEmpty(UserProfile.Street1) ||
				string.IsNullOrEmpty(UserProfile.City) ||
				string.IsNullOrEmpty(UserProfile.Gender) || UserProfile.Gender.Equals("Select") ||
				string.IsNullOrEmpty(UserProfile.State) || UserProfile.State.Equals("Select")||
				string.IsNullOrEmpty(UserProfile.Zip)||
				string.IsNullOrEmpty(UserPassword) ||
				string.IsNullOrEmpty(UserConfirmPassword))
			{
				await _userDialogs.AlertAsync("Please fill all required fields!");
				return;
			}
			if (!System.Text.RegularExpressions.Regex.Match(UserProfile.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
			{
				await _userDialogs.AlertAsync("Email is not valid!");
				return;
			}
			if(UserPassword != UserConfirmPassword)
			{
				await _userDialogs.AlertAsync("Password and Confirmation Password doesn't match.");
				return;
			}
			if (!IsTermsAndConditionChecked)
			{
				await _userDialogs.AlertAsync("Please agree to the terms of use.");
				return;
			}
			IsBusy = true;
			UserProfile.Password = UserPassword;
			StatusResponse resp = await DataUtility.RegistrationStep1Async(SettingsValues.ApiURLValue, UserProfile).ConfigureAwait(false);

			if (resp != null)
			{
				IsBusy = false;
				switch (resp.StatusCode)
				{
					case StatusCode.PasswordRequirementNotMet:
                    case StatusCode.PsswdReqNotMet:
                    case StatusCode.PsswdAtLeast8Chars:
                    case StatusCode.PsswdAtLeastOneOfThese:
                    case StatusCode.PsswdAtLeastOneLowerAndOneUpper:
                        await _userDialogs.AlertAsync(resp.ErrorMessage ?? resp.Message);
						break;
					case StatusCode.EmailAlreadyInUse:
						await _userDialogs.AlertAsync("Email Already In Use.");
						break;
					case StatusCode.Success:

						SetRegistrationValues();

						await _navigationService.Navigate<PatientRegistrationStepTwoViewModel>();

						break;
					case StatusCode.InvalidData:
						await _userDialogs.AlertAsync("Error: Please check the data entered and try again.");
						break;
					case StatusCode.AlreadyOnActiveAccount:
						await _userDialogs.AlertAsync("Already On Active Account.");
						break;
					default:
						await _userDialogs.AlertAsync("There was an error please try again.");
						break;
				}
			}
			else
			{
				await _userDialogs.AlertAsync("An error occurred!");
			}
			IsBusy = false;

			
        }

		private void SetRegistrationValues(bool goBack = false)
		{
			Registration.Instance.PatientID = UserProfile.PatientID.ToString();
			Registration.Instance.FirstName = UserProfile.FirstName;
			Registration.Instance.LastName = UserProfile.LastName;
			Registration.Instance.PreferredName = UserProfile.PreferredName;
			Registration.Instance.Phone = UserProfile.Phone;
			Registration.Instance.Street1 = UserProfile.Street1;
			Registration.Instance.Street2 = UserProfile.Street2;
			Registration.Instance.Zip = UserProfile.Zip;
			Registration.Instance.State = UserProfile.State;
			Registration.Instance.Password = goBack ? string.Empty : UserProfile.Password;
			Registration.Instance.City = UserProfile.City;
			Registration.Instance.DOB = UserProfile.DOB;
			Registration.Instance.Email = UserProfile.Email;
			Registration.Instance.Gender = UserProfile.Gender;
			Registration.Instance.Language = UserProfile.Language;
			Registration.Instance.Established = UserProfile.Established;
			Registration.Instance.Domain = Registration.Instance.IsSelfPay ? PaymentPlan : UserProfile.Domain;
			if (Registration.Instance.IsSelfPay) Registration.Instance.SubscriptionOptionID = Registration.Instance.Subscription.OptionID;
		}

		private async Task GoToPreviousAsync()
		{
			SetRegistrationValues(true);
			IsTermsAndConditionChecked = false;
			await _navigationService.Close(this);
		}

		private async Task GetUIText()
		{
			await DataUtility.GetUITopicListAsync(SettingsValues.ApiURLValue, "Registration2", "en").ContinueWith(async (x) =>
			{
				UITopicValues = x.Result;				
			});
		}
		private UITopic _uITopicValues;
		public UITopic UITopicValues
		{
			get { return _uITopicValues; }
			set { SetProperty(ref _uITopicValues, value); }
		}
		public string GetUIValue(string lblName)
		{
			return UITopicValues?.UITextList.First(x => x.TagName.Equals(lblName))?.Text ?? string.Empty;
		}
	}
}