using Acr.UserDialogs;
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
        AdditionalFamilyMember additionalFamilyMember;

        const int PCP_REQUEST_CODE = 2;
        const int SURGERY_REQUEST_CODE = 3;
        const int MEDICATION_REQUEST_CODE = 4;
        const int ALLERGY_REQUEST_CODE = 5;
        const int PHARMACY_REQUEST_CODE = 6;

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
                            if (medicalInfo != null)
                            {
                                if (!MedicalIssuesList.Any(a => a.ID == issue.ID))
                                {
                                    if (medicalInfo.MedicalIssues.Contains(issue.ID))
                                    { issue.IsChecked = true; }
                                    MedicalIssuesList.Add(issue);
                                }

                            }

                        }
                        var a = MedicalIssuesList;
                        lytOtherMedicalIssueVisible = true;
                        CurativeCheckDTO respGetCurative = await DataUtility.GetCurativeEligibilityForHomeViewDialogAsync(SettingsValues.ApiURLValue, PatientID, Token).ConfigureAwait(false);
                        patientIsCurative = respGetCurative.CurativeEligibilityForHomeViewDialog;
                        patientIsEligibleForCurative = respGetCurative.IsCurative;
                    });
                }
            }
            catch (Exception ex)
            {

            }
        }

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

                                //    if (medicalInfo is null)
                                //    {
                                //        medicalInfo = new MedicalInfo();
                                //        medicalInfo.PatientID = PatientID;  
                                //    }
                                //    else
                                //    {
                                //        lblHeaderIsvisble = false; 
                                //    }
                                //});
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
                                        if (imgCurative != null) imgCurative = false;
                                    }

                                    lblPharmacySelectedVisible = true;
                                    lytAddPharmacy = false;
                                }

                                if (!string.IsNullOrEmpty(medicalInfo.OtherMedicalIssue))
                                {
                                    txtOtherMedicalIssue = medicalInfo.OtherMedicalIssue;
                                    chkOtherMedicalIssue = true;
                                    //txtOtherMedicalIssue.Enabled = true;
                                }

                            });

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
        public void UpdateSurgeriesList()
        {
            var surgeriesList = medicalInfo.Surgeries.Cast<IPatientRegistrationMedicalInfoListItem>().ToList();
            foreach (Surgery surgury in surgeriesList)
            {
                if (!SurgeryList.Any(a => a.Name == surgury.Name))
                {
                    SurgeryList.Add(surgury);
                }
            }

        }
        public void UpdateMedicationsList()
        {
            //medicationsAdapter = new PatientRegistrationMedicalItemAdapter(this, Resources, medicalInfo.Medications.Cast<IPatientRegistrationMedicalInfoListItem>().ToList());
            //lstMedications.Adapter = medicationsAdapter;
            //ListViewHeightHelper.SetListViewHeightBasedOnChildren(lstMedications, true);
            var medicationList = medicalInfo.Medications.Cast<IPatientRegistrationMedicalInfoListItem>().ToList();
            foreach (Medication medication in medicationList)
            {
                if (!MedicationsList.Any(a => a.Name == medication.Name))
                {
                    MedicationsList.Add(medication);
                }
            }
        }
        public void UpdateAllergiesList()
        {
            try
            {
                var allergiesList = medicalInfo.Allergies.Cast<IPatientRegistrationMedicalInfoListItem>().ToList();
                foreach (Allergy allergy in allergiesList)
                {
                    if (!AllergiesList.Any(a => a.Name == allergy.Name))
                    {
                        AllergiesList.Add(allergy);
                    }
                }

                var a = AllergiesList;
            }
            catch (Exception ex)
            {
            }
            //allergiesAdapter = new PatientRegistrationMedicalItemAdapter(this, Resources, medicalInfo.Allergies.Cast<IPatientRegistrationMedicalInfoListItem>().ToList());
            //lstAllergies.Adapter = allergiesAdapter;
            //ListViewHeightHelper.SetListViewHeightBasedOnChildren(lstAllergies, true);
        }
        private async Task<bool> LoginNewUser(string username, string pwd)
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
        private async Task GetPharmacyFromPrimaryAccount()
        {
            MedicalInfo mi = await DataUtility.PatientGetMedicalHistoryAsync(SettingsValues.ApiURLValue, Token, PatientID).ConfigureAwait(false);
            if (mi != null) this.medicalInfo.Pharmacy = mi.Pharmacy;
        }
        #endregion
    }
}
