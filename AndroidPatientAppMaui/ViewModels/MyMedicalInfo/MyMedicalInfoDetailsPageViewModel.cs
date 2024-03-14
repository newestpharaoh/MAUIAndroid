using Acr.UserDialogs;
using CommonLibraryCoreMaui;
using CommonLibraryCoreMaui.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AndroidPatientAppMaui.ViewModels.MyMedicalInfo
{
    public class MyMedicalInfoDetailsPageViewModel : BaseViewModel
    {
        //To define the class level variable.
        string Token = string.Empty;
        int PatientID = 0;
        int noneIndex;
        public bool patientIsCurative = false;
        public bool patientIsEligibleForCurative = false;
        public MedicalInfo medicalInfo;
        public AdditionalFamilyMember additionalFamilyMember;
        List<MedicalIssue> issues;

        public Allergy allergy;
        public Medication medication;
        public Surgery surgery;
        public Pharmacy pharmacy;
        public PCP pcp;

        public int PCP_REQUEST_CODE = 2;
        public int SURGERY_REQUEST_CODE = 3;
        public int MEDICATION_REQUEST_CODE = 4;
        public int ALLERGY_REQUEST_CODE = 5;
        public int PHARMACY_REQUEST_CODE = 6;

        #region Constructor
        public MyMedicalInfoDetailsPageViewModel(INavigation nav)
        {
            try
            {
                Navigation = nav;
                BackCommand = new Command(BackAsync);
                lytAddSurgeryCommand = new Command(lytAddSurgeryAsync);
                lytAddPCPCommand = new Command(lytAddPCPAsync);
                lytAddPharmacyCommand = new Command(lytAddPharmacyAsync);
                lytAddAllergyCommand = new Command(lytAddAllergyAsync);
                lytAddMedicationCommand = new Command(lytAddMedicationAsync);
                imgEditPCPCommand = new Command(imgEditPCPAsync);
                ImgDeletePCPCommand = new Command(ImgDeletePCPAsync);
                imgDeletePharmacyCommand = new Command(imgDeletePharmacyAsync);
                imgEditPharmacyCommand = new Command(imgEditPharmacyAsync);
                AllergySaveCommand = new Command(AllergySaveAsync);
                MedicationSaveCommand = new Command(MedicationSaveAsync);
                SurgurySaveCommand = new Command(SurgurySaveAsync);
                PharmacySaveCommand = new Command(PharmacySaveAsync);
                PCPSaveCommand = new Command(PCPSaveAsync);
                PCPCancelCommand = new Command(PCPCancelAsync);
                PCPSearchCommand = new Command(PCPSearchAsync);
                BtnContinueUpdateCommand = new Command(BtnContinueUpdateAsync);
                BtnContinueRegistrationCommand = new Command(BtnContinueRegistrationAsync);
                BtnAddFamilyMemberCommand = new Command(BtnAddFamilyMemberAsync);


                Token = Preferences.Get("AuthToken", string.Empty);
                PatientID = Preferences.Get("PatientID", 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #endregion

        #region Command 
        public Command BackCommand { get; set; }
        public Command lytAddSurgeryCommand { get; set; }
        public Command lytAddPCPCommand { get; set; }
        public Command lytAddPharmacyCommand { get; set; }
        public Command lytAddAllergyCommand { get; set; }
        public Command lytAddMedicationCommand { get; set; }
        public Command imgEditPCPCommand { get; set; }
        public Command ImgDeletePCPCommand { get; set; }
        public Command imgEditPharmacyCommand { get; set; }
        public Command imgDeletePharmacyCommand { get; set; }
        public Command AllergySaveCommand { get; set; }
        public Command MedicationSaveCommand { get; set; }
        public Command SurgurySaveCommand { get; set; }
        public Command PharmacySaveCommand { get; set; }
        public Command PCPSaveCommand { get; set; }
        public Command PCPCancelCommand { get; set; }
        public Command PCPSearchCommand { get; set; }
        public Command BtnContinueUpdateCommand { get; set; }
        public Command BtnContinueRegistrationCommand { get; set; }
        public Command BtnAddFamilyMemberCommand { get; set; }
        #endregion

        #region Properties
        private ObservableCollection<PCP> _listPrimaryCareProviders = new ObservableCollection<PCP>();
        public ObservableCollection<PCP> listPrimaryCareProviders
        {
            get { return _listPrimaryCareProviders; }
            set
            {
                if (_listPrimaryCareProviders != value)
                {
                    _listPrimaryCareProviders = value;
                    OnPropertyChanged("listPrimaryCareProviders");
                }
            }
        }
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
        private Pharmacy _PharmacySelected;
        public Pharmacy PharmacySelected
        {
            get { return _PharmacySelected; }
            set
            {
                if (_PharmacySelected != value)
                {
                    _PharmacySelected = value;
                    OnPropertyChanged("PharmacySelected");
                }
            }
        }
        private bool _IsChecked = false;
        public bool IsChecked
        {
            get { return _IsChecked; }
            set
            {
                if (_IsChecked != value)
                {
                    _IsChecked = value;
                    OnPropertyChanged("IsChecked");
                }
            }
        }
        private bool _lblPharmacySelectedVisible;
        public bool lblPharmacySelectedVisible
        {
            get { return _lblPharmacySelectedVisible; }
            set
            {
                if (_lblPharmacySelectedVisible != value)
                {
                    _lblPharmacySelectedVisible = value;
                    OnPropertyChanged("lblPharmacySelectedVisible");
                }
            }
        }
        private bool _BtnContinueUpdate = false;
        public bool BtnContinueUpdate
        {
            get { return _BtnContinueUpdate; }
            set
            {
                if (_BtnContinueUpdate != value)
                {
                    _BtnContinueUpdate = value;
                    OnPropertyChanged("BtnContinueUpdate");
                }
            }
        }
        private bool _BtnContinueRegistration = false;
        public bool BtnContinueRegistration
        {
            get { return _BtnContinueRegistration; }
            set
            {
                if (_BtnContinueRegistration != value)
                {
                    _BtnContinueRegistration = value;
                    OnPropertyChanged("BtnContinueRegistration");
                }
            }
        }
        private bool _BtnAddFamilyMember = false;
        public bool BtnAddFamilyMember
        {
            get { return _BtnAddFamilyMember; }
            set
            {
                if (_BtnAddFamilyMember != value)
                {
                    _BtnAddFamilyMember = value;
                    OnPropertyChanged("BtnAddFamilyMember");
                }
            }
        }
        private bool _lblNoResultsFound = false;
        public bool lblNoResultsFound
        {
            get { return _lblNoResultsFound; }
            set
            {
                if (_lblNoResultsFound != value)
                {
                    _lblNoResultsFound = value;
                    OnPropertyChanged("lblNoResultsFound");
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

        private bool _lytPharmacySelected = true;
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

        private bool _AllergiesIsVisible = true;
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
        private bool _lytAddPharmacy = true;
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
        private bool _lytAddSurgery = true;
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
        private bool _lytAddMedication = true;
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
        private bool _lytAddAllergy = true;
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
        private string _txtAllergy = string.Empty;
        public string txtAllergy
        {
            get { return _txtAllergy; }
            set
            {
                if (_txtAllergy != value)
                {
                    _txtAllergy = value;
                    OnPropertyChanged("txtAllergy");
                }
            }
        }
        private string _txtComments = string.Empty;
        public string txtComments
        {
            get { return _txtComments; }
            set
            {
                if (_txtComments != value)
                {
                    _txtComments = value;
                    OnPropertyChanged("txtComments");
                }
            }
        }
        private string _txtMedication = string.Empty;
        public string txtMedication
        {
            get { return _txtMedication; }
            set
            {
                if (_txtMedication != value)
                {
                    _txtMedication = value;
                    OnPropertyChanged("txtMedication");
                }
            }
        }
        private string _txtSurgery = string.Empty;
        public string txtSurgery
        {
            get { return _txtSurgery; }
            set
            {
                if (_txtSurgery != value)
                {
                    _txtSurgery = value;
                    OnPropertyChanged("txtSurgery");
                }
            }
        }
        private string _txtMedicationComments;
        public string txtMedicationComments
        {
            get { return _txtMedicationComments; }
            set
            {
                if (_txtMedicationComments != value)
                {
                    _txtMedicationComments = value;
                    OnPropertyChanged("txtMedicationComments");
                }
            }
        }
        private string _txtSurguryComments;
        public string txtSurguryComments
        {
            get { return _txtSurguryComments; }
            set
            {
                if (_txtSurguryComments != value)
                {
                    _txtSurguryComments = value;
                    OnPropertyChanged("txtSurguryComments");
                }
            }
        }
        private string _lblHeading = "Add Allergy";
        public string lblHeading
        {
            get { return _lblHeading; }
            set
            {
                if (_lblHeading != value)
                {
                    _lblHeading = value;
                    OnPropertyChanged("lblHeading");
                }
            }
        }
        private string _lblMedicationHeading = "Add Medication";
        public string lblMedicationHeading
        {
            get { return _lblMedicationHeading; }
            set
            {
                if (_lblMedicationHeading != value)
                {
                    _lblMedicationHeading = value;
                    OnPropertyChanged("lblMedicationHeading");
                }
            }
        }
        private string _lblPCPHeading = "Add Primary Care Provider";
        public string lblPCPHeading
        {
            get { return _lblPCPHeading; }
            set
            {
                if (_lblPCPHeading != value)
                {
                    _lblPCPHeading = value;
                    OnPropertyChanged("lblPCPHeading");
                }
            }
        }
        private string _lblPCPSearchHeading = "Find Your Primary Care Provider";
        public string lblPCPSearchHeading
        {
            get { return _lblPCPSearchHeading; }
            set
            {
                if (_lblPCPSearchHeading != value)
                {
                    _lblPCPSearchHeading = value;
                    OnPropertyChanged("lblPCPSearchHeading");
                }
            }
        }
        private string _lblSurgeryHeading = "Add Surgery";
        public string lblSurgeryHeading
        {
            get { return _lblSurgeryHeading; }
            set
            {
                if (_lblSurgeryHeading != value)
                {
                    _lblSurgeryHeading = value;
                    OnPropertyChanged("lblSurgeryHeading");
                }
            }
        }
        private string _lblPharmacyHeading = "Add Pharmacy";
        public string lblPharmacyHeading
        {
            get { return _lblPharmacyHeading; }
            set
            {
                if (_lblPharmacyHeading != value)
                {
                    _lblPharmacyHeading = value;
                    OnPropertyChanged("lblPharmacyHeading");
                }
            }
        }
        private string _txtName = string.Empty;
        public string txtName
        {
            get { return _txtName; }
            set
            {
                if (_txtName != value)
                {
                    _txtName = value;
                    OnPropertyChanged("txtName");
                }
            }
        }
        private string _txtAddress1 = string.Empty;
        public string txtAddress1
        {
            get { return _txtAddress1; }
            set
            {
                if (_txtAddress1 != value)
                {
                    _txtAddress1 = value;
                    OnPropertyChanged("txtAddress1");
                }
            }
        }
        private string _txtCity = string.Empty;
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
        private string _StateLbl;
        public string StateLbl
        {
            get { return _StateLbl; }
            set
            {
                if (_StateLbl != value)
                {
                    _StateLbl = value;
                    OnPropertyChanged("StateLbl");
                }
            }
        }
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
        private string _StatePCPLbl;
        public string StatePCPLbl
        {
            get { return _StatePCPLbl; }
            set
            {
                if (_StatePCPLbl != value)
                {
                    _StatePCPLbl = value;
                    OnPropertyChanged("StatePCPLbl");
                }
            }
        }
        private string _StatePCPSearchLbl;
        public string StatePCPSearchLbl
        {
            get { return _StatePCPSearchLbl; }
            set
            {
                if (_StatePCPSearchLbl != value)
                {
                    _StatePCPSearchLbl = value;
                    OnPropertyChanged("StatePCPSearchLbl");
                }
            }
        }
        private List<string> _StatesPCPList = CommonLibraryCoreMaui.Theme.Values.States;
        public List<string> StatesPCPList
        {
            get { return _StatesPCPList; }
            set
            {
                if (_StatesPCPList != value)

                {
                    _StatesPCPList = value;
                    OnPropertyChanged("StatesPCPList");
                }
            }
        }
        private List<string> _StatesPCPSearchList = CommonLibraryCoreMaui.Theme.Values.States;
        public List<string> StatesPCPSearchList
        {
            get { return _StatesPCPSearchList; }
            set
            {
                if (_StatesPCPSearchList != value)

                {
                    _StatesPCPSearchList = value;
                    OnPropertyChanged("StatesPCPSearchList");
                }
            }
        }
        private string _txtZipCode = string.Empty;
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
        private string _txtPhoneNumber = string.Empty;
        public string txtPhoneNumber
        {
            get { return _txtPhoneNumber; }
            set
            {
                if (_txtPhoneNumber != value)

                {
                    _txtPhoneNumber = value;
                    OnPropertyChanged("txtPhoneNumber");
                }
            }
        }
        private string _txtPCPFirstName = string.Empty;
        public string txtPCPFirstName
        {
            get { return _txtPCPFirstName; }
            set
            {
                if (_txtPCPFirstName != value)

                {
                    _txtPCPFirstName = value;
                    OnPropertyChanged("txtPCPFirstName");
                }
            }
        }
        private string _txtPCPSearchFirstName = string.Empty;
        public string txtPCPSearchFirstName
        {
            get { return _txtPCPSearchFirstName; }
            set
            {
                if (_txtPCPSearchFirstName != value)

                {
                    _txtPCPSearchFirstName = value;
                    OnPropertyChanged("txtPCPSearchFirstName");
                }
            }
        }
        private string _txtPCPLastName = string.Empty;
        public string txtPCPLastName
        {
            get { return _txtPCPLastName; }
            set
            {
                if (_txtPCPLastName != value)

                {
                    _txtPCPLastName = value;
                    OnPropertyChanged("txtPCPLastName");
                }
            }
        }
        private string _txtPCPSearchLastName = string.Empty;
        public string txtPCPSearchLastName
        {
            get { return _txtPCPSearchLastName; }
            set
            {
                if (_txtPCPSearchLastName != value)

                {
                    _txtPCPSearchLastName = value;
                    OnPropertyChanged("txtPCPSearchLastName");
                }
            }
        }
        private string _btnPCPAddTxt = "Add";
        public string btnPCPAddTxt
        {
            get { return _btnPCPAddTxt; }
            set
            {
                if (_btnPCPAddTxt != value)

                {
                    _btnPCPAddTxt = value;
                    OnPropertyChanged("btnPCPAddTxt");
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
        /// TODO : To define Load Medical issue List.....
        /// </summary>
        /// <returns></returns>
        public async Task LoadMedicalIssues()
        {
            try
            {
                issues = await DataUtility.GetMedicalIssuesAsync(SettingsValues.ApiURLValue).ConfigureAwait(false);
                if (issues != null)
                {
                    Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                    {
                        MedicalIssue none = issues.FirstOrDefault(x => x.Value.ToLower().Equals("none"));
                        if (none != null)
                        {
                            int ni = issues.IndexOf(none);
                            issues.RemoveAt(ni);
                            issues.Insert(0, none);
                            noneIndex = 0;
                        }

                        foreach (var issue in issues)
                        {
                            if (medicalInfo != null)
                            {
                                if (!MedicalIssuesList.Any(a => a.ID == issue.ID))
                                {
                                    if (medicalInfo.MedicalIssues.Contains(issue.ID))
                                    { issue.IsChecked = true; }
                                    else
                                    {
                                        issue.IsChecked = false;
                                    }
                                    MedicalIssuesList.Add(issue);
                                }

                            }

                        }
                        lytOtherMedicalIssueVisible = true;
                        CurativeCheckDTO respGetCurative = await DataUtility.GetCurativeEligibilityForHomeViewDialogAsync(SettingsValues.ApiURLValue, medicalInfo.PatientID, Token).ConfigureAwait(false);
                        if (respGetCurative != null)
                        {
                            patientIsCurative = respGetCurative.CurativeEligibilityForHomeViewDialog;
                            patientIsEligibleForCurative = respGetCurative.IsCurative;
                        }
                    });
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// TODO : To define Display Medical Info Details.....
        /// </summary>
        /// <returns></returns>
        public async Task DisplayMedicalInfoDeails()
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

                            if (additionalFamilyMember != null)
                            {
                                if (medicalInfo is null)
                                {
                                    medicalInfo = new MedicalInfo();
                                }

                                await GetPharmacyFromPrimaryAccount().ConfigureAwait(false);

                                Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                                {
                                    //BtnAddFamilyMember = true;
                                    //btnContinue.Click -= BtnAddFamilyMember_Click;
                                    //btnContinue.Click += BtnAddFamilyMember_Click;
                                });
                            }
                            else
                            {
                                RegistrationState reg;
                                // await LoginNewUser(reg.Email, reg.Password).ConfigureAwait(false);

                                //Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                                //{
                                //    lblHeaderIsvisble = true;
                                //    if (!reg.IsSelfPay)
                                //    {
                                //        lblHeader = "Step 3 of 3";
                                //    }
                                //    else
                                //    {
                                //        lblHeader = "Step 4 of 4";
                                //    }

                                if (medicalInfo is null)
                                {
                                    medicalInfo = new MedicalInfo();
                                    medicalInfo.PatientID = PatientID;
                                    BtnContinueRegistration = true;
                                }
                                else
                                {
                                    lblHeaderIsvisble = false;
                                    BtnContinueUpdate = true;
                                }

                                Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                                {

                                    UpdateSurgeriesList();
                                    UpdateMedicationsList();
                                    UpdateAllergiesList();

                                    if (medicalInfo.PCP != null)
                                    {
                                        lblPCPSelected = medicalInfo.PCP.Preview;
                                        lytPCPSelected = true;
                                        lytAddPCP = false;
                                    }

                                    if (medicalInfo.Pharmacy != null)
                                    {
                                        if (medicalInfo.Pharmacy.IsCapsule)
                                        {
                                            lblPharmacySelectedVisible = false;
                                            imgCapsule = true;
                                            //imgCapsule.SetImageResource(Resource.Id.curative_lockup_horizontal_a_redorange_300px().getDrawable(R.drawable.monkey, getApplicationContext().getTheme()));
                                        }
                                        else if (medicalInfo.Pharmacy.IsCurative)
                                        {
                                            lblPharmacySelectedVisible = false;
                                            imgCurative = true;
                                        }
                                        else
                                        {
                                            lblPharmacySelectedVisible = true;
                                            lblPharmacySelected = medicalInfo.Pharmacy.ToString();
                                            imgCapsule = false;
                                            if (imgCurative != null)
                                                imgCurative = false;
                                        }

                                        lytPharmacySelected = true;
                                        lblPharmacySelectedVisible = true;
                                        lblPharmacySelected = medicalInfo.Pharmacy.ToString();
                                        lytAddPharmacy = false;
                                    }

                                    if (!string.IsNullOrEmpty(medicalInfo.OtherMedicalIssue))
                                    {
                                        txtOtherMedicalIssue = medicalInfo.OtherMedicalIssue;
                                        chkOtherMedicalIssue = true;
                                        //txtOtherMedicalIssue.Enabled = true;
                                    }

                                });
                            }
                            await LoadMedicalIssues().ConfigureAwait(false);

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
        /// <summary>
        /// TODO : To define update surgeries List...
        /// </summary>
        public void UpdateSurgeriesList()
        {
            try
            {
                var surgeriesList = SurgeryList.ToList();
                SurgeryList = new ObservableCollection<Surgery>(surgeriesList);
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// TODO : To define update medication list....
        /// </summary>
        public void UpdateMedicationsList()
        {
            try
            {
                var medicationList = MedicationsList.ToList();
                MedicationsList = new ObservableCollection<Medication>(medicationList);
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// TODO : To define update allergies list....
        /// </summary>
        public void UpdateAllergiesList()
        {
            try
            {
                var allergiesList = AllergiesList.ToList();
                AllergiesList = new ObservableCollection<Allergy>(allergiesList);
            }
            catch (Exception ex)
            {
            }

        }
        /// <summary>
        /// TODO : To define All updated list.....
        /// </summary>
        public void UpdateList()
        {
            try
            {
                pharmacy = new Pharmacy();
                var medicalissue = MedicalIssuesList.Where(a => a.IsChecked == true).ToList();
                var allergiesList = medicalInfo.Allergies.ToList();
                var medicalList = medicalInfo.Medications.ToList();
                var SurguryList = medicalInfo.Surgeries.ToList();
                MedicalIssuesList = new ObservableCollection<MedicalIssue>(medicalissue);
                AllergiesList = new ObservableCollection<Allergy>(allergiesList);
                MedicationsList = new ObservableCollection<Medication>(medicalList);
                SurgeryList = new ObservableCollection<Surgery>(SurguryList);
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// TODO : To define login new user .....
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        private async Task<bool> LoginNewUser(string username, string pwd)
        {
            try
            {
                TokenResponse resp = await DataUtility.GetTokenResponseAsync(SettingsValues.ApiURLValue, username, pwd, "").ConfigureAwait(false);

                if (resp != null)
                {
                    if (!string.IsNullOrEmpty(resp.access_token) && resp.expires_in != null)
                    {
                        UserInfo userInfo = await DataUtility.GetUserInfo(SettingsValues.ApiURLValue, resp.userid, true, Token).ConfigureAwait(false);

                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// TODO : to define get pharmacy from primary account...
        /// </summary>
        /// <returns></returns>
        private async Task GetPharmacyFromPrimaryAccount()
        {
            try
            {
                MedicalInfo mi = await DataUtility.PatientGetMedicalHistoryAsync(SettingsValues.ApiURLValue, Token, PatientID).ConfigureAwait(false);
                if (mi != null) this.medicalInfo.Pharmacy = mi.Pharmacy;
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// TODO : To define Add surgery click event...
        /// </summary>
        /// <param name="obj"></param>
        private async void lytAddSurgeryAsync(object obj)
        {
            try
            {
                await Navigation.PushModalAsync(new Views.MyMedicalInfo.PatientRegistrationMedicalInfoSurgeryPage(null, 1, this), false);
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// TODO : To define Add PCP click event...
        /// </summary>
        /// <param name="obj"></param>
        private async void lytAddPCPAsync(object obj)
        {
            try
            {
                await Navigation.PushModalAsync(new Views.MyMedicalInfo.PatientRegistrationMedicalInfoPCPSearch(null, 1, this), false);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// TODO : To define Add pharmacy click event...
        /// </summary>
        /// <param name="obj"></param>
        private async void lytAddPharmacyAsync(object obj)
        {
            try
            {
                if (patientIsCurative || patientIsEligibleForCurative)
                {
                    ProcessCurative();
                }
                else
                {
                    ProcessCapsule();
                }
                await Navigation.PushModalAsync(new Views.MyMedicalInfo.PatientRegistrationMedicalInfoPharmacy(null, 1, this), false);
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// TODO : To define process capsule...
        /// </summary>
        public void ProcessCapsule()
        {
            try
            {
                Task.Run(async () =>
                   {
                       PatientProfile patientProfile = await DataUtility.GetPatientProfileAsync(SettingsValues.ApiURLValue, Token, PatientID).ConfigureAwait(false);
                       bool capsuleEligible = await DataUtility.GetZipCapsuleEligibilityAsync(SettingsValues.ApiURLValue, patientProfile.Zip, Token).ConfigureAwait(false);
                       Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                       {
                           // ShowCapsuleMedicalInfo(patientProfile.Zip, patientProfile.PatientID, capsuleEligible);
                       });
                   });
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// TODO : to define Process Curative...
        /// </summary>
        public void ProcessCurative()
        {
            try
            {
                Task.Run(async () =>
                    {
                        PatientProfile patientProfile = await DataUtility.GetPatientProfileAsync(SettingsValues.ApiURLValue, Token, PatientID).ConfigureAwait(false);
                        CurativeCheckDTO respGetCurative = await DataUtility.GetCurativeEligibilityForHomeViewDialogAsync(SettingsValues.ApiURLValue, medicalInfo.PatientID, Token).ConfigureAwait(false);
                        StatusResponse respUpdateCurative = await DataUtility.UpdateCurativeEligibilityForHomeViewDialogAsync(SettingsValues.ApiURLValue, PatientID, false, Token).ConfigureAwait(false);
                        Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                        {
                            //ShowCurativeMedicalInfo(patientProfile.PatientID);
                        });
                    });
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// TODO : To define Add Allergy click event...
        /// </summary>
        /// <param name="obj"></param>
        private async void lytAddAllergyAsync(object obj)
        {
            try
            {
                await Navigation.PushModalAsync(new Views.MyMedicalInfo.PatientRegistrationMedicalInfoAllergy(null, 1, this), false);
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// TODO : To define Add Medication click event...
        /// </summary>
        /// <param name="obj"></param>
        private async void lytAddMedicationAsync(object obj)
        {
            try
            {
                await Navigation.PushModalAsync(new Views.MyMedicalInfo.PatientRegistrationMedicalInfoMedication(null, 1, this), false);
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// TODO : To define Delete PCP click event...
        /// </summary>
        /// <param name="obj"></param>
        private async void ImgDeletePCPAsync(object obj)
        {
            try
            {
                DeleteMedicalItem(medicalInfo.PCP);
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// TODO : To define Delete Pharmacy click event...
        /// </summary>
        /// <param name="obj"></param>
        private async void imgDeletePharmacyAsync(object obj)
        {
            try
            {
                DeleteMedicalItem(medicalInfo.Pharmacy);
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// TODO : To define Get Description....
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetDescription(Type type)
        {
            try
            {
                var descriptions = (DescriptionAttribute[])
                      type.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (descriptions.Length == 0)
                {
                    return null;
                }
                return descriptions[0].Description;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        /// <summary>
        /// TODO : To define Get Dialog Title....
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetDialogTitle(Type type)
        {
            try
            {
                var descriptions = (DialogTitle[])
                        type.GetCustomAttributes(typeof(DialogTitle), false);

                if (descriptions.Length == 0)
                {
                    return null;
                }
                return descriptions[0].Name;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// TODO : To Define Delete Medical Items....
        /// </summary>
        /// <param name="item"></param>
        private async void DeleteMedicalItem(IPatientRegistrationMedicalInfoItem item)
        {
            try
            {
                string description = GetDescription(item.GetType()) ?? string.Empty;
                string title = GetDialogTitle(item.GetType()) ?? string.Empty;
                string confirmationMessage = $"Delete this {description.ToLower()}?";
                bool answer = await Application.Current.MainPage.DisplayAlert(title, confirmationMessage, "Yes", "No");

                if (answer)
                {
                    if (item is PCP)
                    {
                        medicalInfo.PCP = null;
                        lytPCPSelected = false;
                        lytAddPCP = true;
                    }
                    else if (item is Pharmacy)
                    {
                        medicalInfo.Pharmacy = null;
                        lytPharmacySelected = false;
                        lytAddPharmacy = true;
                        Pharmacy ph = (Pharmacy)item;
                        if (ph.IsCurative)
                        {
                            patientIsEligibleForCurative = true;
                        }
                    }
                };
            }
            catch (Exception ex)
            {
            }

        }
        /// <summary>
        /// TODO : To define Delete Medical list Item...
        /// </summary>
        /// <param name="item"></param>
        public async void DeleteMedicalListItem(IPatientRegistrationMedicalInfoListItem item)
        {
            try
            {
                string description = GetDescription(item.GetType()) ?? string.Empty;
                string title = GetDialogTitle(item.GetType()) ?? string.Empty;
                string confirmationMessage = $"Delete this {description.ToLower()}?";
                bool answer = await Application.Current.MainPage.DisplayAlert(title, confirmationMessage, "Yes", "No");
                if (answer)
                {
                    if (item is Surgery)
                    {
                        medicalInfo.Surgeries.RemoveAt(item.Position);
                        UpdateSurgeriesList();
                    }
                    else if (item is Allergy)
                    {
                        medicalInfo.Allergies.RemoveAt(item.Position);
                        UpdateAllergiesList();
                    }
                    else if (item is Medication)
                    {
                        medicalInfo.Medications.RemoveAt(item.Position);
                        UpdateMedicationsList();
                    }
                };
            }
            catch (Exception ex)
            {
                 
            }
        }
        /// <summary>
        /// TODO : To define Edit PCP Click event...
        /// </summary>
        /// <param name="obj"></param>
        private async void imgEditPCPAsync(object obj)
        {
            try
            {
                EditMedicalItem(medicalInfo.PCP);
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// TODO : To define edit pharmacy click event....
        /// </summary>
        /// <param name="obj"></param>
        private async void imgEditPharmacyAsync(object obj)
        {
            try
            {

                EditMedicalItem(medicalInfo.Pharmacy);
            }
            catch (Exception ex)
            {
            }
        }
        public async void EditMedicalItem(IPatientRegistrationMedicalInfoItem item)
        {
            try
            {
                if (item is Pharmacy)
                {
                    if (medicalInfo.Pharmacy.IsCurative || patientIsCurative && !medicalInfo.Pharmacy.IsCurative && medicalInfo.Pharmacy.PharmacyString == "")
                    {

                        ProcessCurative();
                    }
                    else if (((Pharmacy)item).IsCapsule)
                    {
                        ProcessCapsule();
                    }
                    else
                    {
                        await Navigation.PushModalAsync(new Views.MyMedicalInfo.PatientRegistrationMedicalInfoPharmacy(medicalInfo.Pharmacy, PHARMACY_REQUEST_CODE, this), false);
                    }
                }
                else if (item is PCP)
                {
                    await Navigation.PushModalAsync(new Views.MyMedicalInfo.PatientRegistrationMedicalInfoPCPAdd(medicalInfo.PCP, PCP_REQUEST_CODE, this), false);
                }
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// TODO : to define Update Capsule Pharmacy....
        /// </summary>
        public void UpdateCapsulePharmacy()
        {
            try
            {
                Pharmacy px = new Pharmacy() { IsCapsule = true };
                medicalInfo.Pharmacy = px;
                Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                {
                    lytPharmacySelected = true;
                    lytAddPharmacy = false;
                    lblPharmacySelectedVisible = false;
                    imgCapsule = true;
                });
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// TODO : TO define Update Curative Pharmacy
        /// </summary>
        public void UpdateCurativePharmacy()
        {
            try
            {
                Pharmacy px = new Pharmacy() { IsCurative = true };
                medicalInfo.Pharmacy = px;
                Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                {
                    lytPharmacySelected = true;
                    lytAddPharmacy = false;
                    lblPharmacySelectedVisible = false;
                    imgCurative = true;
                    //
                });
            }
            catch (Exception ex)
            { 
            }
        }

        /// <summary>
        /// TODO : to define display allergy details....
        /// </summary>
        /// <returns></returns>
        public async Task DisplayAllergyDeails()
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
                            if (allergy != null)
                            {
                                lblHeading = "Edit Allergy";
                                txtAllergy = allergy.Name;
                                txtComments = allergy.Description;
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
        /// <summary>
        /// TODO : to define Display Medication Details .....
        /// </summary>
        /// <returns></returns>
        public async Task DisplayMedicationDetails()
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
                            if (medication != null)
                            {
                                lblMedicationHeading = "Edit Medication";
                                txtMedication = medication.Name;
                                txtMedicationComments = medication.Description;
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
        /// <summary>
        /// TODO : To define display surgury details....
        /// </summary>
        /// <returns></returns>
        public async Task DisplaySurguryDetails()
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
                            if (surgery != null)
                            {
                                lblSurgeryHeading = "Edit Surgery";
                                txtSurgery = surgery.Name;
                                txtSurguryComments = surgery.Description;
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
        /// <summary>
        /// TODO : to define display pharmacy details....
        /// </summary>
        /// <returns></returns>
        public async Task DisplayPharmacyDetails()
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
                            if (pharmacy != null)
                            {
                                txtName = pharmacy.BusinessName;
                                txtCity = pharmacy.City;
                                txtAddress1 = pharmacy.StreetAddress1;
                                if (!string.IsNullOrEmpty(pharmacy.ZipCode))
                                {
                                    txtZipCode = pharmacy.ZipCode;
                                }
                                StateLbl = pharmacy.State;
                                //  spnrState.SelectState(pharmacy.State);
                                txtPhoneNumber = pharmacy.Description;
                                lblPharmacyHeading = "Edit Pharmacy";
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
        /// <summary>
        /// TODO : to define display PCP details...
        /// </summary>
        /// <returns></returns>
        public async Task DisplayPCPDetails()
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
                            if (pcp != null)
                            {
                                txtPCPFirstName = pcp.FirstName;
                                txtPCPLastName = pcp.LastName;
                                StatePCPLbl = pcp.State;
                                // spnrState.SelectState(pcp.State);
                                lblPCPHeading = "Edit Primary Care Provider";
                                btnPCPAddTxt = "Save";
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
        /// <summary>
        /// TODO : To define display PCP Select Details...
        /// </summary>
        /// <returns></returns>
        public async Task DisplayPCPSelectDetails()
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
        /// <summary>
        /// ToDo: To define the Allergy Save command
        /// </summary>
        /// <param name="obj"></param>
        private async void AllergySaveAsync(object obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtAllergy.Trim()))
                {
                    if (allergy != null)
                    {
                        var indx = AllergiesList.IndexOf(allergy);
                        if (indx != -1)
                        {
                            AllergiesList[indx].Name = txtAllergy;
                            AllergiesList[indx].Description = txtComments;
                        }
                    }
                    else
                    {
                        Allergy allergy = new Allergy()
                        {
                            Name = txtAllergy,
                            Description = txtComments
                        };
                        AllergiesList.Add(allergy);
                    }
                    await Navigation.PopModalAsync();
                }
                else
                {
                    UserDialog.Alert("Please fill all the required fields!");
                }
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// ToDo: To define the Medication Save command
        /// </summary>
        /// <param name="obj"></param>
        private async void MedicationSaveAsync(object obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtMedication.Trim()))
                {
                    if (medication != null)
                    {
                        var indx = MedicationsList.IndexOf(medication);
                        if (indx != -1)
                        {
                            MedicationsList[indx].Name = txtMedication;
                            MedicationsList[indx].Description = txtMedicationComments;
                        }
                    }
                    else
                    {
                        Medication medication = new Medication()
                        {
                            Name = txtMedication,
                            Description = txtMedicationComments,
                        };
                        MedicationsList.Add(medication);
                    }
                    await Navigation.PopModalAsync();
                }
                else
                {
                    UserDialog.Alert("Please fill all the required fields!");
                }
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// ToDo: To define the Surgury Save command
        /// </summary>
        /// <param name="obj"></param>
        private async void SurgurySaveAsync(object obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSurgery.Trim()))
                {
                    if (surgery != null)
                    {
                        var indx = SurgeryList.IndexOf(surgery);
                        if (indx != -1)
                        {
                            SurgeryList[indx].Name = txtSurgery;
                            SurgeryList[indx].Description = txtSurguryComments;
                        }
                    }
                    else
                    {
                        Surgery surgery = new Surgery()
                        {
                            Name = txtSurgery,
                            Description = txtSurguryComments,
                        };
                        SurgeryList.Add(surgery);
                    }
                    await Navigation.PopModalAsync();
                }
                else
                {
                    UserDialog.Alert("Please fill all the required fields!");
                }
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// ToDo: To define the Pharmacy Save command
        /// </summary>
        /// <param name="obj"></param>
        private async void PharmacySaveAsync(object obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtName.Trim()) && !string.IsNullOrEmpty(txtZipCode.Trim()) && !string.IsNullOrEmpty(txtAddress1.Trim()))
                {
                    if (pharmacy != null)
                    {
                        pharmacy.BusinessName = txtName;
                        pharmacy.City = txtCity;
                        pharmacy.State = StateLbl;
                        pharmacy.StreetAddress1 = txtAddress1;
                        pharmacy.ZipCode = txtZipCode;
                        pharmacy.Description = txtPhoneNumber;
                    }
                    else
                    {
                        pharmacy = new Pharmacy();
                        pharmacy.BusinessName = txtName;
                        pharmacy.City = txtCity;
                        pharmacy.State = StateLbl;
                        pharmacy.StreetAddress1 = txtAddress1;
                        pharmacy.ZipCode = txtZipCode;
                        pharmacy.Description = txtPhoneNumber;
                        medicalInfo.Pharmacy = pharmacy;

                        //pharmacy = new Pharmacy();
                        //txtName = pharmacy != null ? pharmacy.BusinessName : ""; 
                        //txtCity = pharmacy != null ? pharmacy.City : "";
                        //StateLbl = pharmacy != null ? pharmacy.State : "";
                        //txtAddress1 = pharmacy != null ? pharmacy.StreetAddress1 : "";
                        //txtZipCode = pharmacy != null ? pharmacy.ZipCode : "";
                        //txtPhoneNumber = pharmacy != null ? pharmacy.Description : "";
                    }
                    await Navigation.PopModalAsync();
                }
                else
                {
                    UserDialog.Alert("Please fill all the required fields!");
                }
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// ToDo: To define the PCP ave command
        /// </summary>
        /// <param name="obj"></param>
        private async void PCPSaveAsync(object obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPCPFirstName.Trim()) && !string.IsNullOrEmpty(txtPCPLastName.Trim()))
                {
                    if (pcp != null)
                    {
                        pcp.FirstName = txtPCPFirstName;
                        pcp.LastName = txtPCPLastName;
                        pcp.State = StatePCPLbl;
                    }
                    else
                    {
                        pcp = new PCP();
                        pcp.FirstName = txtPCPFirstName;
                        pcp.LastName = txtPCPLastName;
                        pcp.State = StatePCPLbl;
                        medicalInfo.PCP = pcp;
                        //txtPCPFirstName = pcp.FirstName;
                        //txtPCPLastName = pcp.LastName;
                        //StatePCPLbl = pcp.State;
                    }
                    await Navigation.PopModalAsync();
                }
                else
                {
                    UserDialog.Alert("Please fill all the required fields!");
                }
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// ToDo: To define the PCP Cancel command
        /// </summary>
        /// <param name="obj"></param>
        private async void PCPCancelAsync(object obj)
        {
            try
            {
                await Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// ToDo: To define the PCP Search command
        /// </summary>
        /// <param name="obj"></param>
        private async void PCPSearchAsync(object obj)
        {
            try
            {
                var firstname = txtPCPSearchFirstName;
                var lastname = txtPCPSearchLastName;
                await Navigation.PushModalAsync(new Views.MyMedicalInfo.PatientRegistrationMedicalInfoPCPSelect(firstname, lastname, this), false);
            }
            catch (Exception ex)
            {
            }
        }
        private bool ValidateMedicalInfo()
        {
            try
            { 
                if (medicalInfo != null)
                {
                    if (medicalInfo.PCP is null || medicalInfo.Pharmacy is null ||
                        (medicalInfo.MedicalIssues.Count == 0 && !chkOtherMedicalIssue) ||
                        (chkOtherMedicalIssue && string.IsNullOrEmpty(txtOtherMedicalIssue.Trim())))
                    {
                        UserDialog.Alert("Please fill all the required fields!");
                    }
                    else
                    {
                        return true;
                    }

                }
                return false;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        /// <summary>
        /// TODO : to define check for other Medical issue...
        /// </summary>
        private void CheckForOtherMedicalIssue()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtOtherMedicalIssue))
                {
                    if (medicalInfo != null) medicalInfo.OtherMedicalIssue = txtOtherMedicalIssue;
                }
                else
                {
                    if (medicalInfo != null) medicalInfo.OtherMedicalIssue = string.Empty;
                }
            }
            catch (Exception ex)
            {
                 
            }
        }
        /// <summary>
        /// ToDo: To define the Btn Continue Update command
        /// </summary>
        /// <param name="obj"></param>
        private async void BtnContinueUpdateAsync(object obj)
        {
            try
            {
                if (ValidateMedicalInfo())
                {
                    CheckForOtherMedicalIssue();

                    medicalInfo.MedicalIssues = MedicalIssuesList.Where(a => a.IsChecked == true).Select(x => x.ID).ToList();
                    medicalInfo.Allergies = AllergiesList.ToList();
                    medicalInfo.Medications = MedicationsList.ToList();
                    medicalInfo.Surgeries = SurgeryList.ToList();

                    StatusResponse resp = await DataUtility.UpdateMedicalHistoryAsync(SettingsValues.ApiURLValue, medicalInfo, Token).ConfigureAwait(false);
                    if (resp != null)
                    {

                        if (patientIsCurative)
                        {
                            var homeDialogVisible = medicalInfo.Pharmacy == null || !medicalInfo.Pharmacy.IsCurative ? true : false;
                            StatusResponse respUpdateCurative = await DataUtility.UpdateCurativeEligibilityForHomeViewDialogAsync(SettingsValues.ApiURLValue, medicalInfo.PatientID, homeDialogVisible, Token).ConfigureAwait(false);
                        }

                        if (resp.StatusCode == StatusCode.Saved)
                        {
                            await Navigation.PopModalAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// ToDo: To define the Btn Continue Registration command
        /// </summary>
        /// <param name="obj"></param>
        private async void BtnContinueRegistrationAsync(object obj)
        {
            try
            {
                if (ValidateMedicalInfo())
                {
                    CheckForOtherMedicalIssue();

                    StatusResponse resp = await DataUtility.RegistrationStep4Async(SettingsValues.ApiURLValue, medicalInfo).ConfigureAwait(false);
                    if (resp != null)
                    {
                        switch (resp.StatusCode)
                        {
                            case StatusCode.Success:
                            case StatusCode.Saved:
                                RegistrationState reg = new RegistrationState();
                                //using (RegistrationStateHelper registrationStateHelper = new RegistrationStateHelper(this))
                                //{
                                //    reg = registrationStateHelper.GetState();
                                //}

                                bool ret = await LoginNewUser(reg.Email, reg.Password).ConfigureAwait(false);
                                if (ret)
                                {
                                    await Navigation.PushModalAsync(new Views.Home.HomePage(), false);
                                }
                                break;
                            default:
                                UserDialog.Alert("There was an error please try again.");
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// ToDo: To define the Btn Add Family Member  command
        /// </summary>
        /// <param name="obj"></param>
        private async void BtnAddFamilyMemberAsync(object obj)
        {
            try
            {
                if (ValidateMedicalInfo())
                {
                    CheckForOtherMedicalIssue();
                    additionalFamilyMember.MedicalHistory = medicalInfo;

                    //using (AccountAddFamilyMemberStateHelper afmStateHelper = new AccountAddFamilyMemberStateHelper(this))
                    //{
                    //    afmStateHelper.AddAdditionalFamilyMember(additionalFamilyMember);
                    //}
                    //Finish();
                }
            }
            catch (Exception ex)
            {
            }
        }


        #endregion
    }
}
