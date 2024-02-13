using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.Models.NavigationParameters;
using CommonLibraryCoreMaui.Services;
using CommonLibraryCoreMaui.ViewModels;
using MvvmCross.Commands;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class PatientPreVisitPatientSelectionIndividualViewModel : BaseViewModel
    {
        public IVisitsService _visitsService;
        public IMvxCommand ContinueCommand => new MvxAsyncCommand(Continue);
      //  public IMvxCommand ResumeVisitCommand => new MvxAsyncCommand(ResumeVisit);
        public ObservableCollection<Patient> _patientsCollection;
        public virtual ObservableCollection<Patient> PatientsCollection
        {
            get { return _patientsCollection; }
            set { SetProperty(ref _patientsCollection, value); }
        }

        private Patient _selectedPatient;
        public Patient SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                SetProperty(ref _selectedPatient, value);
                //if (SelectedPatient != null)
                //    CheckForActiveVisit();
              //  RaisePropertyChanged(() => SelectedPatient);
            }
        }

        private ActiveVisitInfo _actVisitInfo;
        public ActiveVisitInfo ActVisitInfo
        {
            get { return _actVisitInfo; }
            set { SetProperty(ref _actVisitInfo, value); }
        }

        private bool _isResumeVisit;
        public bool IsResumeVisit
        {
            get { return _isResumeVisit; }
            set
            {
                SetProperty(ref _isResumeVisit, value);
               // ResumeVisit();
            }
        }

        public PatientPreVisitPatientSelectionIndividualViewModel(IVisitsService service)
        {
            _visitsService = service;
        }

        public async override Task Initialize()
        {
            IsBusy = true;
            try
            {
                PatientsForVisit resp = await _visitsService.PatientStartVisitStep1(Globals.Instance.UserInfo.LoginID);
                if (resp != null)
                {
                    PatientsCollection = new ObservableCollection<Patient>(resp.Adults);
                }
            }
            catch (Exception)
            {
            }
            IsBusy = false;
            await base.Initialize();
        }
        public async Task CheckForActiveVisit()
        {
            if (SelectedPatient != null)
                ActVisitInfo = await _visitsService.GetPatientActiveVisits(SelectedPatient.PatientID.Value, 0, "");

           // ActVisitInfo = _visitsService.GetPatientActiveVisits(SelectedPatient.PatientID.Value, 0, "").Result;
            
        }

        public async Task ResumeVisit()
        {
            if (ActVisitInfo.ActiveVisits.Count>0)
            {
                var VisitId =  ActVisitInfo.ActiveVisits[0].VisitID;
                var vd =  await _visitsService.GetVisitDetailAsync(VisitId).ConfigureAwait(false);
                if (vd != null)
                {
                    var resp = await _visitsService.PatientRestartVisit(VisitId,"en").ConfigureAwait(false);

                    if (resp.Message == "Success")
                    {
                        StartVisit.Instance.VisitID = Int32.Parse(vd.VisitID);
                        StartVisit.Instance.PatientID = vd.PatientID;
                        StartVisit.Instance.ProviderID = vd.ProviderID;
                        StartVisit.Instance.IsResumeVisit = true;

                        await _navigationService.Navigate<PatientVisitsScreenViewModel, VisitDetailNavigationParam>(new VisitDetailNavigationParam()
                        {
                            VisitId = vd.VisitID,
                            ProviderId = vd.ProviderID.ToString(),
                            ProviderName = vd.ProviderName,
                            PatientFirstName = vd.PatientFirstName,
                            PatientLastName = vd.PatientLastName
                        });
                    }
                }
            }


        }
        private async Task Continue()
        {
            if (SelectedPatient != null)
            {
                StartVisit.Instance.PatientID = SelectedPatient.PatientID;
                StartVisit.Instance.PatientName = SelectedPatient.ToString();
                await _navigationService.Navigate<PatientPreVisitMedicalHistoryViewModel>();
            }
        }
    }
}
