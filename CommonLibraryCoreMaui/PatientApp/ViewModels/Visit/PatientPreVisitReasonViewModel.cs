using CommonLibraryCoreMaui;
using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.Models.NavigationParameters;
using CommonLibraryCoreMaui.Services;
using CommonLibraryCoreMaui.ViewModels;
using MvvmCross.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class PatientPreVisitReasonViewModel : BaseViewModel
    {
        IVisitsService _visitsService;
        RefershTimer providerAvailabilityCheckTimer;

        public IMvxCommand ContinueCommand => new MvxAsyncCommand(Continue);
        public PatientPreVisitReasonViewModel(IVisitsService visitsService)
        {
            _visitsService = visitsService;
        }

        private PreVisitReasonsViewModel _preVisitReasons;
        public PreVisitReasonsViewModel PreVisitReasons
        {
            get { return _preVisitReasons; }
            set { SetProperty(ref _preVisitReasons, value); }
        }

        //private async Task<bool> (string username, string pwd)
        //{
        //    TokenResponse resp = await DataUtility.GetTokenResponseAsync(SettingsValues.ApiURLValue, username, pwd, "Device.GetDeviceId(this)").ConfigureAwait(false);

        //    if (resp != null)
        //    {
        //        if (!string.IsNullOrEmpty(resp.AccessToken) && resp.ExpiresIn != null)
        //        {
        //            CommonAuthSession.Token = resp.AccessToken;
        //            CommonAuthSession.SetTokenExpirationDate(DateTime.Now.AddSeconds(Convert.ToInt32(resp.ExpiresIn)));
        //            UserInfo userInfo = await DataUtility.GetUserInfo(SettingsValues.ApiURLValue, resp.UserId, true, CommonAuthSession.Token).ConfigureAwait(false);
        //            Globals.Instance.UserInfo = userInfo;

        //            return true;
        //        }
        //    }

        //    return false;
        //}
        public async override Task Initialize()
        {
            if (StartVisit.Instance.PatientID != null && StartVisit.Instance.ProviderID != null)
            {
                providerAvailabilityCheckTimer = new RefershTimer(TimeSpan.FromMilliseconds(SettingsValues.PollVisitStatusPeriod), async () =>
                {
                    IProviderStatus ips = ProviderStatusFactory.Get(StartVisit.Instance.VisitID, (int)StartVisit.Instance.PatientID, (int)StartVisit.Instance.ProviderID);
                    if (await ips.IsProviderNotAvailable().ConfigureAwait(false))
                    {
                        await _navigationService.Close(this);
                    }
                });
            }
            IsBusy = true;
            try
            {
                var results1 = await DataUtility.PatientGetSubscriptionInfoAsync(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.PatientID, CommonAuthSession.Token).ConfigureAwait(false);

                if (results1 != null)
                {
                    StartVisit.Instance.Prepay = results1.IsPrepay;
                    StartVisit.Instance.Domain = results1.CurrentSubscriptionPlan;
                }
                List<GenericRecord> reasons = await _visitsService.PatientStartVisitStep4().ConfigureAwait(false);
                var vm = new PreVisitReasonsViewModel();
                vm.SelectedReasons = new List<int>();
                vm.Reasons = reasons;
                PreVisitReasons = vm;
            }
            catch { }
            IsBusy = false;
            await base.Initialize();
        }

        private async Task Continue()
        {
            if (PreVisitReasons.SelectedReasons.Count > 0 || !string.IsNullOrEmpty(PreVisitReasons.OtherReasonsForVisit))
            {
                StartVisit.Instance.OtherReasonsForVisit = PreVisitReasons.OtherReasonsForVisit ?? string.Empty;
                StartVisit.Instance.ReasonsForVisit = PreVisitReasons.SelectedReasons.Select(x => x.ToString()).ToList();
                VisitDetailsResponse resp = await _visitsService.PatientStartVisitStep5(StartVisit.Instance).ConfigureAwait(false);

                if (resp != null)
                {
                    StartVisit.Instance.VisitID = int.Parse(resp.VisitID);
                    await _navigationService.Navigate<PatientVisitsScreenViewModel, VisitDetailNavigationParam>(new VisitDetailNavigationParam()
                    {
                        VisitId = resp.VisitID,
                        ProviderId = resp.ProviderID.ToString(),
                        ProviderName = resp.ProviderName,
                        PatientFirstName = resp.PatientFirstName,
                        PatientLastName = resp.PatientLastName
                    });
                }
            }
            else
            {
                await _userDialogs.AlertAsync("Please provide at least one reason!");
            }
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            providerAvailabilityCheckTimer.Start();
        }

        public override void ViewDisappeared()
        {
            providerAvailabilityCheckTimer.Stop();
            base.ViewDisappeared();
        }
    }

    public class PreVisitReasonsViewModel : MvvmCross.ViewModels.MvxViewModel
    {
        private List<GenericRecord> _reasons;
        public List<GenericRecord> Reasons
        {
            get { return _reasons; }
            set { SetProperty(ref _reasons, value); }
        }

        private List<int> _selectedReasons;
        public List<int> SelectedReasons
        {
            get { return _selectedReasons; }
            set { SetProperty(ref _selectedReasons, value); }
        }

        private string _otherReasonsForVisit;
        public string OtherReasonsForVisit
        {
            get { return _otherReasonsForVisit; }
            set { SetProperty(ref _otherReasonsForVisit, value); }
        }
    }
}