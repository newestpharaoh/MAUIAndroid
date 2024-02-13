using CommonLibraryCoreMaui.Models.NavigationParameters;
using CommonLibraryCoreMaui.ViewModels;
using Microsoft.Maui.ApplicationModel;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Threading.Tasks;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class PatientPostVisitSurveyViewModel : BaseNavigationViewModel<VisitDetailNavigationParam>
    {
        private string _visitID;
        public string VisitID
        {
            get { return _visitID; }
            set { SetProperty(ref _visitID, value); }
        }

        private string _surveyURL;
        public string SurveyURL
        {
            get { return _surveyURL; }
            set { SetProperty(ref _surveyURL, value); }
        }
        // public string SurveyURL => SettingsValues.SurveyUrl + $"?VisitID={VisitID}";

        public IMvxCommand OkayCommand => new MvxAsyncCommand(OkayAsync);

        public PatientPostVisitSurveyViewModel(IMvxNavigationService mvxNavigationService)
        {
            _navigationService = mvxNavigationService;
        }

        public override void Prepare(VisitDetailNavigationParam parameter)
        {
            VisitID = parameter?.VisitId;
            base.Prepare();
        }

        private async Task GetSurveyURL()
        {
            SurveyURL = await DataUtility.GetSurveyMonkeyURL(SettingsValues.ApiURLValue, VisitID);
        }
        private async Task OkayAsync()
        {
            await GetSurveyURL();
            await Browser.OpenAsync(SurveyURL, BrowserLaunchMode.SystemPreferred);
            await _navigationService.Navigate<PatientPostVisitSurveyThanksViewModel, string>(VisitID ?? string.Empty);
        }
        public async Task NoThankksAsync()
        {
            await DataUtility.SendSurveyEmail(SettingsValues.ApiURLValue, SurveyURL, CommonAuthSession.Token, VisitID);
        }
    }
}
