using CommonLibraryCoreMaui;
using CommonLibraryCoreMaui.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.MyMedicalInfo
{
    public class MyMedicalInfoDetailsPageViewModel : BaseViewModel
    {
        //To define the class level variable.
        string Token = string.Empty;
        int PatientID = 0;
        int noneIndex;
        bool patientIsCurative = false;
        bool patientIsEligibleForCurative = false;
        public MedicalInfo medicalInfo;
        #region Constructor
        public MyMedicalInfoDetailsPageViewModel(INavigation nav)
        {
            try
            {
                Navigation = nav;
                BackCommand = new Command(BackAsync);
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
        private ObservableCollection<MedicalIssue> _MedicalIssuesList = new ObservableCollection<MedicalIssue>();
        public ObservableCollection<MedicalIssue> MedicalIssuesList
        {
            get { return _MedicalIssuesList; }
            set
            {
                if (_MedicalIssuesList != value)
                {
                    _MedicalIssuesList = value;
                    OnPropertyChanged("MedicalIssuesList");
                }
            }
        }
        private ObservableCollection<Allergy> _AllergiesList = new ObservableCollection<Allergy>();
        public ObservableCollection<Allergy> AllergiesList
        {
            get { return _AllergiesList; }
            set
            {
                if (_AllergiesList != value)
                {
                    _AllergiesList = value;
                    OnPropertyChanged("AllergiesList");
                }
            }
        }
        private ObservableCollection<Medication> _MedicationsList = new ObservableCollection<Medication>();
        public ObservableCollection<Medication> MedicationsList
        {
            get { return _MedicationsList; }
            set
            {
                if (_MedicationsList != value)
                {
                    _MedicationsList = value;
                    OnPropertyChanged("MedicationsList");
                }
            }
        }
        private ObservableCollection<Surgery> _SurgeryList = new ObservableCollection<Surgery>();
        public ObservableCollection<Surgery> SurgeryList
        {
            get { return _SurgeryList; }
            set
            {
                if (_SurgeryList != value)
                {
                    _SurgeryList = value;
                    OnPropertyChanged("SurgeryList");
                }
            }
        } 
        private string _UserName = Helpers.AppGlobalConstants.userInfo.Name;
        public string UserName
        {
            get { return _UserName; }
            set
            {
                if (_UserName != value)
                {
                    _UserName = value;
                    OnPropertyChanged("UserName");
                }
            }
        }
        private string _lblHeader = "Step 4 of 4";
        public string lblHeader
        {
            get { return _lblHeader; }
            set
            {
                if (_lblHeader != value)
                {
                    _lblHeader = value;
                    OnPropertyChanged("lblHeader");
                }
            }
        }
        private string _txtOtherMedicalIssue;
        public string txtOtherMedicalIssue
        {
            get { return _txtOtherMedicalIssue; }
            set
            {
                if (_txtOtherMedicalIssue != value)
                {
                    _txtOtherMedicalIssue = value;
                    OnPropertyChanged("txtOtherMedicalIssue");
                }
            }
        }
        private string _lblPCPSelected = "PCP Name";
        public string lblPCPSelected
        {
            get { return _lblPCPSelected; }
            set
            {
                if (_lblPCPSelected != value)
                {
                    _lblPCPSelected = value;
                    OnPropertyChanged("lblPCPSelected");
                }
            }
        }
        private string _lblPharmacySelected = "Pharmacy Name";
        public string lblPharmacySelected
        {
            get { return _lblPharmacySelected; }
            set
            {
                if (_lblPharmacySelected != value)
                {
                    _lblPharmacySelected = value;
                    OnPropertyChanged("lblPharmacySelected");
                }
            }
        }
        private bool _imgCapsule = false;
        public bool imgCapsule
        {
            get { return _imgCapsule; }
            set
            {
                if (_imgCapsule != value)
                {
                    _imgCapsule = value;
                    OnPropertyChanged("imgCapsule");
                }
            }
        }
        private bool _imgCurative = false;
        public bool imgCurative
        {
            get { return _imgCurative; }
            set
            {
                if (_imgCurative != value)
                {
                    _imgCurative = value;
                    OnPropertyChanged("imgCurative");
                }
            }
        }
        private bool _lblHeaderIsvisble = false;
        public bool lblHeaderIsvisble
        {
            get { return _lblHeaderIsvisble; }
            set
            {
                if (_lblHeaderIsvisble != value)
                {
                    _lblHeaderIsvisble = value;
                    OnPropertyChanged("lblHeaderIsvisble");
                }
            }
        }
        private bool _lytOtherMedicalIssueVisible = false;
        public bool lytOtherMedicalIssueVisible
        {
            get { return _lytOtherMedicalIssueVisible; }
            set
            {
                if (_lytOtherMedicalIssueVisible != value)
                {
                    _lytOtherMedicalIssueVisible = value;
                    OnPropertyChanged("lytOtherMedicalIssueVisible");
                }
            }
        }
        private bool _chkOtherMedicalIssue = false;
        public bool chkOtherMedicalIssue
        {
            get { return _chkOtherMedicalIssue; }
            set
            {
                if (_chkOtherMedicalIssue != value)
                {
                    _chkOtherMedicalIssue = value;
                    OnPropertyChanged("chkOtherMedicalIssue");
                }
            }
        }
        private bool _lytAddPCP = true;
        public bool lytAddPCP
        {
            get { return _lytAddPCP; }
            set
            {
                if (_lytAddPCP != value)
                {
                    _lytAddPCP = value;
                    OnPropertyChanged("lytAddPCP");
                }
            }
        }
        private bool _lytPCPSelected = false;
        public bool lytPCPSelected
        {
            get { return _lytPCPSelected; }
            set
            {
                if (_lytPCPSelected != value)
                {
                    _lytPCPSelected = value;
                    OnPropertyChanged("lytPCPSelected");
                }
            }
        }

        private bool _lytPharmacySelected = false;
        public bool lytPharmacySelected
        {
            get { return _lytPharmacySelected; }
            set
            {
                if (_lytPharmacySelected != value)
                {
                    _lytPharmacySelected = value;
                    OnPropertyChanged("lytPharmacySelected");
                }
            }
        }

        private bool _AllergiesIsVisible = false;
        public bool AllergiesIsVisible
        {
            get { return _AllergiesIsVisible; }
            set
            {
                if (_AllergiesIsVisible != value)
                {
                    _AllergiesIsVisible = value;
                    OnPropertyChanged("AllergiesIsVisible");
                }
            }
        }
        private bool _lstMedicationsIsVisible = false;
        public bool lstMedicationsIsVisible
        {
            get { return _lstMedicationsIsVisible; }
            set
            {
                if (_lstMedicationsIsVisible != value)
                {
                    _lstMedicationsIsVisible = value;
                    OnPropertyChanged("lstMedicationsIsVisible");
                }
            }
        }
        private bool _lblSuccess = false;
        public bool lblSuccess
        {
            get { return _lblSuccess; }
            set
            {
                if (_lblSuccess != value)
                {
                    _lblSuccess = value;
                    OnPropertyChanged("lblSuccess");
                }
            }
        }
        private bool _lytAddPharmacy;
        public bool lytAddPharmacy
        {
            get { return _lytAddPharmacy; }
            set
            {
                if (_lytAddPharmacy != value)
                {
                    _lytAddPharmacy = value;
                    OnPropertyChanged("lytAddPharmacy");
                }
            }
        }
        private bool _lytAddSurgery;
        public bool lytAddSurgery
        {
            get { return _lytAddSurgery; }
            set
            {
                if (_lytAddSurgery != value)
                {
                    _lytAddSurgery = value;
                    OnPropertyChanged("lytAddSurgery");
                }
            }
        }
        private bool _lytAddMedication;
        public bool lytAddMedication
        {
            get { return _lytAddMedication; }
            set
            {
                if (_lytAddMedication != value)
                {
                    _lytAddMedication = value;
                    OnPropertyChanged("lytAddMedication");
                }
            }
        }
        private bool _lytAddAllergy;
        public bool lytAddAllergy
        {
            get { return _lytAddAllergy; }
            set
            {
                if (_lytAddAllergy != value)
                {
                    _lytAddAllergy = value;
                    OnPropertyChanged("lytAddAllergy");
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
        public async Task LoadMedicalIssues()
        {
            try
            {
                List<MedicalIssue> issues = await DataUtility.GetMedicalIssuesAsync(SettingsValues.ApiURLValue).ConfigureAwait(false);
                if (issues != null)
                {
                    Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                    {
                        MedicalIssue none = issues.FirstOrDefault(x => x.Value.ToLower().Equals("none"));
                        if (none != null)
                        {
                            int noneIndex = issues.IndexOf(none);
                            issues.RemoveAt(noneIndex);
                            issues.Insert(0, none);
                        }

                        foreach (var issue in issues)
                        {
                            //if (medicalInfo != null)
                            //        {
                            //            if (medicalInfo.MedicalIssues.Contains(issue.ID)) chk.Checked = true;
                            //        }
                            MedicalIssuesList.Add(issue);
                        }
                        lytOtherMedicalIssueVisible = true;
                        CurativeCheckDTO respGetCurative = await DataUtility.GetCurativeEligibilityForHomeViewDialogAsync(SettingsValues.ApiURLValue, medicalInfo.PatientID, Token).ConfigureAwait(false);
                        patientIsCurative = respGetCurative.CurativeEligibilityForHomeViewDialog;
                        patientIsEligibleForCurative = respGetCurative.IsCurative;
                    });
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
