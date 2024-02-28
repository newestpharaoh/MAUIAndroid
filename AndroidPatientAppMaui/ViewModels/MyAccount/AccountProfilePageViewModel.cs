using Acr.UserDialogs; 
using AndroidPatientAppMaui.Views.MyAccount;
using CommonLibraryCoreMaui;
using CommonLibraryCoreMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.MyAccount
{
    public class AccountProfilePageViewModel : BaseViewModel
    {  //To define the class level variable.
        AccountMember am;
        AccountSubscriptionInfo info;
        AccountAddFamilyMemberInfo aafmi;
        string familyAccountNoticeString;
        string allInfoViewableString;
        int PatientID = 0;
        string Token = string.Empty; 

        #region Constructor
        public AccountProfilePageViewModel(INavigation nav )
        {
            Navigation = nav;
            BackCommand = new Command(BackAsync);
            UpdateAccountAccessCommand = new Command(UpdateAccountAccessAsync);
            UpdateMedicalInformationCommand = new Command(UpdateMedicalInformationAsync);
            UpdateDemographicsCommand = new Command(UpdateDemographicsAsync);
            lytAddFamilyMemberCommond = new Command(lytAddFamilyMemberAsync); 

            Token = Preferences.Get("AuthToken", string.Empty);
            PatientID = Preferences.Get("PatientID", 0);
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
        private string _UserName;
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
        private string _lblAdditionalMembersInfo = "You have room for @membersCount@ additional family @member_s@ with your current plan.";
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

        #endregion

        #region Methods

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
                                try
                                {
                                    UserName = Helpers.AppGlobalConstants.userInfo.Name;
                                    lblPlan = $"Plan: {info.CurrentSubscriptionPlan}";
                                    if (info.CanAddFamilyMembers)
                                    {
                                        aafmi = await DataUtility.PatientGetAddFamilyMemberInfoAsync(SettingsValues.ApiURLValue, PatientID, Token).ConfigureAwait(false);
                                        Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                                        {

                                            await Task.Run(async () =>
                                            {
                                                UITopic FamilyAccountNotice = await Globals.Instance.GetUTText("FamilyAccountNotice", "en");

                                                if (FamilyAccountNotice != null)
                                                {
                                                    familyAccountNoticeString = FamilyAccountNotice.UITextList.Find(i => i.TagName == "FamilyAccountNotice").Text; //Header
                                                    allInfoViewableString = FamilyAccountNotice.UITextList.Find(i => i.TagName == "AllInfoViewable").Text; //Paragraph
                                                }
                                            });

                                            lblPlan = $"Plan: {info.CurrentSubscriptionPlan}";
                                            familyAcctNoticeTV = familyAccountNoticeString;
                                            allInfoViewableTV = allInfoViewableString;

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
            }
        }

        private async Task GetUIText()
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
                await Navigation.PushModalAsync(new Views.MyMedicalInfo.MyMedicalInfoPage(), false);

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
                await Navigation.PushModalAsync(new Views.MyAccount.UpdateDemographicsPage(), false);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void lytAddFamilyMemberAsync(object obj)
        {
            Task.Run(async () =>
            {
                await AddFamilyMember().ConfigureAwait(false);
            });
        }

        private async Task AddFamilyMember()
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
                        UserDialog.Alert(msg);
                    }
                    else
                    {
                        if (resp.CanAddFamilyMember && SettingsValues.ECommerce)
                        {
                            //string NoOpenSlots_para2 = "";
                            System.Threading.Tasks.Task.Run(async () =>
                            {
                                UITopic AccountProfile = await Globals.Instance.GetUTText("AccountProfile", "en");
                                string NoOpenSlots_para2 = AccountProfile.UITextList.Find(i => i.TagName == "NoOpenSlots_para2").Text;
                                msg = $"You do not have any open slots for additional family members under your current plan. Additional family members may be added at {resp.AddOnCost} per member per month.\n\n";
                                msg += NoOpenSlots_para2;
                                UserDialog.Alert(msg);
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
         
        #endregion
    }
}
