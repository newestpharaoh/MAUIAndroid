﻿using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.ViewModels;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
	public class PatientRegistrationPaymentInformationViewModel : BaseNavigationViewModel<string>
	{
		private PatientSubscriptions _patientSubscriptions;
		public PatientSubscriptions PatientSubscriptionsResponse
		{
			get { return _patientSubscriptions; }
			set { SetProperty(ref _patientSubscriptions, value); }
		}

		private Subscription _selectedSubscriptionPlan;
		public Subscription SelectedSubscriptionPlan
		{
			get { return _selectedSubscriptionPlan; }
			set
			{
				SetProperty(ref _selectedSubscriptionPlan, value);
				IsShowAdditionalFamilyMemberOption = _selectedSubscriptionPlan.Name.Contains("Family");
				TotalPlanCostWithAdditionalMember = SelectedSubscriptionPlan.Cost;
			}
		}

		private bool _isShowAdditionalFamilyMemberOption;
		public bool IsShowAdditionalFamilyMemberOption
		{
			get { return _isShowAdditionalFamilyMemberOption; }
			set { SetProperty(ref _isShowAdditionalFamilyMemberOption, value); }
		}

		private string _selectedNumberOfMember;
		public string SelectedNumberOfMember
		{
			get { return _selectedNumberOfMember; }
			set
			{
				SetProperty(ref _selectedNumberOfMember, value);

				((FamilySubscription)SelectedSubscriptionPlan).AddOn.AdditionalFamilyMembers = Convert.ToInt16(SelectedNumberOfMember);
				TotalPlanCostWithAdditionalMember = $"{((FamilySubscription)SelectedSubscriptionPlan).GetTotalPrice().ToString("$0.00")}";
			}
		}

		private string _totalPlanCostWithAdditionalMember = "$0.00";
		public string TotalPlanCostWithAdditionalMember
		{
			get { return _totalPlanCostWithAdditionalMember; }
			set { SetProperty(ref _totalPlanCostWithAdditionalMember, value); }
		}

		public IMvxCommand ContinueCommand => new MvxAsyncCommand(Continue);

        public async override Task Initialize()
        {
			await GetPatientSubscription();
			await base.Initialize();
        }

		public override void Prepare(string parameter)
		{
			if (!string.IsNullOrEmpty(parameter))
				Registration.Instance.ResignupCode = parameter;
			base.Prepare();
		}

		private async Task GetPatientSubscription()
		{
			PatientSubscriptionsResponse = await DataUtility.GetPatientSubscriptionsAsync(SettingsValues.ApiURLValue).ConfigureAwait(false);
		}

		private async Task Continue()
        {
			if (SelectedSubscriptionPlan != null)
			{
				Registration.Instance.Subscription = SelectedSubscriptionPlan;
				await _navigationService.Navigate<PatientSettingsManageCardInfoViewModel, bool>(true);
			}
        }
    }
}