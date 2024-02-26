using Acr.UserDialogs;
using Android.App;
using CommonLibraryCoreMaui;
using CommonLibraryCoreMaui.Models;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Graphics.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.MyAccount
{
    public class UpdateAccountAccessPageViewModel : BaseViewModel
    {
        //To define the class level variable.....
        public AccountMember am;
        UserInfo userInfo;
        int PatientID = 0;
        string Token = string.Empty;
        #region Constructor
        public string CustomerServicePhone = Preferences.Get("CustomerServicePhone", string.Empty);
        public UpdateAccountAccessPageViewModel(INavigation nav)
        {
            Navigation = nav;
            BackCommand = new Command(BackAsync);
            YesCommand = new Command(YesAsync);
            SubmitCommand = new Command(SubmitAsync);

            Token = Preferences.Get("AuthToken", string.Empty);
            PatientID = Preferences.Get("PatientID", 0);
        }


        #endregion

        #region Command
        public Command BackCommand { get; set; }
        public Command YesCommand { get; set; }
        public Command SubmitCommand { get; set; }
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
        private string HtmlToPlainText(string html)
        {
            // Remove HTML tags using a regular expression
            string plainText = Regex.Replace(html, "<[^>]*>", "");

            // Decode HTML entities
            plainText = System.Net.WebUtility.HtmlDecode(plainText);

            return plainText;
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

        #endregion
    }
}
