using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Acr.UserDialogs.Infrastructure;
using CommonLibraryCoreMaui;
using CommonLibraryCoreMaui.Models;
using DotLiquid;

namespace AndroidPatientAppMaui.ViewModels.MyAccount
{
    public class UpdateCardInformationPageViewModel : BaseViewModel
    {
        // To Define local Class Level variable
        string providingCareInTexasString;
        public AccountCreditCard card;
        bool reactivate = false;
        string inStateNoticeString;
        public string AppName = Preferences.Get("AppName", string.Empty);
        UserInfo userInfo;
        string Token = string.Empty;
        int PatientID = 0;

        #region Constructor
        public UpdateCardInformationPageViewModel(INavigation nav)
        {
            Navigation = nav;
            BackCommand = new Command(BackAsync);
            SaveChangesCommand = new Command(SaveChangesAsync);
            Token = Preferences.Get("AuthToken", string.Empty);
            PatientID = Preferences.Get("PatientID", 0);
        }
        #endregion

        #region Command
        public Command BackCommand { get; set; }
        public Command SaveChangesCommand { get; set; }
        #endregion

        #region Properties
        private List<string> _StatesList = CommonLibraryCoreMaui.Theme.Values.States;
        public List<string> StatesList
        {
            get { return _StatesList; }
            set
            {
                if (_StatesList != value)

                {
                    _StatesList = value;
                    OnPropertyChanged("StatesList");
                }
            }
        }

        private List<string> _CCYearsList = CommonLibraryCoreMaui.Theme.Values.CCYears;
        public List<string> CCYearsList
        {
            get { return _CCYearsList; }
            set
            {
                if (_CCYearsList != value)

                {
                    _CCYearsList = value;
                    OnPropertyChanged("CCYearsList");
                }
            }
        }

        private List<string> _MonthsNumericList = CommonLibraryCoreMaui.Theme.Values.MonthsNumeric;
        public List<string> MonthsNumericList
        {
            get { return _MonthsNumericList; }
            set
            {
                if (_MonthsNumericList != value)

                {
                    _MonthsNumericList = value;
                    OnPropertyChanged("MonthsNumericList");
                }
            }
        }
        private string _lblAttestationCI = "Providing Care for Patients Currently in Texas: In compliance with governing rules and regulations, physicians can only prescribe for and treat* patients currently located within the state of Texas at the time of service.";
        public string lblAttestationCI
        {
            get { return _lblAttestationCI; }
            set
            {
                if (_lblAttestationCI != value)
                {
                    _lblAttestationCI = value;
                    OnPropertyChanged("lblAttestationCI");
                }
            }
        }

        private string _lblHsafsaDisclaimer = "HSA/FSA Disclaimer: eMD Access cannot guarantee HSA/FSA eligibility or reimbursement. At this time, we cannot accept HSA/FSA cards as payment for services.";

        public string lblHsafsaDisclaimer
        {
            get { return _lblHsafsaDisclaimer; }
            set
            {
                if (_lblHsafsaDisclaimer != value)
                {
                    _lblHsafsaDisclaimer = value;
                    OnPropertyChanged("lblHsafsaDisclaimer");
                }
            }
        }

        //private string _lblEmailAssociatedWithPayment = "Email Associated With Payment :testusermd@gmail.com";

        //public string lblEmailAssociatedWithPayment
        //{
        //    get { return _lblEmailAssociatedWithPayment; }
        //    set
        //    {
        //        if (_lblEmailAssociatedWithPayment != value)
        //        {
        //            _lblEmailAssociatedWithPayment = value;
        //            OnPropertyChanged("lblEmailAssociatedWithPayment");
        //        }
        //    }
        //}
        private string _txtFirstName;
        public string txtFirstName
        {
            get { return _txtFirstName; }
            set
            {
                if (_txtFirstName != value)
                {
                    _txtFirstName = value;
                    OnPropertyChanged("txtFirstName");
                }
            }
        }

        private string _txtLastName;
        public string txtLastName
        {
            get { return _txtLastName; }
            set
            {
                if (_txtLastName != value)
                {
                    _txtLastName = value;
                    OnPropertyChanged("txtLastName");
                }
            }
        }

        private string _txtCreditCardNumber;
        public string txtCreditCardNumber
        {
            get { return _txtCreditCardNumber; }
            set
            {
                if (_txtCreditCardNumber != value)
                {
                    _txtCreditCardNumber = value;
                    OnPropertyChanged("txtCreditCardNumber");
                }
            }
        }

        private string _txtSecurityCode;
        public string txtSecurityCode
        {
            get { return _txtSecurityCode; }
            set
            {
                if (_txtSecurityCode != value)
                {
                    _txtSecurityCode = value;
                    OnPropertyChanged("txtSecurityCode");
                }
            }
        }

        private string _txtAddress;
        public string txtAddress
        {
            get { return _txtAddress; }
            set
            {
                if (_txtAddress != value)
                {
                    _txtAddress = value;
                    OnPropertyChanged("txtAddress");
                }
            }
        }

        private string _txtCity;
        public string txtCity
        {
            get { return _txtCity; }
            set
            {
                if (_txtCity != value)
                {
                    _txtCity = value;
                    OnPropertyChanged("txtCity");
                }
            }
        }

        private string _txtZipCode;
        public string txtZipCode
        {
            get { return _txtZipCode; }
            set
            {
                if (_txtZipCode != value)
                {
                    _txtZipCode = value;
                    OnPropertyChanged("txtZipCode");
                }
            }
        }

        private string _CardExpirationMonthLbl;
        public string CardExpirationMonthLbl
        {
            get { return _CardExpirationMonthLbl; }
            set
            {
                if (_CardExpirationMonthLbl != value)
                {
                    _CardExpirationMonthLbl = value;
                    OnPropertyChanged("CardExpirationMonthLbl");
                }
            }
        }

        private string _CardExpirationYearLbl;
        public string CardExpirationYearLbl
        {
            get { return _CardExpirationMonthLbl; }
            set
            {
                if (_CardExpirationYearLbl != value)
                {
                    _CardExpirationMonthLbl = value;
                    OnPropertyChanged("CardExpirationYearLbl");
                }
            }
        }

        private string _BillingStateLbl;
        public string BillingStateLbl
        {
            get { return _BillingStateLbl; }
            set
            {
                if (_BillingStateLbl != value)
                {
                    _BillingStateLbl = value;
                    OnPropertyChanged("BillingStateLbl");
                }
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// ToDo: To define the back command
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
        /// ToDo: To define the Save command
        /// </summary>
        /// <param name="obj"></param>

        private  async void SaveChangesAsync(object obj)
        {
            try
            {
                if (card != null)
                {
                   // HideErrorLabel();

                    if (string.IsNullOrEmpty(txtFirstName.Trim()) ||
                        string.IsNullOrEmpty(txtLastName.Trim()) ||
                        string.IsNullOrEmpty(txtCreditCardNumber.Trim()) ||
                        string.IsNullOrEmpty(txtCity.Trim()) ||
                        string.IsNullOrEmpty(txtAddress.Trim()) ||
                        string.IsNullOrEmpty(txtSecurityCode.Trim()) ||
                       string.IsNullOrEmpty(txtZipCode.Trim()))
                    {
                        UserDialog.Alert("Please fill all the required fields!");
                    }
                    else
                    {
                        card.BillingAddress = txtAddress;
                        card.BillingCity = txtCity;
                        card.BillingZip = txtZipCode;
                        card.CardFirstName = txtFirstName;
                        card.CardLastName = txtLastName;
                        card.CardNumber = txtCreditCardNumber;
                        card.CardSecurityCode = txtSecurityCode;
                        UserDialog.ShowLoading();
                        await Task.Run(async () =>
                        {
                            StatusResponse resp = await DataUtility.PatientUpdateCreditCardInfoAsync(SettingsValues.ApiURLValue, Token, card).ConfigureAwait(false);
                            if (resp != null)
                            {
                                switch (resp.StatusCode)
                                {
                                    case StatusCode.InvalidCreditCard:
                                        UserDialog.Alert("Invalid Credit Card");
                                        break;

                                    case StatusCode.Success:
                                        UserDialog.Alert("Your changes have been saved.");
                                        if (reactivate)
                                        {
                                            Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                                            {
                                               // StartActivity(new Intent(this, typeof(SubscriptionReactivateActivity)));
                                              //  Finish();
                                            });
                                        }
                                        else
                                        {
                                            await GetCreditCarddInfo().ConfigureAwait(false);
                                        }
                                        break;
                                    case StatusCode.CardInformationNotProvided:
                                        UserDialog.Alert("To update your card information you must re-enter your credit card number and security code.");
                                        break;
                                    case StatusCode.Error:
                                        if (!string.IsNullOrEmpty(resp.Message)) UserDialog.Alert(resp.Message);
                                        break;
                                    case StatusCode.PaymentErrorSeePayload:
                                        if (!string.IsNullOrEmpty(resp.Payload)) UserDialog.Alert(resp.Payload);
                                        break;
                                }
                            }
                            else
                            {
                                UserDialogs.Instance.HideLoading();
                                await App.Current.MainPage.DisplayAlert("", "No Network Connection found, Please Connect to Internet first.", "OK");
                            }
                            UserDialog.HideLoading();

                        });
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        /// <summary>
        /// TODO : To Define GetCreditCradInfo...
        /// </summary>
        /// <returns></returns>
        public async Task GetCreditCarddInfo()
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
                            try
                            {
                                UITopic GetUITopicResponse = await Globals.Instance.GetUTText("EnterPaymentInformation", "en");

                                if (GetUITopicResponse != null)
                                {
                                    providingCareInTexasString = GetUITopicResponse.UITextList.Find(i => i.TagName == "ProvidingCareInTexas").Text; //Header
                                    inStateNoticeString = GetUITopicResponse.UITextList.Find(i => i.TagName == "InStateNotice").Text; //Paragraph
                                    inStateNoticeString = inStateNoticeString.Replace("treat*", "treat"); //InStateNotice.replace("treat*", "treat")
                                    Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                                    {
                                        var inStateConcatenatedString = providingCareInTexasString + ": " + inStateNoticeString;
                                        var inStateFormatted = inStateConcatenatedString.Replace("{0}", AppName);//var inStateFormatted = String.Format(inStateConcatenatedString, AppName);
                                        lblAttestationCI = inStateFormatted;
                                        CommonLibraryCoreMaui.Helper.ColorText? ct1 = CommonLibraryCoreMaui.Helper.StringColorHelper.GetColorText(inStateFormatted);
                                        if (ct1.HasValue)
                                        {

                                            //lblAttestationCI.SetText(GetCaption((CommonLibrary.Helper.ColorText)ct1), TextView.BufferType.Spannable);
                                        }
                                    });
                                }

                                var cc = await DataUtility.PatientGetCreditCardInfoAsync(SettingsValues.ApiURLValue, PatientID, Token).ConfigureAwait(false);

                                if (cc != null)
                                {
                                    card = cc;
                                    Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                                    {
                                        txtFirstName = card.CardFirstName;
                                        txtLastName = card.CardLastName;
                                        if (!string.IsNullOrEmpty(card.CardLastName))
                                        {
                                            txtZipCode = card.BillingZip;
                                        }

                                        txtCreditCardNumber = card.CardNumber;
                                        txtCity = card.BillingCity;
                                        txtSecurityCode = card.CardSecurityCode;
                                        txtAddress = card.BillingAddress;
                                        BillingStateLbl = card.BillingState;
                                        CardExpirationMonthLbl = card.CardExpirationMonth;
                                        CardExpirationYearLbl = card.CardExpirationYear;
                                        //((StateSpinner)spnrState).SelectState(card.BillingState);

                                        //int cardExpriationMonthIndex = Array.FindIndex(arrMonth, m => m == card.CardExpirationMonth.PadLeft(2, Convert.ToChar("0")));
                                        //if (cardExpriationMonthIndex == -1)
                                        //{
                                        //    cardExpriationMonthIndex = Array.FindIndex(arrMonth, m => m == DateTime.Now.Month.ToString("D2"));
                                        //}
                                        //spnrCardExpirationMonth.SetSelection(cardExpriationMonthIndex);

                                        //int cardExpirationYearIndex = Array.FindIndex(arrYear, y => y == card.CardExpirationYear.PadLeft(2, Convert.ToChar("0")));
                                        //if (cardExpirationYearIndex == -1)
                                        //{
                                        //    cardExpirationYearIndex = Array.FindIndex(arrYear, m => m == DateTime.Now.Year.ToString().Substring(2, 2));
                                        //}
                                        //spnrCardExpirationYear.SetSelection(cardExpirationYearIndex);
                                    });
                                }
                                else
                                {
                                    Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                                    {
                                        //int cardExpirationMonthIndex = Array.FindIndex(arrMonth, m => m == DateTime.Now.Month.ToString("D2"));
                                        //spnrCardExpirationMonth.SetSelection(cardExpirationMonthIndex);

                                        //int cardExpirationYearIndex = Array.FindIndex(arrYear, m => m == DateTime.Now.Year.ToString().Substring(2, 2));
                                        //spnrCardExpirationYear.SetSelection(cardExpirationYearIndex);
                                    });
                                }
                            }
                            catch (Exception ex)
                            {

                            }
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
        #endregion
    }
}
