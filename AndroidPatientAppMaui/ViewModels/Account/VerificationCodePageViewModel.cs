using Acr.UserDialogs;
using CommonLibraryCoreMaui;
using CommonLibraryCoreMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.Account
{
    public class VerificationCodePageViewModel : BaseViewModel
    {
        //To define the class level variable.
        string Token = string.Empty;
        int Userid = 0;

        #region Constructor
        public VerificationCodePageViewModel(INavigation nav)
        {
            try
            {
                Navigation = nav;
                BackCommand = new Command(BackAsync);
                VerifySignInCommand = new Command(VerifySignInAsync);

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
        public Command BackCommand { get; }
        public Command VerifySignInCommand { get; }
        #endregion

        #region Properties
        private string _verificationCode;
        public string VerificationCode
        {
            get { return _verificationCode; }
            set
            {
                if (_verificationCode != value)
                {
                    _verificationCode = value;
                    OnPropertyChanged("VerificationCode");
                }
            }
        }

        private bool _verificationCodeError = false;
        public bool VerificationCodeError
        {
            get { return _verificationCodeError; }
            set
            {
                if (_verificationCodeError != value)
                {
                    _verificationCodeError = value;
                    OnPropertyChanged("VerificationCodeError");
                }
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// To define the verify sign In command.
        /// </summary>
        /// <param name="obj"></param>
        private async void VerifySignInAsync(object obj)
        {
            try
            {
                App.Current.MainPage = new Views.MainTabs.MainTabPage("Home");
                //if (!string.IsNullOrWhiteSpace(VerificationCode))
                //{
                //    GlobalState global;
                //    if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                //    {
                //        UserDialog.ShowLoading();
                //        await Task.Run(async () =>
                //        {
                //            Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                //            {
                //                StatusResponse resp = await DataUtility.UserAccountAuthenticateCode(SettingsValues.ApiURLValue, Userid, VerificationCode);

                //                if (resp != null)
                //                {
                //                    if (string.IsNullOrEmpty(resp.ErrorMessage))
                //                    {
                //                        if (resp.StatusCode == StatusCode.Success)
                //                        {
                //                            App.Current.MainPage = new Views.MainTabs.MainTabPage();
                //                        }
                //                        else if (!string.IsNullOrEmpty(resp.Message))
                //                        {
                //                            await UserDialogs.Instance.AlertAsync(resp.Message, "Verification failed", "Ok");
                //                        }
                //                    }
                //                    else
                //                    {
                //                        await UserDialogs.Instance.AlertAsync(resp.ErrorMessage, "Verification failed", "Ok");
                //                    }
                //                }
                //                else
                //                {
                //                    await UserDialogs.Instance.AlertAsync("Verification failed. No response from server.");
                //                }
                //            });
                //        }).ConfigureAwait(false);
                //    }
                //    else
                //    {
                //        UserDialogs.Instance.HideLoading();
                //        await App.Current.MainPage.DisplayAlert("", "No Network Connection found, Please Connect to Internet first.", "OK");
                //    }
                //    UserDialog.HideLoading();
                //}
                //else
                //{
                //    VerificationCodeError = true;
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// To define the back button command.
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
