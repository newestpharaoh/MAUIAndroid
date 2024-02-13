using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.Models.NavigationParameters;
using CommonLibraryCoreMaui.Services;
using CommonLibraryCoreMaui.ViewModels;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
	public class PatientSettingsManageSubscriptionMembersViewModel : BaseNavigationViewModel<bool>
	{
		IPatientService _patientService;
		private AccountSubscriptionInfo _accountMemberSubscriptionInfo;
		public AccountSubscriptionInfo AccountMemberSubscriptionInfo
		{
			get { return _accountMemberSubscriptionInfo; }
			set { SetProperty(ref _accountMemberSubscriptionInfo, value); 
				 RaisePropertyChanged(nameof(AccountMemberSubscriptionInfo)); }
		}

		private bool _isShowPlanCancelWarning = false;
		public bool IsShowPlanCancelWarning
		{
			get { return _isShowPlanCancelWarning; }
			set { SetProperty(ref _isShowPlanCancelWarning, value); RaisePropertyChanged(nameof(IsShowPlanCancelWarning)); }
		}

		public IMvxCommand GoToUpdateCreditCardCommand => new MvxAsyncCommand(GoToUpdateCreditCard);
		public IMvxCommand GoToChangePlanCommand => new MvxAsyncCommand(GoToChangePlan);


		public PatientSettingsManageSubscriptionMembersViewModel(IMvxNavigationService mvxNavigationService, IPatientService patientService, IUserDialogs userDialogs)
		{
			_navigationService = mvxNavigationService;
			_patientService = patientService;
			_userDialogs = userDialogs;
		}

		public async override Task Initialize()
		{
			await RefreshAccountMembersList();
			await base.Initialize();
		}

		public async Task RefreshAccountMembersList()
		{
			IsBusy = true;

			try
			{

				var results = await _patientService.GetPatientSubscriptionInfoAsync();
				if (results == null)
				{
					IsBusy = false;
					return;
				}
				//var newplan = await _patientService.PatientGetChangeSubscriptionInfoAsync(;
				Globals.Instance.UserInfo.NewSubscriptionPlan = results.NewSubscriptionPlan;
				if (!string.IsNullOrEmpty(results.CurrentSubscriptionPlan) && !string.IsNullOrEmpty(results.NewSubscriptionPlan))
					//Globals.Instance.UserInfo.NewSubscriptionCost = results.NewSubscriptionPlanCost;
                Globals.Instance.UserInfo.NewSubscriptionCost =
                    SubscriptionsFactory.IsFamilyToIndividualPlan(results.CurrentSubscriptionPlan, results.NewSubscriptionPlan)||
					SubscriptionsFactory.IsFamilyToIndividual365Plan(results.CurrentSubscriptionPlan, results.NewSubscriptionPlan)
									? results.NewSubscriptionPlanCost
                                    : results.CurrentSubscriptionPlanCost;
                else
                    Globals.Instance.UserInfo.NewSubscriptionCost = results.CurrentSubscriptionPlanCost;

				Globals.Instance.UserInfo.CurrentSubscriptionEndDate = results.CurrentSubscriptionEndDate;
				var ssci = Globals.Instance.UserInfo.ShowSubscriptionChangeInfo();
				var sscai = Globals.Instance.UserInfo.ShowSubscriptionCanceledInfo();
				var showSubscriptionChangeBanner = Globals.Instance.UserInfo.ShowSubscriptionChangeBanner
					||( Globals.Instance.UserInfo.CurrentSubscriptionPlan == null && string.IsNullOrEmpty(Globals.Instance.UserInfo.CurrentSubscriptionEndDate) && Globals.Instance.UserInfo.Domain == null);

				if (Globals.Instance.UserInfo.ShowSubscriptionChangeInfo() || 
					Globals.Instance.UserInfo.ShowSubscriptionChangeBanner || 
					Globals.Instance.UserInfo.ShowSubscriptionCanceledInfo() || 
					Globals.Instance.UserInfo.CanceledForPaymentIssues ||
					Globals.Instance.UserInfo.WasPrepay || 
					Globals.Instance.UserInfo.RegistrationFailed|| 
					showSubscriptionChangeBanner)
				{
					IsShowPlanCancelWarning = true;
					results.CurrentSubscriptionPlan = Globals.Instance.UserInfo.ShowSubscriptionCanceledInfo() ? "N/A" : results.CurrentSubscriptionPlan;
				}
				else
				{
					
					IsShowPlanCancelWarning = false;
				}
				results.AccountMembers.Where(x => x.IsActive == false).ToList().ForEach(x => x.PaymentPlan = "Deactivated");
				AccountMemberSubscriptionInfo = results;
				
			}
			catch (Exception ex)
			{
				ReportCrash(ex, Title);
			}

			IsBusy = false;
		}

		private async Task GoToUpdateCreditCard()
		{
			await _navigationService.Navigate<PatientSettingsManageCardInfoViewModel>();
		}

		private async Task GoToChangePlan()
		{
			await _navigationService.Navigate<PatientManageSubscriptionMembersViewModel, AccountSubscriptionInfo>(AccountMemberSubscriptionInfo);
		}
	}

	public class FamilMemberGroup : BaseViewModel
	{
		public AccountMember Member { get; set; }
		public IMvxCommand SelectedFamilyMemberCommand => new MvxAsyncCommand<Tuple<string, AccountMember>>(SelectedFamilyMemberAsync);
		Func<Task> RefreshMemberList { set; get; }

		public FamilMemberGroup(Func<Task> refreshList)
		{
			RefreshMemberList = refreshList;
		}

		private async Task SelectedFamilyMemberAsync(Tuple<string, AccountMember> memberInfo)
		{
			switch (memberInfo.Item1)
			{
				case SettingsValues.UpdateDemographic:
					if (memberInfo.Item2.IsPrivate)
					{
						await _navigationService.Navigate<PatientPrivateAccountProfileViewModel, Tuple<string, string>>
							(new Tuple<string, string> (memberInfo.Item2.DisplayName, "demographics"));
					}
					else
					{
						var patientId = Globals.Instance.UserInfo.PatientID == memberInfo.Item2.PatientID ?
							0 : memberInfo.Item2.PatientID;

						var profileResult = await _navigationService.Navigate<PatientProfileViewModel, ProfileNavigationParam>(new ProfileNavigationParam()
						{
							IsProfile = true,
							PatientId = patientId,
							IsEmailEnabled = ((patientId == 0) || memberInfo.Item2.IsPrivate)
						});
						if (profileResult)
						{
							await RefreshMemberList.Invoke();
						}
					}
					break;
				case SettingsValues.UpdateMedicalInfo:
					if (memberInfo.Item2.IsPrivate)
					{
						await _navigationService.Navigate<PatientPrivateAccountProfileViewModel, Tuple<string, string>>
							(new Tuple<string, string>(memberInfo.Item2.DisplayName, "medical information"));
					}
					else
					{
						await _navigationService.Navigate<PatientMedicalInfoMedicalHistoryDetailViewModel, MedicalHistoryNavigationParam>(
						new MedicalHistoryNavigationParam()
						{
							PatientId = memberInfo.Item2.PatientID,
							Name = memberInfo.Item2.DisplayName
						});
					}
					break;
				case SettingsValues.UpdateAccountAccess:
					var result = await _navigationService.Navigate<PatientSettingsManageSubscriptionMemberDetailViewModel, AccountMember>(memberInfo.Item2);
					if (result)
					{
						await RefreshMemberList.Invoke();
					}
					break;
			}
		}
	}

	public class PatientPrivateAccountProfileViewModel : BaseNavigationViewModel<Tuple<string, string>>
	{
		public string MemberName { get; set; }
		public string OperationType { get; set; }

		public override void Prepare(Tuple<string, string> parameter)
		{
			MemberName = parameter.Item1;
			OperationType = parameter.Item2;
			base.Prepare();
		}
	}
}
