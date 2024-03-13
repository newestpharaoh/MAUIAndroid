using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.Home
{
    public class PatientPreVisitForSomeoneElseViewModel : BaseViewModel
    { //To define the class level variable.
        string Token = string.Empty;
        int Userid = 0;
        #region Constructor
        public PatientPreVisitForSomeoneElseViewModel(INavigation nav)
        {
            try
            {
                Navigation = nav;
                BackCommand = new Command(BackAsync);

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
        #endregion

        #region Properties 
        private bool _lytOtherPatient = false;
        public bool lytOtherPatient
        {
            get { return _lytOtherPatient; }
            set
            {
                if (_lytOtherPatient != value)
                {
                    _lytOtherPatient = value;
                    OnPropertyChanged("lytOtherPatient");
                }
            }
        }
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
        #endregion

    }
}
