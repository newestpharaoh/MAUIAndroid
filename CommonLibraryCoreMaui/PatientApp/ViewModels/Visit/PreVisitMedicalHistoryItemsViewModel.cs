using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.ViewModels;
using MvvmCross.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class PreVisitMedicalHistoryItemsViewModel : BaseViewModel
    {
        private List<MedicalIssue> _medicalIssues;
        public List<MedicalIssue> MedicalIssues
        {
            get { return _medicalIssues; }
            set { SetProperty(ref _medicalIssues, value); }
        }

		private CommonLibraryCoreMaui.Models.MedicalInfo _medicalInfo;
		public CommonLibraryCoreMaui.Models.MedicalInfo MedicalInfo
		{
			get { return _medicalInfo; }
			set { SetProperty(ref _medicalInfo, value); }
		}

		public IMvxCommand SaveMedicalHistoryCommand => new MvxAsyncCommand(SaveMedicalHistoryAsync);

		private async Task SaveMedicalHistoryAsync()
		{
			await _userDialogs.AlertAsync($"Save Action");
		}
	}
}
