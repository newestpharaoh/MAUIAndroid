using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Acr.UserDialogs;
using CommonLibraryCoreMaui.Exceptions;
using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.Models.NavigationParameters;
using CommonLibraryCoreMaui.ViewModels;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class PatientSettingsManageCardInfoViewModel : BaseNavigationViewModel<bool>
    {
        private AccountCreditCard _userCardInfo;
        public event PropertyChangedEventHandler PropertyChanged;
        //public string Locale = Preferences.Get("Locale", string.Empty);
        public AccountCreditCard UserCardInfo
        {
            get { return _userCardInfo; }
            set { SetProperty(ref _userCardInfo, value); }
        }

        private bool _isComingFromRegistration;
        public bool IsComingFromRegistration
        {
            get { return _isComingFromRegistration; }
            set { SetProperty(ref _isComingFromRegistration, value); }
        }

        private string _subscriptionCost;
        public string SubscriptionCost
        {
            get { return _subscriptionCost; }
            set { SetProperty(ref _subscriptionCost, value); }
        }

        private string _subscriptionInfo;
        public string SubscriptionInfo
        {
            get { return _subscriptionInfo; }
            set { SetProperty(ref _subscriptionInfo, value); }
        }

        private string _warningText;
        public string WarningText
        {
            get { return _warningText; }
            set { SetProperty(ref _warningText, value); }
        }
       
        private string _txtPromoCode = string.Empty;
        public string TxtPromoCode
        {
            get
            {
                return _txtPromoCode;
            }
            set { SetProperty(ref _txtPromoCode, value); }
        }

        private bool _isValidPromoCode = true;
        public bool IsValidPromoCode
        {
            get { return _isValidPromoCode; }
            set { SetProperty(ref _isValidPromoCode, value); }
        }

        private bool _isPromoEnble;
        [Required(ErrorMessage = "Promo code invalid. Please remove or re-enter.")]
        public bool IsPromoEnble
        {
            get { return _isPromoEnble; }
            set
            {
                //ValidateProperty(value);
                SetProperty(ref _isPromoEnble, value);
            }
        }

        AccountCreditCard CurrentUserCardInfo;

        bool CardInfoHasChanges => !UserCardInfo.Equals(CurrentUserCardInfo);

        public IMvxCommand SaveCommand => new MvxAsyncCommand(SaveOrUpdateCardInformation);

        public IMvxCommand GoToPreviousCommand => new MvxAsyncCommand(GoToPreviousAsync);
        public MvxInteraction DismissKeyboardView { get; } = new MvxInteraction();

        public PatientSettingsManageCardInfoViewModel(IUserDialogs userDialog, IMvxNavigationService mvxNavigationService)
        {
            _navigationService = mvxNavigationService;
            _userDialogs = userDialog;
        }

        private bool _showWarning;
        public bool ShowWarning
        {
            get { return _showWarning; }
            set { _showWarning = value; RaisePropertyChanged(() => ShowWarning); }
        }
        public override void Prepare(bool parameter)
        {
            IsComingFromRegistration = parameter;
            base.Prepare();
        }

        public async override Task Initialize()
        {
            IsBusy = true;
            try
            {
                if (IsComingFromRegistration)
                {
                    SetCreditCardInfo();
                }
                else
                {
                    await GetCardInformation();
                }
            }
            catch { }
            IsBusy = false;
            await base.Initialize();
        }
     
        private void SetCreditCardInfo()
        {
            if (Registration.Instance.Subscription != null)
            {
                if (Registration.Instance.Subscription is OneTimeSubscription || Registration.Instance.Subscription is Individual365Subscription || Registration.Instance.Subscription is Family365Subscription)
                {
                    SubscriptionInfo = $"Plan starts {DateTime.Now.ToString("MM/dd/yyyy")}.";
                }
                else
                {
                    SubscriptionInfo = $"Plan starts {DateTime.Now.ToString("MM/dd/yyyy")} and will be renewed {new DateTime(DateTime.Now.AddMonths(1).Year, DateTime.Now.AddMonths(1).Month, 1).ToString("MM/dd/yyyy")}.";
                }
                SubscriptionCost = Registration.Instance.Subscription.GetTotalPrice().ToString("$0.00");
            }

            CurrentUserCardInfo = new AccountCreditCard();
            CurrentUserCardInfo.CardFirstName = string.IsNullOrEmpty(Registration.Instance.CardFirstName)
                                                ? Registration.Instance.FirstName : Registration.Instance.CardFirstName;

            CurrentUserCardInfo.CardLastName = string.IsNullOrEmpty(Registration.Instance.CardLastName)
                                                ? Registration.Instance.LastName : Registration.Instance.CardLastName;

            CurrentUserCardInfo.CardNumber = Registration.Instance.CardNumber;
            CurrentUserCardInfo.BillingAddress = Registration.Instance.BillingAddress;
            CurrentUserCardInfo.BillingCity = Registration.Instance.BillingCity;
            CurrentUserCardInfo.CardSecurityCode = Registration.Instance.CardSecurityCode;
            CurrentUserCardInfo.BillingZip = string.IsNullOrEmpty(Registration.Instance.BillingZip)
                                                ? string.Empty : Registration.Instance.BillingZip;

            CurrentUserCardInfo.BillingState = string.IsNullOrEmpty(Registration.Instance.BillingState)
                                                ? "TX" : Registration.Instance.BillingState;
            CurrentUserCardInfo.CardExpirationMonth = string.IsNullOrEmpty(Registration.Instance.CardExpirationMonth)
                                                ? DateTime.Now.Month.ToString() : Registration.Instance.CardExpirationMonth;
            CurrentUserCardInfo.CardExpirationYear = string.IsNullOrEmpty(Registration.Instance.CardExpirationYear)
                                                ? Theme.Values.CCYears[0] : Registration.Instance.CardExpirationYear;

            UserCardInfo = CurrentUserCardInfo.ShallowCopy();
        }

        private async Task GetCardInformation()
        {
            CurrentUserCardInfo = await DataUtility.PatientGetCreditCardInfoAsync(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.PatientID, CommonAuthSession.Token).ConfigureAwait(false);
            if (CurrentUserCardInfo == null)
            {
                CurrentUserCardInfo = new AccountCreditCard()
                {
                    PatientID = Globals.Instance.UserInfo.PatientID,
                    CardExpirationMonth = DateTime.Now.Month.ToString(),
                    CardExpirationYear = Theme.Values.CCYears[0],
                    BillingState = "TX"
                };
            }
            UserCardInfo = CurrentUserCardInfo.ShallowCopy();
        }

        private async Task SaveOrUpdateCardInformation()
        {
            DismissKeyboardView.Raise();
            if (IsComingFromRegistration)
                await SaveCardInformation();
            else
                await UpdateCardInformation();
        }

        private async Task SaveCardInformation()
        {
            WarningText = string.Empty;
            if (string.IsNullOrEmpty(UserCardInfo.CardFirstName) ||
                string.IsNullOrEmpty(UserCardInfo.CardLastName) ||
                string.IsNullOrEmpty(UserCardInfo.CardNumber) ||
                string.IsNullOrEmpty(UserCardInfo.CardSecurityCode) ||
                string.IsNullOrEmpty(UserCardInfo.BillingAddress) ||
                string.IsNullOrEmpty(UserCardInfo.BillingCity) ||
                string.IsNullOrEmpty(UserCardInfo.BillingState) ||
                string.IsNullOrEmpty(UserCardInfo.BillingZip)
                || ShowWarning)
            {
               
                WarningText = !IsValidPromoCode
                            ? "Promo code invalid. Please remove or re-enter!"
                            : "Please fill all the required fields!";

                return;
            }
            else
            {              
                if (!string.IsNullOrEmpty(UserCardInfo.CardExpirationMonth) && !string.IsNullOrEmpty(UserCardInfo.CardExpirationYear))
                {
                    int expMonth = 0;
                    int expYear = 0;
                    DateTime todayDt = DateTime.Now;

                    if (Int32.TryParse(UserCardInfo.CardExpirationMonth, out expMonth) && Int32.TryParse("20" + UserCardInfo.CardExpirationYear, out expYear))
                    {
                        DateTime expDate = new DateTime(expYear, expMonth, 1);
                        DateTime thisMonth = new DateTime(todayDt.Year, todayDt.Month, 1);
                        if (todayDt.Day > 15)
                        {
                            if (thisMonth.Month == expDate.Month && todayDt.Year == expDate.Year)
                                WarningText = "Credit Card expires before the start of the subscription!";
                           // return;
                        }
                    }

                }

            }

            if (!CardInfoHasChanges)
            {
                return;
            }

            try
            {
                var result = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
                {
                    Title = "Account Registration",
                    Message = $"Confirm purchase of {Registration.Instance.Subscription.Name}?",
                    OkText = "Cancel",
                    CancelText = "Confirm"
                });

                if (!result)
                {
                    IsBusy = true;
                    SelfPayBillingInfo req = new SelfPayBillingInfo();
                    Registration.Instance.CardFirstName = req.CardFirstName = UserCardInfo.CardFirstName;
                    Registration.Instance.CardLastName = req.CardLastName = UserCardInfo.CardLastName;
                    Registration.Instance.CardNumber = req.CardNumber = UserCardInfo.CardNumber;
                    Registration.Instance.CardSecurityCode = req.CardSecurityCode = UserCardInfo.CardSecurityCode;
                    Registration.Instance.BillingAddress = req.BillingAddress = UserCardInfo.BillingAddress;
                    Registration.Instance.BillingCity = req.BillingCity = UserCardInfo.BillingCity;
                    Registration.Instance.BillingState = req.BillingState = UserCardInfo.BillingState;
                    Registration.Instance.BillingZip = req.BillingZip = UserCardInfo.BillingZip;
                    Registration.Instance.CardExpirationMonth = req.CardExpirationMonth = UserCardInfo.CardExpirationMonth;
                    Registration.Instance.CardExpirationYear = req.CardExpirationYear = UserCardInfo.CardExpirationYear;
                    req.SubscriptionOptionID = Registration.Instance.Subscription.OptionID.ToString();
                    req.ResignupCode = Registration.Instance.ResignupCode;               

                
                    StatusResponse respPromo;
                    if (!string.IsNullOrEmpty(TxtPromoCode))
                    {
                        respPromo = await DataUtility.ValidatePromoCodeAsync(SettingsValues.ApiURLValue, Registration.Instance.Subscription.OptionID, TxtPromoCode);
                        if (respPromo != null)
                            if (respPromo.StatusCode == StatusCode.SuccessSeePayload)
                            {
                                req.PromoCode = TxtPromoCode;
                                Registration.Instance.PromoCode = TxtPromoCode;
                            }
                    }

                    RegistrationUserInfo resp = await DataUtility.StartSelfPayRegistrationAsync(SettingsValues.ApiURLValue, req).ConfigureAwait(false);

                    if (resp != null)
                    {
                        IsBusy = false;
                        switch (resp.StatusCode)
                        {
                            case StatusCode.Success:            
                                if (resp.PatientID != 0)
                                    Registration.Instance.PatientID = resp.PatientID.ToString();
                                if (!string.IsNullOrEmpty(resp.Email))
                                    Registration.Instance.Email = resp.Email;
                                if (!string.IsNullOrEmpty(resp.FirstName))
                                    Registration.Instance.FirstName = resp.FirstName;
                                if (!string.IsNullOrEmpty(resp.LastName))
                                    Registration.Instance.LastName = resp.LastName;
                                if (!string.IsNullOrEmpty(resp.Gender))
                                    Registration.Instance.Gender = resp.Gender;
                                if (!string.IsNullOrEmpty(resp.Phone))
                                    Registration.Instance.Phone = resp.Phone;
                                if (!string.IsNullOrEmpty(resp.Street1))
                                    Registration.Instance.Street1 = resp.Street1;
                                if (!string.IsNullOrEmpty(resp.Street2))
                                    Registration.Instance.Street2 = resp.Street2;
                                if (!string.IsNullOrEmpty(resp.City))
                                    Registration.Instance.City = resp.City;
                                if (!string.IsNullOrEmpty(resp.State))
                                    Registration.Instance.State = resp.State;
                                if (!string.IsNullOrEmpty(resp.Zip))
                                    Registration.Instance.Zip = resp.Zip;
                                if (!string.IsNullOrEmpty(resp.DOB))
                                    Registration.Instance.DOB = resp.DOB;                             

                                await _navigationService.Navigate<PatientRegistrationStepOneViewModel>();
                                break;
                            case StatusCode.InvalidCreditCard:
                                WarningText = "Invalid Credit Card.";
                                break;
                            case StatusCode.AlreadyRegistered:
                                WarningText = "Already Registered.";
                                break;
                            case StatusCode.ErrorContactCustomerSupport:
                                await _navigationService.Navigate<PatientRegistrationErrorViewModel, StatusCode>(resp.StatusCode);
                                break;
                            default:
                                WarningText = "There was an error please try again.";
                                break;
                        }
                    }
                    IsBusy = false;
                }
            }
            catch (PatientException ex)
            {
                IsBusy = false;
                ReportCrash(ex, Title);
            }
            catch (Exception ex)
            {
                IsBusy = false;
                ReportCrash(ex, Title);
            }

        }

        private async Task UpdateCardInformation()
        {
            WarningText = string.Empty;
            if (string.IsNullOrEmpty(UserCardInfo.CardFirstName) ||
                string.IsNullOrEmpty(UserCardInfo.CardLastName) ||
                string.IsNullOrEmpty(UserCardInfo.CardNumber) ||
                string.IsNullOrEmpty(UserCardInfo.CardSecurityCode) ||
                string.IsNullOrEmpty(UserCardInfo.BillingAddress) ||
                string.IsNullOrEmpty(UserCardInfo.BillingCity) ||
                string.IsNullOrEmpty(UserCardInfo.BillingState) ||
                string.IsNullOrEmpty(UserCardInfo.BillingZip ))
            {
                ShowWarning = false;
                WarningText = "Please fill all the required fields!";
                return;
            }

            if (!CardInfoHasChanges)
            {
                return;
            }

            IsBusy = true;
            try
            {
                var resp = await DataUtility.PatientUpdateCreditCardInfoAsync(SettingsValues.ApiURLValue, CommonAuthSession.Token, UserCardInfo).ConfigureAwait(false);
                IsBusy = false;
                if (resp != null)
                {
                    switch (resp.StatusCode)
                    {
                        case StatusCode.Success:
                            if (Globals.Instance.UserInfo.CanceledForPaymentIssues)
                                await _navigationService.Navigate<PatientSettingsReactivateUserPlanViewModel>();
                            else
                                WarningText = "Your changes have been saved";
                            await GetCardInformation();
                            break;
                        case StatusCode.CardInformationNotProvided:
                            ShowWarning = false;
                            WarningText = "To update your card information you must re-enter your credit card number and security code.";
                            break;
                        case StatusCode.PaymentErrorSeePayload:
                            WarningText = resp.Payload;
                            break;
                        default:
                            if (!string.IsNullOrEmpty(resp.Message))
                                await _userDialogs.AlertAsync(resp.Message);
                            break;
                    }
                }
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

        private async Task GoToPreviousAsync()
        {
            await _navigationService.Close(this);
        }
        public async Task<List<CommonLibraryCoreMaui.Models.UIText>> GetUITopic(string strTopicName)
        {
            var pageText = await DataUtility.GetUITopicListAsync(SettingsValues.ApiURLValue, strTopicName, "en").ConfigureAwait(false);
            return pageText.UITextList;
        }
        private List<UIText> GetPageTextList(string uiTopic)
        {
            Task<List<UIText>> task = Task.Run<List<UIText>>(async () => await GetUITopic(uiTopic));
            return task.Result;
        }
        public async Task VerifyPromoCode()
        {
            ShowWarning = false;
            WarningText = string.Empty;
            var lsttext = GetPageTextList("EnterPaymentInformation");
            var PromoCodeInvalid = lsttext.Find(i => i.TagName == "PromoCodeInvalid").Text;
            if (!string.IsNullOrEmpty(TxtPromoCode))
            {
                StatusResponse respPromo = await DataUtility.ValidatePromoCodeAsync(SettingsValues.ApiURLValue, Registration.Instance.Subscription.OptionID, TxtPromoCode);
                if (respPromo != null)
                {
                    if (respPromo.StatusCode == StatusCode.SuccessSeePayload)
                    {
                        if (!string.IsNullOrEmpty(respPromo.Payload))
                        {
                            IsBusy = false;
                            IsValidPromoCode = true;
                            WarningText = string.Empty;
                        }
                    }
                    else
                    {
                        ShowWarning = true;
                        IsValidPromoCode = false;
                        if (respPromo.StatusCode == StatusCode.Error)
                        {
                            WarningText = respPromo.Message;
                        }
                        else
                            WarningText = PromoCodeInvalid;
                        
                        return;
                    }
                    }
            }
        }
    }

    public class PatientSettingsReactivateUserPlanViewModel : BaseViewModel
    {

        private SubscriptionChangeInfo _subscriptionInfo;
        public SubscriptionChangeInfo SubscriptionInfo
        {
            get { return _subscriptionInfo; }
            set { SetProperty(ref _subscriptionInfo, value); }
        }

        private string _subscriptionInfoDescription1;
        public string SubscriptionInfoDescription1
        {
            get { return _subscriptionInfoDescription1; }
            set { SetProperty(ref _subscriptionInfoDescription1, value); }
        }

        private string _subscriptionInfoDescription2;
        public string SubscriptionInfoDescription2
        {
            get { return _subscriptionInfoDescription2; }
            set { SetProperty(ref _subscriptionInfoDescription2, value); }
        }

        private string _planNameAndCost;
        public string PlanNameAndCost
        {
            get { return _planNameAndCost; }
            set { SetProperty(ref _planNameAndCost, value); }
        }

        private bool _readNoticeCheck = false;
        public bool ReadNoticeCheck
        {
            get { return _readNoticeCheck; }
            set { SetProperty(ref _readNoticeCheck, value); }
        }

        public IMvxCommand ContinueCommand => new MvxAsyncCommand(ContinueAsync);
        public IMvxCommand CancelCommand => new MvxAsyncCommand(CancelAsync);

        public PatientSettingsReactivateUserPlanViewModel(IUserDialogs userDialog, IMvxNavigationService mvxNavigationService)
        {
            _navigationService = mvxNavigationService;
            _userDialogs = userDialog;
        }

        public async override Task Initialize()
        {
            await GetPreviousPlanInfo();
            await base.Initialize();
        }

        public async Task<List<CommonLibraryCoreMaui.Models.UIText>> GetUITopic(string strTopicName)
        {
            var pageText = await DataUtility.GetUITopicListAsync(SettingsValues.ApiURLValue, strTopicName, "en").ConfigureAwait(false);
            return pageText.UITextList;
        }
        private List<UIText> GetPageTextList(string uiTopic)
        {
            Task<List<UIText>> task = Task.Run<List<UIText>>(async () => await GetUITopic(uiTopic));
            return task.Result;
        }
    
        async Task GetPreviousPlanInfo()
        {
            SubscriptionInfo = await DataUtility.GetChangeToPreviousSubscriptionInfoAsync(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.PatientID, CommonAuthSession.Token).ConfigureAwait(false);
            // UITopic - Para1_Reactive
           
            var lsttext = GetPageTextList("UpdManageSubscriptions");
            var enText = string.Format(lsttext.Find(x => x.TagName == "Para1_Reactive").Text, SubscriptionInfo.SubscriptionPlanName, SubscriptionInfo.CostDifference);

            //SubscriptionInfoDescription1 = $"Continue to reactivate your {SubscriptionInfo.SubscriptionPlanName}. Your card will be charged {SubscriptionInfo.CostDifference} today and covers service through the end of month. The subscription amount of {SubscriptionInfo.SubscriptionCost} will then be billed monthly to your card ending in {SubscriptionInfo.Last4ofCC} starting on {SubscriptionInfo.NextBillingDate}.";
            SubscriptionInfoDescription1 = enText;
            Subscription subscription = SubscriptionsFactory.Get(SubscriptionInfo.SubscriptionID);
            if (subscription is FamilySubscription)
            {
                //UITopic -FamilyMbrInfo UITopicID 2
                var FamilyMbrInfo = string.Format(lsttext.Find(x => x.TagName == "FamilyMbrInfo").Text, SubscriptionInfo.SubscriptionMemberLimit, SubscriptionInfo.AdditonalFamilyMemberAmount);
                SubscriptionInfoDescription2 = FamilyMbrInfo;
                //SubscriptionInfoDescription2 = $"Up to {SubscriptionInfo.SubscriptionMemberLimit} family members, including the primary account holder, can be covered by the family plan. Additional family members may be added for {SubscriptionInfo.AdditonalFamilyMemberAmount}/month under Account Profiles. Inactive family members can be reactivated after the subscription change in Account Profiles.";
            }
            else if (subscription is IndividualSubscription)
            {
                SubscriptionInfoDescription2 = SubscriptionInfo.SubscriptionPlanDescription;
            }
            var cost = SubscriptionInfo.SubscriptionCost != "100" || SubscriptionInfo.SubscriptionCost != "200" ? $"{SubscriptionInfo.SubscriptionCost}/month" : $"{SubscriptionInfo.SubscriptionCost}";
            PlanNameAndCost = $"{SubscriptionInfo.SubscriptionPlanName}:" + cost;
        }

        private async Task ContinueAsync()
        {
            await _navigationService.Navigate<PatientSettingsReactivePlannOrderSummaryViewModel, SubscriptionChangeInfo>(SubscriptionInfo);
        }

        private async Task CancelAsync()
        {
            await _navigationService.Close(this);
        }
    }
}
