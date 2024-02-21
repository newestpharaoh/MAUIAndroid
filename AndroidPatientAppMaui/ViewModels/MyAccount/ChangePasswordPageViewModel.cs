﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.MyAccount
{
    public class ChangePasswordPageViewModel :BaseViewModel
    {
        #region Constructor
        public ChangePasswordPageViewModel(INavigation nav)
        {
            Navigation = nav;
            BackCommand = new Command(BackAsync);
            ViewCurrentPasswordCommand = new Command(ViewCurrentPasswordAsync);
            ViewNewPasswordCommand = new Command(ViewNewPasswordAsync);
            ViewConfirmPasswordCommand = new Command(ViewConfirmPasswordAsync);
            ConfirmNewPasswordCommand = new Command(ConfirmNewPasswordAsync);
        }
        #endregion

        #region Command
        public Command BackCommand { get; set; }
        public Command ViewCurrentPasswordCommand { get; set; }
        public Command ViewNewPasswordCommand { get; set; }
        public Command ViewConfirmPasswordCommand { get; set; }
        public Command ConfirmNewPasswordCommand { get; set; }
        #endregion

        #region Properties

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

        private bool _IsCurrentPassword;
        public bool IsCurrentPassword
        {
            get { return _IsCurrentPassword; }
            set
            {
                if (_IsCurrentPassword != value)
                {
                    _IsCurrentPassword = value;
                    OnPropertyChanged("IsCurrentPassword");
                }
            }
        }

        private bool _IsNewPassword;
        public bool IsNewPassword
        {
            get { return _IsNewPassword; }
            set
            {
                if (_IsNewPassword != value)
                {
                    _IsNewPassword = value;
                    OnPropertyChanged("IsNewPassword");
                }
            }
        }

        private bool _IsConfirmPassword;
        public bool IsConfirmPassword
        {
            get { return _IsConfirmPassword; }
            set
            {
                if (_IsConfirmPassword != value)
                {
                    _IsConfirmPassword = value;
                    OnPropertyChanged("IsConfirmPassword");
                }
            }
        }

        private string _CurrentPassword;
        public string CurrentPassword
        {
            get { return _CurrentPassword; }
            set
            {
                if (_CurrentPassword != value)
                {
                    _CurrentPassword = value;
                    OnPropertyChanged("CurrentPassword");
                }
            }
        }

        private string _NewPassword;
        public string NewPassword
        {
            get { return _NewPassword; }
            set
            {
                if (_NewPassword != value)
                {
                    _NewPassword = value;
                    OnPropertyChanged("NewPassword");
                }
            }
        }

        private string _ConfirmPassword;
        public string ConfirmPassword
        {
            get { return _ConfirmPassword; }
            set
            {
                if (_ConfirmPassword != value)
                {
                    _ConfirmPassword = value;
                    OnPropertyChanged("ConfirmPassword");
                }
            }
        }
        #endregion

        #region Methods
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
        /// To DO: Current Password Command
        /// </summary>
        /// <param name="obj"></param>
        private  void ViewCurrentPasswordAsync(object obj)
        {
            if (IsCurrentPassword == true)
                IsCurrentPassword = false;
            else
                IsCurrentPassword = true;
        }

        /// <summary>
        /// To DO: New Password Command
        /// </summary>
        /// <param name="obj"></param>
        private  void ViewNewPasswordAsync(object obj)
        {
            if (IsNewPassword == true)
                IsNewPassword = false;
            else
                IsNewPassword = true;
        }

        /// <summary>
        /// To DO: New Password Command
        /// </summary>
        /// <param name="obj"></param>
        private void ViewConfirmPasswordAsync(object obj)
        {
            if (IsConfirmPassword == true)
                IsConfirmPassword = false;
            else
                IsConfirmPassword = true;
        }

        /// <summary>
        /// To Do: To define Save Changes command
        /// </summary>
        /// <param name="obj"></param>
        private  void ConfirmNewPasswordAsync(object obj)
        {
            if (!ValidateNewPassword())
            {
                return;
            }
            //Call api..
            try
            {
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// TODO : To Apply Password Validations...
        /// </summary>
        private bool ValidateNewPassword()
        {
            if (string.IsNullOrEmpty(CurrentPassword) || string.IsNullOrWhiteSpace(CurrentPassword))
            {
                IsLoginFieldEmpty = false;
                return false;
            }
            if (string.IsNullOrEmpty(NewPassword) || string.IsNullOrWhiteSpace(NewPassword))
            {
                IsLoginFieldEmpty = false;
                return false;
            }
            if (string.IsNullOrEmpty(ConfirmPassword) || string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                IsLoginFieldEmpty = false;
                return false;
            }
            if (CurrentPassword != NewPassword)
            {
                return false;
            }
            return true;
        }

    }
    #endregion
}

