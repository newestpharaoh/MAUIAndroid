using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using CommonLibraryCoreMaui.Factory;
using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.Models.NavigationParameters;
using CommonLibraryCoreMaui.Services;
using CommonLibraryCoreMaui.ViewModels;
using Microsoft.Maui.Storage;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
	public class PatientSettingsManageSubscriptionChangePlanViewModel : BaseNavigationViewModel<PlanChangeNavigationParam, bool>
	{
		Subscription subscription;
		SubscriptionChangeInfo subscriptionInfo;
		IPatientService _patientService;
		public string Locale = Preferences.Get("Locale", string.Empty);
		PlanType _pType;
		public PlanType PType
		{
			get { return _pType; }
			set { SetProperty(ref _pType, value); }
		}

		private SubscriptionPlan _selectedSubscriptionPlan;
		public SubscriptionPlan SelectedSubscriptionPlan
		{
			get { return _selectedSubscriptionPlan; }
			set { SetProperty(ref _selectedSubscriptionPlan, value); }
		}

		private FamilyMembersinPlanViewModel _familyMembersinPlanVM;
		public FamilyMembersinPlanViewModel FamilyMembersinPlanVM
		{
			get { return _familyMembersinPlanVM; }
			set { SetProperty(ref _familyMembersinPlanVM, value); }
		}

		//private string _effectiveDateText;
		//public string EffectiveDateText
		//{
		//	get { return _effectiveDateText; }
		//	set { SetProperty(ref _effectiveDateText, value); }
		//}

		private string _nextBillingDate;
		public string NextBillingDate
		{
			get { return _nextBillingDate; }
			set { SetProperty(ref _nextBillingDate, value); }
		}
		private string _newPlanCost;
		public string NewPlanCost
		{
			get { return _newPlanCost; }
			set { SetProperty(ref _newPlanCost, value); }
		}
		private string _recurring;
		public string Recurring
		{
			get { return _recurring; }
			set { SetProperty(ref _recurring, value); }
		}
		private string _costDifference;
		public string CostDifference
		{
			get { return _costDifference; }
			set { SetProperty(ref _costDifference, value); }
		}

		private string _grantedPromotionCode;
		public string GrantedPromotionCode
		{
			get { return _grantedPromotionCode; }
			set { SetProperty(ref _grantedPromotionCode, value); }
		}

		private string _grantedPromotionCodeText;
		public string GrantedPromotionCodeText
		{
			get { return _grantedPromotionCodeText; }
			set { SetProperty(ref _grantedPromotionCodeText, value); }
		}

		private string _discountNominalDescrip;
		public string DiscountNominalDescrip
		{
			get { return _discountNominalDescrip; }
			set { SetProperty(ref _discountNominalDescrip, value); }
		}

		private string _totalDue;
		public string TotalDue
		{
			get { return _totalDue; }
			set { SetProperty(ref _totalDue, value); }
		}


		private string _planCost;
		public string PlanCost
		{
			get { return _planCost; }
			set { SetProperty(ref _planCost, value); }
		}

		private string _ProratedCostAmt;
		public string ProratedCostAmt
		{
			get { return _ProratedCostAmt; }
			set { SetProperty(ref _ProratedCostAmt, value); }
		}
		//DiscountApplied
		private string _discountApplied;
		public string DiscountApplied
		{
			get { return _discountApplied; }
			set { SetProperty(ref _discountApplied, value); }
		}
		public IMvxCommand PreviousCommand => new MvxAsyncCommand(GoToPreviousPage);
		public IMvxCommand ContinueCommand => new MvxAsyncCommand(ContinueToSavePlan);

		public bool IsAvailable { get; set; }

		public PatientSettingsManageSubscriptionChangePlanViewModel(IMvxNavigationService mvxNavigationService, IPatientService patientService)
		{
			_navigationService = mvxNavigationService;
			_patientService = patientService;
		}

		public override void Prepare(PlanChangeNavigationParam parameter)
		{
			_pType = parameter.Type;
			subscription = parameter.Subscription;
			
			base.Prepare();
		}
		public async Task<List<CommonLibraryCoreMaui.Models.UIText>> GetUITopic(string strTopicName)
		{
			var pageText = await DataUtility.GetUITopicListAsync(SettingsValues.ApiURLValue, strTopicName, Locale).ConfigureAwait(false);
			return pageText.UITextList;
		}
		private List<UIText> GetPageTextList()
		{
			Task<List<UIText>> task = Task.Run<List<UIText>>(async () => await GetUITopic("ManageSubscriptions"));
			return task.Result;
		}
		public async override Task Initialize()
		{
			await base.Initialize();

			IsBusy = true;
			IsAvailable = false;
			
			GrantedPromotionCode = subscription != null? subscription.GrantedPromotionCode:string.Empty;
			subscriptionInfo = await _patientService.PatientGetChangeSubscriptionInfoAsync(subscription.OptionID,subscription.GrantedPromotionCode);
		
			var listText = Task.Run<List<UIText>>(async () => await GetUITopic("UpdManageSubscriptions")).Result;
			var listTextP = Task.Run<List<UIText>>(async () => await GetUITopic("EnterPaymentInformation")).Result;

			if (subscriptionInfo != null)
			{
				var currentPlan = subscriptionInfo.CurrentSubscriptionPlanName == null? "InActive": subscriptionInfo.CurrentSubscriptionPlanName;
				var subPlaNname = subscriptionInfo.SubscriptionPlanName;
				var subscriptionPlan = SubscriptionChangePlanFactory.Get(_pType, listText, subscription.Name, currentPlan, subPlaNname, subscriptionInfo.NextBillingDate, subscriptionInfo.SubscriptionCost, subscriptionInfo.Last4ofCC != null, listTextP);
				SelectedSubscriptionPlan = subscriptionPlan;
				SelectedSubscriptionPlan.Cost = NewPlanCost =!string.IsNullOrEmpty(GrantedPromotionCode)? subscriptionInfo.NewVInvoice.TotalCostInitial.ToString() : subscriptionInfo.CostDifference.ToString(); ;
				if(!string.IsNullOrEmpty(subscription.GrantedPromotionCode)) GrantedPromotionCodeText = subscription.GrantedPromotionCode;

				if (subscriptionInfo.CurrentSubscriptionPlanName == "Family Subscription" &&
					(subscriptionInfo.SubscriptionPlanName == "Individual Subscription" || subscriptionInfo.SubscriptionPlanName == "Individual 365 Plan"))
				{
					PlanCost = subscriptionInfo.NewVInvoice.TotalCostInitial.ToString("F", CultureInfo.InvariantCulture);           // if family -> individual the family plan is current until the end of the month
					DiscountApplied = subscriptionInfo.NewVInvoice.TotalDiscountInitial.ToString("F", CultureInfo.InvariantCulture);
					NewPlanCost =  subscriptionInfo.DueDifference;
				}
				else
				{
					PlanCost = subscriptionInfo.NewVInvoice.TotalCostInitial.ToString("F", CultureInfo.InvariantCulture);
					//var PlanCost1 = subscriptionInfo.NewVInvoice.TotalCostNominal.ToString();
					DiscountApplied = subscriptionInfo.NewVInvoice.Main.DiscountInitialDescrip;
					NewPlanCost = subscriptionInfo.DueDifference;
				}
				//PlanCost = subscriptionInfo.NewVInvoice.TotalCostInitial.ToString();
				//DiscountApplied = subscriptionInfo.NewVInvoice.TotalDiscountInitial.ToString();
				ProratedCostAmt = subscriptionInfo.CurrentVInvoice!=null?subscriptionInfo.CurrentVInvoice.Main.DueInitial.ToString(): string.Empty;
				NextBillingDate = subscriptionInfo.NextBillingDate;

				if (subscriptionInfo.SubscriptionTypeID == 1 || subscriptionInfo.SubscriptionTypeID == 5 || subscriptionInfo.SubscriptionTypeID == 6)//SelectedSubscriptionPlan.Name ==  || this.changePlanSelectedPlan == 5 || this.changePlanSelectedPlan == 6)               // new plan is one-time (72 hour, 365 plans)
					Recurring = "";
				else
					Recurring = listText.Find(i => i.TagName == "Recurring Monthly").Text;
			}
			IsAvailable = true;
			IsBusy = false;
		}

		async Task GetFamilyMembers()
		{
			var members = await _patientService.PatientGetFamilyMemberListAsync();
			if (members != null)
			{
				FamilyMembersinPlanVM = new FamilyMembersinPlanViewModel()
				{
					AdditionalFamilyCostPerMember = subscriptionInfo.AdditonalFamilyMemberAmount,
					AdditionalFamilyMemberLimit = subscriptionInfo.SubscriptionMemberLimit,
					SubscriptionCost = subscriptionInfo.SubscriptionCost,
					ListFamilyMembers = members,
					SelectedMembers = new List<int>()
				};
				FamilyMembersinPlanVM.TotalCost = $"{subscriptionInfo.SubscriptionCost}/month";
			}
		}

		private async Task ContinueToSavePlan()
		{

			await _navigationService.Navigate<PatientSettingsManageSubscriptionOrderSummaryViewModel, OrderPlanChangeNavigationParam>
				(new OrderPlanChangeNavigationParam()
				{
					Type = _pType,
					Subscription = subscriptionInfo
				});
		}

		private async Task GoToPreviousPage()
		{
			await _navigationService.Close(this);
		}
	}

	public class FamilyMembersinPlanViewModel : MvxViewModel
	{
		public string AdditionalFamilyCostPerMember { get; set; }
		public int AdditionalFamilyMemberLimit { get; set; }
		public string SubscriptionCost { get; set; }

		private decimal _additionalFamilytotalCost;
		public decimal AdditionalFamilyTotalCost
		{
			get { return _additionalFamilytotalCost; }
			set { SetProperty(ref _additionalFamilytotalCost, value); RaisePropertyChanged(() => AdditionalFamilyTotalCost); }
		}

		private string _totalCost;
		public string TotalCost
		{
			get { return _totalCost; }
			set { SetProperty(ref _totalCost, value); RaisePropertyChanged(() => TotalCost); }
		}

		private List<BasicFamilyMemberInfo> _lstFamilyMembers;
		public List<BasicFamilyMemberInfo> ListFamilyMembers
		{
			get { return _lstFamilyMembers; }
			set { SetProperty(ref _lstFamilyMembers, value); RaisePropertyChanged(() => ListFamilyMembers); }
		}

		private List<int> _selectedMembers;
		public List<int> SelectedMembers
		{
			get { return _selectedMembers; }
			set { SetProperty(ref _selectedMembers, value); RaisePropertyChanged(() => SelectedMembers); }
		}
	}
}