using Acr.UserDialogs;
using System.Threading.Tasks;
using CommonLibraryCoreMaui.Models;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using MvvmCross.Commands;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    //public class PatientMedicalIssueViewModel : MvxViewModel<Models.NavigationParameters.MedicalHistoryIssueNavigationParam, PrimaryIssue>, IMedicalIssueViewTitle
    public class PatientMedicalIssueViewModel :MvxViewModel<Models.NavigationParameters.MedicalHistoryIssueNavigationParam>, IMedicalIssueViewTitle
    {
        IMvxNavigationService _navigationService;
        IUserDialogs _userDialogs;

        public bool IsEdit { get; set; }
        private PrimaryIssue _issue;
        public PrimaryIssue MedicalIssue
        {
            get => _issue;  //{ return _issue; }
            set
            {
                _issue = value;
           //     OnPropertyChanged(nameof(MedicalIssue));
            }
            //    set { SetProperty(ref _issue, value); }
        }

        private bool _isNavBarHidden;
        public bool IsNaveBarHidden
        {
            get { return _isNavBarHidden; }
            set { SetProperty(ref _isNavBarHidden, value); }
        }

        public IMvxCommand SaveIssueCommand => new MvxAsyncCommand(SaveIssueAsync);

        public string MedicalTitle { get; set; }

        private bool _isValidationHidden = true;
        public bool IsValidationHidden
        {
            get { return _isValidationHidden; }
            set { SetProperty(ref _isValidationHidden, value); }
        }

        public PatientMedicalIssueViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }

        public override void Prepare(Models.NavigationParameters.MedicalHistoryIssueNavigationParam parameter)
        {
            base.Prepare();
            MedicalIssue = parameter.TupleParam.Item1;
            IsNaveBarHidden = parameter.TupleParam.Item2;
            MedicalTitle = $"{(parameter.NavigationParam.NavigationType == Models.NavigationParameters.MedicalInfoNavigationType.VisitHistoryPatient ? "Visit for " : "")}{parameter.NavigationParam.Name}";
            IsEdit = parameter.IsEdit;
        }

        private async Task SaveIssueAsync()
        {
            IsValidationHidden = true;
            if (string.IsNullOrEmpty(MedicalIssue.Name.Trim()))
            {
                IsValidationHidden = false;
                return;
            }
            // await _navigationService.Close(this, MedicalIssue);
            await _navigationService.Close(this);
        }
    }

    public interface IMedicalIssueViewTitle
    {
        string MedicalTitle { get; set; }
    }
}
