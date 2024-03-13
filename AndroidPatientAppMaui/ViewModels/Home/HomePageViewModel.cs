using Acr.UserDialogs;
using AndroidPatientAppMaui.Helpers;
using AndroidPatientAppMaui.Views;
using CommonLibraryCoreMaui;
using CommonLibraryCoreMaui.Models;
using FM.LiveSwitch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.Home
{
    public class HomePageViewModel : BaseViewModel
    {
        //To define the class level variable.
        string Token = string.Empty;
        int Userid = 0;

        #region Constructor
        public HomePageViewModel(INavigation nav)
        {
            try
            {
                Navigation = nav;
                SignOutCommand = new Command(SignOutAsync);
                StartVisitCommand = new Command(StartVisitAsync);

                Token = Preferences.Get("AuthToken", string.Empty);
                Userid = Preferences.Get("UserId", 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #endregion

        #region Command
        public Command SignOutCommand { get; set; }
        public Command StartVisitCommand { get; set; }
        #endregion

        #region Properties

        private string _phoneNumber = "512-421-5678";
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    OnPropertyChanged("PhoneNumber");
                }
            }
        }
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
        private string _Insurance;
        public string Insurance
        {
            get { return _Insurance; }
            set
            {
                if (_Insurance != value)
                {
                    _Insurance = value;
                    OnPropertyChanged("Insurance");
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

        private string _Version;
        public string Version
        {
            get { return _Version; }
            set
            {
                if (_Version != value)
                {
                    _Version = value;
                    OnPropertyChanged("Version");
                }
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// TODO : To Define Get User Info Method.......
        /// </summary>
        /// <returns></returns>
        public async Task GetUserInfo()
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
                            try
                            {
                                Preferences.Set("PatientID", userInfo.PatientID);
                                AppGlobalConstants.userInfo = userInfo;
                                UserName = $"Welcome, {userInfo.Name}";
                                string accountStatus = userInfo.IsActive ? "Active" : "Deactivated";
                                Account = accountStatus;
                                Insurance = $"You are covered by: {userInfo.Domain}";
                                Version = $"Version {AppInfo.VersionString}";
                            }
                            catch (Exception ex) { }
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
        /// To Do: To define Sign Out command
        /// </summary>
        /// <param name="obj"></param>

        private async void SignOutAsync(object obj)
        {
            try
            {
                bool answer = await App.Current.MainPage.DisplayAlert("Sign Out", "Are you sure you want to sign out?", "Yes", "No");
                if (answer)
                {
                    AndroidPatientAppMaui.Helpers.AppGlobalConstants.Token = "";
                    AndroidPatientAppMaui.Helpers.AppGlobalConstants.UserId = 0;
                    AndroidPatientAppMaui.Helpers.AppGlobalConstants.TokenExpirationDate = DateTime.Now;
                    AndroidPatientAppMaui.Helpers.AppGlobalConstants.LoginEmail = "";
                    AndroidPatientAppMaui.Helpers.AppGlobalConstants.userInfo = new UserInfo();
                    Preferences.Set("AuthToken", "");
                    Preferences.Set("UserId", 0);
                    Preferences.Set("PatientID", 0);
                    await Navigation.PushModalAsync(new LoginPage(), false);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// To Do: To define Start Visit Button command....
        /// </summary>
        /// <param name="obj"></param>

        private async void StartVisitAsync(object obj)
        {
            try
            {
                StatusResponse info = await DataUtility.GetRemainingVisitCountAsync(SettingsValues.ApiURLValue, Helpers.AppGlobalConstants.userInfo.LoginID, Token).ConfigureAwait(false);
                if (info != null)
                {
                    if (info.StatusCode == StatusCode.SuccessSeePayload)
                    {
                        if (int.TryParse(info.Payload, out int remainingVisitCount))
                        {
                            if (remainingVisitCount > 0)
                            {
                                Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                                {
                                    if (Helpers.AppGlobalConstants.userInfo.Domain == "Star" || Helpers.AppGlobalConstants.userInfo.Domain == "Star Kids")
                                    {
                                        await Navigation.PushModalAsync(new Views.Home.PatientPreVisitForSomeoneElse(), false);
                                    }
                                    else
                                    {
                                       
                                        await Navigation.PushModalAsync(new Views.Home.PatientPreVisitPatientSelectionStep1(), false);
                                    }
                                });
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

        #endregion

    }
}
