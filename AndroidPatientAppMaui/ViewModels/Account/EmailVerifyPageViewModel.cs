using Acr.UserDialogs;
using AndroidPatientAppMaui.Helpers;
using CommonLibraryCoreMaui;
using CommonLibraryCoreMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace AndroidPatientAppMaui.ViewModels.Account
{
    public class EmailVerifyPageViewModel : BaseViewModel
    {
        //To define the class level variable.
        string Token = string.Empty;
        int Userid = 0;

        #region Constructor
        public EmailVerifyPageViewModel(INavigation nav)
        {
            try
            {
                Navigation = nav;
                BackCommand = new Command(BackAsync);
                SendCodeCommand = new Command(SendCodeAsync);

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
        public Command SendCodeCommand { get; }
        public Command BackCommand { get; }
        #endregion

        #region Properties

        private string _sendingCodeVia = "text";
        public string SendingCodeVia
        {
            get { return _sendingCodeVia; }
            set
            {
                if (_sendingCodeVia != value)
                {
                    _sendingCodeVia = value;
                    OnPropertyChanged("SendingCodeVia");
                }
            }
        }

        private string _phoneNumber;
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

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged("Email");
                }
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// TODO : To define the Get User Contact.....
        /// </summary>
        /// <returns></returns>
        public async Task GetUserContactAsync()
        {
            try
            {
                UserContactPreference userContactPreference = await DataUtility.GetUserContactPreferenceAsync(SettingsValues.ApiURLValue, Token, Userid);
                if (userContactPreference != null)
                {
                    PhoneNumber = userContactPreference.Phone;
                    Email = userContactPreference.Email;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// To define the send code button command.
        /// </summary>
        /// <param name="obj"></param>
        private async void SendCodeAsync(object obj)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(SendingCodeVia))
                {
                    if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                    {
                        UserDialog.ShowLoading();
                        await Task.Run(async () =>
                        {
                            Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                            {
                                StatusResponse resp = await DataUtility.UserAccountSendCode(SettingsValues.ApiURLValue, Userid, SendingCodeVia.ToLower() == "email" ? "email" : "text", "", null);
                                if (resp != null)
                                {
                                    if (string.IsNullOrEmpty(resp.ErrorMessage))
                                    {
                                        if (resp.StatusCode == StatusCode.Success)
                                        {
                                            await Navigation.PushModalAsync(new Views.Account.VerificationCodePage(), false);
                                        }
                                        else if (resp.StatusCode == StatusCode.NotFound)
                                        {
                                            await UserDialogs.Instance.AlertAsync("An error occurred when trying to send you a verification code. If the error continues try selecting the opposite communication option.");
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(resp.Message))
                                            {
                                                await UserDialogs.Instance.AlertAsync($"Verification code request failed. Reason: {resp.Message}");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        await UserDialogs.Instance.AlertAsync(resp.ErrorMessage);
                                    }
                                }
                                else
                                {
                                    await UserDialogs.Instance.AlertAsync("Verification failed. No response from server.");
                                }
                            });
                        }).ConfigureAwait(false);
                    }
                    else
                    {
                        UserDialogs.Instance.HideLoading();
                        await App.Current.MainPage.DisplayAlert("", "No Network Connection found, Please Connect to Internet first.", "OK");
                    }
                }
                else
                {
                    await UserDialogs.Instance.AlertAsync("Please select the option for sending the verification code.", "Alert", "Ok");
                }
                UserDialog.HideLoading();
            }
            catch (Exception ex)
            {
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
