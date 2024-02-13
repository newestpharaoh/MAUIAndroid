using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using CommonLibraryCoreMaui.Exceptions;
using CommonLibraryCoreMaui.Factory;
using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.Models.NavigationParameters;
using CommonLibraryCoreMaui.Services;
using CommonLibraryCoreMaui.ViewModels;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
	public class PatientSettingsAddFamilyMemberOrderSummaryViewModel : BaseNavigationViewModel<AccountAddFamilyMemberInfo, bool>
	{
		IPatientService _patientService;
		public AccountAddFamilyMemberInfo AccountFamilyMemberInfo { get; set; }
		public PlanType ptype { get; set; }

		private string _selectedCardType;
		public string SelectedCardType
		{
			get { return _selectedCardType; }
			set { SetProperty(ref _selectedCardType, value); IsEnabled = true; }
		}

		private bool _isPayCardVisible;
		public bool IsPayCardVisible
		{
			get { return _isPayCardVisible; }
			set { SetProperty(ref _isPayCardVisible, value); }
		}

		private string _warningText;
		public string WarningText
		{
			get { return _warningText; }
			set { SetProperty(ref _warningText, value); }
		}

		private string _last4ofCC;
		public string Last4ofCC
		{
			get { return _last4ofCC; }
			set { SetProperty(ref _last4ofCC, value); RaisePropertyChanged(nameof(Last4ofCC)); IsEnabled = true; }
		}

		private string _cardNumber;
		public string CardNumber
		{
			get { return _cardNumber; }
			set { SetProperty(ref _cardNumber, value); if (UserCardInfo != null) { UserCardInfo.CardNumber = value; } }
		}

		private string _cardSecurityCode;
		public string CardSecurityCode
		{
			get { return _cardSecurityCode; }
			set { SetProperty(ref _cardSecurityCode, value); if (UserCardInfo != null) { UserCardInfo.CardSecurityCode = value; } }
		}

		private string _newSubscriptionCost;
		public string NewSubscriptionCost
		{
			get { return _newSubscriptionCost; }
			set { SetProperty(ref _newSubscriptionCost, value); }
		}

		private string _todaysPayment;
		public string TodaysPayment
		{
			get { return _todaysPayment; }
			set { SetProperty(ref _todaysPayment, value); }
		}

		private string _effectiveDate;
		public string EffectiveDate
		{
			get { return _effectiveDate; }
			set { SetProperty(ref _effectiveDate, value); }
		}

		private bool _isEnabled = true;
		public bool IsEnabled
		{
			get { return _isEnabled; }
			set { SetProperty(ref _isEnabled, value); }
		}

		private AccountCreditCard _userCardInfo;
		public AccountCreditCard UserCardInfo
		{
			get { return _userCardInfo; }
			set { SetProperty(ref _userCardInfo, value); }
		}

		private MvxObservableCollection<AccountMember> _currentPlanAccountMemberList;
		public MvxObservableCollection<AccountMember> CurrentPlanAccountMemberList
		{
			get { return _currentPlanAccountMemberList; }
			set { SetProperty(ref _currentPlanAccountMemberList, value); }
		}

		private MvxObservableCollection<AccountMember> _newPlanAccountMemberList;
		public MvxObservableCollection<AccountMember> NewPlanAccountMemberList
		{
			get { return _newPlanAccountMemberList; }
			set { SetProperty(ref _newPlanAccountMemberList, value); }
		}

		public MvxInteraction NavigateBackToManageMembers { get; } = new MvxInteraction();
		public MvxInteraction DismissKeyboardInteraction { get; } = new MvxInteraction();
		public IMvxCommand AcceptAndPayCommand => new MvxAsyncCommand(AcceptAndPayAsync);
		public IMvxCommand CancelCommand => new MvxAsyncCommand(CancelAsync);

		public SubscriptionChangeInfo SubscriptionInfo { get; private set; }

		public PatientSettingsAddFamilyMemberOrderSummaryViewModel(IMvxNavigationService mvxNavigationService, IPatientService patientService, IUserDialogs userDialogs)
		{
			_navigationService = mvxNavigationService;
			_patientService = patientService;
			_userDialogs = userDialogs;
		}

		public override void Prepare(AccountAddFamilyMemberInfo parameter)
		{
			AccountFamilyMemberInfo = parameter;
			IsPayCardVisible = string.IsNullOrEmpty(AccountFamilyMemberInfo.Last4ofCC);
			Last4ofCC = AccountFamilyMemberInfo.Last4ofCC;
			base.Prepare();
		}

		public override async Task Initialize()
		{
			await RefreshAccountMembersList();
			await base.Initialize();
		}

		public async Task RefreshAccountMembersList()
		{
			IsBusy = true;
			try
			{
				var results = await _patientService.GetPatientSubscriptionInfoAsync();
				if (results == null)
				{
					IsBusy = false;
					return;
				}
				results.AccountMembers.Where(x => x.IsActive == false).ToList().ForEach(x => x.PaymentPlan = "Deactivated");
				var accotnHolder = results.AccountMembers.Where(x => x.IsPrimary == true).FirstOrDefault();
				if(accotnHolder != null)
					accotnHolder.PaymentPlan = "Account Holder";

				var lstCurrentAccountMembers = new List<AccountMember>();
				lstCurrentAccountMembers.Add(new AccountMember() { DisplayName = $"{results.CurrentSubscriptionPlan} Plan", PlanStatus = results.CurrentSubscriptionTotalCost });
				lstCurrentAccountMembers.AddRange(results.AccountMembers.Where(x=>x.IsActive));
				CurrentPlanAccountMemberList = new MvxObservableCollection<AccountMember>(lstCurrentAccountMembers);

				var lstNewAccountMembers = new List<AccountMember>();
				lstNewAccountMembers.Add(new AccountMember() { DisplayName = $"{results.CurrentSubscriptionPlan} Plan", PlanStatus = results.CurrentSubscriptionTotalCost });

				var familyMembers = AccountAddFamilyMember.Instance.AddedFamilyMembersNames;
				for (int i = 0; i < familyMembers.Count; i++)
				{
					lstNewAccountMembers.Add(new AccountMember() { DisplayName = $"{familyMembers[i].FullName}", 
						PlanStatus = familyMembers[i].IsFreeMember ? "Included in family subscription plan" :
						$"{AccountAddFamilyMember.Instance.SingleAdditionalCost} additional monthly fee"
					});
				}
				
				NewPlanAccountMemberList = new MvxObservableCollection<AccountMember>(lstNewAccountMembers);

				int additionalFamilyMembers = familyMembers.Count - AccountAddFamilyMember.Instance.InitialFreeFamilyMemberRemaining;
				float additionalAmount = float.Parse(AccountAddFamilyMember.Instance.AdditionalCost.Replace("$", string.Empty));

				var newlyAddedMembersPayment = additionalAmount;
				var newSubscriptionCost = newlyAddedMembersPayment + float.Parse(results.CurrentSubscriptionTotalCost.Replace("$", string.Empty));

				NewSubscriptionCost = newSubscriptionCost.ToString("$0.00") ?? "$0.00";
				TodaysPayment = AccountAddFamilyMember.Instance.ProratedCost ?? "$0.00";
				EffectiveDate = $"effective {AccountFamilyMemberInfo.NextBillingDate}";

				UserCardInfo = new AccountCreditCard()
				{
					PatientID = Globals.Instance.UserInfo.PatientID,
					CardExpirationMonth = DateTime.Now.Month.ToString(),
					CardExpirationYear = Theme.Values.CCYears[0],
					BillingState = "TX"
				};
			}
			catch (Exception ex)
			{
				ReportCrash(ex, Title);
			}

			IsBusy = false;
		}

		private async Task AcceptAndPayAsync()
		{
			DismissKeyboardInteraction.Raise();
			if (!IsPayCardVisible)
			{
				if (SelectedCardType == "Pay with New Card")
				{
					await UpdateCardInfoAndChangeSubcription();
				}
				else
				{
					await AddFamilyMembers();
				}
			}
			else
			{
				await UpdateCardInfoAndChangeSubcription();
			}
		}

		private async Task AddFamilyMembers()
		{
			IsBusy = true;
			var message = string.Empty;
			if (AccountAddFamilyMember.Instance != null)
			{
				AccountAddFamilyMember.Instance.AdditionalCost = TodaysPayment;
				StatusResponse resp = await DataUtility.PatientAddFamilyMembersAsync(SettingsValues.ApiURLValue, AccountAddFamilyMember.Instance, CommonAuthSession.Token).ConfigureAwait(false);
				if (resp != null)
				{
					IsBusy = false;
					if (resp.StatusCode == StatusCode.Success)
					{
						var membersName = GetAdditionaFamilyMemberNames();
						message = $"{membersName} have been added to your famiy.";

						//await _userDialogs.AlertAsync(message, "Add Family Member").ContinueWith(async (t) =>
						//{
						//	AccountAddFamilyMember.Instance.Clear();
						//	AccountAddFamilyMember.Instance.ClearNameList();
							await _navigationService.Close(this);
						//});
					}
					else if (resp.StatusCode == StatusCode.PaymentErrorSeePayload)
					{
						WarningText = resp.Payload;
					}
					else
					{
						WarningText = "There is some problem. Please try later!";
					}
				}
				else
				{
					IsBusy = false;
					WarningText = "There is some problem. Please try later!";
				}
			}
		}

		private async Task CancelAsync()
		{
			DismissKeyboardInteraction.Raise();
			
			AccountAddFamilyMember.Instance.Clear();
			AccountAddFamilyMember.Instance.ClearNameList();
			await _navigationService.Close(this);
		}

		private string GetAdditionaFamilyMemberNames(bool isOnlyAddedFreeMembers = false)
		{
			var familyMembers = isOnlyAddedFreeMembers ?
				AccountAddFamilyMember.Instance.AddedFamilyMembersNames.Where(x => x.IsFreeMember).ToList()
				: AccountAddFamilyMember.Instance.AddedFamilyMembersNames;

			var additionalFamilyMembers = string.Empty;
			var memberCount = familyMembers.Count;
			for (int i = 0; i < memberCount; i++)
			{
				var fullName = familyMembers[i].FullName;
				if (memberCount > 2 && i == memberCount - 2)
				{
					additionalFamilyMembers += $"{fullName} and ";
					continue;
				}
				if (i == memberCount - 1)
				{
					additionalFamilyMembers += $"{fullName}";
					continue;
				}
				additionalFamilyMembers += $"{fullName}, ";
			}

			return additionalFamilyMembers;
		}

		public async Task<byte[]> GetOrderPreviewBytes()
		{
			IsBusy = true;
			var orderSummaryBytes = await DataUtility.GetAddNewFamilyMemberOrderSummaryAsync(SettingsValues.ApiURLValue, AccountAddFamilyMember.Instance, CommonAuthSession.Token).ConfigureAwait(false);
			IsBusy = false;
			return orderSummaryBytes;
		}

		private async Task UpdateCardInfoAndChangeSubcription()
		{
			WarningText = string.Empty;
			if (string.IsNullOrEmpty(UserCardInfo.CardFirstName) ||
				string.IsNullOrEmpty(UserCardInfo.CardLastName) ||
				string.IsNullOrEmpty(UserCardInfo.CardNumber) ||
				string.IsNullOrEmpty(UserCardInfo.CardSecurityCode) ||
				string.IsNullOrEmpty(UserCardInfo.BillingAddress) ||
				string.IsNullOrEmpty(UserCardInfo.BillingCity) ||
				string.IsNullOrEmpty(UserCardInfo.BillingState) ||
				string.IsNullOrEmpty(UserCardInfo.BillingZip))
			{
				WarningText = "Please fill all the required fields!";
				return;
			}

			IsBusy = true;
			try
			{
				var resp = await DataUtility.PatientUpdateCreditCardInfoAsync(SettingsValues.ApiURLValue, CommonAuthSession.Token, UserCardInfo).ConfigureAwait(false);
				
				if (resp != null)
				{
					switch (resp.StatusCode)
					{
						case StatusCode.Success:
							if (!string.IsNullOrEmpty(resp.Payload))
								Last4ofCC = resp.Payload;
							await AddFamilyMembers();
							break;
						case StatusCode.CardInformationNotProvided:
							IsBusy = false;
							WarningText = "To update your card information you must re-enter your credit card number and security code.";
							break;
						case StatusCode.PaymentErrorSeePayload:
							WarningText = resp.Payload;
							IsEnabled = false;
							CardNumber = string.Empty;
							CardSecurityCode = string.Empty;
							break;
						default:
							IsBusy = false;
							if (!string.IsNullOrEmpty(resp.Message))
								WarningText = resp.Message;
							break;
					}
				}
				IsBusy = false;
			}
			catch (PatientException ex)
			{
				ReportCrash(ex, Title);
			}
			catch (Exception ex)
			{
				ReportCrash(ex, Title);
			}

			IsBusy = false;
		}
	}
}
