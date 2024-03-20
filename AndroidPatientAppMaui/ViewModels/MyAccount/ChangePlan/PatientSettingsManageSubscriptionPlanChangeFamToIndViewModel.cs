using Acr.UserDialogs;
using CommonLibraryCoreMaui;
using CommonLibraryCoreMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.MyAccount.ChangePlan
{
    public class PatientSettingsManageSubscriptionPlanChangeFamToIndViewModel : BaseViewModel
    {
        //To define the class level variable
        public SubscriptionChangeInfo ChangeInfo;
        int PatientID = 0;
        public Subscription selectedSubscription;
       // int OptionID = 0;
        string User = string.Empty;
        string Token = string.Empty;
        Subscription subscription;

        #region Constructor
        public PatientSettingsManageSubscriptionPlanChangeFamToIndViewModel(INavigation nav)
        {
            try
            {
                Navigation = nav;
                BackCommand = new Command(BackAsync);
                CancelCommand = new Command(CancelAsync);
                ContinueCommand = new Command(ContinueAsync);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #endregion

        #region Command
        public Command BackCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command ContinueCommand { get; set; }

        #endregion

        #region Properties  

        private string _lblNewTotal;
        public string lblNewTotal
        {
            get { return _lblNewTotal; }
            set
            {
                if (_lblNewTotal != value)
                {
                    _lblNewTotal = value;
                    OnPropertyChanged("lblNewTotal");
                }
            }
        }

        private string _lblContinue;
        public string lblContinue
        {
            get { return _lblContinue; }
            set
            {
                if (_lblContinue != value)
                {
                    _lblContinue = value;
                    OnPropertyChanged("lblContinue");
                }
            }
        }

        private string _lblNewSubscriptionTotalEffectiveDate;
        public string lblNewSubscriptionTotalEffectiveDate
        {
            get { return _lblNewSubscriptionTotalEffectiveDate; }
            set
            {
                if (_lblNewSubscriptionTotalEffectiveDate != value)
                {
                    _lblNewSubscriptionTotalEffectiveDate = value;
                    OnPropertyChanged("lblNewSubscriptionTotalEffectiveDate");
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

        /// <summary>
        /// To Do: To define Cancel command
        /// </summary>
        /// <param name="obj"></param> 
        private async void CancelAsync(object obj)
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
        /// To Do: To define Continue command
        /// </summary>
        /// <param name="obj"></param> 
        private async void ContinueAsync(object obj)
        {
            try
            {
               // await Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        public async Task GetChangeMembers()
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
                        {   //refresh all the time
                            ChangeInfo = await DataUtility.PatientGetChangeSubscriptionInfoAsync(SettingsValues.ApiURLValue, PatientID, selectedSubscription.OptionID, Token).ConfigureAwait(false);
                            UITopic UpdManageSubscriptions = await Globals.Instance.GetUTText("UpdManageSubscriptions", "en");
                            UITopic EnterPaymentInformation = await Globals.Instance.GetUTText("EnterPaymentInformation", "en");
                            if (UpdManageSubscriptions != null)
                            {
                                if (ChangeInfo != null)
                                {
                                    ////First paragraph
                                    string changePlan = UpdManageSubscriptions.UITextList.Find(i => i.TagName == "ChangePlan").Text; //Replace {0} with old plan name, {1} with the new plan name.  ** indicates text that should be bold
                                    changePlan = changePlan.Replace("**", "");
                                    string formattedchangePlan = String.Format(changePlan, ChangeInfo.CurrentSubscriptionPlanName, ChangeInfo.SubscriptionPlanName);
                                    //Second paragraph
                                    string effectiveDate = UpdManageSubscriptions.UITextList.Find(i => i.TagName == "EffectiveDate").Text; //Replace {0} with start date.  ** indicates text that should be bold
                                    effectiveDate = effectiveDate.Replace("**", "");
                                    string formattedEffectiveDate = String.Format(effectiveDate, ChangeInfo.NextBillingDate);
                                    string fmDeactivate = UpdManageSubscriptions.UITextList.Find(i => i.TagName == "FamilyMembersDeactivate").Text;
                                    string concateDateDeactivate = formattedEffectiveDate + " " + fmDeactivate;
                                  //  SpannableString ssDeactivate = SpannableStringHelper.BoldString(concateDateDeactivate, ChangeInfo.NextBillingDate);

                                    //Third Paragraph
                                    string newTotal = UpdManageSubscriptions.UITextList.Find(i => i.TagName == "NewTotal").Text;
                                    string subscriptionCost = ChangeInfo.DueDifference;//resp.SubscriptionCost;
                                    string proRatedLabel = "";
                                    if (EnterPaymentInformation != null) { proRatedLabel = EnterPaymentInformation.UITextList.Find(i => i.TagName == "ProratedCost").Text; }
                                    string paymentDate = UpdManageSubscriptions.UITextList.Find(i => i.TagName == "PaymentDate").Text;
                                    string nextBillingDate = ChangeInfo.NextBillingDate;
                                    string recMonthly = UpdManageSubscriptions.UITextList.Find(i => i.TagName == "Recurring Monthly").Text;
                                    string concatPayDateMonthly = paymentDate + " " + nextBillingDate + " " + recMonthly;
                                    //  SpannableString ss = SpannableStringHelper.BoldString(concatPayDateMonthly, paymentDate + " " + nextBillingDate + " ");
                                    string proRatedAmt = string.Format("{0:C}", ChangeInfo.NewVInvoice.Main.CostInitial);//"$" + resp.NewVInvoice.Main.CostInitial;//.TotalDueInitial.ToString();
                                    string concatenatedProrate = proRatedLabel + " " + proRatedAmt;
                                    //  SpannableString ssProrated = SpannableStringHelper.BoldString(concatenatedProrate, proRatedLabel);

                                    ////Fourth Paragraph
                                    string pressContinue = UpdManageSubscriptions.UITextList.Find(i => i.TagName == "PressContinue").Text;
                                    lblNewTotal = newTotal + " " + subscriptionCost;
                                    lblContinue = pressContinue;
                                    //lblNewSubscriptionTotalEffectiveDate.SetText(ss, TextView.BufferType.Spannable);
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
        #endregion
    }
}