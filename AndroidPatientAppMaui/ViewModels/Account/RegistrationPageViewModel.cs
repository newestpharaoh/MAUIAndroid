using CommonLibraryCoreMaui;
using CommonLibraryCoreMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.Account
{
    public class RegistrationPageViewModel : BaseViewModel
    {
        //TODO : To Define Local Class Level Variables...
        DateTime dtDOB;
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
        private DateTime _txtDOB;
        public DateTime txtDOB
        {
            get => _txtDOB;
            set
            {
                if (_txtDOB != value)
                {
                    _txtDOB = value;
                    OnPropertyChanged("txtDOB");
                }
            }
        }
        #endregion

        #region Methods

        public async Task GetDeatils()
        {
            try
            {
                RegistrationState reg = new RegistrationState();
                if (!string.IsNullOrEmpty(reg.DOB))
                {
                    dtDOB = DateTime.Parse(reg.DOB);
                    txtDOB = txtDOB = dtDOB;
                }
                else
                {
                    txtDOB = new DateTime(1980, 1, 1);
                }
                FirstName = !string.IsNullOrEmpty(reg.FirstName) ? reg.FirstName : string.Empty;
                LastName = !string.IsNullOrEmpty(reg.LastName) ? reg.LastName : string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

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
                StatusResponse resp = await DataUtility.FindPatientAsync(SettingsValues.ApiURLValue, FirstName, LastName, txtDOB.ToString()).ConfigureAwait(false);
                if (resp != null)
                {
                    RegistrationState reg = new RegistrationState();

                    switch (resp.StatusCode)
                    {
                        case StatusCode.Payload:
                            if (!string.IsNullOrEmpty(resp.Payload))
                            {
                                //  UtilsUI.ShowMsgOkScreen(this, resp.Payload);
                            }
                            break;
                        case StatusCode.NotFound:
                        case StatusCode.NoActiveMatchFoundButEMRMatchFound:
                            // intent = new Intent(this, typeof(PlanSelectionActivity));
                            reg.FirstName = FirstName;
                            reg.LastName = LastName;
                            reg.DOB = txtDOB.ToString("MM/dd/yyyy");
                            reg.IsSelfPay = true;
                            reg.MPI = string.IsNullOrEmpty(resp.Payload) ? null : resp.Payload;
                            Helpers.AppGlobalConstants.RegistrationRespModel = reg;
                            //using (RegistrationStateHelper registrationStateHelper = new RegistrationStateHelper(this))
                            //{
                            //    registrationStateHelper.SetState(reg);
                            //}
                            break;
                        case StatusCode.InActiveMatchFound:

                            if (SettingsValues.ECommerce)
                            {
                                //intent = new Intent(this, typeof(PlanSelectionActivity));
                                reg.FirstName = FirstName;
                                reg.LastName = LastName;
                                reg.DOB = txtDOB.ToString("MM/dd/yyyy");
                                reg.IsSelfPay = true;
                                reg.MPI = string.IsNullOrEmpty(resp.Payload) ? null : resp.Payload;
                                Helpers.AppGlobalConstants.RegistrationRespModel = reg;
                                //using (RegistrationStateHelper registrationStateHelper = new RegistrationStateHelper(this))
                                //{
                                //    registrationStateHelper.SetState(reg);
                                //}
                            }
                            else
                            {
                                //intent = new Intent(this, typeof(PatientRegistrationNotPrimaryAccountHolderActivity));
                            }
                            break;
                        case StatusCode.EmailAlreadyInUse:
                        case StatusCode.AlreadyRegistered:
                            // intent = new Intent(this, typeof(PatientRegistrationSingleMatchFoundActivity));
                            break;
                        case StatusCode.ActivationEmailSent:
                            //intent = new Intent(this, typeof(PatientRegistrationActivationEmailSentActivity));
                            //intent.PutExtra("emailAddress", resp.Payload);
                            //intent.PutExtra("phoneNumber", resp.Payload2);
                            break;
                        case StatusCode.NotPolicyHolder:
                            // intent = new Intent(this, typeof(PatientRegistrationNotPolicyHolderActivity));
                            break;
                        case StatusCode.MultipleMatches:
                            //  intent = new Intent(this, typeof(PatientRegistrationMultipleRecordsFoundActivity));
                            break;
                        case StatusCode.MustBe18ToRegister:
                            UserDialog.Alert("Must be 18 years old to register");
                            break;
                    }
                }
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
            if (string.IsNullOrEmpty(txtDOB.ToString()) || string.IsNullOrWhiteSpace(txtDOB.ToString()))
            {
                IsLoginFieldEmpty = false;
                return false;
            }
            return true;
        }

    }
    #endregion
}


