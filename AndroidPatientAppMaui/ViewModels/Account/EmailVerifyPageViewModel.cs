using AndroidPatientAppMaui.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.Account
{
    public class EmailVerifyPageViewModel : BaseViewModel
    {
        #region Constructor
        public EmailVerifyPageViewModel(INavigation nav)
        {
            Navigation = nav;
            BackCommand = new Command(BackAsync);
            SendCodeCommand = new Command(SendCodeAsync);
        }

        #endregion

        #region Command
        public Command SendCodeCommand { get; }
        public Command BackCommand { get; }
        #endregion

        #region Properties

        private string _sendingCodeVia = "Text";
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

        private string _phoneNumber = "9165485555";
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

        private string _email = AppGlobalConstants.LoginEmail;
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

        private string _resultEmail;
        public string ResultEmail
        {
            get { return _resultEmail; }
            set
            {
                if (_resultEmail != value)
                {
                    _resultEmail = value;
                    OnPropertyChanged("ResultEmail");
                }
            }
        }

        private string _resultPhonenumber;
        public string ResultPhonenumber
        {
            get { return _resultPhonenumber; }
            set
            {
                if (_resultPhonenumber != value)
                {
                    _resultPhonenumber = value;
                    OnPropertyChanged("ResultPhonenumber");
                }
            }
        }

        #endregion

        #region Methods

        public async Task MaskEmail()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Email))
                {
                    string pattern = @"(?<=[\w]{0})[\w\-._\+%]*(?=[\w]{1}@)";
                    ResultEmail = Regex.Replace(Email, pattern, m => new string('*', m.Length));
                }
            }
            catch (Exception ex)
            {
            }
        }

        public async Task MaskPhoneNumber()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(PhoneNumber))
                {
                    ResultPhonenumber = "***-***-" + PhoneNumber.Substring(PhoneNumber.Length - 4);
                }
            }
            catch (Exception ex)
            {
            }
        }



        /// <summary>
        /// To define the send code button command.
        /// </summary>
        /// <param name="obj"></param>
        private async void SendCodeAsync(object obj)
        {
            await Navigation.PushModalAsync(new Views.Account.VerificationCodePage(), false);
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
            catch (Exception ex) { }
        }
        #endregion
    }
}
