using System.Threading.Tasks;
using CommonLibraryCoreMaui.ViewModels;
//using MvvmCross.Commands;
using CommonLibraryCoreMaui.Services;
using CommonLibraryCoreMaui.Models;
using MvvmCross.ViewModels;
using MvvmCross.Commands;
//using MvvmCross.ViewModels;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class HomeViewModel : LogoutFunctionalityViewModel
    {
        private string _customerServiceNumber = "512-421-5678";
        public string CustomerServiceNumber
        {
            get { return _customerServiceNumber; }
            set { SetProperty(ref _customerServiceNumber, value); }
        }

        private bool _isPatientTermed;
        public bool IsPatientTermed
        {
            get { return _isPatientTermed; }
            set
            {
                SetProperty(ref _isPatientTermed, value);
            }
        }

        private bool _isCurativeNewPatient;
        public bool IsCurativeNewPatient
        {
            get { return _isCurativeNewPatient; }
            set
            {
                SetProperty(ref _isCurativeNewPatient, value);
            }
        }

        private HomeZipCodeSearchCapsuleViewModel _homeZipCodeSearchCapsuleResponse;
        public HomeZipCodeSearchCapsuleViewModel HomeZipCodeSearchCapsuleResponse
        {
            get { return _homeZipCodeSearchCapsuleResponse; }
            set
            {
                SetProperty(ref _homeZipCodeSearchCapsuleResponse, value);
            }
        }

        public IVisitsService _visitsService;
        public ICapsuleService _capsuleService;
        public IMedicalHistoryService _medicalService;
        public IMvxCommand StartVisitCommand => new MvxAsyncCommand(StartVisit);
        //bool IsTermed = false;
        public HomeViewModel(IVisitsService vService, ICapsuleService cService, IMedicalHistoryService mService)
        {
            _visitsService = vService;
            _capsuleService = cService;
            _medicalService = mService;

            HomeZipCodeSearchCapsuleResponse = new HomeZipCodeSearchCapsuleViewModel() 
            { 
                ZipCode = string.Empty,
                ShowWarning = false,
                WarningText = string.Empty
            };
        }

        public async override Task Initialize()
        {
            IsBusy = true;
            try
            {
                string customerServicePhone = await Globals.Instance.CustomerServicePhone.ConfigureAwait(false);
                if (!string.IsNullOrEmpty(customerServicePhone))
                    CustomerServiceNumber = customerServicePhone;
                IsPatientTermed = !Globals.Instance.IsTermed;
                if(Globals.Instance.IsCurative) await CheckEligibityForCurative();
                else await CheckUserEligibityForCapsule();

                var res = await DataUtility.GetPatientAccountVisitsAsync(SettingsValues.ApiURLValue, (int)Globals.Instance.UserInfo.PatientID, CommonAuthSession.Token);
                if (res != null)
                {
                    var subscription = (Globals.Instance.UserInfo.CurrentSubscriptionPlan == "Individual Subscription" || Globals.Instance.UserInfo.CurrentSubscriptionPlan == "Individual 365 Plan") ? true : false;
                    Globals.Instance.HadPeviousVisit = res.TotalVisitCount > 0 && subscription;
                }

            }
            catch { }
            IsBusy = false;
            await base.Initialize();
        }

        private async Task StartVisit()
        {
            Models.StartVisit.Instance.Clear();

            StatusResponse resp = await _visitsService.GetRemainingVisitCount(Globals.Instance.UserInfo.LoginID);
          //  GetPatientAccountVisits 
           var res = await DataUtility.GetPatientAccountVisitsAsync(SettingsValues.ApiURLValue, (int)Globals.Instance.UserInfo.PatientID, CommonAuthSession.Token);
            if (res != null)
            {
                var subscription = (Globals.Instance.UserInfo.CurrentSubscriptionPlan == "Individual Subscription" || Globals.Instance.UserInfo.CurrentSubscriptionPlan == "Individual 365 Plan") ? true : false;
                Globals.Instance.HadPeviousVisit = res.TotalVisitCount > 0 && subscription; }
            if (resp.StatusCode == StatusCode.SuccessSeePayload)
            {
                if (int.TryParse(resp.Payload, out int remainingVisitCount))
                {
                    if (remainingVisitCount > 0)
                    {
                        if( !(Globals.Instance.UserInfo.Domain == "Star" ) && !(Globals.Instance.UserInfo.Domain == "Star Kids")) 
                            await _navigationService.Navigate<PatientPreVisitPatientSelectionStep1ViewModel>();
                        else if (Globals.Instance.UserInfo.Domain =="Star" || Globals.Instance.UserInfo.Domain == "Star Kids")
                            await _navigationService.Navigate<PatientPreVisitPatientSelectionFamilyViewModel>();
                        //else
                        //    await _navigationService.Navigate<PatientPreVisitPatientSelectionIndividualViewModel>();
                    }
                    else
                    {
                        await _userDialogs.AlertAsync("You have no remaining visits for this cycle.");
                    }
                }
            }
        }
        public async Task<bool> CheckUserEligibityForCapsule()
		{
            return await _capsuleService.GetCapsuleEligibility(Globals.Instance.UserInfo.PatientID);
        }

     

        public async Task<bool> CheckZipEligibityForCapsule(string zip)
        {
            return await _capsuleService.GetZipCapsuleEligibility(zip);
        }

        public async Task UpdateCapsuleEligibility(int patientId)
		{
			var res = await _capsuleService.UpdateCapsuleEligibility(patientId);
			if (res.StatusCode != StatusCode.Success)
			{
				await _userDialogs.AlertAsync(res.ErrorMessage ?? res.Message);
			}
		}

        public async Task<CurativeCheckDTO> CheckEligibityForCurative()
        {
            CurativeCheckDTO resp = null;
            if (Globals.Instance.HasSeenHomeCurativeDialog == null || !Globals.Instance.HasSeenHomeCurativeDialog)
            {
                Globals.Instance.HasSeenHomeCurativeDialog = false;
                resp = await DataUtility.GetCurativeEligibilityForHomeViewDialogAsync(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.PatientID, CommonAuthSession.Token).ConfigureAwait(false);
                if (resp.IsCurative) Globals.Instance.IsCurative = true;
                if (resp != null && resp.CurativeEligibilityForHomeViewDialog)
                {
                    Globals.Instance.HasSeenHomeCurativeDialog = true;
                    if (resp.IsCurative)
                    {
                        IsCurativeNewPatient = resp.IsNewPatient;
                    }
                }
            }
            return resp;
        }

        public async Task UpdateCurativeEligibility(int patientId, bool homeDialogVisible)
        {
            var res = await DataUtility.UpdateCurativeEligibilityForHomeViewDialogAsync(SettingsValues.ApiURLValue,
                Globals.Instance.UserInfo.PatientID, homeDialogVisible, CommonAuthSession.Token);
            if (res.StatusCode != StatusCode.Success)
            {
                await _userDialogs.AlertAsync(res.ErrorMessage ?? res.Message);
            }
        }

        public async Task UpdateMedicalInfo(bool IsCapsule =false,bool IsCurative=false)
        {
            var medicalResp = await _medicalService.PatientGetMedicalHistory(Globals.Instance.UserInfo.PatientID);
            medicalResp.Pharmacy.BusinessName = "";
            medicalResp.Pharmacy.StreetAddress1 = "";
            medicalResp.Pharmacy.StreetAddress2 = "";
            medicalResp.Pharmacy.City = "";
            medicalResp.Pharmacy.Description = "";
            medicalResp.Pharmacy.State = "";
            medicalResp.Pharmacy.ZipCode = "";
            medicalResp.Pharmacy.IsCapsule = IsCapsule;
            medicalResp.Pharmacy.IsCurative = IsCurative;

            StatusResponse resp = await DataUtility.UpdateMedicalHistoryAsync(SettingsValues.ApiURLValue, medicalResp, CommonAuthSession.Token).ConfigureAwait(false);
			if (resp != null)
			{
				if (resp.StatusCode == StatusCode.Error)
				{
					//handle if there is an error
				}
			}
		}
    }

    public class LogoutFunctionalityViewModel : BaseViewModel
    {
        public async override Task Initialize()
        {
            await base.Initialize();
        }

		public override void ViewAppeared()
		{
			base.ViewAppeared();
		}

		public override void ViewDisappeared()
		{
			base.ViewDisappeared();
		}

		public IMvxCommand LogoutCommand => new MvxAsyncCommand(Logout);

        public MvvmCross.ViewModels.MvxInteraction NavigateBackToLogin { get; } = new MvvmCross.ViewModels.MvxInteraction();

        private async Task Logout()
        {
            var logout = await _userDialogs.ConfirmAsync("Are you sure you want to sign out?", "Sign Out", "Yes", "No");
            if (!logout) return;
            Globals.Instance.HasSeenHomeCurativeDialog =false;
            Globals.Instance.HasSeenCurativeDialog = false;
            var handler = DependencyResolver.Get<IiOSLogout>();
            if (await handler.Logout(_userDialogs)) NavigateBackToLogin.Raise();
        }
    }

    public class HomeZipCodeSearchCapsuleViewModel : MvxViewModel
    {
        private string _zipCode;
        public string ZipCode
        {
            get { return _zipCode; }
            set 
            { 
                _zipCode = value; 
                RaisePropertyChanged(() => ZipCode); 
                if(!string.IsNullOrEmpty(_zipCode))
				{
                    WarningText = string.Empty;
                    ShowWarning = false;
				}
            }
        }

        private string _warningText;
        public string WarningText
        {
            get { return _warningText; }
            set { _warningText = value; RaisePropertyChanged(() => WarningText); }
        }

        private bool _showWarning;
        public bool ShowWarning
        {
            get { return _showWarning; }
            set { _showWarning = value; RaisePropertyChanged(() => ShowWarning); }
        }
    }
}