using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.Account
{
    public class VerificationCodePageViewModel : BaseViewModel
    {
        #region Constructor
        public VerificationCodePageViewModel(INavigation nav)
        {
            Navigation = nav;
            BackCommand = new Command(BackAsync);
            VerifySignInCommand = new Command(VerifySignInAsync);
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
        private void VerifySignInAsync(object obj)
        {
            if (!string.IsNullOrWhiteSpace(VerificationCode))
            {

            }
            else
            {
                VerificationCodeError = true;
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
            catch (Exception ex) { }
        }
        #endregion
    }
}
