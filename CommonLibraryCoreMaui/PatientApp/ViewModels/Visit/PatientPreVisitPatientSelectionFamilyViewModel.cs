using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.Models.NavigationParameters;
using CommonLibraryCoreMaui.Services;
using CommonLibraryCoreMaui.ViewModels;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class PatientPreVisitPatientSelectionFamilyViewModel : BaseViewModel
    {
        public IVisitsService _visitsService;
        public IMvxCommand ContinueCommand => new MvxAsyncCommand(Continue);

        private ObservableCollection<Patient> PatientsCollectionSource;

        public ObservableCollection<Patient> _patientsCollection;
        public virtual ObservableCollection<Patient> PatientsCollection
        {
            get { return _patientsCollection; }
            set { SetProperty(ref _patientsCollection, value); }
        }

        public ObservableCollection<Patient> _adultsCollection;
        public virtual ObservableCollection<Patient> AdultsCollection
        {
            get { return _adultsCollection; }
            set { SetProperty(ref _adultsCollection, value); }
        }
        private bool _isSelectedAdult;
        public bool IsSelectedAdult
        {
            get { return _isSelectedAdult; }
            set
            {
                SetProperty(ref _isSelectedAdult, value);
            }
        }
        private bool _isAlreadyRunning;
        public bool IsAlreadyRunning
        {
            get { return _isAlreadyRunning; }
            set
            {
                SetProperty(ref _isAlreadyRunning, value);
            }
        }

        private bool _selectedPatientTextUpdated;
        public bool SelectedPatientTextUpdated
        {
            get { return _selectedPatientTextUpdated; }
            set
            {
                SetProperty(ref _selectedPatientTextUpdated, value);
                RaisePropertyChanged(() => SelectedPatientTextUpdated);
            }
        }
        private Patient _selectedPatient;
        public Patient SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                SetProperty(ref _selectedPatient, value);
            }
        }


        private string _otherGuardianName;
        public string OtherGuardianName
        {
            get { return _otherGuardianName; }
            set
            {
                SetProperty(ref _otherGuardianName, value);
            }
        }

        public ObservableCollection<GenericRecord> _relationshipsCollection;
        public virtual ObservableCollection<GenericRecord> RelationshipsCollection
        {
            get { return _relationshipsCollection; }
            set { SetProperty(ref _relationshipsCollection, value); }
        }

        private GenericRecord _selectedRelationship;
        public GenericRecord SelectedRelationship
        {
            get { return _selectedRelationship; }
            set
            {
                SetProperty(ref _selectedRelationship, value);
            }
        }

        private string _otherRelationship;
        public string OtherRelationship
        {
            get { return _otherRelationship; }
            set
            {
                SetProperty(ref _otherRelationship, value);
            }
        }
        private bool _readNoticeCheck;
        public bool ReadNoticeCheck
        {
            get { return _readNoticeCheck; }
            set
            {
                SetProperty(ref _readNoticeCheck, value);
            }
        }
        private Patient _selectedAdult;
        public Patient SelectedAdult
        {
            get { return _selectedAdult; }
            set
            {
                SetProperty(ref _selectedAdult, value);
                if (_selectedAdult != null)
                {
                    StartVisit.Instance.OtherGuardianRelationship = string.Empty;
                    StartVisit.Instance.OtherGuardianName = string.Empty;
                    StartVisit.Instance.PatientGuardianID = null;

                    var clonedList = PatientsCollectionSource.Select(objEntity => (Patient)objEntity.Clone()).ToList();
                    if (_selectedAdult.PatientID != null)
                    {
                        Patient patientToRemove = clonedList.Where(x => x.PatientID == _selectedAdult.PatientID).FirstOrDefault();
                        if (patientToRemove != null)
                            if (Globals.Instance.UserInfo.Domain.Contains("Star") || Globals.Instance.UserInfo.Domain.Contains("Star Kids"))
                            {
                                //donothing
                            }
                            else clonedList.Remove(patientToRemove);
                    }
                    PatientsCollection = new ObservableCollection<Patient>(clonedList);
                    _isSelectedAdult = true;
                    SelectedPatient = PatientsCollection[0];
                    SelectedPatient = null;
                    _isSelectedAdult = false;
                }
            }
        }

        public PatientPreVisitPatientSelectionFamilyViewModel(IVisitsService service)
        {
            _visitsService = service;
        }

        private bool _showWarning;
        public bool ShowWarning
        {
            get { return _showWarning; }
            set { _showWarning = value; RaisePropertyChanged(() => ShowWarning); }
        }
        private string _warningText;
        public string WarningText
        {
            get { return _warningText; }
            set { SetProperty(ref _warningText, value); }
        }

        private ActiveVisitInfo _actVisitInfo;
        public ActiveVisitInfo ActVisitInfo
        {
            get { return _actVisitInfo; }
            set { SetProperty(ref _actVisitInfo, value); }
        }
        // public visitExpired : boolean = false;
        private bool _visitExpired;
        public bool VisitExpired
        {
            get { return _visitExpired; }
            set
            {
                SetProperty(ref _visitExpired, value);
                RaisePropertyChanged(() => VisitExpired);
            }
        }
        public async override Task Initialize()
        {
            IsBusy = true;
            ReadNoticeCheck = false;
            ShowWarning = false;
            IsAlreadyRunning = false;
            VisitExpired = false;
            try
            {
                PatientsForVisit resp = await _visitsService.PatientStartVisitStep1(Globals.Instance.UserInfo.LoginID);
                if (resp != null)
                {
                    SelectedAdult = null;
                    PatientsCollectionSource = new ObservableCollection<Patient>(resp.Patients);
                    PatientsCollection = new ObservableCollection<Patient>(resp.Patients);
                    //if (Globals.Instance.UserInfo.Domain == "Star" || Globals.Instance.UserInfo.Domain == "Star Kids")
                    //{
                    //    AdultsCollection = new ObservableCollection<Patient>();
                    //    AdultsCollection.Add(new Patient() { FirstName = "Other", PatientID = null });
                    //}
                    //else
                    //{
                    AdultsCollection = new ObservableCollection<Patient>(resp.Adults);
                    AdultsCollection.Add(new Patient() { FirstName = "Other", PatientID = null });
                    // }
                    RelationshipsCollection = new ObservableCollection<GenericRecord>(Theme.Values.Relationships);
                    SelectedRelationship = null;
                }
            }
            catch { }
            IsBusy = false;
            await base.Initialize();
        }

        public async Task CheckForActiveVisit()
        {
            if (SelectedPatient != null || SelectedRelationship.ID != null)
            {
                var AdultPt = SelectedAdult.PatientID != null ? SelectedAdult.PatientID.Value : 0;
                ActVisitInfo = await _visitsService.GetPatientActiveVisits(SelectedPatient.PatientID.Value, 0, "").ConfigureAwait(false);
            }

        }
        public async Task ResumeVisit()
        {
            if (ActVisitInfo.ActiveVisits.Count > 0)
            {
                var VisitId = ActVisitInfo.ActiveVisits[0].VisitID;
                var vd = await _visitsService.GetVisitDetailAsync(VisitId).ConfigureAwait(false);
                if (vd != null)
                {
                    var resp = await _visitsService.PatientRestartVisit(VisitId, "en").ConfigureAwait(false);

                    if (resp.Message == "Success")
                    {
                        StartVisit.Instance.VisitID = Int32.Parse(vd.VisitID);
                        StartVisit.Instance.PatientID = vd.PatientID;
                        StartVisit.Instance.ProviderID = vd.ProviderID;
                        InitalizeInstance();
                        StartVisit.Instance.IsResumeVisit = true;

                        await _navigationService.Navigate<PatientVisitsScreenViewModel, VisitDetailNavigationParam>(new VisitDetailNavigationParam()
                        {
                            VisitId = vd.VisitID,
                            ProviderId = vd.ProviderID.ToString(),
                            ProviderName = vd.ProviderName,
                            PatientFirstName = vd.PatientFirstName,
                            PatientLastName = vd.PatientLastName,
                            GuardianID = vd.GuardianID
                        }); ;

                        //_visitsService.OtherGuardianName = OtherGuardianName;
                    }

                    else { 
                        VisitExpired = true; 
                   
                    }
                }
            }


        }

        private void InitalizeInstance()
        {
            if (SelectedAdult.PatientID is null)
            {
                if (SelectedRelationship is null || SelectedRelationship.ID == -1 || string.IsNullOrEmpty(OtherGuardianName))
                {
                    ShowWarning = true;
                    WarningText = "Please fill out all fields";
                    return;
                }
                if (SelectedRelationship.ID is null)
                {
                    if (string.IsNullOrEmpty(OtherRelationship)) return;
                    StartVisit.Instance.OtherGuardianRelationship = OtherRelationship;
                }
                else
                {
                    StartVisit.Instance.OtherGuardianRelationship = SelectedRelationship.Value;
                }

                StartVisit.Instance.OtherGuardianName = OtherGuardianName;
            }
            else
            {
                StartVisit.Instance.PatientGuardianID = SelectedAdult.PatientID;
            }

            StartVisit.Instance.PatientID = SelectedPatient.PatientID;
            StartVisit.Instance.PatientName = SelectedPatient.ToString();
        }
        private async Task Continue()
        {
            ShowWarning = false;
            if (SelectedAdult != null && SelectedPatient != null)
            {
                InitalizeInstance();
                //if (SelectedAdult.PatientID is null)
                //{

                //if (SelectedRelationship is null) return;
                //if (string.IsNullOrEmpty(OtherGuardianName)) return;
                //if (SelectedRelationship.ID == -1) return;
                //    if (SelectedRelationship is null || SelectedRelationship.ID == -1 || string.IsNullOrEmpty(OtherGuardianName))
                //    {
                //        ShowWarning = true;
                //        WarningText = "Please fill out all fields";
                //        return;
                //    }
                //    if (SelectedRelationship.ID is null)
                //    {
                //        if (string.IsNullOrEmpty(OtherRelationship)) return;
                //        StartVisit.Instance.OtherGuardianRelationship = OtherRelationship;
                //    }
                //    else
                //    {
                //        StartVisit.Instance.OtherGuardianRelationship = SelectedRelationship.Value;
                //    }

                //    StartVisit.Instance.OtherGuardianName = OtherGuardianName;
                //}
                //else
                //{
                //    StartVisit.Instance.PatientGuardianID = SelectedAdult.PatientID;
                //}

                //StartVisit.Instance.PatientID = SelectedPatient.PatientID;
                //StartVisit.Instance.PatientName = SelectedPatient.ToString();

                await _navigationService.Navigate<PatientPreVisitMedicalHistoryViewModel>();
            }
            else
            {
                ShowWarning = true; 
                return;
            }
        }

    }
}
