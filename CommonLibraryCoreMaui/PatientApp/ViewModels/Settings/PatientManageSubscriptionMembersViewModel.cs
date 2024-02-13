using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using CommonLibraryCoreMaui.Factory;
using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.Models.NavigationParameters;
using CommonLibraryCoreMaui.Services;
using CommonLibraryCoreMaui.ViewModels;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class PatientManageSubscriptionMembersViewModel : BaseNavigationViewModel<AccountSubscriptionInfo>
    {
        IPatientService _patientService;
        public SubscriptionBase CurrentSubscriptionPlan;
        private AccountSubscriptionInfo _accountMemberSubscriptionInfo;
        public AccountSubscriptionInfo AccountMemberSubscriptionInfo
        {
            get { return _accountMemberSubscriptionInfo; }
            set { SetProperty(ref _accountMemberSubscriptionInfo, value); }
        }

        private List<SubscriptionBase> _availableSubscriptionList;
        public List<SubscriptionBase> AvailableSubscriptionList
        {
            get { return _availableSubscriptionList; }
            set { SetProperty(ref _availableSubscriptionList, value); }
        }

        private AccountMember _selectedAccountMember;
        public AccountMember SelectedAccountMember
        {
            get { return _selectedAccountMember; }
            set { SetProperty(ref _selectedAccountMember, value); }
        }

        private SubscriptionBase _selectedSubscriptionPlan;
        public SubscriptionBase SelectedSubscriptionPlan
        {
            get { return _selectedSubscriptionPlan; }
            set
            {
                SetProperty(ref _selectedSubscriptionPlan, value);
                IsDifferentPlanSelected = !(CurrentSubscriptionPlan.OptionID == SelectedSubscriptionPlan.OptionID);
                IsPromoCodeEnable = IsDifferentPlanSelected;
            }
        }
        private bool _isDifferentPlanSelected;
        public bool IsDifferentPlanSelected
        {
            get { return _isDifferentPlanSelected; }
            set { SetProperty(ref _isDifferentPlanSelected, value); }
        }

        private bool _isPlanCanceled;
        public bool IsPlanCanceled
        {
            get { return _isPlanCanceled; }
            set { SetProperty(ref _isPlanCanceled, value); }
        }

        private bool _canChangePlan = true;
        public bool CanChangePlan
        {
            get { return _canChangePlan; }
            set { SetProperty(ref _canChangePlan, value); }
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

        private bool _isPromoCodeEnable = false;
        public bool IsPromoCodeEnable
        {
            get { return _isPromoCodeEnable; }
            set { SetProperty(ref _isPromoCodeEnable, value); }
        }
        public Action ClosePopup;

        public IMvxCommand SavePlanCommand => new MvxAsyncCommand(SavePlan);
        public IMvxCommand CancelPlanForMemberPlanCommand => new MvxAsyncCommand(CancelPlanForMember);
        public IMvxCommand CancelPlanCommand => new MvxAsyncCommand(CancelPlan);
        public MvxInteraction<string> OpenCancelSubscriptionView { get; } = new MvxInteraction<string>();
        public MvxInteraction NavigateToManageSubscription { get; } = new MvxInteraction();
        // public IMvxCommand ValidatePromoCode => new MvxAsyncCommand(VerifyPromoCode);

        private bool _showWarning;
        public bool ShowWarning
        {
            get { return _showWarning; }
            set { _showWarning = value; RaisePropertyChanged(() => ShowWarning); }
        }
        private string _warningText;
        public string WarningText
        {
            get { return _warningText; }
            set { _warningText = value; RaisePropertyChanged(() => WarningText); }
        }
        private bool _isWentToValidate;
        public bool IsWentToValidate
        {
            get { return _isWentToValidate; }
            set { SetProperty(ref _isWentToValidate, value); }
        }

        public PatientManageSubscriptionMembersViewModel()
        {
            _patientService = Mvx.IoCProvider.Resolve<IPatientService>();
            ShowWarning = false;
        }

        public override void Prepare(AccountSubscriptionInfo parameter)
        {
            AccountMemberSubscriptionInfo = parameter;
            RefreshAccountMembersList();
            base.Prepare();
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

        public void RefreshAccountMembersList()
        {
            IsBusy = true;

            try
            {
                CurrentSubscriptionPlan = SubscriptionsFactory.Get(AccountMemberSubscriptionInfo.CurrentSubscriptionPlan, AccountMemberSubscriptionInfo.CanAddFamilyMembers);

                if (Globals.Instance.UserInfo.ShowSubscriptionChangeInfo() || Globals.Instance.UserInfo.ShowSubscriptionCanceledInfo())
                {
                    if (Globals.Instance.UserInfo.ShowSubscriptionChangeInfo())
                    {
                        CanChangePlan = false;
                    }

                    SelectedSubscriptionPlan = CurrentSubscriptionPlan;
                }

                if (AccountMemberSubscriptionInfo.IsPrepay)
                {
                    AvailableSubscriptionList = new List<SubscriptionBase>();
                    if (AccountMemberSubscriptionInfo.CurrentSubscriptionPlan == "N/A")
                    {
                        AvailableSubscriptionList.AddRange(AccountMemberSubscriptionInfo.AvailableSubscriptions);
                        SelectedSubscriptionPlan = AvailableSubscriptionList[0];
                    }
                    else
                    {
                        AvailableSubscriptionList.AddRange(new List<SubscriptionBase>
                        { new IndividualSubscription() { Name = AccountMemberSubscriptionInfo.CurrentSubscriptionPlan } });
                        CanChangePlan = false;
                        SelectedSubscriptionPlan = CurrentSubscriptionPlan = AvailableSubscriptionList[0];
                    }
                }
                else
                {
                    var cOptionId = SubscriptionsFactory.Get(AccountMemberSubscriptionInfo.CurrentSubscriptionPlan, AccountMemberSubscriptionInfo.CanAddFamilyMembers).OptionID;
                    if (cOptionId == 0)
                    {
                        CurrentSubscriptionPlan.Name = AccountMemberSubscriptionInfo.CurrentSubscriptionPlan == "N/A" ? "Payment Plans" : AccountMemberSubscriptionInfo.CurrentSubscriptionPlan;
                        CurrentSubscriptionPlan.PlanDescription = AccountMemberSubscriptionInfo.CurrentSubscriptionPlanDescription;
                        CurrentSubscriptionPlan.Cost = AccountMemberSubscriptionInfo.CurrentSubscriptionPlanCost;
                        SelectedSubscriptionPlan = CurrentSubscriptionPlan;
                        AvailableSubscriptionList = new List<SubscriptionBase> { CurrentSubscriptionPlan };
                        AvailableSubscriptionList.AddRange(AccountMemberSubscriptionInfo.AvailableSubscriptions);
                    }
                    else
                    {
                        SelectedSubscriptionPlan = CurrentSubscriptionPlan = AccountMemberSubscriptionInfo.AvailableSubscriptions.FirstOrDefault(x => x.OptionID == cOptionId);
                        AvailableSubscriptionList = AccountMemberSubscriptionInfo.AvailableSubscriptions;
                    }
                }
            }
            catch (Exception ex)
            {
                ReportCrash(ex, Title);
            }

            IsBusy = false;
        }

        private async Task SavePlan()
        {

            bool canProceed = false;
            SelectedSubscriptionPlan.GrantedPromotionCode = string.Empty;
            WarningText = string.Empty;
            var lsttext = GetPageTextList("EnterPaymentInformation");
            var PromoCodeInvalid = lsttext.Find(i => i.TagName == "PromoCodeInvalid").Text;
            if (SelectedSubscriptionPlan != null)
            {
                if (!string.IsNullOrEmpty(TxtPromoCode))
                {
                    StatusResponse respPromo = await DataUtility.ValidatePromoCodeAsync(SettingsValues.ApiURLValue, SelectedSubscriptionPlan.OptionID, TxtPromoCode);
                
                    if (respPromo.StatusCode == StatusCode.SuccessSeePayload)
                    {
                        SelectedSubscriptionPlan.GrantedPromotionCode = TxtPromoCode;
                        canProceed = true;
                    }
                    else
                    {
                        ShowWarning = true;
                        IsValidPromoCode = false;
                        SelectedSubscriptionPlan.GrantedPromotionCode = string.Empty;
                        canProceed = false;
                        if (respPromo.StatusCode == StatusCode.Error)
                        {
                            WarningText = respPromo.Message;
                        }
                        else  WarningText = PromoCodeInvalid;                
                  
                        //WarningText = $"Promo code invalid. Please remove or re-enter";                      
                    }
                }
                else
                {
                    canProceed = true;
                }

                if (canProceed)
                {
                    if (SelectedSubscriptionPlan.OptionID != CurrentSubscriptionPlan.OptionID)
                    {
                        var type = SubscriptionChangePlanFactory.GetPlanType(SelectedSubscriptionPlan, CurrentSubscriptionPlan);
                        var result = await _navigationService.Navigate<PatientSettingsManageSubscriptionChangePlanViewModel, PlanChangeNavigationParam>(new PlanChangeNavigationParam()
                        {
                            Type = type,
                            Subscription = (Subscription)SelectedSubscriptionPlan,
                        });
                    }
                }
            }
        }
        public async Task VerifyPromoCode()
        {
            ShowWarning = false;
            IsWentToValidate = true;
            SelectedSubscriptionPlan.GrantedPromotionCode = string.Empty;
            var lsttext = GetPageTextList("EnterPaymentInformation");
            var PromoCodeInvalid = lsttext.Find(i => i.TagName == "PromoCodeInvalid").Text;
            if (!string.IsNullOrEmpty(TxtPromoCode))
            {
                StatusResponse respPromo = await DataUtility.ValidatePromoCodeAsync(SettingsValues.ApiURLValue, SelectedSubscriptionPlan.OptionID, TxtPromoCode);
                if (respPromo != null)
                {
                    if (respPromo.StatusCode == StatusCode.SuccessSeePayload)
                    {
                        if (!string.IsNullOrEmpty(respPromo.Payload))
                        {
                            IsBusy = false;
                            IsValidPromoCode = true;
                            ShowWarning = false;
                            SelectedSubscriptionPlan.GrantedPromotionCode = TxtPromoCode;
                            IsDifferentPlanSelected = true;
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
                        else WarningText = PromoCodeInvalid;
                        return;
                    }
                }
            }
            else SelectedSubscriptionPlan.GrantedPromotionCode = TxtPromoCode;
        }

        public async Task CancelPlanForMember()
        {
            IsBusy = true;
            StatusResponse resp1 = await _patientService.ClickCancelPlanButtonAsync();
            if (resp1 != null) IsBusy = false;
            StatusResponse cancel = await _patientService.PatientCancelSubscriptionAsync();
            IsBusy = false;
            if (cancel != null)
            {
                switch (cancel.StatusCode)
                {
                    case StatusCode.Success:
                        ClosePopup?.Invoke();
                        IsBusy = true;
                        var userInfo = await DataUtility.GetUserInfo(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.UserID, true, CommonAuthSession.Token)
                            .ConfigureAwait(false);
                        Globals.Instance.UserInfo = userInfo;
                        IsBusy = false;
                        InvokeOnMainThread(() =>
                        {
                            NavigateToManageSubscription.Raise();
                        });
                        break;
                    case StatusCode.SubscriptionAlreadyDeactivated:
                        ClosePopup?.Invoke();
                        IsPlanCanceled = true;
                        await _userDialogs.AlertAsync(cancel.Message);
                        break;
                }
            }

            
        }

        public async Task CancelPlan()
        {
            IsBusy = true;
            StatusResponse resp1 =await _patientService.ClickCancelPlanHyperlinkAsync();
            if (resp1 != null) IsBusy = false;
            StatusResponse resp = await _patientService.PatientGetCancelSubscriptionDateAsync();

            if (resp != null)
            {
                if (resp.StatusCode == StatusCode.SuccessSeePayload)
                {
                    if (!string.IsNullOrEmpty(resp.Payload))
                    {
                        IsBusy = false;
                        OpenCancelSubscriptionView.Raise(resp.Payload);
                    }
                }
            }

        
            IsBusy = false;
        }
    }
}
