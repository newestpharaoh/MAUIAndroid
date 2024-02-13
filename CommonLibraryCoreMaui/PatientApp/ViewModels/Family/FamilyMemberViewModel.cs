using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.Models.NavigationParameters;
using CommonLibraryCoreMaui.ViewModels;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class FamilyViewModel : BaseViewModel 
    {
		private List<BasicFamilyMemberInfo> _familyMemberList;
		public List<BasicFamilyMemberInfo> FamilyMemberList
		{
			get { return _familyMemberList; }
			set { SetProperty(ref _familyMemberList, value); }
		}

		private BasicFamilyMemberInfo _selectedFamilyMember;
		public BasicFamilyMemberInfo SelectedFamilyMember
		{
			get { return _selectedFamilyMember; }
			set { SetProperty(ref _selectedFamilyMember, value); }
		}

		public bool _isFamilyListEmpty;
		public bool IsFamilyListEmpty
		{
			get { return _isFamilyListEmpty; }
			set { SetProperty(ref _isFamilyListEmpty, value); }
		}

		public string _familyListEmptyText;
		public string FamilyListEmptyText
		{
			get { return _familyListEmptyText; }
			set { SetProperty(ref _familyListEmptyText, value); }
		}

        public bool __canAddFamilyMembers;
        public bool CanAddFamilyMembers
        {
            get { return __canAddFamilyMembers; }
            set { SetProperty(ref __canAddFamilyMembers, value); }
        }

        public IMvxCommand SelectedFamilyMemberCommand => new MvxAsyncCommand<BasicFamilyMemberInfo>(SelectedFamilyMemberAsync);
		public IMvxCommand AddFamilyMemberCommand => new MvxAsyncCommand(AddFamilyMember);

		public override void ViewAppearing()
		{
			base.ViewAppearing();
			MvxNotifyTask.Create(GetFamilyMembersList);
		}

		private async Task GetFamilyMembersList()
		{
			IsBusy = true;
			IsFamilyListEmpty = false;
			FamilyListEmptyText = string.Empty;
            CanAddFamilyMembers = false;

            try
			{
				var results1 = await DataUtility.PatientGetSubscriptionInfoAsync(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.PatientID, CommonAuthSession.Token).ConfigureAwait(false);

				if (results1 != null)
				{
					CanAddFamilyMembers = results1.CanAddFamilyMembers;
				}

                var results = await DataUtility.PatientGetFamilyMemberListAsync(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.PatientID, CommonAuthSession.Token).ConfigureAwait(false);

                if (results == null || (results.Count == 1 && results[0].IsPrimary == true))
                {
                    IsFamilyListEmpty = true;
                    FamilyListEmptyText = "No Added Family Members";
                }

                if (results != null)
                {
                    FamilyMemberList = results.Where(x => x.IsPrimary == false).ToList();
                }
            }
			catch (Exception ex)
			{
				ReportCrash(ex, Title);
			}

			IsBusy = false;
		}

		private async Task SelectedFamilyMemberAsync(BasicFamilyMemberInfo fmember)
		{
			await _navigationService.Navigate<FamilyMemberViewModel, BasicFamilyMemberInfo>(fmember);
		}

		private async Task AddFamilyMember()
		{
			await _navigationService.Navigate<PatientSettingsManageSubscriptionMembersViewModel, bool>(true);
		}
	}

    public class FamilyMemberViewModel : BaseNavigationViewModel<BasicFamilyMemberInfo>
	{
		private BasicFamilyMemberInfo _familyMemberInfo;
		public BasicFamilyMemberInfo FamilyMemberInfo
		{
			get { return _familyMemberInfo; }
			set { SetProperty(ref _familyMemberInfo, value); }
		}

		public IMvxCommand GoProfileCommand => new MvxAsyncCommand<object>(GoToProfile);
		public IMvxCommand GoManageMedicalHistoryCommand => new MvxAsyncCommand(GoToManageMedicalHistory);

		public FamilyMemberViewModel(IMvxNavigationService mvxNavigationService)
		{
			_navigationService = mvxNavigationService;
		}

		public override Task Initialize()
        {
            return base.Initialize();
        }

		public override void Prepare(BasicFamilyMemberInfo parameter)
		{
			FamilyMemberInfo = parameter;
			base.Prepare();
		}

		private async Task GoToProfile(object arg)
		{
			await _navigationService.Navigate<PatientProfileViewModel, ProfileNavigationParam>(new ProfileNavigationParam()
			{
				IsProfile = true, 
				PatientId = Convert.ToInt32(FamilyMemberInfo.PatientID), 
				IsEmailEnabled = false 
			});
		}

		private async Task GoToManageMedicalHistory()
		{
			await _navigationService.Navigate<PatientMedicalInfoMedicalHistoryDetailViewModel, MedicalHistoryNavigationParam>(
			new MedicalHistoryNavigationParam()
			{
				PatientId = Convert.ToInt32(FamilyMemberInfo.PatientID),
				Name = FamilyMemberInfo.DisplayName
			});
		}
	}
}