using System.Threading.Tasks;
using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.Models.NavigationParameters;
using CommonLibraryCoreMaui.Services;
using CommonLibraryCoreMaui.ViewModels;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
	public class PatientMedicalInfoMedicalHistoryDetailViewModel : BaseNavigationViewModel<MedicalHistoryNavigationParam>
	{
		public IMedicalHistoryService _medicalHistoryService;
		public MedicalHistoryNavigationParam PatientInfo;

		private PreVisitMedicalHistoryItemsViewModel _medicalHistory;
		public PreVisitMedicalHistoryItemsViewModel MedicalHistory
		{
			get { return _medicalHistory; }
			set { SetProperty(ref _medicalHistory, value); }
		}
		public IMvxCommand EditMedicalHistroyCommand => new MvxAsyncCommand(EditMedicalHistroyAsync);

		public PatientMedicalInfoMedicalHistoryDetailViewModel(IMedicalHistoryService medicalHistoryService, IMvxNavigationService navigationService)
		{
			_medicalHistoryService = medicalHistoryService;
			_navigationService = navigationService;
		}

		public override void Prepare(MedicalHistoryNavigationParam parameter)
		{
			PatientInfo = parameter;
			base.Prepare();
		}

		public async override Task Initialize()
		{
			await GetMedicalInfo();
			await base.Initialize();
		}

		private async Task GetMedicalInfo()
		{
			IsBusy = true;
			try
			{
				CommonLibraryCoreMaui.Models.MedicalInfo medicalInfo = await _medicalHistoryService.PatientGetMedicalHistory(PatientInfo.PatientId).ConfigureAwait(false);
				var vm = new PreVisitMedicalHistoryItemsViewModel();
				vm.MedicalInfo = medicalInfo;
				vm.MedicalIssues = await _medicalHistoryService.GetMedicalIssues().ConfigureAwait(false);
				MedicalHistory = vm;
			}
			catch { }
			IsBusy = false;
		}

		private async Task EditMedicalHistroyAsync()
		{
			var result = await _navigationService.Navigate<PatientMedicalnfoViewModel, MedicalHistoryNavigationParam>(
				new MedicalHistoryNavigationParam() { PatientId = PatientInfo.PatientId, Name= PatientInfo.Name, NavigationType = MedicalInfoNavigationType.My });
			if(result)
			{
				await Initialize();
			}
		}
	}
}
