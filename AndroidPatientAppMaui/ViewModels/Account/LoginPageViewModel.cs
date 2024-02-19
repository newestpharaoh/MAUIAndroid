using Acr.UserDialogs;
using CommonLibraryCoreMaui;
using CommonLibraryCoreMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
//using static CoreFoundation.CFBundle;

namespace AndroidPatientAppMaui.ViewModels.Account
{
    public class LoginPageViewModel : BaseViewModel
    {
        //TODO : To Define Local Class Level Variables...
        private const string _emailRegex = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";
        public string AppBrandName = Preferences.Get("AppBrandName", string.Empty);
        public string AppName = Preferences.Get("AppName", string.Empty);
        bool showUpdateDialog = false;

        #region Constructor
        public LoginPageViewModel(INavigation nav)
        {
            Navigation = nav;
            ViewPasswordCommand = new Command(ViewPasswordAsync);
            ForgotPasswordCommand = new Command(ForgotPasswordAsync);
            SignInCommand = new Command(SignInAsync);
            RegisterCommand = new Command(RegisterAsync);
        }

        #endregion

        #region Command

        public Command ViewPasswordCommand { get; set; }
        public Command ForgotPasswordCommand { get; }
        public Command SignInCommand { get; }
        public Command RegisterCommand { get; }
        #endregion

        #region Properties

        private string _Email = "testusermed25@gmail.com";
        public string Email
        {
            get { return _Email; }
            set
            {
                if (_Email != value)
                {
                    _Email = value;
                    OnPropertyChanged("Email");
                }
            }
        }

        private string _Password = "SomePassword9!";
        public string Password
        {
            get { return _Password; }
            set
            {
                if (_Password != value)
                {
                    _Password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        private bool _isLoginFieldEmpty = true;
        public bool IsLoginFieldEmpty
        {
            get { return _isLoginFieldEmpty; }
            set
            {
                if (_isLoginFieldEmpty != value)
                {
                    _isLoginFieldEmpty = value;
                    OnPropertyChanged("IsLoginFieldEmpty");
                }
            }
        }

        private bool _IsPassword;
        public bool IsPassword
        {
            get { return _IsPassword; }
            set
            {
                if (_IsPassword != value)
                {
                    _IsPassword = value;
                    OnPropertyChanged("IsPassword");
                }
            }
        }

        #endregion

        #region Methods

        public async Task GetAppSettings()
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
                            showUpdateDialog = false;//bool showUpdateDialog = false;
                                                     //  PackageInfo pi = Application.Context.PackageManager.GetPackageInfo(Application.Context.PackageName, 0);
                            var pi = AppInfo.VersionString;
                            var AppName = await DataUtility.GetSiteSettingsAsync(SettingsValues.ApiURLValue).ConfigureAwait(false);
                            if (AppName != null)
                            {
                                Preferences.Set("AppBrandName", AppName.Brand);
                                Preferences.Set("AppName", AppName.ApplicationName);
                            }
                            if (pi != null)
                            {
                                showUpdateDialog = await DataUtility.IsAppVersionLessThanRecommendation(SettingsValues.ApiURLValue, pi, "Patient").ConfigureAwait(false); //<--This is Bryans update //await DataUtility.CheckIfLatestAppVersionAsync(SettingsValues.ApiURLValue, pi.VersionName).ConfigureAwait(false);
                            }

                            if (showUpdateDialog)
                            {
                            }
                            UserDialog.HideLoading();
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
        /// To DO: View Password Command
        /// </summary>
        /// <param name="obj"></param>
        private async void ViewPasswordAsync(object obj)
        {
            if (IsPassword == true)
                IsPassword = false;
            else
                IsPassword = true;
        }


        /// <summary>
        /// To define the forgot password command.
        /// </summary>
        /// <param name="obj"></param>
        private async void ForgotPasswordAsync(object obj)
        {
            await Navigation.PushModalAsync(new Views.ForgotPasswordPage(), false);
        }

        /// <summary>
        /// To define the signIn command.
        /// </summary>
        /// <param name="obj"></param>
        private async void SignInAsync(object obj)
        {
            if (!ValidateLogIn())
            {
                return;
            }
            //Call api..
            try
            {
                await Login();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// To call the login api.
        /// </summary>
        /// <returns></returns>
        public async Task Login()
        {
            UserDialog.ShowLoading();
            // Call login api..
            try
            {
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {
                    await Task.Run(async () =>
                    {
                        TokenResponse resp = await DataUtility.GetTokenResponseAsync(SettingsValues.ApiURLValue, Email, Password, "0").ConfigureAwait(false);
                        if (resp != null)
                        {
                            if (!string.IsNullOrEmpty(resp.error))
                            {
                                Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                                {
                                    if (resp.error.ToLower().Equals("invalid credentials"))
                                    {
                                        await UserDialogs.Instance.AlertAsync("Login failed. Invalid credentials.");
                                    }
                                    else if (resp.error.ToLower().Equals("too many failed logins"))
                                    {
                                    }
                                    else
                                    {
                                        await UserDialogs.Instance.AlertAsync("Information incorrect. Please review and try again.");
                                    }
                                });
                            }
                            else if (!string.IsNullOrEmpty(resp.access_token) && resp.expires_in != null)
                            {
                                Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                                {
                                    AndroidPatientAppMaui.Helpers.AppGlobalConstants.Token = resp.access_token;
                                    AndroidPatientAppMaui.Helpers.AppGlobalConstants.UserId = resp.userid;
                                    AndroidPatientAppMaui.Helpers.AppGlobalConstants.TokenExpirationDate = DateTime.Now.AddSeconds(Convert.ToInt32(resp.expires_in));

                                    AndroidPatientAppMaui.Helpers.AppGlobalConstants.LoginEmail = Email;

                                    Preferences.Set("AuthToken", resp.access_token);
                                    Preferences.Set("UserId", resp.userid);
                                    await Navigation.PushModalAsync(new Views.Account.EmailVerifyPage(), false);
                                });
                            }
                            else
                            {
                                await UserDialogs.Instance.AlertAsync("Login failed. No token received from server."); //TODO
                            }
                        }
                        else
                        {
                            await UserDialogs.Instance.AlertAsync("Login failed. No response from server."); //TODO
                        }

                    }).ConfigureAwait(false);
                }
                else
                {
                    UserDialogs.Instance.Loading().Hide();
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
        /// To define the register command.
        /// </summary>
        /// <param name="obj"></param>
        private async void RegisterAsync(object obj)
        {
            await Navigation.PushModalAsync(new Views.RegistrationPage(), false);
        }

        /// <summary>
        /// TODO : To Apply Login Validations...
        /// </summary>
        private bool ValidateLogIn()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrWhiteSpace(Email))
            {
                IsLoginFieldEmpty = false;
                return false;
            }
            //bool isValid4 = Regex.IsMatch(Email, _emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            //if (!isValid4)
            //{
            //    //UserDialogs.Instance.HideLoading();
            //    //CustomControls.ToastControl.ShowValidationToast("Email address is not valid.");
            //    return false;
            //}
            if (string.IsNullOrEmpty(Password) || string.IsNullOrWhiteSpace(Password))
            {
                IsLoginFieldEmpty = false;
                return false;
            }
            return true;
        }
        #endregion
    }
}
