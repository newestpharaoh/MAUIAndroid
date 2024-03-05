using Acr.UserDialogs;
using Acr.UserDialogs.Infrastructure;
using CommonLibraryCoreMaui;
using CommonLibraryCoreMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.MyAccount
{
    public class ChangePlanPageViewModel : BaseViewModel
    {
       
        //To define the class level variable.
        string Token = string.Empty;
        public AccountSubscriptionInfo info;
        public Subscription currentSubscription;
        public Subscription selectedSubscription;
        UserInfo userInfo;
        public bool planChange;
        bool planChoose;
        #region Constructor
        public ChangePlanPageViewModel(INavigation nav)
        {
            try
            {

                Navigation = nav;
                BackCommand = new Command(BackAsync);
                ContinueCommand = new Command(ContinueCommandAsync);
                CancelPlanCommand = new Command(CancelPlanCommandAsnc);
                 
                Token = Preferences.Get("AuthToken", string.Empty); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }  
        #endregion

        #region Command
        public Command BackCommand { get; set; }
        public Command ContinueCommand { get; set; }
        public Command CancelPlanCommand { get; set; }
        #endregion

        #region Properties
        private string _lblHeader = "Your Subscription";
        public string lblHeader
        {
            get { return _lblHeader; }
            set
            {
                if (_lblHeader != value)
                {
                    _lblHeader = value;
                    OnPropertyChanged("lblHeader");
                }
            }
        }

        private bool _lytPrepay = false;
        public bool lytPrepay
        {
            get { return _lytPrepay; }
            set
            {
                if (_lytPrepay != value)
                {
                    _lytPrepay = value;
                    OnPropertyChanged("lytPrepay");
                }
            }
        }
        private bool _lblCancelPlan = false;
        public bool lblCancelPlan
        {
            get { return _lblCancelPlan; }
            set
            {
                if (_lblCancelPlan != value)
                {
                    _lblCancelPlan = value;
                    OnPropertyChanged("lblCancelPlan");
                }
            }
        }
        private bool _spnrPaymentPlans = true;
        public bool spnrPaymentPlans
        {
            get { return _spnrPaymentPlans; }
            set
            {
                if (_spnrPaymentPlans != value)
                {
                    _spnrPaymentPlans = value;
                    OnPropertyChanged("spnrPaymentPlans");
                }
            }
        }
        private List<Subscription> _PaymentPlansList;
        public List<Subscription> PaymentPlansList
        {
            get { return _PaymentPlansList; }
            set
            {
                if (_PaymentPlansList != value)
                {
                    _PaymentPlansList = value;
                    OnPropertyChanged("PaymentPlansList");
                }
            }
        }
        private string _PaymentPlansSelectedItems;
        public string PaymentPlansSelectedItems
        {
            get { return _PaymentPlansSelectedItems; }
            set
            {
                if (_PaymentPlansSelectedItems != value)
                {
                    _PaymentPlansSelectedItems = value;
                    OnPropertyChanged("PaymentPlansSelectedItems");
                }
            }
        }
        private string _promoCode = "Apply a Promotional code";
        public string promoCode
        {
            get { return _promoCode; }
            set
            {
                if (_promoCode != value)
                {
                    _promoCode = value;
                    OnPropertyChanged("promoCode");
                }
            }
        }
        private int _pickerPaymentPlans;
        public int pickerPaymentPlans
        {
            get { return _pickerPaymentPlans; }
            set
            {
                if (_pickerPaymentPlans != value)
                {
                    _pickerPaymentPlans = value;
                    OnPropertyChanged("pickerPaymentPlans");
                }
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// TODO : To Define GetChnagePlanInfo...
        /// </summary>
        /// <returns></returns>
        public async Task GetChangePlanInfo()
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
                            if (planChange) lblHeader = "Change Plan";
                            if (info != null) currentSubscription = SubscriptionsFactory.Get(info);

                            if (planChoose)
                            {
                                lblCancelPlan = false;
                            }
                            else
                            {
                                if (userInfo != null)
                                {
                                    lblCancelPlan = !userInfo.IsOnetimePlan;
                                }
                            }
                            if (info != null)
                            {
                                Subscription ps = SubscriptionsFactory.Get(info);

                                lytPrepay = info.IsPrepay ? true : false;

                                int pos = 0;
                                if (info.IsPrepay && ps != null)
                                {
                                    spnrPaymentPlans = false;
                                    PaymentPlansList = new List<Subscription>() { new IndividualSubscription() { Name = info.CurrentSubscriptionPlan } };
                                    PaymentPlansSelectedItems = PaymentPlansList[0].ToString(); // Select the only item
                                }
                                else
                                {
                                    spnrPaymentPlans = true; // Ensure it's enabled
                                    PaymentPlansList = info.AvailableSubscriptions.Cast<Subscription>().ToList();
                                    PaymentPlansSelectedItems = PaymentPlansList[0].ToString();
                                }

                                if (ps != null)
                                {
                                    Predicate<Subscription> subscriptionFinder = (Subscription p) => { return p.OptionID == ps.OptionID; };
                                    pos = PaymentPlansList.FindIndex(subscriptionFinder);
                                    if (pos == -1) pos = 0;
                                }


                                // Set the selection of the Picker
                                pickerPaymentPlans = pos;


                                //SpinnerAdapter<Subscription> adapter;
                                //int pos = 0;
                                //if (info.IsPrepay && ps != null)
                                //{
                                //    spnrPaymentPlans.Enabled = false;
                                //    adapter = new SpinnerAdapter<Subscription>(this, Application.Context.Resources.GetIdentifier("spnrPaymentPlans", "id", Application.Context.PackageName), new List<Subscription>() { new IndividualSubscription() { Name = info.CurrentSubscriptionPlan } }, Resources.DisplayMetrics);
                                //}
                                //else
                                //{
                                //    adapter = new SpinnerAdapter<Subscription>(this, Application.Context.Resources.GetIdentifier("spnrPaymentPlans", "id", Application.Context.PackageName), info.AvailableSubscriptions.Cast<Subscription>().ToList(), Resources.DisplayMetrics);
                                //}

                                //adapter.SetDropDownViewResource(Application.Context.Resources.GetIdentifier("spnrPaymentPlans", "id", Application.Context.PackageName));
                                //spnrPaymentPlans.Adapter = adapter;

                                //if (ps != null)
                                //{
                                //    Predicate<Subscription> subscriptionFinder = (Subscription p) => { return p.OptionID == ps.OptionID; };
                                //    pos = adapter.GetElementPosition(subscriptionFinder);
                                //    if (pos == -1) pos = 0;
                                //}

                                //spnrPaymentPlans.SetSelection(pos);
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
        /// TODO : To Define Continue Command...
        /// </summary>
        /// <returns></returns>
        private async void ContinueCommandAsync(object obj)
        {
            try
            {
                bool canProceed = false;
                if (selectedSubscription != null)
                {
                    StatusResponse respPromo = await DataUtility.ValidatePromoCodeAsync(SettingsValues.ApiURLValue, selectedSubscription.OptionID, promoCode);

                    bool promoIsValid = false;

                    if (!string.IsNullOrEmpty(promoCode))
                    {
                        if (selectedSubscription is IndividualSubscription ||
                            selectedSubscription is FamilySubscription ||
                            selectedSubscription is OneTimeSubscription)
                        {
                            UserDialog.Alert("Promo Code is only available for 365 plans.");
                            canProceed = false;
                        }
                        else
                        {
                            if (respPromo.StatusCode == StatusCode.SuccessSeePayload)
                            {
                                promoIsValid = true;
                                canProceed = true;
                            }
                            else if (respPromo.StatusCode == StatusCode.Error)
                            {
                                promoIsValid = false;
                                UserDialog.Alert(respPromo.Message);
                                canProceed = false;
                            }
                            else
                            {
                                promoIsValid = false;
                                UserDialog.Alert("Promo Code is invalid. Please remove or re-enter");
                                canProceed = false;
                            }
                        }
                    }
                    else
                    {
                        canProceed = true;
                    }

                    if (canProceed)
                    {
                        if (currentSubscription != null)
                        {
                            if (currentSubscription.OptionID != selectedSubscription.OptionID)
                            {
                                //Scenario 3: from family to Individual
                                if (selectedSubscription is IndividualSubscription && (currentSubscription is FamilySubscription || currentSubscription is Family365Subscription))
                                {
                                    //intent = new Intent(this, typeof(PatientSettingsManageSubscriptionPlanChangeFamToIndActivity));

                                }
                                //Scenario 4: fam to Ind365
                                else if (selectedSubscription is Individual365Subscription && currentSubscription is FamilySubscription)
                                {
                                    //intent = new Intent(this, typeof(PatientSettingsManageSubscriptionPlanChangeFamToInd365Activity));
                                    //intent.PutExtra("promoIsValid", promoIsValid);
                                    //intent.PutExtra("promoCode", promoCode.Text);
                                }
                                //Scenario 2
                                else if (
                                (selectedSubscription is IndividualSubscription && currentSubscription is OneTimeSubscription) ||
                                (selectedSubscription is FamilySubscription && currentSubscription is OneTimeSubscription) ||
                                (selectedSubscription is FamilySubscription && currentSubscription is Family365Subscription) ||
                                (selectedSubscription is FamilySubscription && currentSubscription is IndividualSubscription) ||
                                (selectedSubscription is FamilySubscription && currentSubscription is Individual365Subscription))
                                {
                                    //  intent = new Intent(this, typeof(PatientSettingsManageSubscriptionPlanChangeAnyToSubscrActivity));
                                }
                                //Scenario 1
                                else if (
                                (selectedSubscription is Individual365Subscription && currentSubscription is IndividualSubscription) ||
                                (selectedSubscription is Family365Subscription && currentSubscription is IndividualSubscription) ||
                                (selectedSubscription is Family365Subscription && currentSubscription is FamilySubscription) ||
                                (selectedSubscription is Family365Subscription && currentSubscription is Individual365Subscription) ||
                                (selectedSubscription is Individual365Subscription && currentSubscription is OneTimeSubscription) ||
                                (selectedSubscription is Family365Subscription && currentSubscription is OneTimeSubscription))
                                {
                                    //intent = new Intent(this, typeof(PatientSettingsManageSubscriptionPlanChangeAnyTo365Activity));
                                    //intent.PutExtra("promoIsValid", promoIsValid);
                                    //intent.PutExtra("promoCode", promoCode.Text);
                                }
                                //^^em1417 new verbiage/////////////////////////////////////////////////////////////////////////////////


                                else if (selectedSubscription is FamilySubscription && currentSubscription is IndividualSubscription ||
                                    selectedSubscription is FamilySubscription && currentSubscription is OneTimeSubscription)
                                {
                                    //from ind to fam - end of cycle - block changing
                                    // intent = new Intent(this, typeof(PatientSettingsManageSubscriptionPlanChangeIndToFamActivity));
                                }
                                else if (selectedSubscription is IndividualSubscription && currentSubscription is OneTimeSubscription)
                                {
                                    //starts immediately
                                    //from one time to ind
                                    // intent = new Intent(this, typeof(PatientSettingsManageSubscriptionPlanChangeOneTimeToIndActivity));
                                }


                                //OneTime to OneTime(365) /////////////


                                //72 hour to Individual365 or Family365
                                else if ((selectedSubscription is Family365Subscription || selectedSubscription is Individual365Subscription) && (currentSubscription is OneTimeSubscription))//else if (selectedSubscription is Family365Subscription && currentSubscription is Individual365Subscription)
                                {
                                    //  intent = new Intent(this, typeof(PatientSettingsManageSubscriptionPlanChangeOneTimeToOneTimeActivity));
                                }

                                //else if (selectedSubscription is Individual365Subscription && currentSubscription is OneTimeSubscription)
                                //{
                                //    intent = new Intent(this, typeof(PatientSettingsManageSubscriptionPlanChangeOneTimeToOneTimeActivity));
                                //}

                                //Individual365 to Family365
                                else if (selectedSubscription is Family365Subscription && currentSubscription is Individual365Subscription)
                                {
                                    // intent = new Intent(this, typeof(PatientSettingsManageSubscriptionPlanChangeOneTimeToOneTimeActivity));
                                }

                                ////////////////////////////////////




                                //Regular to 365////////////////////
                                else
                                {
                                    // intent = new Intent(this, typeof(PatientSettingsManageSubscriptionPlanChangeOneTimeToOneTimeActivity));
                                }
                            }
                        }
                        else
                        {
                            //Scenario 2 for inactives
                            if (selectedSubscription is IndividualSubscription || selectedSubscription is FamilySubscription)
                            {
                                //   intent = new Intent(this, typeof(PatientSettingsManageSubscriptionPlanChangeAnyToSubscrActivity));
                            }
                            //else if(selectedSubscription is FamilySubscription)
                            //{
                            //    intent = new Intent(this, typeof(PatientSettingsManageSubscriptionPlanChangeIndToFamActivity));
                            //}

                            //Scenario 1 for inactives
                            else if (selectedSubscription is Individual365Subscription || selectedSubscription is Family365Subscription || selectedSubscription is OneTimeSubscription)
                            {
                                //intent = new Intent(this, typeof(PatientSettingsManageSubscriptionPlanChangeAnyTo365Activity));
                                //intent.PutExtra("promoIsValid", promoIsValid);
                                //intent.PutExtra("promoCode", promoCode.Text);
                            }
                            else
                            {
                                //intent = new Intent(this, typeof(PatientSettingsManageSubscriptionPlanChangeOneTimeToOneTimeActivity));
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// TODO : To Define Cancel Plan Command...
        /// </summary>
        /// <param name="obj"></param>
        private async void CancelPlanCommandAsnc(object obj)
        {
            try
            {
                if (spnrPaymentPlans && userInfo != null)
                {
                    if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                    {
                        UserDialog.ShowLoading();
                        await Task.Run(async () =>
                        {
                            Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                            {
                                StatusResponse resp = await DataUtility.PatientGetCancelSubscriptionDateAsync(SettingsValues.ApiURLValue, userInfo.PatientID, Token).ConfigureAwait(false);
                                //if (resp != null)
                                //{
                                //    if (resp.StatusCode == StatusCode.SuccessSeePayload)
                                //    {
                                //        if (!string.IsNullOrEmpty(resp.Payload))
                                //        {
                                //            //string userInfo = await DataUtility.ClickCancelPlanHyperlink(SettingsValues.ApiURLValue, global.UserInfo.PatientID, Token, "en");

                                //            ReusableDialog dialog;
                                //            if (selectedSubscription is FamilySubscription)
                                //            {
                                //                dialog = ReusableDialog.Instance(this, typeof(Dialogs.CancelFamilyPlanDialogFragment), "Cancel Plan", "Cancel Plan", "Keep My Plan", resp.Payload);
                                //            }
                                //            else
                                //            {
                                //                dialog = ReusableDialog.Instance(this, typeof(Dialogs.CancelPlanDialogFragment), "Cancel Plan", "Cancel Plan", "Keep My Plan", resp.Payload);
                                //            }

                                //            StatusResponse respLnk = await DataUtility.ClickCancelPlanHyperlink(SettingsValues.ApiURLValue, global.UserInfo.PatientID, Token, "en");

                                //            dialog.PositiveButtonClicked += async (args) =>
                                //            {
                                //                StatusResponse cancel = await DataUtility.PatientCancelSubscriptionAsync(SettingsValues.ApiURLValue, global.UserInfo.PatientID, Token).ConfigureAwait(false);
                                //                if (cancel != null)
                                //                {
                                //                    switch (cancel.StatusCode)
                                //                    {
                                //                        case StatusCode.Success:
                                //                            RunOnUiThread(() =>
                                //                            {
                                //                                dialog.Dismiss();
                                //                                BackToManageSubscription();
                                //                            });
                                //                            break;
                                //                        case StatusCode.SubscriptionAlreadyDeactivated:
                                //                            break;
                                //                    }
                                //                }
                                //                else
                                //                {

                                //                }
                                //                StatusResponse respBtn = await DataUtility.ClickCancelPlanButton(SettingsValues.ApiURLValue, global.UserInfo.PatientID, Token, "en");

                                //            };
                                //            RunOnUiThread(() =>
                                //            {
                                //                dialog.Show(this.FragmentManager, "dialog");
                                //            });
                                //        }
                                //    }
                                //}
                            });
                        });
                }
            }
                else
                {
                    //ReusableDialog dialog = ReusableDialog.Instance(this, typeof(Dialogs.CancelPrepayPlanDialogFragment), "Cancel Plan", "Close");
                    //FragmentTransaction transcation = FragmentManager.BeginTransaction();
                    //dialog.Show(transcation, "dialog");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// ToDo: To define the back command
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
