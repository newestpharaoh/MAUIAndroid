using CommonLibraryCoreMaui.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.Home
{
    public class PatientPreVisitForMePageViewModel : BaseViewModel
    {
        //To define the class level variable.
        string Token = string.Empty;
        int Userid = 0;
        public AccountMember am;
        Patient selectedPatient;
        #region Constructor
        public PatientPreVisitForMePageViewModel(INavigation nav)
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

        private string _lblMemberName;
        public string lblMemberName
        {
            get { return _lblMemberName; }
            set
            {
                if (_lblMemberName != value)
                {
                    _lblMemberName = value;
                    OnPropertyChanged("lblMemberName");
                }
            }
        }

        private ObservableCollection<AccountMember> _AccountHolderList = new ObservableCollection<AccountMember>();
        public ObservableCollection<AccountMember> AccountHolderList
        {
            get { return _AccountHolderList; }
            set
            {
                if (_AccountHolderList != value)
                {
                    _AccountHolderList = value;
                    OnPropertyChanged("AccountHolderList");
                }
            }
        }
        #endregion

        #region Methods

        public async void LoadPatients()
        {
            // Get App settings api..
            try
            {
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
                /// <summary>
                /// To define the back button command.
                /// </summary>
                /// <param name="obj"></param>
                private async void ContinueAsync(object obj)
                {
                    try
                    {
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                #endregion
            }
}
