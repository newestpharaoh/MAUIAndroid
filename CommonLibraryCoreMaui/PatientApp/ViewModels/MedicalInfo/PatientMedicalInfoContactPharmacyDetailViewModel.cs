using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.Models.NavigationParameters;
using CommonLibraryCoreMaui.PatientApp.ViewModels.MedicalInfo;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    //public class PatientMedicalInfoContactPharmacyDetailViewModel : MvxViewModel<Models.NavigationParameters.MedicalHistoryPharmacyNavigationParam, Pharmacy>, IMedicalIssueViewTitle
    public class PatientMedicalInfoContactPharmacyDetailViewModel : MvxViewModel<Models.NavigationParameters.MedicalHistoryPharmacyNavigationParam>, IMedicalIssueViewTitle
    {
        public IMvxNavigationService _navigationService;
        public IUserDialogs _userDialogs;

        private Pharmacy _patientPharmacy;
        public Pharmacy PatientPharmacy
        {
            get { return _patientPharmacy; }
            set { SetProperty(ref _patientPharmacy, value); }
        }

        private string _headingText;
        public string HeadingText
        {
            get { return _headingText; }
            set { SetProperty(ref _headingText, value); }
        }

        private string _pharmacyText;
        public string PharmacyText
        {
            get { return _pharmacyText; }
            set { SetProperty(ref _pharmacyText, value); }
        }

        private bool _findAdd = false;
        public bool FindAdd
        {
            get { return _findAdd; }
            set { SetProperty(ref _findAdd, value); }
        }

        private string _addPharmacyText;
        public string AddPharmacyText
        {
            get { return _addPharmacyText; }
            set { SetProperty(ref _addPharmacyText, value); }
        }

        public string MedicalTitle { get; set; }

        private bool _isValidationHidden = true;
        public bool IsValidationHidden
        {
            get { return _isValidationHidden; }
            set { SetProperty(ref _isValidationHidden, value); }
        }
        private bool _isSearching = true;
        public bool IsSearching
        {
            get { return _isSearching; }
            set { SetProperty(ref _isSearching, value); }
        }
        private string _businessNameText;
        public string BusinessNameText
        {
            get { return _businessNameText; }
            set { SetProperty(ref _businessNameText, value); }
        }

        private string _zipText;
        public string ZipText
        {
            get { return _zipText; }
            set { SetProperty(ref _zipText, value); }
        }
        private bool _searchEnable = false;
        public bool SearchEnable
        {
            get { return _searchEnable; }
            set { SetProperty(ref _searchEnable, value); }
        }

        public IMvxCommand SaveProviderCommand => new MvxAsyncCommand(SearchPharmacyAsync);
        public MedicalHistoryNavigationParam NavigationParam { get; set; }

        public IMvxCommand SavePharmacyCommand => new MvxAsyncCommand(SavePharmacyAsync);

        public IMvxCommand SearchPharmacyCommand => new MvxAsyncCommand(SearchPharmacyAsync);

        public PatientMedicalInfoContactPharmacyDetailViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
        }

        private async Task SavePharmacyAsync()
        {
            this.IsValidationHidden = true;
            if (!PatientPharmacy.IsValid())
            {
                this.IsValidationHidden = false;
                return;
            }
          //  await _navigationService.Close(this, PatientPharmacy);
            await _navigationService.Close(this);
        }
        private async Task SearchPharmacyAsync()
        {
         //   var result = await _navigationService.Navigate < PatientMedicalInfoPharmacyResultsViewModel, PharmacyNavigationParam, Tuple< Pharmacy, bool>>
         var result = await _navigationService.Navigate<PatientMedicalInfoPharmacyResultsViewModel, PharmacyNavigationParam>
                    (new PharmacyNavigationParam()
                    {
                        BusinessName = PatientPharmacy?.BusinessName,
                        StreetAddress = PatientPharmacy?.StreetAddress1,
                        City = PatientPharmacy?.City,
                        State = PatientPharmacy?.State,
                        ZipCode = PatientPharmacy?.ZipCode,
                        NavigationParam = this.NavigationParam
                    });
            if (result == null) return;
            //PatientPharmacy = result.Item1;
            //IsSearching = result.Item2;

            if (IsSearching)
            {
               // await _navigationService.Close(this, PatientPharmacy);
                await _navigationService.Close(this);
            }
            else
            {
                HeadingText = "Add Pharmacy";
                BusinessNameText = "Name*";
                ZipText = "ZipCode";

                PatientPharmacy = new Pharmacy() { BusinessName = string.Empty, StreetAddress1 = string.Empty, City = string.Empty, State = string.Empty, ZipCode = string.Empty };
            }
        }
        public override void Prepare(Models.NavigationParameters.MedicalHistoryPharmacyNavigationParam parameter)
        {
            PharmacyText = "Please include pharmacy name and zip code.";
            AddPharmacyText = "Please include pharmacy name and address.If address is not known, please include a minimum of nearest cross streets.";

            PatientPharmacy = parameter.TupleParam.Item1;
            HeadingText = parameter.TupleParam.Item2 ? "Add Pharmacy" : "Edit Pharmacy";
            PharmacyText = string.IsNullOrEmpty(PatientPharmacy.PharmacyString) || string.IsNullOrEmpty(PatientPharmacy.BusinessName) ? PharmacyText : AddPharmacyText;
            FindAdd = IsSearching = string.IsNullOrEmpty(PatientPharmacy.BusinessName) ? true : false; // true --> find pharmacy hidden  false --> add farmacyhidden
            HeadingText = IsSearching ? "Find Pharmacy" : HeadingText;
            BusinessNameText = IsSearching ? "Pharmacy Name*" : "Name*";
            ZipText = IsSearching ? "Zip Code*" : "Zip Code";

            IsSearching = parameter.TupleParam.Item2;
            NavigationParam = parameter.NavigationParam;

            if (PatientPharmacy == null) { PatientPharmacy = new Pharmacy() { State = "TX" }; }

            MedicalTitle = $"{(parameter.NavigationParam.NavigationType == Models.NavigationParameters.MedicalInfoNavigationType.VisitHistoryPatient ? "Visit for " : "")}{parameter.NavigationParam.Name}";
            base.Prepare();
        }


        //public searchPharmacy()
        //{
        //    this.error = false;


        //    if (this.operationType == "Search")
        //    {

        //        let lstateLongValue = this.States.filter(item => item.value === this.State)[0].viewValue;
        //        this.ps.PharmacySearch(this.BusinessName, this.StreetAddress1, this.City, lstateLongValue, this.ZipCode).subscribe(resp => {
        //            this.pharmacySearched = resp;
        //            this.searched = true;
        //            this.isLoaded = true;
        //            this.select(this.pharmacySearched[0], 0);
        //            this.searched = true;
        //            this.isLoaded = true;
        //        });
        //    }
        //    else if (this.BusinessName != "" && this.ZipCode != "")
        //    {

        //        let pharmacy: IPharmacy =
        //            {
        //        BusinessName: this.BusinessName, 
        //            StreetAddress1: this.StreetAddress1,
        //            StreetAddress2: "",             
        //            City: this.City,
        //            State: this.State,
        //            ZipCode: this.ZipCode,
        //            Description: this.Description,
        //            IsCapsule: false,
        //            IsCurative: false
        //        }

        //        this.dialogRef.close(pharmacy);
        //    }
        //    else
        //    {
        //        this.error = true;
        //        if (this.BusinessName == "")
        //            this.errorText = this.errorBusinessNameHiden //"You must enter a pharmacy name";
        //        else if (this.ZipCode == "")
        //            this.errorText = this.errorZipcodeHidden //"You must enter a address name";


        //    }

        //}

        //public savePharmacy()
        //{

        //    if (this.BusinessName != "" && this.ZipCode != "")
        //    {
        //        let pharmacy: IPharmacy = {
        //        BusinessName: this.BusinessName,
        //        StreetAddress1: this.StreetAddress1,
        //        StreetAddress2: "",
        //        City: this.City,
        //        State: this.State,
        //        ZipCode: this.ZipCode,
        //        Description: this.Description,
        //        IsCapsule: false,
        //        IsCurative: false
        //        }

        //        this.dialogRef.close(pharmacy);
        //    }
        //    else
        //    {
        //        //this.snackBarService.dismiss();
        //        //this.snackBarService.open('Please fill the form', undefined, { duration: 2000 });
        //    }
        //}

    }
}
