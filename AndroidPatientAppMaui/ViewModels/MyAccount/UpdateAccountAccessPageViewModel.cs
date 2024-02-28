using Acr.UserDialogs;
using CommonLibraryCoreMaui;
using CommonLibraryCoreMaui.Models;

namespace AndroidPatientAppMaui.ViewModels.MyAccount
{
    public class UpdateAccountAccessPageViewModel : BaseViewModel
    {
        //To define the class level variable.....
        public AccountMember am;
        public StatusResponse resp;
        UserInfo userInfo;
        int PatientID = 0;
        string Token = string.Empty;
        bool hasActiveCoverage;
        string privateEmail = string.Empty;
        string args = string.Empty;
        #region Constructor
        public string CustomerServicePhone = Preferences.Get("CustomerServicePhone", string.Empty);
        public UpdateAccountAccessPageViewModel(INavigation nav)
        {
            Navigation = nav;
            BackCommand = new Command(BackAsync);
            YesCommand = new Command(YesAsync);
            SubmitCommand = new Command(SubmitAsync);
            RemoveUserSubmitCommand = new Command(RemoveUserSubmitAsync);
            ContinueToManageSubsCommand = new Command(ContinueToManageSubsAsync);
            ReactivateUserSubmitCommand = new Command(ReactivateUserSubmitAsync);

            Token = Preferences.Get("AuthToken", string.Empty);
            PatientID = Preferences.Get("PatientID", 0);
        }
        #endregion

        #region Command
        public Command BackCommand { get; set; }
        public Command YesCommand { get; set; }
        public Command SubmitCommand { get; set; }
        public Command ContinueToManageSubsCommand { get; set; }
        public Command RemoveUserSubmitCommand { get; set; }
        public Command ReactivateUserSubmitCommand { get; set; }
        #endregion

        #region Properties

        private string _lblMemberName;
        public string lblMemberName
        {
            get { return _lblMemberName; }
            set
            {
                if (_lblMemberName != value)
                {
                    _lblMemberName = value;
                    OnPropertyChanged("lblMemberName");
                }
            }
        }
        private bool _imgInfoPrivatize = false;
        public bool imgInfoPrivatize
        {
            get { return _imgInfoPrivatize; }
            set
            {
                if (_imgInfoPrivatize != value)
                {
                    _imgInfoPrivatize = value;
                    OnPropertyChanged("imgInfoPrivatize");
                }
            }
        }
        private bool _lytPrivatizeButton = false;
        public bool lytPrivatizeButton
        {
            get { return _lytPrivatizeButton; }
            set
            {
                if (_lytPrivatizeButton != value)
                {
                    _lytPrivatizeButton = value;
                    OnPropertyChanged("lytPrivatizeButton");
                }
            }
        }
        private bool _btnPrivatizeUser = true;
        public bool btnPrivatizeUser
        {
            get { return _btnPrivatizeUser; }
            set
            {
                if (_btnPrivatizeUser != value)
                {
                    _btnPrivatizeUser = value;
                    OnPropertyChanged("btnPrivatizeUser");
                }
            }
        }
        private bool _btnRemoveUser = true;
        public bool btnRemoveUser
        {
            get { return _btnRemoveUser; }
            set
            {
                if (_btnRemoveUser != value)
                {
                    _btnRemoveUser = value;
                    OnPropertyChanged("btnRemoveUser");
                }
            }
        }
        private bool _lytPrivatizeInfo = false;
        public bool lytPrivatizeInfo
        {
            get { return _lytPrivatizeInfo; }
            set
            {
                if (_lytPrivatizeInfo != value)
                {
                    _lytPrivatizeInfo = value;
                    OnPropertyChanged("lytPrivatizeInfo");
                }
            }
        }

        private bool _lytReactivate = false;
        public bool lytReactivate
        {
            get { return _lytReactivate; }
            set
            {
                if (_lytReactivate != value)
                {
                    _lytReactivate = value;
                    OnPropertyChanged("lytReactivate");
                }
            }
        }
        private bool _lytDeactivate = false;
        public bool lytDeactivate
        {
            get { return _lytDeactivate; }
            set
            {
                if (_lytDeactivate != value)
                {
                    _lytDeactivate = value;
                    OnPropertyChanged("lytDeactivate");
                }
            }
        }
        private string _lblError;
        public string lblError
        {
            get { return _lblError; }
            set
            {
                if (_lblError != value)
                {
                    _lblError = value;
                    OnPropertyChanged("lblError");
                }
            }
        }
        private string _lblInfo;
        public string lblInfo
        {
            get { return _lblInfo; }
            set
            {
                if (_lblInfo != value)
                {
                    _lblInfo = value;
                    OnPropertyChanged("lblInfo");
                }
            }
        }
        private string _txtEmail;
        public string txtEmail
        {
            get { return _txtEmail; }
            set
            {
                if (_txtEmail != value)
                {
                    _txtEmail = value;
                    OnPropertyChanged("txtEmail");
                }
            }
        }
        private string _removeUserAccount = "REMOVE User Account";
        public string removeUserAccount
        {
            get { return _removeUserAccount; }
            set
            {
                if (_removeUserAccount != value)
                {
                    _removeUserAccount = value;
                    OnPropertyChanged("removeUserAccount");
                }
            }
        }
        private string _TxtImportant = "IMPORTANT";
        public string TxtImportant
        {
            get { return _TxtImportant; }
            set
            {
                if (_TxtImportant != value)
                {
                    _TxtImportant = value;
                    OnPropertyChanged("TxtImportant");
                }
            }
        }
        private string _tvYouAreRemoving = "You are removing this user from your family account. After removal, they can still have access to their visit history. They can create a separate account in the future.";
        public string tvYouAreRemoving
        {
            get { return _tvYouAreRemoving; }
            set
            {
                if (_tvYouAreRemoving != value)
                {
                    _tvYouAreRemoving = value;
                    OnPropertyChanged("tvYouAreRemoving");
                }
            }
        }
        private string _wantToContinueTxt = "Want to continue?";
        public string wantToContinueTxt
        {
            get { return _wantToContinueTxt; }
            set
            {
                if (_wantToContinueTxt != value)
                {
                    _wantToContinueTxt = value;
                    OnPropertyChanged("wantToContinueTxt");
                }
            }
        }
        private string _submitEmailTxt = "Submit the user's email address below. This should be different from the family account primary email address.";
        public string submitEmailTxt
        {
            get { return _submitEmailTxt; }
            set
            {
                if (_submitEmailTxt != value)
                {
                    _submitEmailTxt = value;
                    OnPropertyChanged("submitEmailTxt");
                }
            }
        }
        private string _onceYouClickSubmitTxt = "Once you click submit, an email will be sent to the user at the e-mail address below, allowing them to create a new, separate account.";
        public string onceYouClickSubmitTxt
        {
            get { return _onceYouClickSubmitTxt; }
            set
            {
                if (_onceYouClickSubmitTxt != value)
                {
                    _onceYouClickSubmitTxt = value;
                    OnPropertyChanged("onceYouClickSubmitTxt");
                }
            }
        }
        private string _activeCoverageAvailableTxt = "Active Coverage Available";
        public string activeCoverageAvailableTxt
        {
            get { return _activeCoverageAvailableTxt; }
            set
            {
                if (_activeCoverageAvailableTxt != value)
                {
                    _activeCoverageAvailableTxt = value;
                    OnPropertyChanged("activeCoverageAvailableTxt");
                }
            }
        }
        private string _coveredAccessTxt = "This patient has covered access to NormanMD through their employer or insurance plan.";
        public string coveredAccessTxt
        {
            get { return _coveredAccessTxt; }
            set
            {
                if (_coveredAccessTxt != value)
                {
                    _coveredAccessTxt = value;
                    OnPropertyChanged("coveredAccessTxt");
                }
            }
        }
        private string _enterNewEmailTxt = "Enter new email";
        public string enterNewEmailTxt
        {
            get { return _enterNewEmailTxt; }
            set
            {
                if (_enterNewEmailTxt != value)
                {
                    _enterNewEmailTxt = value;
                    OnPropertyChanged("enterNewEmailTxt");
                }
            }
        }
        private string _txtNewEmailTxt;
        public string txtNewEmailTxt
        {
            get { return _txtNewEmailTxt; }
            set
            {
                if (_txtNewEmailTxt != value)
                {
                    _txtNewEmailTxt = value;
                    OnPropertyChanged("txtNewEmailTxt");
                }
            }
        }
        private string _TxtReactivateUserlblInfo2= "The amount of @cost@ will be charged now and the new monthly subscription cost of @cost2@ will be reflected in your next bill.";
        public string TxtReactivateUserlblInfo2
        {
            get { return _TxtReactivateUserlblInfo2; }
            set
            {
                if (_TxtReactivateUserlblInfo2 != value)
                {
                    _TxtReactivateUserlblInfo2 = value;
                    OnPropertyChanged("TxtReactivateUserlblInfo2");
                }
            }
        }
        private string _txtPrivateEmail;
        public string txtPrivateEmail
        {
            get { return _txtPrivateEmail; }
            set
            {
                if (_txtPrivateEmail != value)
                {
                    _txtPrivateEmail = value;
                    OnPropertyChanged("txtPrivateEmail");
                }
            }
        }
        private bool _WantToContinue = false;
        public bool WantToContinue
        {
            get { return _WantToContinue; }
            set
            {
                if (_WantToContinue != value)
                {
                    _WantToContinue = value;
                    OnPropertyChanged("WantToContinue");
                }
            }
        }
        private bool _tvSubmitEmail = false;
        public bool tvSubmitEmail
        {
            get { return _tvSubmitEmail; }
            set
            {
                if (_tvSubmitEmail != value)
                {
                    _tvSubmitEmail = value;
                    OnPropertyChanged("tvSubmitEmail");
                }
            }
        }
        private bool _tvOnceYouClickSubmit = false;
        public bool tvOnceYouClickSubmit
        {
            get { return _tvOnceYouClickSubmit; }
            set
            {
                if (_tvOnceYouClickSubmit != value)
                {
                    _tvOnceYouClickSubmit = value;
                    OnPropertyChanged("tvOnceYouClickSubmit");
                }
            }
        }
        private bool _activeCoverageAvailable = false;
        public bool activeCoverageAvailable
        {
            get { return _activeCoverageAvailable; }
            set
            {
                if (_activeCoverageAvailable != value)
                {
                    _activeCoverageAvailable = value;
                    OnPropertyChanged("activeCoverageAvailable");
                }
            }
        }
        private bool _coveredAccess = false;
        public bool coveredAccess
        {
            get { return _coveredAccess; }
            set
            {
                if (_coveredAccess != value)
                {
                    _coveredAccess = value;
                    OnPropertyChanged("coveredAccess");
                }
            }
        }
        private bool _tvEnterNewEmail = false;
        public bool tvEnterNewEmail
        {
            get { return _tvEnterNewEmail; }
            set
            {
                if (_tvEnterNewEmail != value)
                {
                    _tvEnterNewEmail = value;
                    OnPropertyChanged("tvEnterNewEmail");
                }
            }
        }
        private bool _txtNewEmail = false;
        public bool txtNewEmail
        {
            get { return _txtNewEmail; }
            set
            {
                if (_txtNewEmail != value)
                {
                    _txtNewEmail = value;
                    OnPropertyChanged("txtNewEmail");
                }
            }
        }
        private bool _lytPrivateEmail = false;
        public bool lytPrivateEmail
        {
            get { return _lytPrivateEmail; }
            set
            {
                if (_lytPrivateEmail != value)
                {
                    _lytPrivateEmail = value;
                    OnPropertyChanged("lytPrivateEmail");
                }
            }
        }
        private bool _opacitygrid = false;
        public bool opacitygrid
        {
            get { return _opacitygrid; }
            set
            {
                if (_opacitygrid != value)
                {
                    _opacitygrid = value;
                    OnPropertyChanged("opacitygrid");
                }
            }
        }
        private bool _ReactivateUserlblInfo2 = true;
        public bool ReactivateUserlblInfo2
        {
            get { return _ReactivateUserlblInfo2; }
            set
            {
                if (_ReactivateUserlblInfo2 != value)
                {
                    _ReactivateUserlblInfo2 = value;
                    OnPropertyChanged("ReactivateUserlblInfo2");
                }
            }
        }
        #endregion

        #region Methods 
        /// <summary>
        /// TODO : To Define Get User Info Method.......
        /// </summary>
        /// <returns></returns>
        public async Task GetUpdateAccountAccessInfo()
        {
            // Get App settings api..
            try
            {
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {
                    UserDialog.ShowLoading();
                    await Task.Run(async () =>
                    {
                        lblMemberName = am.DisplayName;
                        lblInfo = CustomerServicePhone;
                        if (!am.IsActive)
                        {
                            lytReactivate = true;
                        }
                        else
                        {
                            lytReactivate = false;
                        }

                        if (!am.IsPrivate)
                        {
                            lytPrivatizeInfo = false;
                            lytPrivatizeButton = true;
                            imgInfoPrivatize = true;
                        }
                        else
                        {
                            lytPrivatizeInfo = true;
                            lytPrivatizeButton = false;
                            imgInfoPrivatize = false;
                        }

                        if (am.IsActive)
                        {
                            lytDeactivate = true;
                        }
                        else
                        {
                            lytDeactivate = false;
                        }

                        if (am.IsPrimary)
                        {
                            btnPrivatizeUser = false;
                            btnRemoveUser = false;
                        }

                        if (Helpers.AppGlobalConstants.userInfo.CurrentSubscriptionPlan != null || Helpers.AppGlobalConstants.userInfo.Domain != null)
                        {
                            var isCurrentPlanARC = (Helpers.AppGlobalConstants.userInfo.Domain == "Austin Regional Clinic" || Helpers.AppGlobalConstants.userInfo.CurrentSubscriptionPlan == "Austin Regional Clinic");
                            if (isCurrentPlanARC) { btnPrivatizeUser = false; }
                        }

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
        /// TODO : To Define Get Remove User From Family Account Details Method.......
        /// </summary>
        /// <returns></returns>
        public async Task GetRemoveUserFromFamilyAccountDetails()
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
                            UITopic RemoveFamilyMember = await Globals.Instance.GetUTText("RemoveFamilyMember", "en");
                            if (RemoveFamilyMember != null)
                            {
                                string aca = RemoveFamilyMember.UITextList.Find(i => i.TagName == "ActiveCoverageAvailable").Text;
                                string ca = RemoveFamilyMember.UITextList.Find(i => i.TagName == "CoveredAccess").Text;
                                string AppName = Preferences.Get("AppName", string.Empty);
                                string formattedCA = String.Format(ca, AppName);

                                string important = RemoveFamilyMember.UITextList.Find(i => i.TagName == "Important").Text;
                                string permanentlyRemoving = RemoveFamilyMember.UITextList.Find(i => i.TagName == "PermanentlyRemoving").Text;
                                string wantToContinue = RemoveFamilyMember.UITextList.Find(i => i.TagName == "WantToContinue").Text;
                                string submitEmail = RemoveFamilyMember.UITextList.Find(i => i.TagName == "SubmitEmail").Text;
                                string removeUserAccount = RemoveFamilyMember.UITextList.Find(i => i.TagName == "RemoveUserAccount").Text;
                                string onceYouClickSubmit = RemoveFamilyMember.UITextList.Find(i => i.TagName == "PrivateEmail").Text;
                                //
                                Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                                {
                                    TxtImportant = important;
                                    tvYouAreRemoving = permanentlyRemoving;

                                    if (!string.IsNullOrEmpty(privateEmail))
                                    {
                                        lytPrivateEmail = true;
                                        activeCoverageAvailableTxt = aca;
                                        coveredAccessTxt = formattedCA;

                                        if (hasActiveCoverage)
                                        {
                                            activeCoverageAvailable = true;
                                            coveredAccess = true;
                                        }
                                        else
                                        {
                                            WantToContinue = true;
                                            wantToContinueTxt = wantToContinue;
                                            tvOnceYouClickSubmit = true;
                                            onceYouClickSubmitTxt = onceYouClickSubmit;
                                        }
                                        txtPrivateEmail = privateEmail;
                                    }
                                    else
                                    {
                                        wantToContinueTxt = wantToContinue;
                                        WantToContinue = true;
                                        submitEmailTxt = submitEmail;
                                        tvSubmitEmail = true;
                                        tvEnterNewEmail = true;
                                        txtNewEmail = true;
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
        /// <summary>
        /// To Do: To define back command...
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
        /// TODO : To Define Continue To Yes Button Command.....
        /// </summary>
        /// <param name="obj"></param>
        private async void YesAsync(object obj)
        {
            try
            {
                StatusResponse resp = await DataUtility.PatientMakeFamilyMemberPrivateAsync(SettingsValues.ApiURLValue, PatientID, am.PatientID, txtEmail, Token, "kkazmierczak@vertex.com");
                if (resp != null)
                {
                    if (resp.StatusCode == StatusCode.Success)
                    {
                        UserDialog.Alert("Success");
                    }
                    else if (resp.StatusCode == StatusCode.EmailAlreadyInUse)
                    {
                        UserDialog.Alert("Email Already In Use");
                    }
                    else if (resp.StatusCode == StatusCode.UserAlreadyPrivate)
                    {
                        UserDialog.Alert("User Is Already Private");
                    }
                    else if (resp.StatusCode == StatusCode.PatientUnderAge)
                    {
                        UserDialog.Alert("Patient Under Age");
                    }
                }
            }
            catch (Exception ex)
            { }
        }
        /// <summary>
        /// TODO : To Define Continue To Submmit Button Command.....
        /// </summary>
        /// <param name="obj"></param>
        private async void SubmitAsync(object obj)
        {
            StatusResponse resp = await DataUtility.DeactivateFamilyMemberAsync(SettingsValues.ApiURLValue, PatientID, am.PatientID, Token).ConfigureAwait(false);
            if (resp != null)
            {
                if (resp.StatusCode == StatusCode.Success || resp.StatusCode == StatusCode.SuccessSeePayload)
                {
                    string newMonthlyTotal = string.Empty;

                    if (resp.StatusCode == StatusCode.SuccessSeePayload)
                    {
                        newMonthlyTotal = resp.Payload;
                    }

                    UserDialog.Alert(newMonthlyTotal);
                }
            }
        }
        /// <summary>
        /// TODO : To Define Continue To Manage Subscrption Command.....
        /// </summary>
        /// <param name="obj"></param> 
        private async void ContinueToManageSubsAsync(object obj)
        {
            try
            {
                await Navigation.PushModalAsync(new Views.MyAccount.ManageSubscriptionPage(), false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        /// <summary>
        /// TODO : To Define Get Remove User Button Command.....
        /// </summary>
        /// <returns></returns>
        public async Task GetRemoveUserButtonAsync()
        {
            try
            {
                StatusResponse resp = await DataUtility.RemovePrivateFamilyMemberInfoAsync(SettingsValues.ApiURLValue, am.PatientID, Token).ConfigureAwait(false);
                if (resp != null)
                {
                    if (resp.StatusCode == StatusCode.Success)
                    {
                        privateEmail = resp.Payload2;
                    }
                }

                StatusResponse respCoverage = await DataUtility.GetActiveCoverageAsync(SettingsValues.ApiURLValue, am.PatientID, Token).ConfigureAwait(false);
                if (respCoverage != null)
                {
                    if (respCoverage.Message == "ActiveSubscriber")
                    {
                        hasActiveCoverage = true;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// TODO : To Define Remove User Submit Button.....
        /// </summary>
        /// <param name="obj"></param>
        private async void RemoveUserSubmitAsync(object obj)
        {
            string newEmail = txtNewEmailTxt;

            StatusResponse resp2 = await DataUtility.RemoveFamilyMemberAsync(SettingsValues.ApiURLValue, PatientID, am.PatientID, newEmail, true, Token).ConfigureAwait(false);

            if (resp2 != null)
            {
                if (resp2.StatusCode == StatusCode.Success || resp2.StatusCode == StatusCode.SuccessSeePayload)
                {
                    string newMonthlyTotal = string.Empty;

                    if (resp2.StatusCode == StatusCode.SuccessSeePayload)
                    {
                        newMonthlyTotal = resp2.Payload;
                    }

                    UserDialog.Alert(newMonthlyTotal);

                }
                else if (resp2.StatusCode == StatusCode.EmailAlreadyInUse)
                {
                    Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                    {
                        UserDialog.Alert("Email Already In Use");
                    });
                }
                else
                {
                    Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                    {
                        UserDialog.Alert("There was an error. Please try again.");
                    });
                }
            }
        }
        /// <summary>
        /// TODO : To Define Reactive User Submit Button.....
        /// </summary>
        /// <param name="obj"></param>
        private async void ReactivateUserSubmitAsync(object obj)
        {
            StatusResponse reactResp = await DataUtility.ReactivateFamilyMemberAsync(SettingsValues.ApiURLValue, PatientID, am.PatientID, Token).ConfigureAwait(false);
             
            string newMonthlyTotal = string.Empty;

            if (reactResp != null)
            {
                if (reactResp.StatusCode == StatusCode.Success || reactResp.StatusCode == StatusCode.SuccessSeePayload) ;
                if (reactResp.StatusCode == StatusCode.SuccessSeePayload)
                {
                    newMonthlyTotal = reactResp.Payload;
                }
            }

            UserDialog.Alert(newMonthlyTotal);
        }
        /// <summary>
        /// TODO : To Define Reactive User Button.....
        /// </summary>
        /// <returns></returns>
        public async Task GetReactivateUserButtonAsync()
        {
            try
            {
                resp = await DataUtility.ReactivateFamilyMemberInfoAsync(SettingsValues.ApiURLValue, PatientID, am.PatientID, Token).ConfigureAwait(false);
                if (resp != null)
                {
                    switch (resp.StatusCode)
                    {
                        case StatusCode.SuccessSeePayload:
                            Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                            {
                                args = string.Concat(resp.Payload, "_", resp.Payload2);
                                if (!string.IsNullOrEmpty(args))
                                {
                                    string[] ss = args.Split("_".ToCharArray());
                                    if (ss.Length > 1)
                                    {
                                        if (ss[0].ToLower().Equals("no cost"))
                                        {
                                            ReactivateUserlblInfo2 = false;
                                        }
                                        else
                                        {
                                            TxtReactivateUserlblInfo2 = TxtReactivateUserlblInfo2.Replace("@cost@", ss[0]).Replace("@cost2@", ss[1]);
                                            // lblInfo2.SetText(SpannableStringHelper.HighlightStrings(lblInfo2.Text, new List<string>() { ss[0], ss[1] }, Android.Graphics.Color.Red), TextView.BufferType.Spannable);
                                        }
                                    }
                                }
                            });
                            break;

                        case StatusCode.UserCantBeAdded:
                            UserDialog.Alert(resp.Message);
                            break;
                    }
                }
            }
            catch (Exception ex)
            { 
            }
        }
        #endregion
    }
}
