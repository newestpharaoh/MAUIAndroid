using Acr.UserDialogs;
using CommonLibraryCoreMaui;
using CommonLibraryCoreMaui.Models; 
using AndroidPatientAppMaui.Views.MyAccount;
using CommunityToolkit.Maui.Views;

namespace AndroidPatientAppMaui.ViewModels.MyAccount
{
    public class ManageSubscriptionPageViewModel : BaseViewModel
    {
        #region Constructor 
        //To define the class level variable.
        public string AppName = Preferences.Get("AppName", string.Empty);
        string Token = string.Empty;
        int Userid = 0;
        UserInfo userInfo;
        AccountSubscriptionInfo info;
        bool hasPromoCode;
        public ManageSubscriptionPage manageSubscriptionP;
        public ManageSubscriptionPageViewModel(INavigation nav, ManageSubscriptionPage manageSubscriptionPage)
        {
            try
            {
                Navigation = nav;
                BackCommand = new Command(BackAsync);
                ChangePlanCommand = new Command(ChangePlanAsync);
                UpdatePaymentCommand = new Command(UpdatePaymentAsync);
                DownloadOrderSummary = new Command(DownloadOrderSummaryAsync); 

                Token = Preferences.Get("AuthToken", string.Empty);
                Userid = Preferences.Get("UserId", 0);
                manageSubscriptionP = manageSubscriptionPage;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
         
        #endregion

        #region Command
        public Command BackCommand { get; set; }
        public Command ChangePlanCommand { get; set; }
        public Command UpdatePaymentCommand { get; set; }
        public Command DownloadOrderSummary { get; set; } 
        #endregion

        #region Properties
        private bool _lytPaymentIssuesInfoIsVisible = false;
        public bool lytPaymentIssuesInfoIsVisible
        {
            get { return _lytPaymentIssuesInfoIsVisible; }
            set
            {
                if (_lytPaymentIssuesInfoIsVisible != value)
                {
                    _lytPaymentIssuesInfoIsVisible = value;
                    OnPropertyChanged("lytPaymentIssuesInfoIsVisible");
                }
            }
        }
        private bool _lytChangeInfoIsVisible = false;
        public bool lytChangeInfoIsVisible
        {
            get { return _lytChangeInfoIsVisible; }
            set
            {
                if (_lytChangeInfoIsVisible != value)
                {
                    _lytChangeInfoIsVisible = value;
                    OnPropertyChanged("lytChangeInfoIsVisible");
                }
            }
        }
        private bool _lytInfoIsVisible = false;
        public bool lytInfoIsVisible
        {
            get { return _lytInfoIsVisible; }
            set
            {
                if (_lytInfoIsVisible != value)
                {
                    _lytInfoIsVisible = value;
                    OnPropertyChanged("lytInfoIsVisible");
                }
            }
        }
        private bool _lblChangeInfo = false;
        public bool lblChangeInfo
        {
            get { return _lblChangeInfo; }
            set
            {
                if (_lblChangeInfo != value)
                {
                    _lblChangeInfo = value;
                    OnPropertyChanged("lblChangeInfo");
                }
            }
        }
        private string _ChangeInfo;
        public string ChangeInfo
        {
            get { return _ChangeInfo; }
            set
            {
                if (_ChangeInfo != value)
                {
                    _ChangeInfo = value;
                    OnPropertyChanged("ChangeInfo");
                }
            }
        }
        private bool _lblChangeInfo1 = false;
        public bool lblChangeInfo1
        {
            get { return _lblChangeInfo1; }
            set
            {
                if (_lblChangeInfo1 != value)
                {
                    _lblChangeInfo1 = value;
                    OnPropertyChanged("lblChangeInfo1");
                }
            }
        }
        private string _ChangeInfo1;
        public string ChangeInfo1
        {
            get { return _ChangeInfo1; }
            set
            {
                if (_ChangeInfo1 != value)
                {
                    _ChangeInfo1 = value;
                    OnPropertyChanged("ChangeInfo1");
                }
            }
        }
        private string _lblCurrentSubscription;
        public string lblCurrentSubscription
        {
            get { return _lblCurrentSubscription; }
            set
            {
                if (_lblCurrentSubscription != value)
                {
                    _lblCurrentSubscription = value;
                    OnPropertyChanged("lblCurrentSubscription");
                }
            }
        }
        private string _txtPromoCodeUsed = "Super Suver!";
        public string txtPromoCodeUsed
        {
            get { return _txtPromoCodeUsed; }
            set
            {
                if (_txtPromoCodeUsed != value)
                {
                    _txtPromoCodeUsed = value;
                    OnPropertyChanged("txtPromoCodeUsed");
                }
            }
        }
        private string _lblPlanCost = "Plan Cost:";
        public string lblPlanCost
        {
            get { return _lblPlanCost; }
            set
            {
                if (_lblPlanCost != value)
                {
                    _lblPlanCost = value;
                    OnPropertyChanged("lblPlanCost");
                }
            }
        }
        private string _txtPlanCost = "$200.00";
        public string txtPlanCost
        {
            get { return _txtPlanCost; }
            set
            {
                if (_txtPlanCost != value)
                {
                    _txtPlanCost = value;
                    OnPropertyChanged("txtPlanCost");
                }
            }
        }
        private string _lblDiscountApplied = "Discount Applied:";
        public string lblDiscountApplied
        {
            get { return _lblDiscountApplied; }
            set
            {
                if (_lblDiscountApplied != value)
                {
                    _lblDiscountApplied = value;
                    OnPropertyChanged("lblDiscountApplied");
                }
            }
        }
        private string _txtDiscountApplied = "$66.00";
        public string txtDiscountApplied
        {
            get { return _txtDiscountApplied; }
            set
            {
                if (_txtDiscountApplied != value)
                {
                    _txtDiscountApplied = value;
                    OnPropertyChanged("txtDiscountApplied");
                }
            }
        }
        private string _lblTotalUsed = "Total:";
        public string lblTotalUsed
        {
            get { return _lblTotalUsed; }
            set
            {
                if (_lblTotalUsed != value)
                {
                    _lblTotalUsed = value;
                    OnPropertyChanged("lblTotalUsed");
                }
            }
        }
        private string _txtTotalUsed = "$99.00";
        public string txtTotalUsed
        {
            get { return _txtTotalUsed; }
            set
            {
                if (_txtTotalUsed != value)
                {
                    _txtTotalUsed = value;
                    OnPropertyChanged("txtTotalUsed");
                }
            }
        }
        private string _lblExpDate = "Expiration Date";
        public string lblExpDate
        {
            get { return _lblExpDate; }
            set
            {
                if (_lblExpDate != value)
                {
                    _lblExpDate = value;
                    OnPropertyChanged("lblExpDate");
                }
            }
        }
        private string _txtExpDate = "9/8/2022";
        public string txtExpDate
        {
            get { return _txtExpDate; }
            set
            {
                if (_txtExpDate != value)
                {
                    _txtExpDate = value;
                    OnPropertyChanged("txtExpDate");
                }
            }
        }
        private string _managesubscription_yourpaymentmethod = "Subscriptions are renewed monthly.";
        public string managesubscription_yourpaymentmethod
        {
            get { return _managesubscription_yourpaymentmethod; }
            set
            {
                if (_managesubscription_yourpaymentmethod != value)
                {
                    _managesubscription_yourpaymentmethod = value;
                    OnPropertyChanged("managesubscription_yourpaymentmethod");
                }
            }
        }
        private string _lblPlanDescription;
        public string lblPlanDescription
        {
            get { return _lblPlanDescription; }
            set
            {
                if (_lblPlanDescription != value)
                {
                    _lblPlanDescription = value;
                    OnPropertyChanged("lblPlanDescription");
                }
            }
        }
        private bool _lytCancellationInfo = false;
        public bool lytCancellationInfo
        {
            get { return _lytCancellationInfo; }
            set
            {
                if (_lytCancellationInfo != value)
                {
                    _lytCancellationInfo = value;
                    OnPropertyChanged("lytCancellationInfo");
                }
            }
        }
        private bool _lytInactive = false;
        public bool lytInactive
        {
            get { return _lytInactive; }
            set
            {
                if (_lytInactive != value)
                {
                    _lytInactive = value;
                    OnPropertyChanged("lytInactive");
                }
            }
        }
        private bool _lytInactive_Inactive = false;
        public bool lytInactive_Inactive
        {
            get { return _lytInactive_Inactive; }
            set
            {
                if (_lytInactive_Inactive != value)
                {
                    _lytInactive_Inactive = value;
                    OnPropertyChanged("lytInactive_Inactive");
                }
            }
        }
        private bool _lytInactive_ActiveUntil = false;
        public bool lytInactive_ActiveUntil
        {
            get { return _lytInactive_ActiveUntil; }
            set
            {
                if (_lytInactive_ActiveUntil != value)
                {
                    _lytInactive_ActiveUntil = value;
                    OnPropertyChanged("lytInactive_ActiveUntil");
                }
            }
        }
        private bool _lytInactiveNonPayment = false;
        public bool lytInactiveNonPayment
        {
            get { return _lytInactiveNonPayment; }
            set
            {
                if (_lytInactiveNonPayment != value)
                {
                    _lytInactiveNonPayment = value;
                    OnPropertyChanged("lytInactiveNonPayment");
                }
            }
        }
        private bool _lytPromoCode = false;
        public bool lytPromoCode
        {
            get { return _lytPromoCode; }
            set
            {
                if (_lytPromoCode != value)
                {
                    _lytPromoCode = value;
                    OnPropertyChanged("lytPromoCode");
                }
            }
        }
        private bool _lytPlanCost = false;
        public bool lytPlanCost
        {
            get { return _lytPlanCost; }
            set
            {
                if (_lytPlanCost != value)
                {
                    _lytPlanCost = value;
                    OnPropertyChanged("lytPlanCost");
                }
            }
        }
        private bool _lytDiscountApplied = false;
        public bool lytDiscountApplied
        {
            get { return _lytDiscountApplied; }
            set
            {
                if (_lytDiscountApplied != value)
                {
                    _lytDiscountApplied = value;
                    OnPropertyChanged("lytDiscountApplied");
                }
            }
        }

        private bool _lytExpDate = false;
        public bool lytExpDate
        {
            get { return _lytExpDate; }
            set
            {
                if (_lytExpDate != value)
                {
                    _lytExpDate = value;
                    OnPropertyChanged("lytExpDate");
                }
            }
        }
        private bool _lytChoosePlan = false;
        public bool lytChoosePlan
        {
            get { return _lytChoosePlan; }
            set
            {
                if (_lytChoosePlan != value)
                {
                    _lytChoosePlan = value;
                    OnPropertyChanged("lytChoosePlan");
                }
            }
        }
        private bool _lytActive = false;
        public bool lytActive
        {
            get { return _lytActive; }
            set
            {
                if (_lytActive != value)
                {
                    _lytActive = value;
                    OnPropertyChanged("lytActive");
                }
            }
        }
        private bool _lytChangePlan;
        public bool lytChangePlan
        {
            get { return _lytChangePlan; }
            set
            {
                if (_lytChangePlan != value)
                {
                    _lytChangePlan = value;
                    OnPropertyChanged("lytChangePlan");
                }
            }
        }
        private bool _lytPayment;
        public bool lytPayment
        {
            get { return _lytPayment; }
            set
            {
                if (_lytPayment != value)
                {
                    _lytPayment = value;
                    OnPropertyChanged("lytPayment");
                }
            }
        }
        private bool _btnChangePlan;
        public bool btnChangePlan
        {
            get { return _btnChangePlan; }
            set
            {
                if (_btnChangePlan != value)
                {
                    _btnChangePlan = value;
                    OnPropertyChanged("btnChangePlan");
                }
            }
        }

        #endregion

        #region Properties for Popup page 
        private string _BillingPoliciesName;
        public string BillingPoliciesName
        {
            get { return _BillingPoliciesName; }
            set
            {
                if (_BillingPoliciesName != value)
                {
                    _BillingPoliciesName = value;
                    OnPropertyChanged("BillingPoliciesName");
                }
            }
        }
        private string _MonthlySubscriptionPlan;
        public string MonthlySubscriptionPlan
        {
            get { return _MonthlySubscriptionPlan; }
            set
            {
                if (_MonthlySubscriptionPlan != value)
                {
                    _MonthlySubscriptionPlan = value;
                    OnPropertyChanged("MonthlySubscriptionPlan");
                }
            }
        }
        private string _MonthlySubscriptionPlanP1;
        public string MonthlySubscriptionPlanP1
        {
            get { return _MonthlySubscriptionPlanP1; }
            set
            {
                if (_MonthlySubscriptionPlanP1 != value)
                {
                    _MonthlySubscriptionPlanP1 = value;
                    OnPropertyChanged("MonthlySubscriptionPlanP1");
                }
            }
        }
        private string _MonthlySubscriptionPlanP1B1;
        public string MonthlySubscriptionPlanP1B1
        {
            get { return _MonthlySubscriptionPlanP1B1; }
            set
            {
                if (_MonthlySubscriptionPlanP1B1 != value)
                {
                    _MonthlySubscriptionPlanP1B1 = value;
                    OnPropertyChanged("MonthlySubscriptionPlanP1B1");
                }
            }
        }
        private string _MonthlySubscriptionPlanP1B2;
        public string MonthlySubscriptionPlanP1B2
        {
            get { return _MonthlySubscriptionPlanP1B2; }
            set
            {
                if (_MonthlySubscriptionPlanP1B2 != value)
                {
                    _MonthlySubscriptionPlanP1B2 = value;
                    OnPropertyChanged("MonthlySubscriptionPlanP1B2");
                }
            }
        }
        private string _TablePurchaseDate;
        public string TablePurchaseDate
        {
            get { return _TablePurchaseDate; }
            set
            {
                if (_TablePurchaseDate != value)
                {
                    _TablePurchaseDate = value;
                    OnPropertyChanged("TablePurchaseDate");
                }
            }
        }
        private string _TableAutoRenewalDate;
        public string TableAutoRenewalDate
        {
            get { return _TableAutoRenewalDate; }
            set
            {
                if (_TableAutoRenewalDate != value)
                {
                    _TableAutoRenewalDate = value;
                    OnPropertyChanged("TableAutoRenewalDate");
                }
            }
        }
        private string _Table_1st_28_Col1;
        public string Table_1st_28_Col1
        {
            get { return _Table_1st_28_Col1; }
            set
            {
                if (_Table_1st_28_Col1 != value)
                {
                    _Table_1st_28_Col1 = value;
                    OnPropertyChanged("Table_1st_28_Col1");
                }
            }
        }
        private string _Table_1st_28_Col2;
        public string Table_1st_28_Col2
        {
            get { return _Table_1st_28_Col2; }
            set
            {
                if (_Table_1st_28_Col2 != value)
                {
                    _Table_1st_28_Col2 = value;
                    OnPropertyChanged("Table_1st_28_Col2");
                }
            }
        }
        private string _Table_29th_Col1;
        public string Table_29th_Col1
        {
            get { return _Table_29th_Col1; }
            set
            {
                if (_Table_29th_Col1 != value)
                {
                    _Table_29th_Col1 = value;
                    OnPropertyChanged("Table_29th_Col1");
                }
            }
        }
        private string _Table_29th_Col2;
        public string Table_29th_Col2
        {
            get { return _Table_29th_Col2; }
            set
            {
                if (_Table_29th_Col2 != value)
                {
                    _Table_29th_Col2 = value;
                    OnPropertyChanged("Table_29th_Col2");
                }
            }
        }
        private string _Table_30th_Col1;
        public string Table_30th_Col1
        {
            get { return _Table_30th_Col1; }
            set
            {
                if (_Table_30th_Col1 != value)
                {
                    _Table_30th_Col1 = value;
                    OnPropertyChanged("Table_30th_Col1");
                }
            }
        }
        private string _Table_30th_Col2;
        public string Table_30th_Col2
        {
            get { return _Table_30th_Col2; }
            set
            {
                if (_Table_30th_Col2 != value)
                {
                    _Table_30th_Col2 = value;
                    OnPropertyChanged("Table_30th_Col2");
                }
            }
        }
        private string _Table_31th_Col1;
        public string Table_31th_Col1
        {
            get { return _Table_31th_Col1; }
            set
            {
                if (_Table_31th_Col1 != value)
                {
                    _Table_31th_Col1 = value;
                    OnPropertyChanged("Table_31th_Col1");
                }
            }
        }
        private string _Table_31th_Col2;
        public string Table_31th_Col2
        {
            get { return _Table_31th_Col2; }
            set
            {
                if (_Table_31th_Col2 != value)
                {
                    _Table_31th_Col2 = value;
                    OnPropertyChanged("Table_31th_Col2");
                }
            }
        }
        private string _UpdatingCreditCardName;
        public string UpdatingCreditCardName
        {
            get { return _UpdatingCreditCardName; }
            set
            {
                if (_UpdatingCreditCardName != value)
                {
                    _UpdatingCreditCardName = value;
                    OnPropertyChanged("UpdatingCreditCardName");
                }
            }
        }
        private string _UpdatingCreditCard;
        public string UpdatingCreditCard
        {
            get { return _UpdatingCreditCard; }
            set
            {
                if (_UpdatingCreditCard != value)
                {
                    _UpdatingCreditCard = value;
                    OnPropertyChanged("UpdatingCreditCard");
                }
            }
        }
        private string _ChangePlanInfo;
        public string ChangePlanInfo
        {
            get { return _ChangePlanInfo; }
            set
            {
                if (_ChangePlanInfo != value)
                {
                    _ChangePlanInfo = value;
                    OnPropertyChanged("ChangePlanInfo");
                }
            }
        }
        private string _SubscriptionPlanChanges;
        public string SubscriptionPlanChanges
        {
            get { return _SubscriptionPlanChanges; }
            set
            {
                if (_SubscriptionPlanChanges != value)
                {
                    _SubscriptionPlanChanges = value;
                    OnPropertyChanged("SubscriptionPlanChanges");
                }
            }
        }
        private string _NoRefunds;
        public string NoRefunds
        {
            get { return _NoRefunds; }
            set
            {
                if (_NoRefunds != value)
                {
                    _NoRefunds = value;
                    OnPropertyChanged("NoRefunds");
                }
            }
        }
        private string _Cancellation;
        public string Cancellation
        {
            get { return _Cancellation; }
            set
            {
                if (_Cancellation != value)
                {
                    _Cancellation = value;
                    OnPropertyChanged("Cancellation");
                }
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// TODO : To define the Patient Subscription Info....
        /// </summary>
        /// <returns></returns>
        public async Task GetPatientSubscriptionInfo()
        {
            // Get App settings api..
            try
            { 
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {
                    UserDialog.ShowLoading();
                    await Task.Run(async () =>
                    {
                        Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                        {
                            UserInfo userInfo = await DataUtility.GetUserInfo(SettingsValues.ApiURLValue, Userid, true, Token).ConfigureAwait(false);
                            info = await DataUtility.PatientGetSubscriptionInfoAsync(SettingsValues.ApiURLValue, userInfo.PatientID, Token).ConfigureAwait(false);
                            if (info != null)
                            {
                                Subscription currentSubscription = SubscriptionsFactory.Get(info);
                                if (currentSubscription is null) //inactive
                                {
                                    if (userInfo.CanceledForPaymentIssues)
                                    {
                                        lytInactive = false;
                                        lytInactiveNonPayment = true;
                                        lytChangePlan = true;
                                        btnChangePlan = false;
                                        lytChoosePlan = false;
                                        // lytPaymentIssuesInfo = true;
                                    }
                                    else
                                    {
                                        lytInactive = true;
                                        lytInactive_Inactive = true;
                                        lytInactiveNonPayment = false;
                                        lytChangePlan = false;
                                        lytChoosePlan = true;
                                        //lytPaymentIssuesInfo = true;
                                    }

                                    lytActive = false;
                                    lytCancellationInfo = false;
                                    // //lytPaymentIssuesInfo =false;
                                }
                                else
                                {
                                    System.Threading.Tasks.Task.Run(async () =>
                                    {
                                        //await GetPatientSubscriptionInfo().ConfigureAwait(false);
                                        UITopic ChangePlanBanner = await Globals.Instance.GetUTText("ChangePlanBanner", "en");
                                        if (ChangePlanBanner != null)
                                        {
                                            //Paragraphs
                                            string newPlanName = ChangePlanBanner.UITextList.Find(i => i.TagName == "NewPlanName").Text;
                                            string newPlanCost = ChangePlanBanner.UITextList.Find(i => i.TagName == "NewPlanCost").Text;
                                            string newPlanStart = ChangePlanBanner.UITextList.Find(i => i.TagName == "NewPlanStart").Text;


                                            string formattedNewPlanName;
                                            string formattedNewPlanCost;
                                            string formattedNewPlanStart;
                                            if (userInfo.CurrentSubscriptionPlan == "Family Subscription" && (userInfo.NewSubscriptionPlan == "Individual Subscription" || userInfo.NewSubscriptionPlan == "Individual 365 Plan"))
                                            {
                                                formattedNewPlanName = String.Format(newPlanName, info.NewSubscriptionPlan);
                                                formattedNewPlanCost = String.Format(newPlanCost, info.NewSubscriptionPlanCost);
                                                formattedNewPlanStart = String.Format(newPlanStart, userInfo.NewSubscriptionStartDate);
                                            }
                                            else
                                            {
                                                formattedNewPlanName = String.Format(newPlanName, info.CurrentSubscriptionPlan);
                                                formattedNewPlanCost = String.Format(newPlanCost, info.CurrentSubscriptionPlanCost);
                                                formattedNewPlanStart = String.Format(newPlanStart, userInfo.NewSubscriptionStartDate);
                                            }

                                            string formattedNewPlanNC = formattedNewPlanName + " " + formattedNewPlanCost;
                                            formattedNewPlanStart = formattedNewPlanStart + ".";
                                            //RunOnUiThread(() =>
                                            //{
                                            //your code here
                                            ChangeInfo = formattedNewPlanNC;
                                            ChangeInfo1 = formattedNewPlanStart;
                                        }

                                    });
                                    lytInactive = false;
                                    lytActive = true;
                                    lytChangePlan = true;
                                    lytChoosePlan = false;
                                    lytInactiveNonPayment = false;

                                    bool showSubscriptionChangeBanner = false;
                                    if (userInfo != null)
                                    {
                                        showSubscriptionChangeBanner = userInfo.ShowSubscriptionChangeBanner && string.IsNullOrEmpty(info.CurrentSubscriptionEndDate);
                                    }

                                    //  Helpers.Banner banner = showSubscriptionChangeBanner ? Helpers.Banner.ChangeIndToFam : Helpers.BannerHelper.BannerDisplay(info, currentSubscription, userInfo);

                                    //switch (banner)
                                    //{
                                    //    case Helpers.Banner.Cancel:
                                    //        lblCancellationInfo = lblCancellationInfo.Replace("@date@", info.CurrentSubscriptionEndDate);
                                    //        lblCancellationInfo.MovementMethod = Android.Method.LinkMovementMethod.Instance;
                                    //        lblCancellationInfo.SetText(GetCallSpannableString(lblCancellationInfo), TextView.BufferType.Spannable);
                                    //        lytCancellationInfo = true;
                                    //        lytChoosePlan = true;
                                    //        lytChangePlan =false;
                                    //         //lytPaymentIssuesInfo =false;

                                    //        lytActive =false;

                                    //        if (userInfo.CanceledForPaymentIssues || userInfo.RegistrationFailed)
                                    //        {
                                    //            lytInactive =false;
                                    //            lytInactiveNonPayment = true;
                                    //            lytChangePlan = true;
                                    //            btnChangePlan = false;
                                    //            lytChoosePlan =false;
                                    //            lytPaymentIssuesInfo = true;
                                    //        }
                                    //        else
                                    //        {
                                    //            lytInactive = true;
                                    //            lytInactiveNonPayment =false;
                                    //            lytChangePlan =false;
                                    //            lytChoosePlan = true;
                                    //             //lytPaymentIssuesInfo =false;
                                    //            lytInactive_ActiveUntil = true;
                                    //            lblActiveUntil = $" until {info.CurrentSubscriptionEndDate}\u0020";
                                    //        }

                                    //        break;
                                    //    case Helpers.Banner.Change:
                                    //        lblChangeInfo = true;
                                    //        //lblChangeInfo = lblChangeInfo.Replace("@date@", info.NewSubscriptionStartDate).Replace("@cost@", info.NewSubscriptionTotalCost);
                                    //        lblChangeInfo1 = true;
                                    //        lblChangeInfo2.MovementMethod = Android.Method.LinkMovementMethod.Instance;
                                    //        lblChangeInfo2.SetText(GetCallSpannableString(lblChangeInfo2), TextView.BufferType.Spannable);
                                    //        lytPaymentIssuesInfo = true;
                                    //        lytCancellationInfo =false;
                                    //        if (info.NewSubscriptionPlan != null) { btnChangePlan = false; }
                                    //        break;
                                    //    case Helpers.Banner.ChangeIndToFam:
                                    //        lblChangeInfo = true;
                                    //        lblChangeInfo = lblChangeInfo.Replace("@date@", userInfo.NewSubscriptionStartDate).Replace("@cost@", info.CurrentSubscriptionTotalCost);
                                    //        lblChangeInfo1 = true;
                                    //        lblChangeInfo2.MovementMethod = Android.Method.LinkMovementMethod.Instance;
                                    //        lblChangeInfo2.SetText(GetCallSpannableString(lblChangeInfo2), TextView.BufferType.Spannable);
                                    //        lytPaymentIssuesInfo = true;
                                    //        lytCancellationInfo =false;
                                    //        break;
                                    //    default:
                                    //         //lytPaymentIssuesInfo =false;
                                    //        lytCancellationInfo =false;
                                    //        break;
                                    //}



                                    //Check if plan has promo code
                                    if (info.GrantedPromotionID == null)
                                    {
                                        hasPromoCode = false;
                                    }
                                    else
                                    {
                                        hasPromoCode = true;
                                    }

                                    //Promo Code section
                                    if (hasPromoCode)//(currentSubscription is Individual365Subscription || currentSubscription is Family365Subscription)
                                    {
                                        lytPromoCode = true;
                                        lytPlanCost = true; ;
                                        lytDiscountApplied = true;

                                        txtDiscountApplied = info.CurrentVInvoice.Main.DiscountNominalDescrip.Replace("$", "$-");

                                        txtPromoCodeUsed = info.GrantedPromotionCode;
                                        txtPlanCost = info.CurrentSubscriptionPlanCost;
                                    }
                                    txtTotalUsed = info.CurrentSubscriptionTotalDue;


                                    if (userInfo.IsOnetimePlan)
                                    {
                                        lytExpDate = true;
                                        txtExpDate = info.CurrentSubscriptionEndDate;
                                    }

                                    //what condition?//
                                    //txtExpDate UI

                                    System.Threading.Tasks.Task.Run(async () =>
                                    {
                                        UITopic UpdManageSubscriptions = await Globals.Instance.GetUTText("UpdManageSubscriptions", "en");//UITopic subscrLimits = await Globals.Instance.GetUTText("SubscrLimits", "en");
                                        string subscrRenewedMonthly = UpdManageSubscriptions.UITextList.Find(i => i.TagName == "SubscrRenewedMonthly").Text;
                                        string paymentMethod = UpdManageSubscriptions.UITextList.Find(i => i.TagName == "PaymentMethod").Text;
                                        //RunOnUiThread(() => {
                                        //txtManageSubscription = subscrRenewedMonthly;
                                        //txtManageSubscription2 = paymentMethod;
                                        //  });

                                    });
                                    //
                                    //lblCurrentSubscription = info.CurrentSubscriptionPlan;
                                    //lblPlanDescription = info.CurrentSubscriptionPlanDescription;

                                    if (info.IsPrepay)
                                    {
                                        lblPlanDescription = info.CurrentSubscriptionPlan.Replace("_Brand_name_", AppName);

                                        //  lblPlanDescription = Application.Context.Resources.GetString(Application.Context.Resources.GetIdentifier("managesubcription_prepaidplan", "string", Application.Context.PackageName)).Replace("_insurance_employer_plan_", info.CurrentSubscriptionPlan).Replace("_Brand_name_", AppName);
                                        lblCurrentSubscription = info.CurrentSubscriptionPlan;
                                    }
                                    else
                                    {
                                        lblPlanDescription = info.CurrentSubscriptionPlanDescription;
                                        // if (!string.IsNullOrEmpty(info.CurrentSubscriptionPlanDescription)) lblPlanDescriptionFormatted = Html.FromHtml("<span>" + info.CurrentSubscriptionPlanDescription.Replace("\\n", "\n") + "</span>");
                                        lblCurrentSubscription = info.CurrentSubscriptionPlan;
                                    }

                                    lytPayment = info.IsPrepay ? false : true;
                                    //lytPaymentIssuesInfo =false;
                                }
                            }
                        });

                    }).ConfigureAwait(false);
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    await App.Current.MainPage.DisplayAlert("", "No Network Connection found, Please Connect to Internet first.", "OK");
                }
                UserDialog.HideLoading();
            }
            catch (Exception ex)
            {
                UserDialog.HideLoading();
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// To Do: To define Download Order command
        /// </summary>
        /// <param name="obj"></param> 
        private async void DownloadOrderSummaryAsync(object obj)
        {
            try
            {
                byte[] order = await DataUtility._GetLastOrderSummaryAsync(SettingsValues.ApiURLValue, userInfo.PatientID, Token).ConfigureAwait(false);

                //if (order != null)
                //{
                //    await OpenFile(order).ConfigureAwait(false);
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
            } 
        }

        /// <summary>
        /// To Do: To define Back command
        /// </summary>
        /// <param name="obj"></param> 
        private async void BackAsync(object obj)
        {
            try
            {
                await Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        /// <summary>
        /// To Do: To define Change Plan command
        /// </summary>
        /// <param name="obj"></param>

        private async void ChangePlanAsync(object obj)
        {
            try
            {
                var isPlanChange = true;
                await Navigation.PushModalAsync(new Views.MyAccount.ChangePlanPage(info, isPlanChange), false);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        /// <summary>
        /// To Do: To define Update Payment command
        /// </summary>
        /// <param name="obj"></param>

        private async void UpdatePaymentAsync(object obj)
        {
            try
            {
                await Navigation.PushModalAsync(new Views.MyAccount.UpdateCardInformationPage(), false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        } 
        public async Task GetBillingPoliciesInfo()
        {
            // Get App settings api..
            try
            {
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {
                    UserDialog.ShowLoading();
                    await Task.Run(async () =>
                    {
                        Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                        {
                            UITopic TermsOfUse = await Globals.Instance.GetUTText("TermsOfUse", "en");
                            if (TermsOfUse != null)
                            {
                                try
                                {
                                    //Headers
                                    string billingPolicies = TermsOfUse.UITextList.Find(i => i.TagName == "BillingPolicies").Text;
                                    string updatingCreditCard = TermsOfUse.UITextList.Find(i => i.TagName == "UpdatingCreditCard").Text;
                                    string cancellation = TermsOfUse.UITextList.Find(i => i.TagName == "Cancellation").Text;
                                    string subscriptionPlanChanges = TermsOfUse.UITextList.Find(i => i.TagName == "SubscriptionPlanChanges").Text;
                                    string monthlySubscriptionPlan = TermsOfUse.UITextList.Find(i => i.TagName == "MonthlySubscriptionPlanBillingSchedule").Text;

                                    string tablePurchaseDate = TermsOfUse.UITextList.Find(i => i.TagName == "Table_PurchaseDate").Text;
                                    string tableAutoRenewalDate = TermsOfUse.UITextList.Find(i => i.TagName == "Table_AutoRenewalDate").Text;
                                    //
                                    //Paragraphs
                                    string updateCC = TermsOfUse.UITextList.Find(i => i.TagName == "CreditCardInfo").Text;
                                    string noRefunds = TermsOfUse.UITextList.Find(i => i.TagName == "CancelPolicies").Text;
                                    string changePlanInfo = TermsOfUse.UITextList.Find(i => i.TagName == "ChangePlanInfo").Text;
                                    string monthlySubscriptionPlanP1 = TermsOfUse.UITextList.Find(i => i.TagName == "MonthlySubscriptionPlanBillingSchedule_P1").Text;
                                    string monthlySubscriptionPlanP1B1 = TermsOfUse.UITextList.Find(i => i.TagName == "MonthlySubscriptionPlanBillingSchedule_P1_B1").Text;
                                    string monthlySubscriptionPlanP1B2 = TermsOfUse.UITextList.Find(i => i.TagName == "MonthlySubscriptionPlanBillingSchedule_P1_B2").Text;
                                    string table_1st_28_Col1 = TermsOfUse.UITextList.Find(i => i.TagName == "Table_1st_28_Col1").Text;
                                    string table_29th_Col1 = TermsOfUse.UITextList.Find(i => i.TagName == "Table_29th_Col1").Text;
                                    string table_30th_Col1 = TermsOfUse.UITextList.Find(i => i.TagName == "Table_30th_Col1").Text;
                                    string table_31st_Col1 = TermsOfUse.UITextList.Find(i => i.TagName == "Table_31st_Col1").Text;

                                    string table_1st_28_Col2 = TermsOfUse.UITextList.Find(i => i.TagName == "Table_1st_28_Col2").Text.Replace("{break}", "\n\n");
                                    string table_29th_Col2 = TermsOfUse.UITextList.Find(i => i.TagName == "Table_29th_Col2").Text.Replace("{break}", "\n\n");
                                    string table_30th_Col2 = TermsOfUse.UITextList.Find(i => i.TagName == "Table_30th_Col2").Text.Replace("{break}", "\n\n");
                                    string table_31st_Col2 = TermsOfUse.UITextList.Find(i => i.TagName == "Table_31st_Col2").Text.Replace("{break}", "\n\n");

                                    if (billingPolicies != null)
                                    {
                                        BillingPoliciesName = billingPolicies;
                                    }
                                    if (monthlySubscriptionPlan != null)
                                    {
                                        MonthlySubscriptionPlan = monthlySubscriptionPlan;
                                    }
                                    if (monthlySubscriptionPlanP1 != null)
                                    {
                                        MonthlySubscriptionPlanP1 = monthlySubscriptionPlanP1;
                                    }
                                    if (monthlySubscriptionPlanP1B1 != null)
                                    {
                                        MonthlySubscriptionPlanP1B1 = "• " + monthlySubscriptionPlanP1B1;
                                    }
                                    if (monthlySubscriptionPlanP1B2 != null)
                                    {
                                        MonthlySubscriptionPlanP1B2 = "• " + monthlySubscriptionPlanP1B2;
                                    }
                                    if (tablePurchaseDate != null)
                                    {
                                        TablePurchaseDate = tablePurchaseDate;
                                    }
                                    if (tableAutoRenewalDate != null)
                                    {
                                        TableAutoRenewalDate = tableAutoRenewalDate;
                                    }
                                    if (table_1st_28_Col1 != null)
                                    {
                                        Table_1st_28_Col1 = table_1st_28_Col1;
                                    }
                                    if (table_1st_28_Col2 != null)
                                    {
                                        Table_1st_28_Col2 = table_1st_28_Col2;
                                    }
                                    if (table_29th_Col1 != null)
                                    {
                                        Table_29th_Col1 = table_29th_Col1;
                                    }
                                    if (table_29th_Col2 != null)
                                    {
                                        Table_29th_Col2 = table_29th_Col2;
                                    }
                                    if (table_30th_Col1 != null)
                                    {
                                        Table_30th_Col1 = table_30th_Col1;
                                    }
                                    if (table_30th_Col2 != null)
                                    {
                                        Table_30th_Col2 = table_30th_Col2;
                                    }
                                    if (table_31st_Col1 != null)
                                    {
                                        Table_31th_Col1 = table_31st_Col1;
                                    }
                                    if (table_31st_Col2 != null)
                                    {
                                        Table_31th_Col2 = table_31st_Col2;
                                    }
                                    if (updatingCreditCard != null)
                                    {
                                        UpdatingCreditCardName = updatingCreditCard;
                                    }
                                    if (updateCC != null)
                                    {
                                        UpdatingCreditCard = updateCC;
                                    }
                                    if (subscriptionPlanChanges != null)
                                    {
                                        ChangePlanInfo = subscriptionPlanChanges;
                                    }
                                    if (changePlanInfo != null)
                                    {
                                        SubscriptionPlanChanges = changePlanInfo;
                                    }
                                    if (cancellation != null)
                                    {
                                        NoRefunds = cancellation;
                                    }
                                    string myNoRefunds = String.Format(noRefunds, AppName);
                                    if (myNoRefunds != null)
                                    {
                                        Cancellation = myNoRefunds;
                                    }
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                        });
                    }).ConfigureAwait(false);
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    await App.Current.MainPage.DisplayAlert("", "No Network Connection found, Please Connect to Internet first.", "OK");
                }
                UserDialog.HideLoading();
            }
            catch (Exception ex)
            {
                UserDialog.HideLoading();
                Console.WriteLine(ex);
            }
        }
        #endregion
    }
}
