using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.ViewModels;
using System.Collections.Generic;
using MvvmCross.ViewModels;
using System.Linq;
using MvvmCross.Navigation;
using Acr.UserDialogs;
using CommonLibraryCoreMaui.Models.NavigationParameters;
using CommonLibraryCoreMaui.Services;
using MvvmCross.Plugin.Messenger;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
	public class PatientMedicalnfoViewModel : BaseNavigationViewModel<MedicalHistoryNavigationParam, bool>, IMedicalIssueViewTitle
	{
		#region Variable
		public MedicalHistoryNavigationParam PatientMedicalInfo;
		CommonLibraryCoreMaui.Models.MedicalInfo medicalInfo;

        private MedicalIssuesViewModel _medicalIssuesViewModel;
        public MedicalIssuesViewModel MedicalIssuesViewModel
        {
            get { return _medicalIssuesViewModel; }
            set { SetProperty(ref _medicalIssuesViewModel, value); RaisePropertyChanged(() => MedicalIssuesViewModel); }
        }

        private MvxObservableCollection<Allergy> allergies;
		public MvxObservableCollection<Allergy> Allergies
		{
			get { return allergies; }
			set { SetProperty(ref allergies, value); RaisePropertyChanged(() => Allergies); }
		}

		private MvxObservableCollection<Medication> medications;
		public MvxObservableCollection<Medication> Medications
		{
			get { return medications; }
			set { SetProperty(ref medications, value); }
		}

		private MvxObservableCollection<Surgery> surgeries;
		public MvxObservableCollection<Surgery> Surgeries
		{
			get { return surgeries; }
			set { SetProperty(ref surgeries, value); }
		}

		private Pharmacy pharmacy;
		public Pharmacy Pharmacy
		{
			get { return pharmacy; }
			set 
			{ 
				SetProperty(ref pharmacy, value); 
				PharmacyValue = Pharmacy?.ToString() ?? null;
				IsCapsuleValue = Pharmacy?.IsCapsule ?? false;
				if (Globals.Instance.IsCurative)
				{
					IsCurativeValue = Pharmacy?.IsCurative ?? false;
				}
            }
		}

		private PCP pcp;
		public PCP Pcp
		{
			get { return pcp; }
			set { SetProperty(ref pcp, value); PCPValue = Pcp?.Preview; }
		}

		private string pcpValue;
		public string PCPValue
		{
			get { return pcpValue; }
			set { SetProperty(ref pcpValue, value); }
		}

		private string pharmacyValue;
		public string PharmacyValue
		{
			get { return pharmacyValue; }
			set 
			{ 
				SetProperty(ref pharmacyValue, value); 
				if(!string.IsNullOrEmpty(PharmacyValue))
				{
					IsPharmacyViewVisible = true;
				}
				else
				{
					IsPharmacyViewVisible = Globals.Instance.IsCurative?  IsCurativeValue: IsCapsuleValue;
				}
			}
		}

        //IsCurativeValue
        private bool isCurativeValue;
        public bool IsCurativeValue
        {
            get { return isCurativeValue; }
            set
            {
                SetProperty(ref isCurativeValue, value);
                if (IsCurativeValue)
                {
                    IsPharmacyViewVisible = true;
                }
                else
                {
                    IsPharmacyViewVisible = !string.IsNullOrEmpty(PharmacyValue);
                }
            }
        }

        private bool isCapsuleValue;
		public bool IsCapsuleValue
		{
			get { return isCapsuleValue; }
			set 
			{ 
				SetProperty(ref isCapsuleValue, value); 
				if(IsCapsuleValue)
				{
					IsPharmacyViewVisible = true;
				}
				else
				{
					IsPharmacyViewVisible = !string.IsNullOrEmpty(PharmacyValue);
				}
			}
		}

		private bool isPharmacyViewVisible;
		public bool IsPharmacyViewVisible
		{
			get { return isPharmacyViewVisible; }
			set { SetProperty(ref isPharmacyViewVisible, value); }
		}

		private bool isCapsuleValueChanged;
		public bool IsCapsuleValueChanged
		{
			get { return isCapsuleValueChanged; }
			set { SetProperty(ref isCapsuleValueChanged, value); }
		}

		private bool isAddPharmacy;
		public bool IsAddPharmacy
		{
			get { return isAddPharmacy; }
			set { SetProperty(ref isAddPharmacy, value); }
		}

		public bool IsSelfPay
		{
			get { return Registration.Instance.IsSelfPay; }
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

		private RadioSelectionViewModel _radioSelectionResponse;
		public RadioSelectionViewModel RadioSelectionResponse
		{
			get { return _radioSelectionResponse; }
			set
			{
				SetProperty(ref _radioSelectionResponse, value);
			}
		}

		public IMvxCommand SaveMedicalHistoryCommand => new MvxAsyncCommand(SaveMedicalHistoryAsync);
		public IMvxCommand CancelMedicalHistoryCommand => new MvxAsyncCommand(CancelMedicalHistoryAsync);

		public IMvxCommand AddIssueCommand => new MvxAsyncCommand<object>(AddIssueAsync);
		public IMvxCommand EditIssueCommand => new MvxAsyncCommand<object>(EditIssueAsync);
		public IMvxCommand DeleteIssueCommand => new MvxAsyncCommand<object>(DeleteIssueAsync);

		public IMvxCommand AddPCCommand => new MvxAsyncCommand(AddPCAsync);
		public IMvxCommand EditPCCommand => new MvxAsyncCommand(EditPCAsync);
		public IMvxCommand DeletePCCommand => new MvxAsyncCommand(DeletePCAsync);

		public IMvxCommand AddPharmacyCommand => new MvxAsyncCommand(AddPharmacyAsync);
		public IMvxCommand EditPharmacyCommand => new MvxAsyncCommand(EditPharmacyAsync);
		//public IMvxCommand EditPharmacyForCapsuleCommand => new MvxAsyncCommand(EditPharmacyForCapsuleAsync);
        public IMvxCommand EditPharmacyForSeleCommand => new MvxAsyncCommand(EditPharmacyForSelectiveAsync);
        public IMvxCommand DeletePharmacyCommand => new MvxAsyncCommand(DeletePharmacyAsync);

		//public IMvxCommand AddCapsulePharmacyCommand => new MvxCommand(AddCapsulePharmacy);
        //
        public IMvxCommand AddSelPharmacyCommand => new MvxCommand(AddSelectivePharmacy);
        public IMvxCommand EditCapsulePharmacyCommand => new MvxAsyncCommand(EditCapsulePharmacyAsync);
        
        public IMvxCommand EditSelPharmacyCommand => new MvxAsyncCommand(EditSelectivePharmacyCommand);
      //  public IMvxCommand AddSelectivePharmacyCommand => new MvxCommand(AddSelectivePharmacy);
        public MvxInteraction<bool> OpenCapsuleDialog { get; } = new MvxInteraction<bool>();
        //
        public MvxInteraction<bool> OpenCurativeDialog { get; } = new MvxInteraction<bool>();
        //public MvxInteraction NavigateToFamilyMembers { get; } = new MvxInteraction();

        public MvxInteraction NavigateToFamilyMembers { get; } = new MvxInteraction();
		public ICapsuleService _capsuleService;
		public IMedicalHistoryService _medicalService;
		
		public string MedicalTitle { get; set; }

        private readonly MvxSubscriptionToken _token;

        private void OnPCPMessage(PCPMessage pcpMessage)
        {
            Pcp = pcpMessage.Pcp;
        }

        private bool _isValidationHidden = true;
        public bool IsValidationHidden
        {
            get { return _isValidationHidden; }
            set { SetProperty(ref _isValidationHidden, value); }
        }
		private bool isEligibleForCurative;
        public bool IsEligibleForCurative
        {
            get { return isEligibleForCurative; }
            set { SetProperty(ref isEligibleForCurative, value); }
        }

        private bool isEligibleForCapsule;
		public bool IsEligibleForCapsule
		{
			get { return isEligibleForCapsule; }
			set { SetProperty(ref isEligibleForCapsule, value); }
		}
		
		#endregion

		#region Initialization
		public PatientMedicalnfoViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs, IMvxMessenger messenger,
			ICapsuleService cService, IMedicalHistoryService mService)
		{
			_navigationService = navigationService;
			_userDialogs = userDialogs;
            _token = messenger.Subscribe<PCPMessage>(OnPCPMessage);
			_capsuleService = cService;
			_medicalService = mService;

			HomeZipCodeSearchCapsuleResponse = new HomeZipCodeSearchCapsuleViewModel()
			{
				ZipCode = string.Empty,
				ShowWarning = false,
				WarningText = string.Empty
			};

			RadioSelectionResponse = new RadioSelectionViewModel();
		}

		public override void Prepare(MedicalHistoryNavigationParam parameter)
		{
			PatientMedicalInfo = new MedicalHistoryNavigationParam()
			{
				PatientId = parameter.PatientId,
				PatientAdditionalFamilyMember = parameter.PatientAdditionalFamilyMember,
				NavigationType = parameter.NavigationType,
				Name = parameter.Name
			};

            MedicalTitle = $"{(parameter.NavigationType == MedicalInfoNavigationType.VisitHistoryPatient ? "Visit for " : "")}{parameter.Name}";
			

			base.Prepare();
        }

		public async override Task Initialize()
		{
			await base.Initialize();
			IsBusy = true;
			try
			{
				await CheckZipEligibityForCapsule();
                await CheckEligibityForCurative();
                medicalInfo = new CommonLibraryCoreMaui.Models.MedicalInfo();
				if (PatientMedicalInfo.PatientId == 0)
				{
                    List<MedicalIssue> medicalIssues = await DataUtility.GetMedicalIssuesAsync(SettingsValues.ApiURLValue).ConfigureAwait(false);
                    List<MedicalIssueEx> medicalIssuesX = new List<MedicalIssueEx>();
                    foreach (MedicalIssue mi in medicalIssues)
                    {
                        MedicalIssueEx x = new MedicalIssueEx();
                        x.ID = mi.ID;
                        x.IsChecked = mi.IsChecked;
                        x.Value = mi.Value;
                        x.Description = mi.Description;
                        medicalIssuesX.Add(x);
                    }

                    MedicalIssuesViewModel model = new MedicalIssuesViewModel();
                    model.MedicalIssues = new MvxObservableCollection<MedicalIssueEx>(medicalIssuesX);
                    MedicalIssuesViewModel = model;

                    if (PatientMedicalInfo.NavigationType == MedicalInfoNavigationType.AddMember)
					{
						await GetPharmacyFromPrimaryAccount().ConfigureAwait(false);
					}
				}
				else
				{
					await GetMedicalHistory();
				}
			}
			catch { }
			
			IsBusy = false;
		}

		private async Task GetPharmacyFromPrimaryAccount()
		{
			CommonLibraryCoreMaui.Models.MedicalInfo mi = await DataUtility.PatientGetMedicalHistoryAsync(SettingsValues.ApiURLValue, CommonAuthSession.Token, Globals.Instance.UserInfo.PatientID).ConfigureAwait(false);
			Pharmacy = medicalInfo.Pharmacy = mi?.Pharmacy;
		}
        #endregion
        #region Curative
        public async Task CheckEligibityForCurative()
        {
			await LoginNewUser(Registration.Instance.Email, Registration.Instance.Password).ConfigureAwait(false);
			//var newPatient = PatientMedicalInfo.NavigationType == MedicalInfoNavigationType.Registration ? true : false;
			IsEligibleForCurative = Globals.Instance.IsCurative;
        }

        public void UpdateMedicalInfoForSelective(bool isPhxSelected = true)
        {
            if (Pharmacy == null)
            {
                Pharmacy = new Pharmacy();
            }
            Pharmacy.SetEmpty();
			if (Globals.Instance.IsCurative)			 
				IsCurativeValue = Pharmacy.IsCurative = isPhxSelected; 					
			else IsCapsuleValue = Pharmacy.IsCapsule = isPhxSelected;
        }
        public void UpdateMedicalInfoForCurativee(bool isCurative = true)
        {
            if (Pharmacy == null)
            {
                Pharmacy = new Pharmacy();
            }
            Pharmacy.SetEmpty();
            IsCurativeValue = Pharmacy.IsCurative = isCurative;
        }
        #endregion
        #region Capsule
        public async Task CheckZipEligibityForCapsule()
		{
			await LoginNewUser(Registration.Instance.Email, Registration.Instance.Password).ConfigureAwait(false);

			var zip = PatientMedicalInfo.NavigationType == MedicalInfoNavigationType.Registration ?
				Registration.Instance.Zip : Globals.Instance.UserInfo.Zip;
			
			IsEligibleForCapsule = await _capsuleService.GetZipCapsuleEligibility(zip);
		}

		public async Task<bool> GetZipCapsuleEligibility(string zip)
		{
			return await _capsuleService.GetZipCapsuleEligibility(zip);
		}

		public void UpdateMedicalInfoForCapsule(bool isCapsule = true)
		{
			if (Pharmacy == null)
			{
				Pharmacy = new Pharmacy();
			}
			Pharmacy.SetEmpty();
			IsCapsuleValue = Pharmacy.IsCapsule = isCapsule;
		}
		#endregion

		#region Primary Care
		private async Task AddPCAsync()
		{
			var result = await _navigationService.Navigate<PatientMedicalInfoContactPCPSearchViewModel, MedicalHistoryPCPNavigationParam>(
				new MedicalHistoryPCPNavigationParam() { TupleParam = new Tuple<PCP, bool>(null, true), NavigationParam = PatientMedicalInfo });
			if (result == null) return;
		//	Pcp = result;
		}

		private async Task EditPCAsync()
		{
			var result = await _navigationService.Navigate<PatientMedicalInfoContactPCPSearchViewModel, MedicalHistoryPCPNavigationParam>(
                new MedicalHistoryPCPNavigationParam() { TupleParam = new Tuple<PCP, bool>(Pcp, false), NavigationParam = PatientMedicalInfo });
            if (result == null) return;
			//Pcp = result;
		}

		private async Task DeletePCAsync()
		{
			var response = await _userDialogs.ConfirmAsync("Are you sure you want to delete?");
			if (!response)
				return;
			Pcp = null;
		}

		#endregion

		#region Pharmacy

		private void AddSelectivePharmacy()
		{
            IsAddPharmacy = true;
			if(Globals.Instance.IsCurative) OpenCurativeDialog.Raise(true);
            else OpenCapsuleDialog.Raise(true);
        }

  //      private void AddCapsulePharmacy()
		//{
		//	OpenCapsuleDialog.Raise(true);
		//}

		private async Task EditCapsulePharmacyAsync()
		{
			if (Pharmacy.IsCapsule)
			{
				IsAddPharmacy = false;
				OpenCapsuleDialog.Raise(false);
			}
			else
			{
				var result = await _navigationService.Navigate<PatientMedicalInfoContactPharmacyDetailViewModel, MedicalHistoryPharmacyNavigationParam>(
				new MedicalHistoryPharmacyNavigationParam() { TupleParam = new Tuple<Pharmacy, bool>(Pharmacy, false), NavigationParam = PatientMedicalInfo });
				if (result == null) return;
			//	Pharmacy = result;
			}
		}
        //  
        private async Task EditSelectivePharmacyCommand()
        {
            if (Pharmacy.IsCapsule)
            {
                IsAddPharmacy = false;
                OpenCapsuleDialog.Raise(false);
            }
			else if (Pharmacy.IsCurative)
            {
                IsAddPharmacy = false;
                OpenCurativeDialog.Raise(false);
            }
            else
            {
                var result = await _navigationService.Navigate<PatientMedicalInfoContactPharmacyDetailViewModel, MedicalHistoryPharmacyNavigationParam>(
                new MedicalHistoryPharmacyNavigationParam() { TupleParam = new Tuple<Pharmacy, bool>(Pharmacy, false), NavigationParam = PatientMedicalInfo });
                if (result == null) return;
               // Pharmacy = result;
            }
        }

        private async Task AddPharmacyAsync()
		{
            var result = await _navigationService.Navigate<PatientMedicalInfoContactPharmacyDetailViewModel, MedicalHistoryPharmacyNavigationParam>(
               new MedicalHistoryPharmacyNavigationParam() { TupleParam = new Tuple<Pharmacy, bool>(Pharmacy, true), NavigationParam = PatientMedicalInfo });
            if (result == null) return;
           // Pharmacy = result;
        }

		private async Task EditPharmacyAsync()
		{
			var result = await _navigationService.Navigate<PatientMedicalInfoContactPharmacyDetailViewModel, MedicalHistoryPharmacyNavigationParam>(
                new MedicalHistoryPharmacyNavigationParam() { TupleParam = new Tuple<Pharmacy, bool>(Pharmacy, false), NavigationParam = PatientMedicalInfo });
			if (result == null) return;
			//Pharmacy = result;
		}
        
        private async Task EditPharmacyForSelectiveAsync()
        {
            var result = await _navigationService.Navigate<PatientMedicalInfoContactPharmacyDetailViewModel, MedicalHistoryPharmacyNavigationParam>(
                new MedicalHistoryPharmacyNavigationParam() { TupleParam = new Tuple<Pharmacy, bool>(Pharmacy, false), NavigationParam = PatientMedicalInfo });
            if (result == null) return;
           // Pharmacy = result;
            IsCapsuleValue = Pharmacy.IsCapsule = false;
           if(Globals.Instance.IsCurative) IsCurativeValue = Pharmacy.IsCurative = false;
        }

        //      private async Task EditPharmacyForCapsuleAsync()
        //{
        //	var result = await _navigationService.Navigate<PatientMedicalInfoContactPharmacyDetailViewModel, MedicalHistoryPharmacyNavigationParam, Pharmacy>(
        //		new MedicalHistoryPharmacyNavigationParam() { TupleParam = new Tuple<Pharmacy, bool>(Pharmacy, false), NavigationParam = PatientMedicalInfo });
        //	if (result == null) return;
        //	Pharmacy = result;
        //	IsCapsuleValue = Pharmacy.IsCapsule = false;
        //}

        private async Task DeletePharmacyAsync()
		{
			var response = await _userDialogs.ConfirmAsync("Are you sure you want to delete?");
			if (!response)
				return;
			Pharmacy = null;
		}
        #endregion

        #region Medical Issues
        private async Task EditIssueAsync(object issueObj)
        {
            var issue = (Tuple<PrimaryIssueType, int>)issueObj;
            MedicalHistoryIssueNavigationParam param = new MedicalHistoryIssueNavigationParam()
            {
                IsEdit = true,
                NavigationParam = PatientMedicalInfo
            };

            if (issue.Item1 == PrimaryIssueType.Allergy)
            {
                param.TupleParam = new Tuple<PrimaryIssue, bool>(Allergies[issue.Item2], false);
                await _navigationService.Navigate<PatientMedicalIssueViewModel, MedicalHistoryIssueNavigationParam>

                (param).ContinueWith(async (x) =>
                {
                    await RaisePropertyChanged(nameof(Medications));
                });
            }
            else if (issue.Item1 == PrimaryIssueType.Medication)
            {
                param.TupleParam = new Tuple<PrimaryIssue, bool>(Medications[issue.Item2], false);
                await _navigationService.Navigate<PatientMedicalIssueViewModel, MedicalHistoryIssueNavigationParam>
                    (param).ContinueWith(async (x) =>
                    {
                        await RaisePropertyChanged(nameof(Medications));
                    });
            }
            else if (issue.Item1 == PrimaryIssueType.Surgery)
            {
                param.TupleParam = new Tuple<PrimaryIssue, bool>(Surgeries[issue.Item2], false);
                await _navigationService.Navigate<PatientMedicalIssueViewModel, MedicalHistoryIssueNavigationParam>
                    (param).ContinueWith(async (x) =>
                    {
                        await RaisePropertyChanged(nameof(Surgeries));
                    });
            }
        }

        private async Task DeleteIssueAsync(object issueObj)
		{
			var issue = (Tuple<PrimaryIssueType, int>)issueObj;
			var response = await _userDialogs.ConfirmAsync("Are you sure you want to delete?");
			if (!response)
				return;
			
			if (issue.Item1 == PrimaryIssueType.Allergy)
			{
				Allergies.RemoveAt(issue.Item2);
				Allergies.ToList().ForEach(a => a.Position = Allergies.IndexOf(a));
			}
			else if (issue.Item1 == PrimaryIssueType.Medication)
			{
				Medications.RemoveAt(issue.Item2);
				Medications.ToList().ForEach(m => m.Position = Medications.IndexOf(m));
			}
			else if (issue.Item1 == PrimaryIssueType.Surgery)
			{
				Surgeries.RemoveAt(issue.Item2);
				Surgeries.ToList().ForEach(s => s.Position = Surgeries.IndexOf(s));
			}
		}

		private async Task AddIssueAsync(object issueType)
		{
			var type = (PrimaryIssueType)issueType;
			var issue = MedicalIssueFactory.GetIssueTitlesOnly(type);
			var resultedIssue = 
				await _navigationService.Navigate<PatientMedicalIssueViewModel, MedicalHistoryIssueNavigationParam>
					(new MedicalHistoryIssueNavigationParam() { TupleParam = new Tuple<PrimaryIssue, bool>(issue, false), NavigationParam = PatientMedicalInfo });
			if (resultedIssue != null)
			{
				//if (type == PrimaryIssueType.Allergy)
				//{
				//	var aIssue = resultedIssue.Clone() as Allergy;
				//	aIssue.EditCommand = EditIssueCommand;
				//	aIssue.DeleteCommand = DeleteIssueCommand;

				//	Allergies = Allergies ?? new MvxObservableCollection<Allergy>();
				//	aIssue.Position = Allergies.Count == 0 ? 0 : Allergies.Max(x => x.Position) + 1;
				//	Allergies.Add(aIssue);
				//	Allergies.ToList().ForEach(a => a.Position = Allergies.IndexOf(a));
				//}
				//else if (type == PrimaryIssueType.Medication)
				//{
				//	var mIssue = resultedIssue.Clone() as Medication;
				//	mIssue.EditCommand = EditIssueCommand;
				//	mIssue.DeleteCommand = DeleteIssueCommand;

				//	Medications = Medications ?? new MvxObservableCollection<Medication>();
				//	mIssue.Position = Medications.Count == 0 ? 0 : Medications.Max(x => x.Position) + 1;
				//	Medications.Add(mIssue);
				//	Medications.ToList().ForEach(a => a.Position = Medications.IndexOf(a));
				//}
				//else if (type == PrimaryIssueType.Surgery)
				//{
				//	var sIssue = resultedIssue.Clone() as Surgery;
				//	sIssue.EditCommand = EditIssueCommand;
				//	sIssue.DeleteCommand = DeleteIssueCommand;

				//	Surgeries = Surgeries ?? new MvxObservableCollection<Surgery>();
				//	sIssue.Position = Surgeries.Count == 0 ? 0 : Surgeries.Max(x => x.Position) + 1;
				//	Surgeries.Add(sIssue);
				//	Surgeries.ToList().ForEach(a => a.Position = Surgeries.IndexOf(a));
				//}
			}
		}
		#endregion

		#region Medical History Get, Update

		private async Task CancelMedicalHistoryAsync()
		{
			await _navigationService.Close(this);
		}

		private async Task GetMedicalHistory()
		{
			var medicalIssues = await DataUtility.GetMedicalIssuesAsync(SettingsValues.ApiURLValue).ConfigureAwait(false);
            List<MedicalIssueEx> medicalIssuesX = new List<MedicalIssueEx>();
			CommonLibraryCoreMaui.Models.MedicalInfo mi = await DataUtility.PatientGetMedicalHistoryAsync(SettingsValues.ApiURLValue, CommonAuthSession.Token, PatientMedicalInfo.PatientId).ConfigureAwait(false);
			Pharmacy = medicalInfo.Pharmacy = mi?.Pharmacy;
			Pcp = medicalInfo.PCP = mi?.PCP;

			medicalIssues.ForEach((issue) => 
			{
				if (mi.MedicalIssues.Any(x => x == issue.ID))
					issue.IsChecked = true;

                MedicalIssueEx mx = new MedicalIssueEx();
                mx.ID = issue.ID;
                mx.Description = issue.Description;
                mx.Value = issue.Value;
                mx.IsChecked = issue.IsChecked;
                medicalIssuesX.Add(mx);
			});

			medicalInfo.Allergies = mi?.Allergies;
			medicalInfo.Medications = mi?.Medications;
			medicalInfo.Surgeries = mi?.Surgeries;

            MedicalIssuesViewModel model = new MedicalIssuesViewModel();
            model.MedicalIssues = new MvxObservableCollection<MedicalIssueEx>(medicalIssuesX);
            model.OtherMedicalHistoryIssue = medicalInfo.OtherMedicalIssue = mi?.OtherMedicalIssue;
            MedicalIssuesViewModel = model;

			Allergies = new MvxObservableCollection<Allergy>(medicalInfo.Allergies);
			Medications = new MvxObservableCollection<Medication>(medicalInfo.Medications);
			Surgeries = new MvxObservableCollection<Surgery>(medicalInfo.Surgeries);

			Allergies.ToList().ForEach(a => { a.EditCommand = EditIssueCommand; a.DeleteCommand = DeleteIssueCommand; a.Position = Allergies.IndexOf(a); });
			Medications.ToList().ForEach(a => { a.EditCommand = EditIssueCommand; a.DeleteCommand = DeleteIssueCommand; a.Position = Medications.IndexOf(a); });
			Surgeries.ToList().ForEach(a => { a.EditCommand = EditIssueCommand; a.DeleteCommand = DeleteIssueCommand; a.Position = Surgeries.IndexOf(a); });
		}

		private async Task SaveMedicalHistoryAsync()
		{
			medicalInfo.PatientID = PatientMedicalInfo.PatientId;
			medicalInfo.PCP = Pcp;
			medicalInfo.Pharmacy = Pharmacy;
			medicalInfo.Allergies = Allergies?.ToList() ?? new List<Allergy>(); 
			medicalInfo.Medications = Medications?.ToList() ?? new List<Medication>();
			medicalInfo.Surgeries = Surgeries?.ToList() ?? new List<Surgery>();
			medicalInfo.MedicalIssues = MedicalIssuesViewModel.MedicalIssues.Where(x => x.IsChecked).Select(x => x.ID).ToList();
            medicalInfo.OtherMedicalIssue = MedicalIssuesViewModel.OtherMedicalHistoryIssue ?? string.Empty;

			if (ValidateMedicalInfo())
			{
				if (PatientMedicalInfo.NavigationType == MedicalInfoNavigationType.Registration)
					await RegistrationToLogin();
				else if (PatientMedicalInfo.NavigationType == MedicalInfoNavigationType.AddMember)
					UpdateMedicalInfoForAddMember();
				else
					await UpdateMedicalInfo();
			}
		}

		private void UpdateMedicalInfoForAddMember()
		{
			PatientMedicalInfo.PatientAdditionalFamilyMember.MedicalHistory = medicalInfo;
			AccountAddFamilyMember.Instance.AddAdditionalFamilyMember(PatientMedicalInfo.PatientAdditionalFamilyMember);
			AccountAddFamilyMember.Instance.UpdateFamilyMembersNameList(PatientMedicalInfo.PatientAdditionalFamilyMember, AccountAddFamilyMember.Instance.IncludedInPlan);
			NavigateToFamilyMembers.Raise();
		}

		private async Task UpdateMedicalInfo()
		{
			IsBusy = true;
			StatusResponse resp = await DataUtility.UpdateMedicalHistoryAsync(SettingsValues.ApiURLValue, medicalInfo, CommonAuthSession.Token).ConfigureAwait(false);
			if (resp != null)
			{
				if (Globals.Instance.IsCurative)
				{
					Globals.Instance.HasSeenCurativeDialog = true;
                    var HomeDialogVisible = medicalInfo.Pharmacy==null ||!medicalInfo.Pharmacy.IsCurative ? true : false;
                    await DataUtility.UpdateCurativeEligibilityForHomeViewDialogAsync(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.PatientID, HomeDialogVisible, CommonAuthSession.Token).ConfigureAwait(false);
				}


				if (resp.StatusCode == StatusCode.Error)
				{
					IsBusy = false;
					await _userDialogs.AlertAsync("There is an error occurred!!");
				}
				else
				{
					IsBusy = false;
					await _navigationService.Close(this);
				}
			}
			IsBusy = false;
		}

		private async Task RegistrationToLogin()
		{
			StatusResponse resp = await DataUtility.RegistrationStep4Async(SettingsValues.ApiURLValue, medicalInfo).ConfigureAwait(false);
			if (resp != null)
			{
				switch (resp.StatusCode)
				{
					case StatusCode.Success:
					case StatusCode.Saved:
						bool ret = await LoginNewUser(Registration.Instance.Email, Registration.Instance.Password).ConfigureAwait(false);
						if (ret)
						{
							if (Globals.Instance.UserInfo.ProviderID is null || Globals.Instance.UserInfo.ProviderID == 0)
							{
								UserInfo userInfo = await DataUtility.GetUserInfo(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.UserID, true, CommonAuthSession.Token).ConfigureAwait(false);
								//var brandname = await DataUtility.GetSiteSettingsAsync(SettingsValues.ApiURLValue).ConfigureAwait(false);
								Globals.Instance.UserInfo = userInfo;
								//Globals.Instance.AppBrandName = brandname;
							}
							var container = new SimpleInjector.Container();
							container.Register<IiOSLogout, Services.PatientLogoutService>();
							container.Verify();
							DependencyResolver.SetupContainer(container);
							CommonAuthSession.IsAutheticated = true;
							await _navigationService.Navigate<HomeViewModel>();
						}
						break;
					default:
						await _userDialogs.AlertAsync("There was an error please try again.");
						break;
				}
			}
		}

		private bool ValidateMedicalInfo()
		{
            IsValidationHidden = true;
            if (medicalInfo != null)
			{
                if (medicalInfo.PCP is null || 
					(medicalInfo.MedicalIssues.Count == 0 && string.IsNullOrEmpty(medicalInfo.OtherMedicalIssue))) //medicalInfo.Pharmacy is null ||
                {
                    IsValidationHidden = false;
                }
				else
				{
					return true;
				}

			}
			return false;
		}

		private async Task<bool> LoginNewUser(string username, string pwd)
		{
			TokenResponse resp = await DataUtility.GetTokenResponseAsync(SettingsValues.ApiURLValue, username, pwd, "Device.GetDeviceId(this)").ConfigureAwait(false);

			if (resp != null)
			{
				if (!string.IsNullOrEmpty(resp.access_token) && resp.expires_in != null)
				{
					CommonAuthSession.Token = resp.access_token;
					CommonAuthSession.SetTokenExpirationDate(DateTime.Now.AddSeconds(Convert.ToInt32(resp.expires_in)));
					UserInfo userInfo = await DataUtility.GetUserInfo(SettingsValues.ApiURLValue, resp.userid, true, CommonAuthSession.Token).ConfigureAwait(false);
					Globals.Instance.UserInfo = userInfo;

					return true;
				}
			}

			return false;
		}

		#endregion
	}

    public class PCPMessage : MvxMessage
    {
        public PCPMessage(object sender, PCP pcp) : base(sender)
        {
            Pcp = pcp;
        }

        public PCP Pcp
        {
            get;
            private set;
        }
    }
	public class PharmacyMessage : MvxMessage
	{
		public PharmacyMessage(object sender, Pharmacy pharmacy) : base(sender)
		{
			Pharmacy = pharmacy;
		}

		public Pharmacy Pharmacy
		{
			get;
			private set;
		}
	}
	public class MedicalIssuesViewModel : MvxViewModel
    {
        private MvxObservableCollection<MedicalIssueEx> _medicalIssues;

        public MvxObservableCollection<MedicalIssueEx> MedicalIssues
        {
            get { return _medicalIssues; }
            set { _medicalIssues = value; RaisePropertyChanged(() => MedicalIssues); }
        }

        private string _otherMedicalHistoryIssue;
        public string OtherMedicalHistoryIssue
        {
            get { return _otherMedicalHistoryIssue; }
            set { _otherMedicalHistoryIssue = value; RaisePropertyChanged(() => OtherMedicalHistoryIssue); }
        }
    }

    public class MedicalIssueEx : MvxViewModel
    {
        public int ID { get; set; }
        public string Value { get; set; }
        private bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked; }
            set { _isChecked = value; RaisePropertyChanged(() => IsChecked); }
        }
        public string Description { get; set; }
    }
}