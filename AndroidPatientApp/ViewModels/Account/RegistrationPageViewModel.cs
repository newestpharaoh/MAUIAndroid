using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientApp.ViewModels.Account
{
    public class RegistrationPageViewModel : BaseViewModel
    {
        //TODO : To Define Local Class Level Variables...

        #region Constructor
        public RegistrationPageViewModel(INavigation nav)
        {
            try
            {
                Navigation = nav;
                RegisterCommand = new Command(RegisterAsync);
                BackCommand = new Command(BackAsync);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        
        }


        #endregion

        #region Command
        public Command RegisterCommand { get; set; }
        public Command BackCommand { get; set; }
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
        /// To Do: To define Register command
        /// </summary>
        /// <param name="obj"></param>
        public async void RegisterAsync(object obj)
        {

            if (!ValidateRegister())
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
        /// TODO : To Apply Register Validations...
        /// </summary>
        private bool ValidateRegister()
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
                return true;
            }
            
        }
        #endregion
    }


