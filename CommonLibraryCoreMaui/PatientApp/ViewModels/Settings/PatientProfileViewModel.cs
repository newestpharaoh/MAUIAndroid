using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Acr.UserDialogs;
using CommonLibraryCoreMaui.Exceptions;
using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.Models.NavigationParameters;
using CommonLibraryCoreMaui.PatientApp.ViewModels;
using CommonLibraryCoreMaui.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Linq;

namespace CommonLibraryCoreMaui.ViewModels
{
	public class PatientProfileViewModel : BaseNavigationViewModel<ProfileNavigationParam, bool>
	{
		public IPatientService _patientService;
		public int _patientId = 0;
		public int PatientId
		{
			get { return _patientId; }
			set { SetProperty(ref _patientId, value); }
		}

		private PatientProfile _userProfile;
		public PatientProfile UserProfile
		{
			get { return _userProfile; }
			set { SetProperty(ref _userProfile, value); }
		} 

		private NotificationPreferencesViewModel _notificationPreferences;
		public NotificationPreferencesViewModel NotificationPreferences
		{
			get { return _notificationPreferences; }
			set { SetProperty(ref _notificationPreferences, value); }
		}

		private bool _isProfile;
		public bool IsProfile
		{
			get { return _isProfile; }
			set { SetProperty(ref _isProfile, value); }
		}

		public ObservableCollection<GenericRecord> _relationshipsCollection;
		public virtual ObservableCollection<GenericRecord> RelationshipsCollection
		{
			get { return _relationshipsCollection; }
			set { SetProperty(ref _relationshipsCollection, value); }
		}

		private GenericRecord _selectedRelationship;
		public GenericRecord SelectedRelationship
		{
			get { return _selectedRelationship; }
			set
			{
				SetProperty(ref _selectedRelationship, value);
				UserProfile.Relationship = _selectedRelationship.Value;
				if(_selectedRelationship.ID !=null)
				{
					OtherRelationship = string.Empty;
				}
			}
		}

		private string _otherRelationship;
		public string OtherRelationship
		{
			get { return _otherRelationship; }
			set
			{
				SetProperty(ref _otherRelationship, value);
				UserProfile.OtherRelationship = value;
			}
        }
		//

		private bool _hadPeviousVisit;
        public bool HadPeviousVisit
        {
            get { return _hadPeviousVisit; }
            set
            {			
                SetProperty(ref _hadPeviousVisit, value);
              
            }
        }
        private string _title;
		public string UserTitle
		{
			get { return _title; }
			set
			{
				SetProperty(ref _title, value);
				UserProfile.Title = value;
			}
		}
		PatientProfile CurrentUserProfile;

		bool ProfileHasChanges => !UserProfile.Equals(CurrentUserProfile);

		public IMvxCommand SaveCommand => new MvxAsyncCommand(UpdateProfileInformation);
		public IMvxCommand ForgetCommand => new MvxAsyncCommand(GoToForgetPassword);
		public IMvxCommand CancelCommand => new MvxAsyncCommand(GoToPreviousPage);
		public IMvxCommand ContinueCommand => new MvxAsyncCommand(ContinueAddMember);
		public IMvxCommand ImageUploadCommand => new MvxAsyncCommand<UpdateProfileImageParam>(UpdateProfileImage);

		public PatientProfileViewModel(IMvxNavigationService mvxNavigationService, IUserDialogs userDialogs, IPatientService patientService)
		{
            _navigationService = mvxNavigationService;
            _userDialogs = userDialogs;
            _patientService = patientService;
        }

		public async override Task Initialize()
		{
            IsBusy = true;
            try
            {
                Title = IsProfile ? "Profile" : "Add Family Member";
			    await GetProfile();
            }
            catch { }
			IsBusy = false;
            await base.Initialize();
		}

		public override void Prepare(ProfileNavigationParam parameter)
		{
			IsProfile = parameter.IsProfile;
			PatientId = parameter.PatientId;
			base.Prepare();
		}

		private async Task GoToForgetPassword()
		{
			await _navigationService.Navigate<PatientProfileForgotPasswordViewModel>();
		}

		private async Task GoToPreviousPage()
		{
			await _navigationService.Close(this);
		}

		private async Task ContinueAddMember()
		{
			if (string.IsNullOrEmpty(UserProfile.FirstName) ||
			   string.IsNullOrEmpty(UserProfile.LastName) ||
			   string.IsNullOrEmpty(UserProfile.DOB) ||
			   UserProfile.Gender.Equals("Select") || UserProfile.Gender.Equals("S")||
			   string.IsNullOrEmpty(UserProfile.Relationship) || UserProfile.Relationship.Equals("Select") ||
			   string.IsNullOrEmpty(UserProfile.PrimaryPhone) )
			{
				await _userDialogs.AlertAsync("Please fill all the required fields!");
				return;
			}

			if (SelectedRelationship.ID == null && string.IsNullOrEmpty(OtherRelationship))
			{
				await _userDialogs.AlertAsync("Please fill Other relationship!");
				return;
			}

			// Get all family member
			var members = await _patientService.PatientGetFamilyMemberListAsync();
			if (members != null)
			{
				// See if new member is a duplicate and if so show alert
				foreach (var member in members)
				{
					if (member.FirstName.Trim().ToLower() == UserProfile.FirstName.Trim().ToLower() &&
						member.LastName.Trim().ToLower() == UserProfile.LastName.Trim().ToLower())
					{
						if (UserProfile.DOB != null)
						{
							var dob = Convert.ToDateTime(UserProfile.DOB).ToShortDateString();

							if (member.DOB == dob)
							{
								await _userDialogs.AlertAsync("Family member already exist!");
								return;
							}
						}
					}
				}
			}



			if (UserTitle == "Select")
			{
				UserTitle = string.Empty;
			}

			var additionalFamilyMember = new AdditionalFamilyMember();
			additionalFamilyMember.FamilyMemberInformation = UserProfile;
			await _navigationService.Navigate<PatientMedicalnfoViewModel, MedicalHistoryNavigationParam>(
					new MedicalHistoryNavigationParam() { PatientId = UserProfile.PatientID, PatientAdditionalFamilyMember=additionalFamilyMember, NavigationType = MedicalInfoNavigationType.AddMember });
		}

		private async Task UpdateProfileInformation()
		{
            UserProfile.NotificationPreference = NotificationPreferences.NotificationPreference;
            if (string.IsNullOrEmpty(UserProfile.FirstName) ||
                string.IsNullOrEmpty(UserProfile.LastName) ||
				string.IsNullOrEmpty(UserProfile.DOB) ||
				UserProfile.Gender.Equals("Select") || UserProfile.Gender.Equals("S")) 
			{
				await _userDialogs.AlertAsync("Please fill all the required fields!");
				return;
			}
			if (PatientId == 0)
			{
				if (!System.Text.RegularExpressions.Regex.Match(UserProfile.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success ||
				string.IsNullOrEmpty(UserProfile.Email) ||
				string.IsNullOrEmpty(UserProfile.Address1) ||
				string.IsNullOrEmpty(UserProfile.City) ||
				string.IsNullOrEmpty(UserProfile.State) ||
				string.IsNullOrEmpty(UserProfile.Zip))
				{
					await _userDialogs.AlertAsync("Please fill all the required fields!");
					return;
				}
			}
            
			if (PatientId != 0 || !IsProfile)
			{
				if (SelectedRelationship.ID == null && string.IsNullOrEmpty(OtherRelationship))
				{
					await _userDialogs.AlertAsync("Please fill Other relationship!");
					return;
				}
				if (string.IsNullOrEmpty(UserProfile.Relationship) || UserProfile.Relationship == "Select")
				{
					await _userDialogs.AlertAsync("Please select relationship!");
					return;
				}
			}

			if (!ProfileHasChanges)
                return;

			if (UserTitle == "Select")
			{
				UserTitle = string.Empty;
			}

			IsBusy = true;
            try
            {
				CheckNullValues();

                var response = await _patientService.UpdatePatientProfileAsync(UserProfile);
                CurrentUserProfile = UserProfile.ShallowCopy();
				var userInfo = await DataUtility.GetUserInfo(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.UserID, true, CommonAuthSession.Token)
						.ConfigureAwait(false);
				Globals.Instance.UserInfo = userInfo;
				IsBusy = false;
                await _userDialogs.AlertAsync("Profile updated").ContinueWith(async(t)=> 
				{
					await _navigationService.Close(this);
				});
            }
            catch (ProviderException ex)
            {
                ReportCrash(ex, Title);
            }
            catch (Exception ex)
            {
                ReportCrash(ex, Title);
            }

            IsBusy = false;
        }

		private void CheckNullValues()
		{
			if (UserProfile.PreferredName == null)
				UserProfile.PreferredName = "no-update";
			if (UserProfile.AlternatePhone == null)
				UserProfile.AlternatePhone = "no-update";
			if (UserProfile.Address1 == null)
				UserProfile.Address1 = "no-update";
			if (UserProfile.Address2 == null)
				UserProfile.Address2 = "no-update";
			if (UserProfile.City == null)
				UserProfile.City = "no-update";
			if (UserProfile.State == null)
				UserProfile.State = "no-update";
			if (UserProfile.Zip == null)
				UserProfile.Zip = "no-update";
			if (UserProfile.Relationship == null)
				UserProfile.Relationship = "no-update";
			if (UserProfile.OtherRelationship == null)
				UserProfile.OtherRelationship = "no-update";
			if (UserProfile.NotificationPreference == null)
				UserProfile.NotificationPreference = "no-update";
		}

		private async Task GetProfile()
		{
			RelationshipsCollection = new ObservableCollection<GenericRecord>(Theme.Values.Relationships);
			if (!IsProfile)
			{
				HadPeviousVisit = false;
                UserProfile = new PatientProfile()
				{                    
					Title = Theme.Values.UserTitles[0],
					Gender = Theme.Values.GenderOptions[0],
					Language = Theme.Values.Languages[0],
					State = "TX"
				};
				SelectedRelationship = Theme.Values.Relationships[0];
				UserTitle = Theme.Values.UserTitles[0];
			}
			else
			{
                HadPeviousVisit = !Globals.Instance.HadPeviousVisit;
                CurrentUserProfile = await _patientService.GetPatientProfile(PatientId);
                
                if (CurrentUserProfile != null)
				{
				
                    UserProfile = CurrentUserProfile.ShallowCopy();
					UserProfile.Photo = CurrentUserProfile.Photo;
					if (string.IsNullOrEmpty(UserProfile.Title)) 
					{ UserTitle = Theme.Values.UserTitles[0]; }
					else
					{ UserTitle = UserProfile.Title; }
					NotificationPreferences = new NotificationPreferencesViewModel()
					{
						Email = MaskHelper.MaskEmail(UserProfile.Email),
						Phone = MaskHelper.MaskPhoneNumber(UserProfile.PrimaryPhone),
						NotificationPreference = UserProfile.NotificationPreference
					};
					SelectedRelationship = RelationshipsCollection.Where(x => x.Value == UserProfile.Relationship).Select(x => x).FirstOrDefault() ?? Theme.Values.Relationships[0];
					if (!string.IsNullOrEmpty(UserProfile.OtherRelationship))
					{
						OtherRelationship = UserProfile.OtherRelationship;
					}
				}
			}
        }

		public async Task<bool> UpdateProfileImage(UpdateProfileImageParam param)
		{
			IsBusy = true;
			var success = false;
            try
            {
                var response = await _patientService.UploadPatientImageAsync(param);
                IsBusy = false;
                if (string.IsNullOrEmpty(response.ErrorMessage))
                {
                    success = true;
                    _userDialogs.Toast("Patient Profile Photo Updated Successfully.");
                }
                else
                {
                    await _userDialogs.AlertAsync(response.ErrorMessage);
                }
            }
            catch (ProviderException ex)
            {
                IsBusy = false;
                ReportCrash(ex, Title);
                await _userDialogs.AlertAsync(ex.Message);
            }
            catch (Exception ex)
            {
                IsBusy = false;
                ReportCrash(ex, Title);
                await _userDialogs.AlertAsync(ex.Message);
            }

            return success;
		}
	}

    public class UpdateProfileImageParam
    {
        public int ProfileId { get; set; }
        public byte[] ProfileImage { get; set; }
    }
}