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
using Microsoft.Maui.Storage;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
	public class PatientSettingsManageSubscriptionOrderSummaryViewModel : BaseNavigationViewModel<OrderPlanChangeNavigationParam>
	{
		IPatientService _patientService;
		public SubscriptionChangeInfo OrderSubscriptionPlan { get; set; }
		public PlanType ptype { get; set; }

		private string _effectiveDateText;
		public string EffectiveDateText
		{
			get { return _effectiveDateText; }
			set { SetProperty(ref _effectiveDateText, value); IsEnabled = true; }
		}

		private bool _isPayCardVisible;
		public bool IsPayCardVisible
		{
			get { return _isPayCardVisible; }
			set { SetProperty(ref _isPayCardVisible, value); }
		}

		private string _selectedCardType;
		public string SelectedCardType
		{
			get { return _selectedCardType; }
			set { SetProperty(ref _selectedCardType, value); }
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
			set { SetProperty(ref _last4ofCC, value); RaisePropertyChanged(nameof(Last4ofCC)); }
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

		private SubscriptionPlan _selectedSubscriptionPlan;
		public SubscriptionPlan SelectedSubscriptionPlan
		{
			get { return _selectedSubscriptionPlan; }
			set { SetProperty(ref _selectedSubscriptionPlan, value); }
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
		public string Locale = Preferences.Get("Locale", string.Empty);

		public PatientSettingsManageSubscriptionOrderSummaryViewModel(IMvxNavigationService mvxNavigationService, IPatientService patientService, IUserDialogs userDialogs)
		{
			_navigationService = mvxNavigationService;
			_patientService = patientService;
			_userDialogs = userDialogs;
		}
		public async Task<List<CommonLibraryCoreMaui.Models.UIText>> GetUITopic(string strTopicName)
		{
			var pageText = await DataUtility.GetUITopicListAsync(SettingsValues.ApiURLValue, strTopicName, Locale).ConfigureAwait(false);
			return pageText.UITextList;
		}
		private List<UIText> GetPageTextList()
        {
            Task<List<UIText>> task = Task.Run<List<UIText>>(async () => await GetUITopic("UpdManageSubscriptions"));
            return task.Result;
        }

		//OrderSubscriptionPlan.NewVInvoice.Main.DiscountInitialDescrip
		private string _discountInitialDescrip;
		public string DiscountInitialDescrip
		{
			get { return _discountInitialDescrip; }
			set { SetProperty(ref _discountInitialDescrip, value); }
		}

		private string _ProratedCostAmt;
		public string ProratedCostAmt
		{
			get { return _ProratedCostAmt; }
			set { SetProperty(ref _ProratedCostAmt, value); }
		}
		public override void Prepare(OrderPlanChangeNavigationParam parameter)
		{
			try
			{
				ptype = parameter.Type;
				OrderSubscriptionPlan = parameter.Subscription;			

				SelectedSubscriptionPlan = SubscriptionChangePlanFactory.Get(ptype, GetPageTextList(), OrderSubscriptionPlan.SubscriptionPlanName, OrderSubscriptionPlan.CurrentSubscriptionPlanName
									, OrderSubscriptionPlan.SubscriptionPlanName, OrderSubscriptionPlan.NextBillingDate, OrderSubscriptionPlan.SubscriptionCost,
									string.IsNullOrEmpty(OrderSubscriptionPlan.Last4ofCC));
				EffectiveDateText = $"to be billed {OrderSubscriptionPlan.NextBillingDate}";
				IsPayCardVisible = string.IsNullOrEmpty(OrderSubscriptionPlan.Last4ofCC);
				var disc = OrderSubscriptionPlan.NewVInvoice.Main.DiscountInitialDescrip.Replace("$", "");			
				DiscountInitialDescrip = "$-" + disc;
				Last4ofCC = OrderSubscriptionPlan.Last4ofCC;
			}
			catch (Exception ex)
			{
				ReportCrash(ex, Title);
			}
			base.Prepare();
		}
		
		public override Task Initialize()
		{
			RefreshAccountMembersList();
			return base.Initialize();
		}

		public void RefreshAccountMembersList()
		{
			IsBusy = true;
			try
			{
				var lstCurrentAccountMembers = new List<AccountMember>();
				if (OrderSubscriptionPlan.CurrentSubscriptionPlanName != null)
				{
					lstCurrentAccountMembers.Add(new AccountMember() { DisplayName = $"{OrderSubscriptionPlan.CurrentSubscriptionPlanName} Plan", PlanStatus = OrderSubscriptionPlan.CurrentSubscriptionCost });
					lstCurrentAccountMembers.AddRange(OrderSubscriptionPlan.FamilyMemberList.Where(x => x.IsActive));
				}
				CurrentPlanAccountMemberList = new MvxObservableCollection<AccountMember>(lstCurrentAccountMembers);

				var lstNewAccountMembers = new List<AccountMember>();
				lstNewAccountMembers.Add(new AccountMember() { DisplayName = $"{OrderSubscriptionPlan.SubscriptionPlanName} Plan", PlanStatus = OrderSubscriptionPlan.SubscriptionCost });
				lstNewAccountMembers.AddRange(OrderSubscriptionPlan.NewFamilyMemberList);
				NewPlanAccountMemberList = new MvxObservableCollection<AccountMember>(lstNewAccountMembers);
				ProratedCostAmt = OrderSubscriptionPlan.CurrentVInvoice!=null?"$" + OrderSubscriptionPlan.CurrentVInvoice.Main.DueInitial.ToString():string.Empty;
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
					await ChangeSubscription();
				}
			}
			else
			{
				await UpdateCardInfoAndChangeSubcription();
			}
		}
	
		private async Task ChangeSubscription()
		{
			IsBusy = true;
			WarningText = string.Empty;
			AccountSubscriptionChange asc = new AccountSubscriptionChange();
			asc.NewSubscriptionOptionID = OrderSubscriptionPlan.SubscriptionID;
			asc.PatientID = Globals.Instance.UserInfo.PatientID;
			asc.Locale = Locale;
			asc.PromoCode = OrderSubscriptionPlan.SubscriptionPromoCode;

			StatusResponse changeSubscription = await _patientService.PatientChangeSubscriptionAsync(asc);
			
			if (changeSubscription != null)
			{
				if (changeSubscription.StatusCode == StatusCode.PaymentErrorSeePayload)
				{
					IsBusy = false;
					WarningText = changeSubscription.Payload ?? "An error has occured. Please try again.";
				}
				else if (changeSubscription.StatusCode != StatusCode.Success)
				{
					IsBusy = false;
					WarningText = changeSubscription.ErrorMessage ?? "An error has occured. Please try again.";
				}
				else if (changeSubscription.StatusCode == StatusCode.Success)
				{
					var userInfo = await DataUtility.GetUserInfo(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.UserID, true, CommonAuthSession.Token)
						.ConfigureAwait(false);
					Globals.Instance.UserInfo = userInfo;
					IsBusy = false;
					InvokeOnMainThread(() =>
					{
						NavigateBackToManageMembers.Raise();
					});
				}
			}
			IsBusy = false;
		}

		private async Task CancelAsync()
		{
			DismissKeyboardInteraction.Raise();
			await _navigationService.Close(this);
		}

		public async Task<byte[]> GetOrderPreviewBytes()
		{
			IsBusy = true;
			var orderSummaryBytes = await DataUtility.GetNewOrderSummaryAsync(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.PatientID, OrderSubscriptionPlan.SubscriptionID, CommonAuthSession.Token, OrderSubscriptionPlan.SubscriptionPromoCode).ConfigureAwait(false);
			IsBusy = false;
			return orderSummaryBytes;
		}

		private async Task UpdateCardInfoAndChangeSubcription()
		{
			try
			{
				
				if (UserCardInfo != null)
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
				}

			IsBusy = true;
			
				var resp = await DataUtility.PatientUpdateCreditCardInfoAsync(SettingsValues.ApiURLValue, CommonAuthSession.Token, UserCardInfo).ConfigureAwait(false);
				
				if (resp != null)
				{
					switch (resp.StatusCode)
					{
						case StatusCode.Success:
							if(!string.IsNullOrEmpty(resp.Payload))
								Last4ofCC = resp.Payload;
							await ChangeSubscription();
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

	public class PatientSettingsReactivePlannOrderSummaryViewModel : BaseNavigationViewModel<SubscriptionChangeInfo>
	{
		IPatientService _patientService;
		public SubscriptionChangeInfo OrderSubscriptionPlan { get; set; }
		public PlanType ptype { get; set; }

		private string _effectiveDateText;
		public string EffectiveDateText
		{
			get { return _effectiveDateText; }
			set { SetProperty(ref _effectiveDateText, value); }
		}

		private string _warningText;
		public string WarningText
		{
			get { return _warningText; }
			set { SetProperty(ref _warningText, value); }
		}

		private SubscriptionPlan _selectedSubscriptionPlan;
		public SubscriptionPlan SelectedSubscriptionPlan
		{
			get { return _selectedSubscriptionPlan; }
			set { SetProperty(ref _selectedSubscriptionPlan, value); }
		}

		private AccountCreditCard _userCardInfo;
		public AccountCreditCard UserCardInfo
		{
			get { return _userCardInfo; }
			set { SetProperty(ref _userCardInfo, value); }
		}

		private MvxObservableCollection<AccountMember> _newPlanAccountMemberList;
		public MvxObservableCollection<AccountMember> NewPlanAccountMemberList
		{
			get { return _newPlanAccountMemberList; }
			set { SetProperty(ref _newPlanAccountMemberList, value); }
		}

		public MvxInteraction NavigateBackToManageMembers { get; } = new MvxInteraction();
		public IMvxCommand AcceptAndPayCommand => new MvxAsyncCommand(AcceptAndPayAsync);
		public IMvxCommand CancelCommand => new MvxAsyncCommand(CancelAsync);

		public PatientSettingsReactivePlannOrderSummaryViewModel(IMvxNavigationService mvxNavigationService, IPatientService patientService, IUserDialogs userDialogs)
		{
			_navigationService = mvxNavigationService;
			_userDialogs = userDialogs;
			_patientService = patientService;
		}

		public override void Prepare(SubscriptionChangeInfo parameter)
		{
			OrderSubscriptionPlan = parameter;
			EffectiveDateText = $"effective {OrderSubscriptionPlan.NextBillingDate}";
			base.Prepare();
		}

		public override Task Initialize()
		{
			GetPlanInfo();
			return base.Initialize();
		}

		void GetPlanInfo()
		{
			var lstNewAccountMembers = new List<AccountMember>();
			lstNewAccountMembers.Add(new AccountMember() { DisplayName = $"{OrderSubscriptionPlan.SubscriptionPlanName} Plan", PaymentPlan = OrderSubscriptionPlan.SubscriptionCost });
			lstNewAccountMembers.AddRange(OrderSubscriptionPlan.FamilyMemberList.Where(x=>x.PlanStatus != null));
			NewPlanAccountMemberList = new MvxObservableCollection<AccountMember>(lstNewAccountMembers);

		}
		public string Locale = Preferences.Get("Locale", string.Empty);
		private async Task ChangeSubscription()
		{
			IsBusy = true;
			WarningText = string.Empty;
			AccountSubscriptionChange asc = new AccountSubscriptionChange();
			asc.NewSubscriptionOptionID = OrderSubscriptionPlan.SubscriptionID;
			asc.PatientID = Globals.Instance.UserInfo.PatientID;
			asc.Locale = Locale;
			asc.PromoCode = OrderSubscriptionPlan.SubscriptionPromoCode;

			StatusResponse changeSubscription = await _patientService.PatientChangeSubscriptionAsync(asc);
			if (changeSubscription != null)
			{
				if (changeSubscription.StatusCode == StatusCode.Success)
				{
					var userInfo = await DataUtility.GetUserInfo(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.UserID, true, CommonAuthSession.Token)
						.ConfigureAwait(false);
					IsBusy = false;
					Globals.Instance.UserInfo = userInfo;
					InvokeOnMainThread(() =>
					{
						NavigateBackToManageMembers.Raise();
					});
				}
				if (changeSubscription.StatusCode != StatusCode.Success)
				{
					IsBusy = false;
					WarningText = changeSubscription.ErrorMessage ?? "An error has occured. Please try again.";
				}
			}
			IsBusy = false;
		}

		private async Task AcceptAndPayAsync()
		{
			await ChangeSubscription();
		}

		private async Task CancelAsync()
		{
			await _navigationService.Close(this);
		}

		public async Task<byte[]> GetOrderPreviewBytes()
		{
			IsBusy = true;
			var orderSummaryBytes = await DataUtility.GetNewOrderSummaryAsync(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.PatientID, OrderSubscriptionPlan.SubscriptionID, CommonAuthSession.Token, OrderSubscriptionPlan.SubscriptionPromoCode).ConfigureAwait(false);
			IsBusy = false;
			return orderSummaryBytes;
		}
	}
}
