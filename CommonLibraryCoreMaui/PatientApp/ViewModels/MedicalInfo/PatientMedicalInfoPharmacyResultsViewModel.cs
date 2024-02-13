using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.Models.NavigationParameters;
using CommonLibraryCoreMaui.ViewModels;
using MvvmCross.Commands;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels.MedicalInfo
{
    public class PatientMedicalInfoPharmacyResultsViewModel
    : BaseNavigationViewModel<PharmacyNavigationParam, Tuple<Pharmacy, bool>>, IMedicalIssueViewTitle
    {
        public IMvxMessenger _messenger;

        string BusinessName, StrretAddress, City, State, ZipCode;
        private List<Pharmacy> _listPharmacy;
        public List<Pharmacy> ListPharmacy
        {
            get { return _listPharmacy; }
            set { SetProperty(ref _listPharmacy, value); }
        }

        private Pharmacy _selectedPharmacy;
        public Pharmacy SelectedPharmacy
        {
            get { return _selectedPharmacy; }
            set { SetProperty(ref _selectedPharmacy, value); }
        }

        public IMvxCommand SelectPharmacyCommand => new MvxAsyncCommand(SelectPharmacyAsync);
        public IMvxCommand SelectOtherPharmacyCommand => new MvxAsyncCommand(SelectOtherPharmacyAsync);

        public MvvmCross.ViewModels.MvxInteraction NavigateBackToMedicalHistory { get; } = new MvvmCross.ViewModels.MvxInteraction();

        public string MedicalTitle { get; set; }

        public PatientMedicalInfoPharmacyResultsViewModel(IMvxMessenger messenger)
        {
            _messenger = messenger;
        }

        private async Task SelectPharmacyAsync()
        {
            if (SelectedPharmacy == null)
            {
                await _userDialogs.AlertAsync("Please select Pharmacy.");
                return;
            }

            NavigateBackToMedicalHistory.Raise();

          //  await _navigationService.Close(this, new Tuple<Pharmacy, bool>(SelectedPharmacy, true));
            await _navigationService.Close(this);

            //var message = new PharmacyMessage(this, SelectedPharmacy);
            //_messenger.Publish(message);
        }

        private async Task SelectOtherPharmacyAsync()
        {
          //  await _navigationService.Close(this, new Tuple<Pharmacy, bool>(new Pharmacy(), false));
            await _navigationService.Close(this);
        }

        private async Task Process()
        {
            try
            {
                IsBusy = true;
                ListPharmacy = await DataUtility.SearchPharmacyAsync(SettingsValues.ApiURLValue,
                    !string.IsNullOrEmpty(BusinessName) ? BusinessName : null,
                    !string.IsNullOrEmpty(StrretAddress) ? StrretAddress : null,
                    !string.IsNullOrEmpty(City) ? City : null,
                    !string.IsNullOrEmpty(State) ? State : null,
                    !string.IsNullOrEmpty(ZipCode) ? ZipCode : null);

                if (ListPharmacy.Count == 0)
                {
                    ListPharmacy = null;
                    return;
                }
                else
                {


                }
            }
            catch { }
            IsBusy = false;
        }
        public override void ViewAppearing()
        {
            base.ViewAppearing();
            MvxNotifyTask.Create(Process);
        }

        public override void Prepare(PharmacyNavigationParam parameter)
        {
            BusinessName = parameter.BusinessName;
            StrretAddress = parameter.StreetAddress;
            City = parameter.City;
            State = parameter.State;
            ZipCode = parameter.ZipCode;
            base.Prepare();

            MedicalTitle = $"{(parameter.NavigationParam.NavigationType == Models.NavigationParameters.MedicalInfoNavigationType.VisitHistoryPatient ? "Visit for " : "")}{parameter.NavigationParam.Name}";
        }
    }

}
