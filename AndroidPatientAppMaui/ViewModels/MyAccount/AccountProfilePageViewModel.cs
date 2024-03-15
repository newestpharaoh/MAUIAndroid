using Acr.UserDialogs;
using AndroidPatientAppMaui.Views.MyAccount;
using CommonLibraryCoreMaui;
using CommonLibraryCoreMaui.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.MyAccount
{
    public class AccountProfilePageViewModel : BaseViewModel
    {  //To define the class level variable.
       public AccountMember am;
        public AccountSubscriptionInfo info;
        AccountAddFamilyMemberInfo aafmi;
        string familyAccountNoticeString;
        string allInfoViewableString;
        int PatientID = 0;
        string User = string.Empty;
        string Token = string.Empty;

        #region Constructor
        public AccountProfilePageViewModel(INavigation nav)
        {
            try
            {
                Navigation = nav;
                BackCommand = new Command(BackAsync);
                UpdateAccountAccessCommand = new Command(UpdateAccountAccessAsync);
                UpdateMedicalInformationCommand = new Command(UpdateMedicalInformationAsync);
                UpdateDemographicsCommand = new Command(UpdateDemographicsAsync);
                lytAddFamilyMemberCommond = new Command(lytAddFamilyMemberAsync);

                Token = Preferences.Get("AuthToken", string.Empty);
                PatientID = Preferences.Get("PatientID", 0);
                User = Preferences.Get("UserName",string.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #endregion

        #region Command
        public Command BackCommand { get; set; }
        public Command UpdateAccountAccessCommand { get; set; }
        public Command UpdateMedicalInformationCommand { get; set; }
        public Command UpdateDemographicsCommand { get; set; }
        public Command lytAddFamilyMemberCommond { get; set; }
        #endregion

        #region Properties  

        private ObservableCollection<AccountMember> _AccountHolderList = new ObservableCollection<AccountMember>();
        public ObservableCollection<AccountMember> AccountHolderList
        {
            get { return _AccountHolderList; }
            set
            {
                if (_AccountHolderList != value)
                {
                    _AccountHolderList = value;
                    OnPropertyChanged("AccountHolderList");
                }
            }
        }
        private string _UserName = Helpers.AppGlobalConstants.userInfo.Name;
        public string UserName
        {
            get { return _UserName; }
            set
            {
                if (_UserName != value)
                {
                    _UserName = value;
                    OnPropertyChanged("UserName");
                }
            }
        }
        private string _lblPlan;
        public string lblPlan
        {
            get { return _lblPlan; }
            set
            {
                if (_lblPlan != value)
                {
                    _lblPlan = value;
                    OnPropertyChanged("lblPlan");
                }
            }
        }
        private string _familyAcctNoticeTV = "Family Account Notice";
        public string familyAcctNoticeTV
        {
            get { return _familyAcctNoticeTV; }
            set
            {
                if (_familyAcctNoticeTV != value)
                {
                    _familyAcctNoticeTV = value;
                    OnPropertyChanged("familyAcctNoticeTV");
                }
            }
        }
        private string _allInfoViewableTV = "All medical information on family accounts is viewable by other family members. Members may create a private personal account within a family subscription plan by clicking the \"Family\" icon below and selection \"Make User Private\".";
        public string allInfoViewableTV
        {
            get { return _allInfoViewableTV; }
            set
            {
                if (_allInfoViewableTV != value)
                {
                    _allInfoViewableTV = value;
                    OnPropertyChanged("allInfoViewableTV");
                }
            }
        }
        private bool _familyAcctNotice = false;
        public bool familyAcctNotice
        {
            get { return _familyAcctNotice; }
            set
            {
                if (_familyAcctNotice != value)
                {
                    _familyAcctNotice = value;
                    OnPropertyChanged("familyAcctNotice");
                }
            }
        }
        private string _lblAdditionalMembersInfo = "You have room for @membersCount@ additional family @member_s@  with your current plan.";
        public string lblAdditionalMembersInfo
        {
            get { return _lblAdditionalMembersInfo; }
            set
            {
                if (_lblAdditionalMembersInfo != value)
                {
                    _lblAdditionalMembersInfo = value;
                    OnPropertyChanged("lblAdditionalMembersInfo");
                }
            }
        }
        private bool _lytAdditionalMembersInfo = false;
        public bool lytAdditionalMembersInfo
        {
            get { return _lytAdditionalMembersInfo; }
            set
            {
                if (_lytAdditionalMembersInfo != value)
                {
                    _lytAdditionalMembersInfo = value;
                    OnPropertyChanged("lytAdditionalMembersInfo");
                }
            }
        }
         private bool _IsExpanderVisible = false;
        public bool IsExpanderVisible
        {
            get { return _IsExpanderVisible; }
            set
            {
                if (_IsExpanderVisible != value)
                {
                    _IsExpanderVisible = value;
                    OnPropertyChanged("IsExpanderVisible");
                }
            }
        }

        #endregion

        #region Methods
        public async Task GetAccountMembers()
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                // Get App settings api..
                try
                {
                    Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                    {
                        UserName = UserName != null ? Helpers.AppGlobalConstants.userInfo.Name : User;
                        //   lblPlan = info.CurrentSubscriptionPlan != null ? $"Plan: {info.CurrentSubscriptionPlan}" : string.Empty;
                        UITopic FamilyAccountNotice = await Globals.Instance.GetUTText("FamilyAccountNotice", "en");

                        if (FamilyAccountNotice != null)
                        {
                            familyAccountNoticeString = FamilyAccountNotice.UITextList.Find(i => i.TagName == "FamilyAccountNotice").Text; //Header
                            allInfoViewableString = FamilyAccountNotice.UITextList.Find(i => i.TagName == "AllInfoViewable").Text; //Paragraph
                        }
                        AccountAddFamilyMemberState state = new AccountAddFamilyMemberState();
                        if (state.AdditionalFamilyMembers != null)
                        {
                            if (state.PrimaryPatientID != 0)
                            {
                                ShowConfirmAddMemberFamilyDialog();
                            }
                        }
                        else
                        {
                            Task.Run(async () =>
                            {
                                await GetMembers().ConfigureAwait(false);
                            });
                        }
                    });
                }
                catch (Exception ex)
                {
                    UserDialog.HideLoading();
                }
            }
        }
        public class FragmentArguments
        {
            public List<AdditionalFamilyMember> Members;
            public string AddOnCost { get; set; }
        }

        private async Task ShowConfirmAddMemberFamilyDialog()
        {
            AccountAddFamilyMemberState state = new AccountAddFamilyMemberState();


            FragmentArguments fa = new FragmentArguments()
            {
                Members = state.AdditionalFamilyMembers
            };


            AccountAddFamilyMemberInfo resp = Helpers.AppGlobalConstants.userInfo.Domain == "Austin Regional Clinic" ? await DataUtility.PatientGetAddFamilyMemberInfoAsync(SettingsValues.ApiURLValue, Helpers.AppGlobalConstants.userInfo.PatientID, Token).ConfigureAwait(false) : await DataUtility.PatientGetAddFamilyMemberInfoAsync(SettingsValues.ApiURLValue, Helpers.AppGlobalConstants.userInfo.PatientID, Token, state.AdditionalFamilyMembers.Count + 1).ConfigureAwait(false);

            if (resp.CanAddFamilyMember)
            {

                if (state.AdditionalFamilyMembers.Count + 1 > state.FreeFamilyMemberRemaining)
                {
                    //this dialog only needs addon cost

                    await Task.Run(async () =>
                    {
                        await ProcessExistingFamilyMembers(state);
                    });

                }
                else
                {

                    await AddFamilyMembers(state);

                }

            }
            else
            {
                Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                {
                    await UserDialog.AlertAsync("You do not have room on your account to add any more family members.", "Add Family Member", "Confirm");
                    await AddFamilyMembers(state);
                });

            }

        }
        private async Task ProcessExistingFamilyMembers(AccountAddFamilyMemberState state)
        {
            if (state.AdditionalFamilyMembers.Count > state.FreeFamilyMemberRemaining)
            {
                string msg = string.Empty;
                string msg_members = string.Join(" and ", state.AdditionalFamilyMembers.Select(prop => prop.FamilyMemberInformation.FullName));
                msg = $"{msg_members} {(state.AdditionalFamilyMembers.Count == 1 ? "has" : "have")} been added to your family.";

                string myString = "test";
                // Intent intent = new Intent(this, typeof(OrderSummaryActivity));
                // intent.PutExtra<AccountSubscriptionInfo>("account_subscription_info", info);
                SubscriptionChangeInfo changeInfo = new SubscriptionChangeInfo() { Last4ofCC = aafmi.Last4ofCC, NextBillingDate = aafmi.NextBillingDate };
                //intent.PutExtra<SubscriptionChangeInfo>("subscription_change_info", changeInfo);
                // StartActivity(intent);
                //RunOnUiThread(() =>
                //{

                //});
                //Finish();

            }
            else
            {
                await AddFamilyMembers(state);
            }
        }
        private async Task AddFamilyMembers(AccountAddFamilyMemberState state)
        {
            StatusResponse resp = await DataUtility._PatientAddFamilyMembersAsync(SettingsValues.ApiURLValue, state, Token).ConfigureAwait(false);

            if (resp != null)
            {
                if (resp.StatusCode == StatusCode.Success)
                {
                    Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                    {
                        string msg = string.Empty;

                        string msg_members = string.Join(" and ", state.AdditionalFamilyMembers.Select(prop => prop.FamilyMemberInformation.FullName));
                        msg = $"{msg_members} {(state.AdditionalFamilyMembers.Count == 1 ? "has" : "have")} been added to your family.\n\nThere will be no change to your subscription at this time.";

                        //using (AccountAddFamilyMemberStateHelper afmStateHelper = new AccountAddFamilyMemberStateHelper(this))
                        //{
                        //    afmStateHelper.Clear();
                        //}

                        // ReusableDialog afmdialog = ReusableDialog.Instance(this, null, "Add Family Member", "Ok", null, null, true, true, msg);
                        await UserDialog.AlertAsync("Add Family Member", msg, "Ok");

                        //afmdialog.NegativeButtonClicked += async (args2) =>
                        //{
                        //    if (afmdialog.IsVisible) afmdialog.Dismiss();
                        //    await GetMembers().ConfigureAwait(false);
                        //};

                        //afmdialog.Show(this.FragmentManager, "afmdialog");
                    });
                }
                else if (resp.StatusCode == StatusCode.PaymentErrorSeePayload)
                {
                    //resp.Payload 
                }
            }
        }
        public async Task GetMembers()
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
                            info = await DataUtility.PatientGetSubscriptionInfoAsync(SettingsValues.ApiURLValue, PatientID, Token).ConfigureAwait(false);
                            if (info != null)
                            {
                                if (info.CanAddFamilyMembers)
                                {
                                    aafmi = await DataUtility.PatientGetAddFamilyMemberInfoAsync(SettingsValues.ApiURLValue, PatientID, Token).ConfigureAwait(false);
                                }
                                Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                                {
                                    lblPlan = info.CurrentSubscriptionPlan != null ? $"Plan: {info.CurrentSubscriptionPlan}" : string.Empty;
                                    familyAcctNoticeTV = familyAccountNoticeString != null ? familyAccountNoticeString : string.Empty;
                                    allInfoViewableTV = allInfoViewableString != null ? allInfoViewableString : string.Empty;
                                    AccountHolderList = new ObservableCollection<AccountMember>(info.AccountMembers);
                                    //list.SetAdapter(new Adapters.AccountProfilesExpandableListAdapter(this, info.AccountMembers, Resources));
                                    if (info.IsFamilyPlan)//(info.CurrentSubscriptionPlan == "Family Subscription" || info.CurrentSubscriptionPlan == "Family 365 Plan")
                                    {
                                        familyAcctNotice = true;
                                    }
                                    lytAdditionalMembersInfo = info.CanAddFamilyMembers ? true : false;

                                    if (aafmi != null)
                                    {
                                        lblAdditionalMembersInfo = $"You have room for {aafmi.FreeFamilyMembersRemaining} additional family member{(aafmi.FreeFamilyMembersRemaining == 1 ? "" : "s")} with your current plan.";
                                    }
                                });
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

        private async Task GetUIText()
        {
            try
            {
                await Task.Run(async () =>
                   {
                       UITopic FamilyAccountNotice = await Globals.Instance.GetUTText("FamilyAccountNotice", "en");

                       if (FamilyAccountNotice != null)
                       {
                           string familyAccountNoticeString = FamilyAccountNotice.UITextList.Find(i => i.TagName == "FamilyAccountNotice").Text; //Header
                           string allInfoViewableString = FamilyAccountNotice.UITextList.Find(i => i.TagName == "AllInfoViewable").Text; //Paragraph
                       }
                   });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
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

        /// <summary>
        /// To Do: To define Update Medical Info command
        /// </summary>
        /// <param name="obj"></param> 
        private async void UpdateMedicalInformationAsync(object obj)
        {
            try
            {
                //App.Current.MainPage = new Views.MyMedicalInfo.MyMedicalInfoPage();
               // App.Current.MainPage = new Views.MainTabs.MainTabPage("MyMedicalPage");
                //await Navigation.PushModalAsync(new Views.MyMedicalInfo.MyMedicalInfoPage(), false);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// To Do: To define Update Account Access command
        /// </summary>
        /// <param name="obj"></param> 
        private async void UpdateAccountAccessAsync(object obj)
        {
            try
            {
                int selectedPatientId = PatientID;
                am = info.AccountMembers.FirstOrDefault(x => PatientID == selectedPatientId);
                if (am != null)
                {
                    await Navigation.PushModalAsync(new Views.MyAccount.UpdateAccountAccessPage(am), false);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        /// <summary>
        /// To Do: To define Update Demographics command
        /// </summary>
        /// <param name="obj"></param> 
        private async void UpdateDemographicsAsync(object obj)
        {
            try
            {
                int selectedPatientId = PatientID;
                am = info.AccountMembers.FirstOrDefault(x => PatientID == selectedPatientId);
                if (am != null)
                {
                    BasicFamilyMemberInfo bfmi = new BasicFamilyMemberInfo();
                    bfmi.DisplayName = am.DisplayName;
                    bfmi.DOB = am.DOB;
                    bfmi.FirstName = am.FirstName;
                    bfmi.IsActive = am.IsActive;
                    bfmi.IsPrimary = am.IsPrimary;
                    bfmi.LastName = am.LastName;
                    bfmi.PatientID = am.PatientID;
                    await Navigation.PushModalAsync(new Views.MyAccount.UpdateDemographicsPage(bfmi), false);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void lytAddFamilyMemberAsync(object obj)
        {
            try
            {
                Task.Run(async () =>
                    {
                        await AddFamilyMember().ConfigureAwait(false);
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async Task AddFamilyMember()
        {
            try
            {
                AccountAddFamilyMemberInfo resp = await DataUtility.PatientGetAddFamilyMemberInfoAsync(SettingsValues.ApiURLValue, PatientID, Token).ConfigureAwait(false);

                if (resp != null)
                {
                    Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                    {
                        string msg = string.Empty;
                        bool proceed = true;
                        if (resp.FreeFamilyMembersRemaining > 0)
                        {
                            msg = $"You have room for {resp.FreeFamilyMembersRemaining} additional family members with your current payment.\n\nThe patient will be added under this plan.";
                            msgDialog(resp, msg);
                        }
                        else
                        {
                            if (resp.CanAddFamilyMember && SettingsValues.ECommerce)
                            {
                                //string NoOpenSlots_para2 = "";
                                await Task.Run(async () =>
                                 {
                                     UITopic AccountProfile = await Globals.Instance.GetUTText("AccountProfile", "en");
                                     string NoOpenSlots_para2 = AccountProfile.UITextList.Find(i => i.TagName == "NoOpenSlots_para2").Text;
                                     msg = $"You do not have any open slots for additional family members under your current plan. Additional family members may be added at {resp.AddOnCost} per member per month.\n\n";
                                     msg += NoOpenSlots_para2;
                                     msgDialog(resp, msg);
                                     // UserDialog.Alert(msg, "Add Family Member", "Confirm");
                                 });
                                //msg = $"You do not have any open slots for additional family members under your current plan. Additional family members may be added at {resp.AddOnCost} per member per month.\n\nYou will be immediately charged the add-on rate for this month. Your new subscription will begin at the start of the next billing cycle.";
                                //msg = $"You do not have any open slots for additional family members under your current plan. Additional family members may be added at {resp.AddOnCost} per member per month.\n\nAdd-on charges are processed within 24 hours of purchase. Updated subscription fees will begin at the start of the next billing cycle.";
                                //msg = $"You do not have any open slots for additional family members under your current plan. Additional family members may be added at {resp.AddOnCost} per member per month.\n\n";

                            }
                            else
                            {
                                msg = "You do not have room on your account to add any more family members.";
                                proceed = false;
                            }
                        }

                        if (!proceed)
                        {
                            //  UtilsUI.ShowMsgOkScreen(this, msg);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        private async void msgDialog(AccountAddFamilyMemberInfo resp, string msg)
        {


            //ReusableDialog dialog = ReusableDialog.Instance(this, null, "Add Family Member", "Confirm", "Cancel", null, true, true, msg);
            var dialog = await UserDialogs.Instance.ConfirmAsync(msg, "Add Family Member","Confirm", "Cancel");

            if (dialog)
            {
                // User clicked on Confirm button
                AccountAddFamilyMemberState state = new AccountAddFamilyMemberState();
                //using (AccountAddFamilyMemberStateHelper afmStateHelper = new AccountAddFamilyMemberStateHelper(this))
                //{
                //    afmStateHelper.Clear();
                //    state = afmStateHelper.GetState();
                //}

                state.PrimaryPatientID = PatientID;
                state.AdditionalCost = info.AvailableSubscriptionAddOns[0].Cost.Replace(@"/mo.", string.Empty).Replace(@"$", string.Empty);
                state.FreeFamilyMemberRemaining = resp.FreeFamilyMembersRemaining;
                state.ExistingFamilyMembers = info?.AccountMembers;
                state.ProratedAddOnCost = resp.ProratedAddOnCost;
                state.SingleAddOnCost = resp.SingleAddOnCost;
                state.NoActiveCardOnFile = resp.NoActiveCardOnFile;

                //using (AccountAddFamilyMemberStateHelper afmStateHelper = new AccountAddFamilyMemberStateHelper(this))
                //{
                //    afmStateHelper.SetState(state);

                //}
                Helpers.AppGlobalConstants.state = state;
                await Navigation.PushModalAsync(new Views.Family.PatientSettingsManageSubscriptionAddFamilyMember());


                //var intent = new Intent(this, typeof(PatientSettingsManageSubscriptionAddFamilyMemberActivity));
                //StartActivity(intent);

                // dialog.Show(this.FragmentManager, "dialog");
            }
            #endregion
        }
    }
}