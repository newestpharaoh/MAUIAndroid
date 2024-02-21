using Acr.UserDialogs;
using CommonLibraryCoreMaui;
using CommonLibraryCoreMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.MyAccount
{
    public class BilingPoliciesPageViewModel : BaseViewModel
    {
        public string AppName = Preferences.Get("AppName", string.Empty);
        #region Constructor
        public BilingPoliciesPageViewModel(INavigation nav)
        {
            Navigation = nav;
            BackCommand = new Command(BackAsync);
        }
        #endregion

        #region Command
        public Command BackCommand { get; set; }
        #endregion

        #region Properties
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

                                BillingPoliciesName = billingPolicies;
                                MonthlySubscriptionPlan = monthlySubscriptionPlan;
                                MonthlySubscriptionPlanP1 = monthlySubscriptionPlanP1;
                                MonthlySubscriptionPlanP1B1 = "• " + monthlySubscriptionPlanP1B1;
                                MonthlySubscriptionPlanP1B2 = "• " + monthlySubscriptionPlanP1B2;
                                TablePurchaseDate = tablePurchaseDate;
                                TableAutoRenewalDate = tableAutoRenewalDate;
                                Table_1st_28_Col1 = table_1st_28_Col1;
                                Table_1st_28_Col2 = table_1st_28_Col2;
                                Table_29th_Col1 = table_29th_Col1;
                                Table_29th_Col2 = table_29th_Col2;
                                Table_30th_Col1 = table_30th_Col1;
                                Table_30th_Col2 = table_30th_Col2;
                                Table_31th_Col1 = table_31st_Col1;
                                Table_31th_Col2 = table_31st_Col2;
                                UpdatingCreditCardName = updatingCreditCard;
                                UpdatingCreditCard = updateCC;
                                ChangePlanInfo = subscriptionPlanChanges;
                                SubscriptionPlanChanges = changePlanInfo;
                                NoRefunds = cancellation;
                                string myNoRefunds = String.Format(noRefunds, AppName);
                                Cancellation = myNoRefunds;
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
        #endregion
    }
}
