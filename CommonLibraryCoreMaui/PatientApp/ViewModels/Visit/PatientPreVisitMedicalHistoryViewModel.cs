using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.Models.NavigationParameters;
using CommonLibraryCoreMaui.Services;
using CommonLibraryCoreMaui.ViewModels;
using MvvmCross.Commands;
using System.Threading.Tasks;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class PatientPreVisitMedicalHistoryViewModel : BaseViewModel
    {
        public IMedicalHistoryService _medicalHistoryService;

        public IMvxCommand ContinueCommand => new MvxAsyncCommand(Continue);
		public IMvxCommand UpdateCommand => new MvxAsyncCommand(Update);
        public IMvxCommand UpdateBestContactNumber => new MvxAsyncCommand(UpdateContactNumber);
        public PatientPreVisitMedicalHistoryViewModel(IMedicalHistoryService medicalHistoryService)
        {
            _medicalHistoryService = medicalHistoryService;
			PatientName = StartVisit.Instance.PatientName;
		}

        private string _patientName;
        public string PatientName
        {
            get { return _patientName; }
            set
            {
                SetProperty(ref _patientName, value);
            }
        }

        private PreVisitMedicalHistoryItemsViewModel _medicalHistory;
        public PreVisitMedicalHistoryItemsViewModel MedicalHistory
        {
            get { return _medicalHistory; }
            set
            {
                SetProperty(ref _medicalHistory, value);
            }
        }

        public async override Task Initialize()
        {
            IsBusy = true;
            try
            {
                CommonLibraryCoreMaui.Models.MedicalInfo medicalInfo = await _medicalHistoryService.PatientGetMedicalHistory((int)StartVisit.Instance.PatientID).ConfigureAwait(false);
                var vm = new PreVisitMedicalHistoryItemsViewModel();
                vm.MedicalInfo = medicalInfo;
                vm.MedicalIssues = await _medicalHistoryService.GetMedicalIssues().ConfigureAwait(false);
                MedicalHistory = vm;
            }
            catch { }
            IsBusy = false;
            await base.Initialize();
        }

        private async Task Continue()
        {
            await _navigationService.Navigate<PatientPreVisitProviderSelectionViewModel>();
        }

		private async Task Update()
		{
			var result = await _navigationService.Navigate<PatientMedicalnfoViewModel, MedicalHistoryNavigationParam>(
				new MedicalHistoryNavigationParam() { PatientId = (int)StartVisit.Instance.PatientID, Name = StartVisit.Instance.PatientName , NavigationType = MedicalInfoNavigationType.VisitHistoryPatient });
			if (result)
			{
				await Initialize();
			}
		}

        private async Task UpdateContactNumber()
        {

            //TODO: WORK to add a function
            //Temporarly save the contact number for visit
            //var result = await _navigationService.Navigate<PatientMedicalnfoViewModel, MedicalHistoryNavigationParam, bool>(
            //    new MedicalHistoryNavigationParam() { PatientId = (int)StartVisit.Instance.PatientID, Name = StartVisit.Instance.PatientName, NavigationType = MedicalInfoNavigationType.VisitHistoryPatient });
            //if (result)
            //{
                await Initialize();
          //  }

        }
    }
}
