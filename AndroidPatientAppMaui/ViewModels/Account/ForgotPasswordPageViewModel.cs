using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.Account
{
    public class ForgotPasswordPageViewModel : BaseViewModel
    {
        //TODO : To Define Local Class Level Variables...
        private const string _emailRegex = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";

        #region Constructor
        public ForgotPasswordPageViewModel(INavigation nav)
        {
            try
            {

                Navigation = nav;
                BackCommand = new Command(BackAsync);
                SubmitCommand = new Command(SubmitAsync);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
        #endregion

        #region Command
        public Command BackCommand { get; set; }
        public Command SubmitCommand { get; set; }
        #endregion

        #region Properties
        private string _Email;
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

        private string _FisrtName;
        public string FirstName
        {
            get { return _FisrtName; }
            set
            {
                if (_FisrtName != value)
                {
                    _FisrtName = value;
                    OnPropertyChanged("FisrtName");
                }
            }
        }

        private string _LastName;
        public string LastName
        {
            get { return _LastName; }
            set
            {
                if (_LastName != value)
                {
                    _LastName = value;
                    OnPropertyChanged("LastName");
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
        /// To Do: To define Submit command
        /// </summary>
        /// <param name="obj"></param>

        private async void SubmitAsync(object obj)
        {
            if (!ValidateForgot())
            {
                return;
            }
            //Call api..
            try
            {
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// TODO : To Apply Forgot Validations...
        /// </summary>
        private bool ValidateForgot()
        {
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrWhiteSpace(FirstName))
            {
                IsLoginFieldEmpty = false;
                return false;
            }
            if (string.IsNullOrEmpty(LastName) || string.IsNullOrWhiteSpace(LastName))
            {
                IsLoginFieldEmpty = false;
                return false;
            }
            if (string.IsNullOrEmpty(Email) || string.IsNullOrWhiteSpace(Email))
            {
                IsLoginFieldEmpty = false;
                return false;
            }
            return true;
        }
        #endregion
    }
}
