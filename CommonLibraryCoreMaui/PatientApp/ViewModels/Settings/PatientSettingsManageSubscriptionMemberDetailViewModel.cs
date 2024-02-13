using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Acr.UserDialogs;
using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.Services;
using CommonLibraryCoreMaui.ViewModels;
using Microsoft.Maui.Storage;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class PatientSettingsManageSubscriptionMemberDetailViewModel : BaseNavigationViewModel<AccountMember, bool>
	{
		private IPatientService _patientService;
		public string AppBrandname = Preferences.Get("AppName", string.Empty);
        bool hasCoverage = false;
        AccountMember _accountMember;
		public AccountMember AccountMember
		{
			get { return _accountMember; }
			set { SetProperty(ref _accountMember, value); }
		}

		private FamilyMemberOptionsViewModel _accountMemberOptionViewModel;
		public FamilyMemberOptionsViewModel AccountMemberOptionViewModel
		{
			get { return _accountMemberOptionViewModel; }
			set
			{
				SetProperty(ref _accountMemberOptionViewModel, value);
			}
		}

		string _warningText;
		public string WarningText
		{
			get { return _warningText; }
			set { SetProperty(ref _warningText, value); }
		}

		FamilyMemberOption _familyMemberOption;
		public FamilyMemberOption FamilyMemberOption
		{
			get { return _familyMemberOption; }
			set { SetProperty(ref _familyMemberOption, value); }
		}

        private UITopic _uITopicValues;
        public UITopic UITopicValues
        {
            get { return _uITopicValues; }
            set { SetProperty(ref _uITopicValues, value); }
        }
        public IMvxCommand ReturnCommand => new MvxAsyncCommand(Return);
		public MvxInteraction<MemberType> OpenPopupForMember { get; } = new MvxInteraction<MemberType>();

		public MvxCommandBase PrivateUserCommand => new MvxCommand(PrivateUser);
		public MvxCommandBase ReactivateUserCommand => new MvxAsyncCommand(ReactivateUser);
		public MvxCommandBase DeactivateUserCommandForPrimaryUserCommand => new MvxCommand(DeactivateUserCommandForPrimaryUser);
		public MvxCommandBase DeactivateUserCommand => new MvxCommand(DeactivateUser);
		public MvxCommandBase RemoveUserCommand => new MvxAsyncCommand(RemoveUser);

		public PatientSettingsManageSubscriptionMemberDetailViewModel(IMvxNavigationService mvxNavigationService, IUserDialogs userDialogs, IPatientService patientService)
		{
			_navigationService = mvxNavigationService;
			_userDialogs = userDialogs;
			_patientService = patientService;
		}

        public override async Task Initialize()
        {
            await GetUIText();      
        }

        public override void Prepare(AccountMember parameter)
		{
			_accountMember = parameter;

			var acMember = new FamilyMemberOptionsViewModel();
			acMember.AccountMember = _accountMember;
			acMember.AccountMemberOption = FamilyOptionsFactory.Get(_accountMember);        

            foreach (var option in acMember.AccountMemberOption)
			{
				option.SubmitCommand = GetCommand(option.MemberType, acMember.AccountMember.IsPrimary);
			}

			AccountMemberOptionViewModel = acMember;
        
           
            base.Prepare();
		}

		private MvxCommandBase GetCommand(MemberType memberType, bool isPrimary)
		{
			switch(memberType)
			{
				case MemberType.Private:
					return PrivateUserCommand;
				case MemberType.Reactivate:
					return ReactivateUserCommand;
				case MemberType.Deactivate:
					if (isPrimary)
						return DeactivateUserCommandForPrimaryUserCommand;
					else
						return DeactivateUserCommand;
				default:
					return RemoveUserCommand;
			}
		}

        private async Task GetUIText()
        {
            await DataUtility.GetUITopicListAsync(SettingsValues.ApiURLValue, "RemoveFamilyMember", "en").ContinueWith(async (x) =>
            {
                UITopicValues = x.Result;
            });
        }
        public string GetUIValue(string lblName)
        {
           
            return UITopicValues?.UITextList.First(x => x.TagName.Equals(lblName))?.Text ?? string.Empty;
        }
        private void PrivateUser()
		{
			WarningText = string.Empty;
			FamilyMemberOption = new FamilyMemberOption()
			{
				FamilyMemberType = MemberType.Private,
				AccountMember = AccountMember,
				TitleText = "PRIVATIZE User Account	",
				DescriptionText = "Privatizing a user's account will allow them to create a separate sign-in, where only this user will have access to their medical information and visit history.",
				DescriptionText2 = "Submit the user's email address below. This should be different from the family account primary email address.",
				EmailTitleText = "Enter new email",
				ConfirmEmailText = string.Empty,
				WarningText = string.Empty,
				SubmitText = "SUBMIT",
				CancelText = "CANCEL"
			};
			OpenPopupForMember.Raise(FamilyMemberOption.FamilyMemberType);
		}

		private async Task ReactivateUser()
		{
			WarningText = string.Empty;
			var resp = await _patientService.ReactivateFamilyMemberInfoAsync(AccountMember.PatientID);
			if (resp != null)
			{
				if (resp.StatusCode == StatusCode.SuccessSeePayload)
				{
					var descrptionText = "This family member will be reactivated immediately.";

					FamilyMemberOption = new FamilyMemberOption()
					{
						FamilyMemberType = MemberType.Reactivate,
						AccountMember = AccountMember,
						TitleText = "REACTIVATE User Account",
						DescriptionText = descrptionText,
						DescriptionText2 = "Click submit.",
						ConfirmEmailText = string.Empty,
						WarningText = string.Empty,
						SubmitText = "SUBMIT",
						CancelText = "CANCEL",
						Payload = resp.Payload,
						Payload2 = resp.Payload2
					};
					OpenPopupForMember.Raise(FamilyMemberOption.FamilyMemberType);
				}
				else if (resp.StatusCode == StatusCode.UserCantBeAdded)
				{
					WarningText = resp.Message;
				}
			}
		}

		private void DeactivateUser()
		{
			WarningText = string.Empty;
			FamilyMemberOption = new FamilyMemberOption()
			{
				FamilyMemberType = MemberType.Deactivate,
				AccountMember = AccountMember,
				TitleText = "DEACTIVATE User Account",
				DescriptionText = String.Format("You are removing this user's access to {0} services under your account.", AppBrandname),
				DescriptionText2 = "Click submit and their account will deactivate at the end of the billing cycle. You will still have access to their past information.",
				ConfirmEmailText = string.Empty,
				WarningText = string.Empty,
				SubmitText = "SUBMIT",
				CancelText = "CANCEL"
			};
			OpenPopupForMember.Raise(FamilyMemberOption.FamilyMemberType);
		}

		private void DeactivateUserCommandForPrimaryUser()
		{
			WarningText = string.Empty;
			FamilyMemberOption = new FamilyMemberOption()
			{
				FamilyMemberType = MemberType.Deactivate,
				AccountMember = AccountMember,
				TitleText = "DEACTIVATE User Account",
				DescriptionText = "Account owners cannot deactivate their account. To suspend service the subscrption plan must be canceled on the Manage Subscriptions page. Canceling a subscription will turn off access for all users on the account.",
				DescriptionText2 = "Continue to Manage Subscriptions",
				ConfirmEmailText = string.Empty,
				WarningText = string.Empty,
				SubmitText = string.Empty,
				CancelText = "CLOSE"
			};
			OpenPopupForMember.Raise(FamilyMemberOption.FamilyMemberType);
		}

		private async Task RemoveUser()
		{
			WarningText = string.Empty;
			string privateEmail = string.Empty;
			string DescriptionText2Titlestr = string.Empty;
            string DescriptionText2str = string.Empty;
            var res = await _patientService.GetActiveCoverage(AccountMember.PatientID);
			if (res != null)
			{
				if (res.Message == "ActiveSubscriber")
					hasCoverage = true;
			}
            var resp = await _patientService.RemovePrivateFamilyMemberInfoAsync(AccountMember.PatientID);
			if (resp != null)
				if (resp.StatusCode == StatusCode.Success)	privateEmail = resp.Payload2;			
			
              if(!hasCoverage && !AccountMemberOptionViewModel.AccountMember.IsPrivate) {

				DescriptionText2Titlestr = GetUIValue("WantToContinue");
				DescriptionText2str = GetUIValue("SubmitEmail");
            }
			  else if(!hasCoverage && AccountMemberOptionViewModel.AccountMember.IsPrivate) {
                DescriptionText2Titlestr = GetUIValue("WantToContinue");
                DescriptionText2str = GetUIValue("PrivateEmail");
            }
			if (hasCoverage)
			{
				DescriptionText2Titlestr = GetUIValue("ActiveCoverageAvailable");
				DescriptionText2str = GetUIValue("CoveredAccess").Replace("{0}",AppBrandname);
            }
            FamilyMemberOption = new FamilyMemberOption()
			{
				FamilyMemberType = MemberType.Remove,
				AccountMember = AccountMember,
				TitleText = GetUIValue("RemoveUserAccount"),
				TitleTextImportant = GetUIValue("Important"),
				DescriptionText = GetUIValue("PermanentlyRemoving"),
      
                DescriptionText2Title = DescriptionText2Titlestr,
                DescriptionText2 = DescriptionText2str,         
				EmailTitleText = GetUIValue("Newemail"),             
				EmailText = string.Empty,
				ConfirmationEmailTitleText = $"{privateEmail}",
				WarningText = hasCoverage ? GetUIValue("DependentCoveredAccess") :string.Empty,
				SubmitText = "SUBMIT",
				CancelText = "CANCEL"
			};
			OpenPopupForMember.Raise(FamilyMemberOption.FamilyMemberType);
		}

		private async Task Return()
		{
			await _navigationService.Close(this);
		}
	}

	public class FamilyMemberOptionsViewModel : MvxViewModel
	{
		private List<AccountMemberOptions> _aMemberOptions;
		AccountMember _accountMember;

		public List<AccountMemberOptions> AccountMemberOption
		{
			get { return _aMemberOptions; }
			set { _aMemberOptions = value; RaisePropertyChanged(() => AccountMemberOption); }
		}

		public AccountMember AccountMember
		{
			get { return _accountMember; }
			set { SetProperty(ref _accountMember, value); }
		}
	}

	public class FamilyMemberOption : BaseViewModel
	{
		private IPatientService _patientService;
		public MemberType FamilyMemberType { get; set; }
		public bool IsWarningVisible { get; set; } = true;
		AccountMember _accountMember;
		public AccountMember AccountMember
		{
			get { return _accountMember; }
			set { SetProperty(ref _accountMember, value); }
		}

		private bool _isEmailViewVisible = true;
		public bool IsEmailViewVisible
		{
			get { return _isEmailViewVisible; }
			set { SetProperty(ref _isEmailViewVisible, value); }
		}
        private string _titleTextImportant = string.Empty;
        public string TitleTextImportant
        {
            get { return _titleTextImportant; }
            set { SetProperty(ref _titleTextImportant, value); }
        }


        private string _titleText = string.Empty;
		public string TitleText
		{
			get { return _titleText; }
			set { SetProperty(ref _titleText, value); }
		}

		private string _descriptionText = string.Empty;
		public string DescriptionText
		{
			get { return _descriptionText; }
			set { SetProperty(ref _descriptionText, value); }
		}
        //Want to continue?
        private string _descriptionText2Title = string.Empty;
        public string DescriptionText2Title
        {
            get { return _descriptionText2Title; }
            set { SetProperty(ref _descriptionText2Title, value); }
        }

        private string _descriptionText2 = string.Empty;
		public string DescriptionText2
		{
			get { return _descriptionText2; }
			set { SetProperty(ref _descriptionText2, value); }
		}

		private string _emailTitleText = string.Empty;
		public string EmailTitleText
		{
			get { return _emailTitleText; }
			set { SetProperty(ref _emailTitleText, value); }
		}

		private string _emailText = string.Empty;
		public string EmailText
		{
			get { return _emailText; }
			set
			{
				SetProperty(ref _emailText, value);
				if (_emailText.Length > 3)
				{
					WarningText = string.Empty;
				}
			}
		}

		private string _confirmationEmailTitleText = string.Empty;
		public string ConfirmationEmailTitleText
		{
			get { return _confirmationEmailTitleText; }
			set { SetProperty(ref _confirmationEmailTitleText, value); }
		}

		private string _confirmEmailText = string.Empty;
		public string ConfirmEmailText
		{
			get { return _confirmEmailText; }
			set
			{
				SetProperty(ref _confirmEmailText, value);
				if (_confirmEmailText.Length > 3)
				{
					WarningText = string.Empty;
				}
			}
		}

		private string _warningText = string.Empty;
		public string WarningText
		{
			get { return _warningText; }
			set { SetProperty(ref _warningText, value); }
		}

		private string _submitText = string.Empty;
		public string SubmitText
		{
			get { return _submitText; }
			set { SetProperty(ref _submitText, value); }
		}

		private string _cancelText = string.Empty;
		public string CancelText
		{
			get { return _cancelText; }
			set { SetProperty(ref _cancelText, value); }
		}

		private string _payload = string.Empty;
		public string Payload
		{
			get { return _payload; }
			set { SetProperty(ref _payload, value); }
		}

		private string _payload2 = string.Empty;
		public string Payload2
		{
			get { return _payload2; }
			set { SetProperty(ref _payload2, value); }
		}

		public Action ClosePopup;

		public IMvxCommand SubmitCommand => new MvxAsyncCommand(SubmitPrivateMemberAsync);
		public IMvxCommand CancelCommand => new MvxCommand(CancelPrivateMemberAsync);
		public MvxInteraction ReturnToMemberList { get; } = new MvxInteraction();

		public FamilyMemberOption()
		{
			_patientService = Mvx.IoCProvider.Resolve<IPatientService>();
		}

		private async Task SubmitPrivateMemberAsync() 
		{
			switch (FamilyMemberType)
			{
				case MemberType.Private:
					await PrivateUser();
					break;
				case MemberType.Reactivate:
					await ReactivateUser();
					break;
				case MemberType.Deactivate:
					await DeactivateUser();
					break;
				default:
					await RemoveUser();
					break;
			}
		}
		private void CancelPrivateMemberAsync() 
		{
			ClosePopup?.Invoke();
		}


		private async Task PrivateUser()
		{
			WarningText = string.Empty;
			if (!Regex.Match(EmailText, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success ||
				string.IsNullOrEmpty(EmailText))
			{
				WarningText = "Email is not valid!";
				return;
			}
			IsBusy = true;
			StatusResponse resp = await _patientService.PatientMakeFamilyMemberPrivateAsync(AccountMember.PatientID, EmailText);
			if (resp != null)
			{
				if (resp.StatusCode == StatusCode.Success)
				{
					IsBusy = false;
					ClosePopup?.Invoke();
					ReturnToMemberList.Raise();
				}
				else if (resp.StatusCode == StatusCode.EmailAlreadyInUse)
				{
					IsBusy = false;
					WarningText = "Email Already In Use";
					return;
				}
				else if (resp.StatusCode == StatusCode.UserAlreadyPrivate)
				{
					IsBusy = false;
					WarningText = "User Is Already Private";
					return;
				}
				else if (resp.StatusCode == StatusCode.PatientUnderAge)
				{
					IsBusy = false;
					WarningText = "Patient Under Age";
					return;
				}
			}
			IsBusy = false;
		}

		private async Task ReactivateUser()
		{
			IsBusy = true;
			var reactResp = await _patientService.ReactivateFamilyMemberAsync(AccountMember.PatientID);
			
			if (reactResp != null)
			{
				if (reactResp.StatusCode == StatusCode.Success || reactResp.StatusCode == StatusCode.SuccessSeePayload)
				{
					if (reactResp.StatusCode == StatusCode.SuccessSeePayload && !string.IsNullOrEmpty(reactResp.Payload) && !string.IsNullOrEmpty(reactResp.Payload2))
					{
						DateTime nextMonth = DateTime.Now.AddMonths(1);
						DateTime date = new DateTime(nextMonth.Year, nextMonth.Month, 1);
						_userDialogs.Toast($"Your new monthly total of {reactResp.Payload} will start on {reactResp.Payload2}", TimeSpan.FromMilliseconds(SettingsValues.AddOnToastPeriod));
					}
					IsBusy = false;
					ClosePopup?.Invoke();
					ReturnToMemberList.Raise();
				}
			}

			IsBusy = false;
		}

		private async Task DeactivateUser()
		{
			IsBusy = true;
			var results = await _patientService.DeactivateFamilyMemberAsync(AccountMember.PatientID);
			if (results != null)
			{
				if (results.StatusCode == StatusCode.Success || results.StatusCode == StatusCode.SuccessSeePayload)
				{
					if (results.StatusCode == StatusCode.SuccessSeePayload && !string.IsNullOrEmpty(results.Payload))
					{
						DateTime nextMonth = DateTime.Now.AddMonths(1);
						DateTime date = new DateTime(nextMonth.Year, nextMonth.Month, 1);
						_userDialogs.Toast($"Your new monthly total of {results.Payload} will start on {date:MM/dd/yyyy}", TimeSpan.FromMilliseconds(SettingsValues.AddOnToastPeriod));
					}
					IsBusy = false;
					ClosePopup?.Invoke();
					ReturnToMemberList.Raise();
				}
				else if (results.StatusCode == StatusCode.UserAlreadyActive)
				{
					IsBusy = false;
					await _userDialogs.AlertAsync("User already deactivated!!");
					ClosePopup?.Invoke();
					ReturnToMemberList.Raise();
				}
				else if (results.StatusCode == StatusCode.NotFound)
				{
					IsBusy = false;
					await _userDialogs.AlertAsync("Error: User not found");
					ClosePopup?.Invoke();
					ReturnToMemberList.Raise();
				}
			}
			IsBusy = false;
		}

		private async Task RemoveUser()
		{
			var isNewEmail = true;
			var emailText = EmailText;

            if (!string.IsNullOrEmpty(ConfirmEmailText))
            {
                if (!Regex.Match(ConfirmEmailText, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
                {
                    WarningText = "Please confirm with valid Email!";
                    return;
                }
                isNewEmail = false;
                emailText = ConfirmEmailText;
            }
            else if (!Regex.Match(EmailText, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success ||
				string.IsNullOrEmpty(EmailText))
			{
				WarningText = "Email is not valid!";
				return;
			}

			IsBusy = true;
			StatusResponse resp2 = await _patientService.RemoveFamilyMemberAsync(AccountMember.PatientID, emailText, isNewEmail);

			if (resp2 != null)
			{
				if (resp2.StatusCode == StatusCode.Success || resp2.StatusCode == StatusCode.SuccessSeePayload)
				{
					if (resp2.StatusCode == StatusCode.SuccessSeePayload && !string.IsNullOrEmpty(resp2.Payload))
					{
						DateTime nextMonth = DateTime.Now.AddMonths(1);
						DateTime date = new DateTime(nextMonth.Year, nextMonth.Month, 1);
						_userDialogs.Toast($"Your new monthly total of {resp2.Payload} will start on {date:MM/dd/yyyy}", TimeSpan.FromMilliseconds(SettingsValues.AddOnToastPeriod));
					}
					IsBusy = false;
					ClosePopup?.Invoke();
					ReturnToMemberList.Raise();
				}
				else if (resp2.StatusCode == StatusCode.EmailAlreadyInUse)
				{
					IsBusy = false;
					WarningText = "Email already in use!";
					return;
				}
				else
				{
					IsBusy = false;
					WarningText = resp2.Message;
					return;
				}
			}
			IsBusy = false;
		}
	}
}
