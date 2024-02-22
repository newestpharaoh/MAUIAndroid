using Acr.UserDialogs;
using CommonLibraryCoreMaui;
using CommonLibraryCoreMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;

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
        bool hasPromoCode;
        public ManageSubscriptionPageViewModel(INavigation nav)
        {
            Navigation = nav;
            BackCommand = new Command(BackAsync);
            ChangePlanCommand = new Command(ChangePlanAsync);
            UpdatePaymentCommand = new Command(UpdatePaymentAsync);
            DownloadOrderSummary = new Command(DownloadOrderSummaryAsync);
            InfoCommand = new Command(InfoAsync);


            Token = Preferences.Get("AuthToken", string.Empty);
            Userid = Preferences.Get("UserId", 0);
        }

        #endregion

        #region Command
        public Command BackCommand { get; set; }
        public Command ChangePlanCommand { get; set; }
        public Command UpdatePaymentCommand { get; set; }
        public Command DownloadOrderSummary { get; set; }
        public Command InfoCommand { get; set; }
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

        #region Methods

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
                            AccountSubscriptionInfo info = await DataUtility.PatientGetSubscriptionInfoAsync(SettingsValues.ApiURLValue, userInfo.PatientID, Token).ConfigureAwait(false);
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
                await Navigation.PushModalAsync(new Views.MyAccount.ChangePlanPage(), false);

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

        /// <summary>
        /// To Do: To define Info command
        /// </summary>
        /// <param name="obj"></param>

        private async void InfoAsync(object obj)
        {
            try
            {
                await Navigation.PushModalAsync(new Views.Popup.InfoPopupPage(), false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #endregion
    }
}
