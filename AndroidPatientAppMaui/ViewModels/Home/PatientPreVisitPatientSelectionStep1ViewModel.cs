using Acr.UserDialogs;
using CommonLibraryCoreMaui;
using CommonLibraryCoreMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.Home
{
    public class PatientPreVisitPatientSelectionStep1ViewModel : BaseViewModel
    {
        //To define the class level variable.
        string Token = string.Empty;
        int Userid = 0;
        public string AppName = Preferences.Get("AppName", string.Empty);
        #region Constructor
        public PatientPreVisitPatientSelectionStep1ViewModel(INavigation nav)
        {
            try
            {
                Navigation = nav;
                BackCommand = new Command(BackAsync);
                ContinueCommand = new Command(ContinueAsync);
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
        public Command BackCommand { get; set; }
        public Command ContinueCommand { get; set; }
        #endregion

        #region Properties 
        private string _lblNotInTexas = "Not in Texas?";
        public string lblNotInTexas
        {
            get { return _lblNotInTexas; }
            set
            {
                if (_lblNotInTexas != value)
                {
                    _lblNotInTexas = value;
                    OnPropertyChanged("lblNotInTexas");
                }
            }
        }
        private string _AppNametxt  ;
        public string AppNametxt
        {
            get { return _AppNametxt; }
            set
            {
                if (_AppNametxt != value)
                {
                    _AppNametxt = value;
                    OnPropertyChanged("AppNametxt");
                }
            }
        }
        private bool _rbtnVisitForMe = true;
        public bool rbtnVisitForMe
        {
            get { return _rbtnVisitForMe; }
            set
            {
                if (_rbtnVisitForMe != value)
                {
                    _rbtnVisitForMe = value;
                    OnPropertyChanged("rbtnVisitForMe");
                }
            }
        }
        private bool _rbtnVisitForSomeoneElse = false;
        public bool rbtnVisitForSomeoneElse
        {
            get { return _rbtnVisitForSomeoneElse; }
            set
            {
                if (_rbtnVisitForSomeoneElse != value)
                {
                    _rbtnVisitForSomeoneElse = value;
                    OnPropertyChanged("rbtnVisitForSomeoneElse");
                }
            }
        }
        private bool _btnContinue = false;
        public bool btnContinue
        {
            get { return _btnContinue; }
            set
            {
                if (_btnContinue != value)
                {
                    _btnContinue = value;
                    OnPropertyChanged("btnContinue");
                }
            }
        }
        private bool _chkAgree = false;
        public bool chkAgree
        {
            get { return _chkAgree; }
            set
            {
                if (_chkAgree != value)
                {
                    _chkAgree = value;
                    OnPropertyChanged("chkAgree");
                }
            }
        }
        #endregion

        #region Methods
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

        /// <summary>
        /// To define the back Continue command.
        /// </summary>
        /// <param name="obj"></param>
        private async void ContinueAsync(object obj)
        {
            try
            {
                if (rbtnVisitForMe)
                {
                    await Navigation.PushModalAsync(new Views.Home.PatientPreVisitForMePage(), false);

                }
                else if(rbtnVisitForSomeoneElse){
                    await Navigation.PushModalAsync(new Views.Home.PatientPreVisitForSomeoneElse(), false);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public async Task GetPaymentInformation()
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
                            AppNametxt = AppName;
                            StartVisitState startVisit = new StartVisitState(); 
                            if (startVisit.VisitForMe != null)
                            {
                                if ((bool)startVisit.VisitForMe)
                                {
                                    rbtnVisitForMe = true;
                                }
                                else
                                {
                                    rbtnVisitForSomeoneElse = true;
                                }

                                btnContinue = true;
                                chkAgree = true;
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
                Console.WriteLine(ex);
            }
        }
        #endregion 
    }
}
