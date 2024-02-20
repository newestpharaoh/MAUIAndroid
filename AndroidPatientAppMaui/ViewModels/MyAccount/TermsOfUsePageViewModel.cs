using Acr.UserDialogs;
using CommonLibraryCoreMaui;
using CommonLibraryCoreMaui.Models;
using FM.LiveSwitch.Sdp;
using Microsoft.Maui.Controls.PlatformConfiguration;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.MyAccount
{
   public class TermsOfUsePageViewModel : BaseViewModel
    {
        public string AppName = Preferences.Get("AppName", string.Empty);
        #region Constructor 
        public TermsOfUsePageViewModel(INavigation nav)
        {
            Navigation = nav;
            BackCommand = new Command(BackAsync);
        }
        #endregion

        #region Command
        public Command BackCommand { get; set; }
        #endregion

        #region Properties
        private string _TermsOfUseName;
        public string TermsOfUseName
        {
            get { return _TermsOfUseName; }
            set
            {
                if (_TermsOfUseName != value)
                {
                    _TermsOfUseName = value;
                    OnPropertyChanged("TermsOfUseName");
                }
            }
        }
        private string _PageTitle;
        public string PageTitle
        {
            get { return _PageTitle; }
            set
            {
                if (_PageTitle != value)
                {
                    _PageTitle = value;
                    OnPropertyChanged("PageTitle");
                }
            }
        }
        private string _PageAccess;
        public string PageAccess
        {
            get { return _PageAccess; }
            set
            {
                if (_PageAccess != value)
                {
                    _PageAccess = value;
                    OnPropertyChanged("PageAccess");
                }
            }
        }
        private string _Subscription;
        public string Subscription
        {
            get { return _Subscription; }
            set
            {
                if (_Subscription != value)
                {
                    _Subscription = value;
                    OnPropertyChanged("Subscription");
                }
            }
        } 
        private string _Account;
        public string Account
        {
            get { return _Account; }
            set
            {
                if (_Account != value)
                {
                    _Account = value;
                    OnPropertyChanged("Account");
                }
            }
        }
        private string _TexasResistent;
        public string TexasResistent
        {
            get { return _TexasResistent; }
            set
            {
                if (_TexasResistent != value)
                {
                    _TexasResistent = value;
                    OnPropertyChanged("TexasResistent");
                }
            }
        }
        private string _MembershipAccess;
        public string MembershipAccess
        {
            get { return _MembershipAccess; }
            set
            {
                if (_MembershipAccess != value)
                {
                    _MembershipAccess = value;
                    OnPropertyChanged("MembershipAccess");
                }
            }
        }
        private string _UseOfService;
        public string UseOfService
        {
            get { return _UseOfService; }
            set
            {
                if (_UseOfService != value)
                {
                    _UseOfService = value;
                    OnPropertyChanged("UseOfService");
                }
            }
        }
        private string _PaidAccess;
        public string PaidAccess
        {
            get { return _PaidAccess; }
            set
            {
                if (_PaidAccess != value)
                {
                    _PaidAccess = value;
                    OnPropertyChanged("PaidAccess");
                }
            }
        }
        private string _PurchaseInfo;
        public string PurchaseInfo
        {
            get { return _PurchaseInfo; }
            set
            {
                if (_PurchaseInfo != value)
                {
                    _PurchaseInfo = value;
                    OnPropertyChanged("PurchaseInfo");
                }
            }
        }
        private string _SeventyTwoHourPlan;
        public string SeventyTwoHourPlan
        {
            get { return _SeventyTwoHourPlan; }
            set
            {
                if (_SeventyTwoHourPlan != value)
                {
                    _SeventyTwoHourPlan = value;
                    OnPropertyChanged("SeventyTwoHourPlan");
                }
            }
        }
        private string _IndividualFamily365Plan;
        public string IndividualFamily365Plan
        {
            get { return _IndividualFamily365Plan; }
            set
            {
                if (_IndividualFamily365Plan != value)
                {
                    _IndividualFamily365Plan = value;
                    OnPropertyChanged("IndividualFamily365Plan");
                }
            }
        }
         private string _MonthlySubscrPlan;
        public string MonthlySubscrPlan
        {
            get { return _MonthlySubscrPlan; }
            set
            {
                if (_MonthlySubscrPlan != value)
                {
                    _MonthlySubscrPlan = value;
                    OnPropertyChanged("MonthlySubscrPlan");
                }
            }
        }
         private string _RefundsCancellations;
        public string RefundsCancellations
        {
            get { return _RefundsCancellations; }
            set
            {
                if (_RefundsCancellations != value)
                {
                    _RefundsCancellations = value;
                    OnPropertyChanged("RefundsCancellations");
                }
            }
        }
        private string _CancelPolicies;
        public string CancelPolicies
        {
            get { return _CancelPolicies; }
            set
            {
                if (_CancelPolicies != value)
                {
                    _CancelPolicies = value;
                    OnPropertyChanged("CancelPolicies");
                }
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// To Do: To define back command
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

        public async Task GetTermsOfUseInfo()
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
                                //Headers
                                string termsOfUse = TermsOfUse.UITextList.Find(i => i.TagName == "TermsOfUse").Text;
                                string welcome = TermsOfUse.UITextList.Find(i => i.TagName == "Welcome").Text;
                                string subscr = TermsOfUse.UITextList.Find(i => i.TagName == "Subscr").Text;
                                string account = TermsOfUse.UITextList.Find(i => i.TagName == "Account").Text;
                                string membershipAccess = TermsOfUse.UITextList.Find(i => i.TagName == "MembershipAccess").Text;
                                string paidAccess = TermsOfUse.UITextList.Find(i => i.TagName == "PaidAccess").Text;
                                string refundsCancellations = TermsOfUse.UITextList.Find(i => i.TagName == "RefundsCancellations").Text;
                                //Paragraphs
                                string arcInfo = TermsOfUse.UITextList.Find(i => i.TagName == "ARCInfo").Text;
                                string texasResident = TermsOfUse.UITextList.Find(i => i.TagName == "TexasResident").Text;
                                string useOfService = TermsOfUse.UITextList.Find(i => i.TagName == "UseOfService").Text;
                                string purchaseInfo = TermsOfUse.UITextList.Find(i => i.TagName == "PurchaseInfo").Text;
                                string cancelPolicies = TermsOfUse.UITextList.Find(i => i.TagName == "NoRefunds").Text;

                                string myWelcome = String.Format(welcome, AppName);
                                string myARCInfo = String.Format(arcInfo, AppName);
                                string myTexasResident = String.Format(texasResident, AppName.Replace(" ", ""));
                                string myCancelPolicies = String.Format(cancelPolicies, AppName);
                                string myUseOfService = useOfService.Replace("{break}", "\n\n");
                                string myPurchaseInfo = purchaseInfo.Replace("{break}", "\n\n");

                                string seventyTwoHourPlan = TermsOfUse.UITextList.Find(i => i.TagName == "72HourPlan").Text; ;
                                string individualFamily365Plan = TermsOfUse.UITextList.Find(i => i.TagName == "IndivFamily365Plan").Text;
                                string monthlySubscrPlan = TermsOfUse.UITextList.Find(i => i.TagName == "MonthlySubscrPlan").Text;

                                TermsOfUseName = termsOfUse;
                                PageTitle = myWelcome;
                                PageAccess = myARCInfo;
                                Subscription = subscr;
                                Account = account;
                                TexasResistent = myTexasResident;
                                MembershipAccess = membershipAccess;
                                UseOfService = myUseOfService;
                                PaidAccess = paidAccess;
                                PurchaseInfo = myPurchaseInfo;
                                SeventyTwoHourPlan = "     •  " + seventyTwoHourPlan;
                                IndividualFamily365Plan = "     •  " + individualFamily365Plan;
                                MonthlySubscrPlan = monthlySubscrPlan.Replace("{break}", "\n\n")
                                             .Replace("{color:#14B38A}", "")
                                             .Replace("{color}", "");

                                RefundsCancellations = refundsCancellations;
                                CancelPolicies = myCancelPolicies;
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
        #endregion
    }
}
