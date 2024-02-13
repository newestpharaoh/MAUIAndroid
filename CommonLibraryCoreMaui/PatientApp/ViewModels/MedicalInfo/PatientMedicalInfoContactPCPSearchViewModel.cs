using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.Models.NavigationParameters;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using MvvmCross.Navigation;


namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class PatientMedicalInfoContactPCPSearchViewModel : MvxViewModel<MedicalHistoryPCPNavigationParam>, IMedicalIssueViewTitle
    {
		public IMvxNavigationService _navigationService;
		public IUserDialogs _userDialogs;

		private PCP _patientPCP;
		public PCP PatientPCP
		{
			get { return _patientPCP; }
			set { _patientPCP = value; }
		}

		private bool _isSearching = true;
		public bool IsSearching
		{
			get { return _isSearching; }
			set { _isSearching = value; }
		}

        private bool _isValidationHidden = true;
        public bool IsValidationHidden
        {
            get { return _isValidationHidden; }
            set { _isValidationHidden = value; }
        }

        public string MedicalTitle { get; set; }

        private string _headingText;
		public string HeadingText
		{
			get { return _headingText; }
			set { _headingText = value; }
		}

        private string _stateLabel;
        public string StateLabel
        {
            get { return _stateLabel; }
            set { _stateLabel = value; }
        }

        private string _firstNameLabel;
        public string FirstNameLabel
        {
            get { return _firstNameLabel; }
            set { _firstNameLabel = value; }
        }

        private string _lastNameLabel;
        public string LastNameLabel
        {
            get { return _lastNameLabel; }
            set { _lastNameLabel = value; }
        }

        public MedicalHistoryNavigationParam NavigationParam { get; set; }

        public IMvxCommand SearchProviderCommand => new MvxAsyncCommand(SearchProviderAsync);
		public IMvxCommand SaveProviderCommand => new MvxAsyncCommand(SaveProviderAsync);
		public IMvxCommand CancelProviderCommand => new MvxAsyncCommand(CancelProviderAsync);

		public PatientMedicalInfoContactPCPSearchViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs)
		{
			_navigationService = navigationService;
			_userDialogs = userDialogs;
		}

		public override void Prepare(MedicalHistoryPCPNavigationParam parameter)
		{
			HeadingText = parameter.TupleParam.Item2 ? "Find Your Primary Care Provider" : "Edit Primary Care Provider";
            FirstNameLabel = parameter.TupleParam.Item2 ? "First Name" : "First Name*";
            LastNameLabel = parameter.TupleParam.Item2 ? "Last Name" : "Last Name*";
            StateLabel = parameter.TupleParam.Item2 ? "State" : "State*";
            PatientPCP = parameter.TupleParam.Item1 ?? new PCP() { State = "TX" };
            if (PatientPCP.State.Length > 2)
            {
                string desc = PatientPCP.State;
                PatientPCP.State = EnumEx.GetValueFromDescription<State>(desc).ToString();
            }

			IsSearching = parameter.TupleParam.Item2;
            NavigationParam = parameter.NavigationParam;
            MedicalTitle = $"{(parameter.NavigationParam.NavigationType == Models.NavigationParameters.MedicalInfoNavigationType.VisitHistoryPatient ? "Visit for " : "")}{parameter.NavigationParam.Name}";
            base.Prepare();
		}

		private async Task CancelProviderAsync()
		{
			await _navigationService.Close(this);
		}

        private async Task SaveProviderAsync()
        {
            IsValidationHidden = true;
            if (string.IsNullOrEmpty(PatientPCP.FirstName.Trim()) ||
                string.IsNullOrEmpty(PatientPCP.LastName.Trim()))
            {
                IsValidationHidden = false;
                return;
            }
            //  await _navigationService.Close(this, PatientPCP);
            await _navigationService.Close(this);
        }

        private async Task SearchProviderAsync()
		{
			var result = await _navigationService.Navigate<PatientMedicalInfoContactPCPResultsViewModel, PrimaryCareNavigationParam>
					(new PrimaryCareNavigationParam() { FirstName = PatientPCP?.FirstName, LastName = PatientPCP?.LastName, State = PatientPCP?.State,
                        NavigationParam = this.NavigationParam });
			if (result == null) return;
			//PatientPCP = result.Item1;
			//IsSearching = result.Item2;

            if (IsSearching)
            {
                //  await _navigationService.Close(this, PatientPCP);
                await _navigationService.Close(this);
            }
            else
            {
                HeadingText = "Add Primary Care Provider";
                FirstNameLabel = "First Name*";
                LastNameLabel = "Last Name*";
                StateLabel = "State*";
                PatientPCP = new PCP() { State = "TX", FirstName = string.Empty, LastName = string.Empty };
            }
        }
	}
}
