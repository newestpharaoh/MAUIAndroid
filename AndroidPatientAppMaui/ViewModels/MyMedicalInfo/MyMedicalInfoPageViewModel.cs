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
    public class MyMedicalInfoPageViewModel : BaseViewModel
    {

        //To define the class level variable.
        string Token = string.Empty;
        int PatientID = 0;
        bool nonVisit = false;
        bool family = false;
        MedicalInfo medicalInfo;

        #region Constructor
        public MyMedicalInfoPageViewModel(INavigation nav)
        {
            try
            {
                Navigation = nav;
                BackCommand = new Command(BackAsync);
                UpdateMedicalInfoCommand = new Command(UpdateMedicalInfoAsync);
                EditCommand = new Command(EditAsync);
                ContinueCommand = new Command(ContinueAsync);

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
        public Command UpdateMedicalInfoCommand { get; set; }
        public Command ContinueCommand { get; set; }
        public Command EditCommand { get; set; }
        #endregion

        #region Properties
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
        private string _MadiacalHistoryName;
        public string MadiacalHistoryName
        {
            get { return _MadiacalHistoryName; }
            set
            {
                if (_MadiacalHistoryName != value)
                {
                    _MadiacalHistoryName = value;
                    OnPropertyChanged("MadiacalHistoryName");
                }
            }
        }
        private string _OtherMedicalIssue;
        public string OtherMedicalIssue
        {
            get { return _OtherMedicalIssue; }
            set
            {
                if (_OtherMedicalIssue != value)
                {
                    _OtherMedicalIssue = value;
                    OnPropertyChanged("OtherMedicalIssue");
                }
            }
        }
        private string _lblPCP;
        public string lblPCP
        {
            get { return _lblPCP; }
            set
            {
                if (_lblPCP != value)
                {
                    _lblPCP = value;
                    OnPropertyChanged("lblPCP");
                }
            }
        }
        private string _lblPharmacy;
        public string lblPharmacy
        {
            get { return _lblPharmacy; }
            set
            {
                if (_lblPharmacy != value)
                {
                    _lblPharmacy = value;
                    OnPropertyChanged("lblPharmacy");
                }
            }
        }
        private bool _lytInfo = false;
        public bool lytInfo
        {
            get { return _lytInfo; }
            set
            {
                if (_lytInfo != value)
                {
                    _lytInfo = value;
                    OnPropertyChanged("lytInfo");
                }
            }
        }
        private bool _IsOtherMEdicalIssueVisible = false;
        public bool IsOtherMEdicalIssueVisible
        {
            get { return _IsOtherMEdicalIssueVisible; }
            set
            {
                if (_IsOtherMEdicalIssueVisible != value)
                {
                    _IsOtherMEdicalIssueVisible = value;
                    OnPropertyChanged("IsOtherMEdicalIssueVisible");
                }
            }
        }
        private bool _IslblPCPVisible = false;
        public bool IslblPCPVisible
        {
            get { return _IslblPCPVisible; }
            set
            {
                if (_IslblPCPVisible != value)
                {
                    _IslblPCPVisible = value;
                    OnPropertyChanged("IslblPCPVisible");
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
        private bool _IslblPharmacyVisible = false;
        public bool IslblPharmacyVisible
        {
            get { return _IslblPharmacyVisible; }
            set
            {
                if (_IslblPharmacyVisible != value)
                {
                    _IslblPharmacyVisible = value;
                    OnPropertyChanged("IslblPharmacyVisible");
                }
            }
        }
        private bool _lytUpdateOrContinue = false;
        public bool lytUpdateOrContinue
        {
            get { return _lytUpdateOrContinue; }
            set
            {
                if (_lytUpdateOrContinue != value)
                {
                    _lytUpdateOrContinue = value;
                    OnPropertyChanged("lytUpdateOrContinue");
                }
            }
        }
        private bool _lytNoMedicalInfo = false;
        public bool lytNoMedicalInfo
        {
            get { return _lytNoMedicalInfo; }
            set
            {
                if (_lytNoMedicalInfo != value)
                {
                    _lytNoMedicalInfo = value;
                    OnPropertyChanged("lytNoMedicalInfo");
                }
            }
        }
        private bool _btnContinueVisible = true;
        public bool btnContinueVisible
        {
            get { return _btnContinueVisible; }
            set
            {
                if (_btnContinueVisible != value)
                {
                    _btnContinueVisible = value;
                    OnPropertyChanged("btnContinueVisible");
                }
            }
        }
        private bool _lytEditMedicalHistory = true;
        public bool lytEditMedicalHistory
        {
            get { return _lytEditMedicalHistory; }
            set
            {
                if (_lytEditMedicalHistory != value)
                {
                    _lytEditMedicalHistory = value;
                    OnPropertyChanged("lytEditMedicalHistory");
                }
            }
        }
        #endregion

        #region Methods
        public void UpdateList()
        {
            try
            {
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
        /// To define the Update Medical Info button command.
        /// </summary>
        /// <param name="obj"></param>
        private void UpdateMedicalInfoAsync(object obj)
        {
            try
            {
                UpdateMedicalInfo($"Visit for {Helpers.AppGlobalConstants.userInfo.Name}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        /// <summary>
        /// To define the Edit button command.
        /// </summary>
        /// <param name="obj"></param>
        private void EditAsync(object obj)
        {
            try
            {
                UpdateMedicalInfo(Helpers.AppGlobalConstants.userInfo.Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        /// <summary>
        /// To define the Continue button command.
        /// </summary>
        /// <param name="obj"></param>
        private async void ContinueAsync(object obj)
        {
            try
            {
                var PatientName = Helpers.AppGlobalConstants.userInfo.Name;
                await Navigation.PushModalAsync(new Views.MyMedicalInfo.ProviderSelectionPage(PatientName), false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        /// <summary>
        /// To define the Update Medical Info.
        /// </summary>
        /// <param name="title"></param>
        private async void UpdateMedicalInfo(string title)
        {
            try
            {
                await Navigation.PushModalAsync(new Views.MyMedicalInfo.MyMedicalInfoDetailsPage(title, medicalInfo), false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public async Task DisplayMedicalInfo(int patientId)
        {
            // Get App settings api..
            try
            {
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {
                    UserDialog.ShowLoading();
                    await Task.Run(async () =>
                    {
                        nonVisit = true;
                        if (nonVisit)
                        {
                            lytEditMedicalHistory = true;
                            UserName = Helpers.AppGlobalConstants.userInfo.Name;
                        }
                        else
                        {
                            lytInfo = true;
                            lytUpdateOrContinue = true;
                            UserName = $"Visit for {Helpers.AppGlobalConstants.userInfo.Name}";
                        }
                        Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                        {
                            medicalInfo = await DataUtility.PatientGetMedicalHistoryAsync(SettingsValues.ApiURLValue, Token, PatientID).ConfigureAwait(false);
                            List<MedicalIssue> issues = await DataUtility.GetMedicalIssuesAsync(SettingsValues.ApiURLValue).ConfigureAwait(false);
                            if (medicalInfo != null)
                            {
                                if (medicalInfo.MedicalIssues.Count > 0 || !string.IsNullOrEmpty(medicalInfo.OtherMedicalIssue))
                                {
                                    if (issues != null)
                                    {
                                        foreach (int medicalIssue in medicalInfo.MedicalIssues)
                                        {
                                            MedicalIssue mi = issues.FirstOrDefault(x => x.ID == medicalIssue);
                                            if (mi != null && !MedicalIssuesList.Any(item => item.ID == mi.ID))
                                            {
                                                MedicalIssuesList.Add(mi);
                                            }
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(medicalInfo.OtherMedicalIssue))
                                    {
                                        IsOtherMEdicalIssueVisible = true;
                                        OtherMedicalIssue = medicalInfo.OtherMedicalIssue;
                                    }
                                }
                                if (medicalInfo.PCP != null)
                                {
                                    if (!string.IsNullOrEmpty(medicalInfo.PCP.Preview))
                                    {
                                        lblPCP = medicalInfo.PCP.Preview;
                                        IslblPCPVisible = true;
                                    }
                                }
                                if (medicalInfo.Pharmacy != null)
                                {
                                    if (medicalInfo.Pharmacy.IsCapsule)
                                    {
                                        imgCapsule = true;
                                        IslblPharmacyVisible = false;
                                    }
                                    else if (medicalInfo.Pharmacy.IsCurative)
                                    {
                                        IslblPharmacyVisible = false;
                                        imgCurative = true;
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(medicalInfo.Pharmacy.ToString()))
                                        {
                                            lblPharmacy = medicalInfo.Pharmacy.ToString();
                                            IslblPharmacyVisible = true;
                                            imgCapsule = false;
                                            imgCurative = false;
                                        }
                                    }
                                }
                                if (medicalInfo.Allergies != null)
                                {
                                    foreach (Allergy allergy in medicalInfo.Allergies)
                                    {
                                        if (!AllergiesList.Any(a => a.Name == allergy.Name))
                                        {
                                            AllergiesList.Add(allergy);
                                        }
                                    }
                                }
                                if (medicalInfo.Medications != null)
                                {
                                    foreach (Medication medication in medicalInfo.Medications)
                                    {
                                        if (!MedicationsList.Any(a => a.Name == medication.Name))
                                        {
                                            MedicationsList.Add(medication);
                                        }
                                    }
                                }
                                if (medicalInfo.Surgeries != null)
                                {
                                    foreach (Surgery surgery in medicalInfo.Surgeries)
                                    {
                                        if (!SurgeryList.Any(a => a.Name == surgery.Name))
                                        {
                                            SurgeryList.Add(surgery);
                                        }
                                    }
                                }
                                if (!nonVisit)
                                {
                                    if (lytNoMedicalInfo != null)
                                    {
                                        bool notProvided = medicalInfo.IsNotProvided();
                                        lytNoMedicalInfo = notProvided ? true : false;
                                        btnContinueVisible = notProvided ? false : true;
                                    }
                                }
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
