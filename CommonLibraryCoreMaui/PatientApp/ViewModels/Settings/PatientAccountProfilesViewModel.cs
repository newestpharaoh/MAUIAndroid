using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
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
	public class PatientAccountProfilesViewModel : BaseNavigationViewModel<bool>
	{
		IPatientService _patientService;
        public string Locale = Preferences.Get("Locale", string.Empty);
        private List<UIText> UIText { get; set; }

        private MvxObservableCollection<FamilMemberGroup> _familyMemberGroupCollection;
		public MvxObservableCollection<FamilMemberGroup> FamilyMemberGroupCollection
		{
			get { return _familyMemberGroupCollection; }
			set { SetProperty(ref _familyMemberGroupCollection, value);}
		}

		private AccountSubscriptionInfo _accountMemberSubscriptionInfo;
		public AccountSubscriptionInfo AccountMemberSubscriptionInfo
		{
			get { return _accountMemberSubscriptionInfo; }
			set { SetProperty(ref _accountMemberSubscriptionInfo, value); }
		}

		private PatientManageSubscriptionMembersViewModel _subscriptionMembersViewModel;
		public PatientManageSubscriptionMembersViewModel SubscriptionMembersViewModel
		{
			get { return _subscriptionMembersViewModel; }
			set { SetProperty(ref _subscriptionMembersViewModel, value); }
		}

		private string _additionalMemberName = string.Empty;
		public string AdditionalMemberName
		{
			get { return _additionalMemberName; }
			set { SetProperty(ref _additionalMemberName, value); }
		}

		private string _currentReaminingMemberText = string.Empty;
		public string CurrentReaminingMemberText
		{
			get { return _currentReaminingMemberText; }
			set { SetProperty(ref _currentReaminingMemberText, value); }
		}

		private int _currentReminingMember = 0;
		public int CurrentReminingMember
		{
			get { return _currentReminingMember; }
			set 
			{ 
				SetProperty(ref _currentReminingMember, value);
				var memberText = value == 1 ? "member" : "members";
				CurrentReaminingMemberText = value!=0?$"You have room for {value} additional family {memberText} with your current plan.":string.Empty;
			}
		}

		private string _additionalMemberCost = string.Empty;
		public string AdditionalMemberCost
		{
			get { return _additionalMemberCost; }
			set { SetProperty(ref _additionalMemberCost, value); }
		}

		private string _currentSubscriptionPlan = string.Empty;
		public string CurrentSubscriptionPlan
		{
			get { return _currentSubscriptionPlan; }
			set { SetProperty(ref _currentSubscriptionPlan, value); }
		}

		private bool _isFamilyMemberFatched = true;
		public bool IsFamilyMemberFatched
		{
			get { return _isFamilyMemberFatched; }
			set { SetProperty(ref _isFamilyMemberFatched, value); }
		}

		private bool _isFamilyPlan = false;
		public bool IsFamilyPlan
		{
			get { return _isFamilyPlan; }
			set { SetProperty(ref _isFamilyPlan, value); }
		}

		private bool _isFamTextHidden = false;
		public bool IsFamTextHidden
		{
			get { return _isFamTextHidden; }
			set { SetProperty(ref _isFamTextHidden, value); }
		}

		private bool _canAddFamilyMembers = true;
		public bool CanAddFamilyMembers
		{
			get { return _canAddFamilyMembers; }
			set { SetProperty(ref _canAddFamilyMembers, value); }
		}

		private bool isFamilyMemberAadded { get; set; } = false;

		AccountAddFamilyMemberInfo accountAddFamilyMemberInfo { get; set; }
		public MvxInteraction<Dictionary<int, bool>> FillItemsExpand { get; } = new MvxInteraction<Dictionary<int, bool>>();
		public MvxInteraction RefreshAccountProfilePage { get; } = new MvxInteraction();
		public IMvxCommand AddFamilyMemberCommand => new MvxAsyncCommand(AddFamilyMember);

		public PatientAccountProfilesViewModel(IMvxNavigationService mvxNavigationService, IPatientService patientService, IUserDialogs userDialogs)
		{
			_navigationService = mvxNavigationService;
			_patientService = patientService;
			_userDialogs = userDialogs;
		}

		public async override Task Initialize()
		{
			await RefreshAccountMembersList();
			await GetUITexts();
            await base.Initialize();
		}

		public override void ViewAppeared()
		{
			base.ViewAppeared();
			if (AccountAddFamilyMember.Instance.AdditionalFamilyMembers != null && !isFamilyMemberAadded)
			{
				_ = ShowAddMember();
			}
			if(isFamilyMemberAadded)
			{
				isFamilyMemberAadded = false;
			}
			
		}

        public async Task<List<CommonLibraryCoreMaui.Models.UIText>> GetUITopic(string strTopicName)
        {
            var pageText = await DataUtility.GetUITopicListAsync(SettingsValues.ApiURLValue, strTopicName, Locale).ConfigureAwait(false);
            return pageText.UITextList;
        }

		public async Task<List<UIText>> GetUITexts()
		{
            return UIText = Task.Run<List<UIText>>(async () => await GetUITopic("AccountProfile")).Result;
        }

		public string GetText(string NameTag)
		{
			string enText = string.Empty;
			if (UIText != null)
			{
				enText = UIText.Find(x => x.TagName == NameTag).Text;
            }
			return enText;
        }

        public async Task ShowAddMember()
		{
			string additionalFamilyMembers = GetAdditionaFamilyMemberNames();
			var msg = string.Format(GetText("AddedToFamily"), $"{additionalFamilyMembers}");
         //   await _userDialogs.AlertAsync($"{additionalFamilyMembers} have been added to your famiy.", "Add Family Member").ContinueWith(async (t) =>
           await _userDialogs.AlertAsync(msg, GetText("AddFamilyMember")).ContinueWith(async (t) =>
            {
                await CheckIfAddNewFamilyMember();
            });        
         }

#pragma warning disable 0162
        private async Task AddFamilyMember()
		{
			try
			{
				IsBusy = true;
				accountAddFamilyMemberInfo = await _patientService.PatientGetAddFamilyMemberInfoAsync();
				CurrentReminingMember = accountAddFamilyMemberInfo.FreeFamilyMembersRemaining;
				IsBusy = false;

				string msg = string.Empty;
				if (accountAddFamilyMemberInfo.IncludedInPlan && accountAddFamilyMemberInfo.CanAddFamilyMember)
				{
					var memberText = accountAddFamilyMemberInfo.FreeFamilyMembersRemaining > 1 ? "members" : "member";
                    var msg_txt = string.Format(GetText("YouHaveRoom"), $"{accountAddFamilyMemberInfo.FreeFamilyMembersRemaining}",$"{memberText}");

					//	msg = $"You have room for {accountAddFamilyMemberInfo.FreeFamilyMembersRemaining} additional family {memberText} with your current plan. The patient will be added under this payment plan.";
					msg = msg_txt;
				}
				else
				{
					if (SettingsValues.ECommerce && accountAddFamilyMemberInfo.CanAddFamilyMember)
					{
                        var msg_txt = string.Format(GetText("NoOpenSlots"), $"{accountAddFamilyMemberInfo.SingleAddOnCost}");                      
                        msg = msg_txt + Environment.NewLine  + GetText("NoOpenSlots_para2");
					}
					else
					{                     
                        await _userDialogs.AlertAsync(GetText("YouDoNotHaveRoom"));
						return;
					}
				}
				var UTitle = GetText("AddFamilyMember");

                var result = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
				{
					Title = UTitle,
					Message = msg,
					OkText = "Cancel",
					CancelText = "Continue"
				});

				if (!result)
				{
					AccountAddFamilyMember.Instance.InitialFreeFamilyMemberRemaining = accountAddFamilyMemberInfo.FreeFamilyMembersRemaining;
					AccountAddFamilyMember.Instance.IncludedInPlan = accountAddFamilyMemberInfo.IncludedInPlan;
					AccountAddFamilyMember.Instance.FreeFamilyMemberRemaining = accountAddFamilyMemberInfo.FreeFamilyMembersRemaining;
					AccountAddFamilyMember.Instance.PrimaryPatientID = Globals.Instance.UserInfo.PatientID;
					AccountAddFamilyMember.Instance.AdditionalCost = accountAddFamilyMemberInfo.AddOnCost;
					AccountAddFamilyMember.Instance.SingleAdditionalCost = accountAddFamilyMemberInfo.SingleAddOnCost;
					AccountAddFamilyMember.Instance.ProratedCost = accountAddFamilyMemberInfo.ProratedAddOnCost;
					isFamilyMemberAadded = await _navigationService.Navigate<PatientProfileViewModel, ProfileNavigationParam>(new ProfileNavigationParam()
					{
						IsProfile = false,
						//PatientId = 0,
						IsEmailEnabled = false
					});
					if(isFamilyMemberAadded)
					{
						await CheckIfAddNewFamilyMember();
					}
				}

			}
			catch (Exception ex)
			{
				ReportCrash(ex, Title);
				await _userDialogs.AlertAsync(ex.Message);
			}
		}

		private async Task AddAnotherFamilyMember()
		{
			try
			{
				IsBusy = true;
				string msg = string.Empty;
				var numberOfFamilyMembers = 0;
				if (AccountAddFamilyMember.Instance.AdditionalFamilyMembers.Count > 0)
					numberOfFamilyMembers = AccountAddFamilyMember.Instance.AdditionalFamilyMembers.Count + 1;

				accountAddFamilyMemberInfo = await _patientService.PatientGetAddFamilyMemberInfoAsync(numberOfFamilyMembers);
				CurrentReminingMember = accountAddFamilyMemberInfo.FreeFamilyMembersRemaining;
				IsBusy = false;

				if (accountAddFamilyMemberInfo.IncludedInPlan && accountAddFamilyMemberInfo.CanAddFamilyMember)
				{
					AccountAddFamilyMember.Instance.FreeFamilyMemberRemaining = accountAddFamilyMemberInfo.FreeFamilyMembersRemaining;
					AccountAddFamilyMember.Instance.IncludedInPlan = accountAddFamilyMemberInfo.IncludedInPlan;
					AccountAddFamilyMember.Instance.PrimaryPatientID = Globals.Instance.UserInfo.PatientID;
					AccountAddFamilyMember.Instance.AdditionalCost = accountAddFamilyMemberInfo.AddOnCost;
					AccountAddFamilyMember.Instance.SingleAdditionalCost = accountAddFamilyMemberInfo.SingleAddOnCost;
					AccountAddFamilyMember.Instance.ProratedCost = accountAddFamilyMemberInfo.ProratedAddOnCost;
					isFamilyMemberAadded = await _navigationService.Navigate<PatientProfileViewModel, ProfileNavigationParam>(new ProfileNavigationParam()
					{
						IsProfile = false,
						//PatientId = 0,
						IsEmailEnabled = false
					});
					if (isFamilyMemberAadded)
					{
						await CheckIfAddNewFamilyMember();
					}
					return;
				}
				else
				{
					if (SettingsValues.ECommerce)
					{
						if (accountAddFamilyMemberInfo.CanAddFamilyMember)
						{							
							var msg_txt = string.Format(GetText("YouDoNotHaveRoom"),$"{accountAddFamilyMemberInfo.SingleAddOnCost}");
							msg = msg_txt;
                                //$"You do not have any open slots for additional family member under your current payment plan. The patient will be added as an additional family member at {accountAddFamilyMemberInfo.SingleAddOnCost} for 5 additional visits per month. The difference in billing will start immediately and the new subscription total will begin at the start of the next billing cycle.";
                        }
						else
						{
							await _userDialogs.AlertAsync(GetText("YouDoNotHaveRoom")).ContinueWith(async (t) =>
							{
								await PatientAddFamilyMember(true);
							});
							return;
						}
					}
					else
					{
						await _userDialogs.AlertAsync(GetText("YouDoNotHaveRoom")).ContinueWith(async (t) =>
						{
							await PatientAddFamilyMember(true);
						});
						return;
					}
				}
				AccountAddFamilyMember.Instance.FreeFamilyMemberRemaining = accountAddFamilyMemberInfo.FreeFamilyMembersRemaining;
				AccountAddFamilyMember.Instance.IncludedInPlan = accountAddFamilyMemberInfo.IncludedInPlan;
				AccountAddFamilyMember.Instance.PrimaryPatientID = Globals.Instance.UserInfo.PatientID;
				AccountAddFamilyMember.Instance.AdditionalCost = accountAddFamilyMemberInfo.AddOnCost;
				AccountAddFamilyMember.Instance.SingleAdditionalCost = accountAddFamilyMemberInfo.SingleAddOnCost;
				AccountAddFamilyMember.Instance.ProratedCost = accountAddFamilyMemberInfo.ProratedAddOnCost;

				var uiTitle= GetText("AddAnotherFamilyMember");

                var result = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
				{
					Title = uiTitle,
					Message = msg,
					OkText = "Cancel",
					CancelText = "Continue"
				});

				if (!result)
				{
					isFamilyMemberAadded = await _navigationService.Navigate<PatientProfileViewModel, ProfileNavigationParam>(new ProfileNavigationParam()
					{
						IsProfile = false,
						//PatientId = 0,
						IsEmailEnabled = false
					});
					if(isFamilyMemberAadded)
					{
						if (AccountAddFamilyMember.Instance.AdditionalFamilyMembers.Count > 0)
						{
							accountAddFamilyMemberInfo = await _patientService.PatientGetAddFamilyMemberInfoAsync(AccountAddFamilyMember.Instance.AdditionalFamilyMembers.Count);
							AccountAddFamilyMember.Instance.AdditionalCost = accountAddFamilyMemberInfo.AddOnCost;
							AccountAddFamilyMember.Instance.SingleAdditionalCost = accountAddFamilyMemberInfo.SingleAddOnCost;
							AccountAddFamilyMember.Instance.ProratedCost = accountAddFamilyMemberInfo.ProratedAddOnCost;
						}
						await CheckIfAddNewFamilyMember();
					}
				}
				else
				{
					await CheckIfAddNewFamilyMember();
				}
			}
			catch (Exception ex)
			{
				ReportCrash(ex, Title);
				await _userDialogs.AlertAsync(ex.Message);
			}
		}

		private async Task CheckIfAddNewFamilyMember()
		{
			if (AccountAddFamilyMember.Instance?.AddedFamilyMembersNames == null)
				return;

			if (AccountAddFamilyMember.Instance.InitialFreeFamilyMemberRemaining >=
									AccountAddFamilyMember.Instance.AddedFamilyMembersNames.Count)
			{
				await PatientAddFamilyMember(true);
			}
			else
			{
				var isRefresh = await _navigationService.Navigate<PatientSettingsAddFamilyMemberOrderSummaryViewModel, AccountAddFamilyMemberInfo>(accountAddFamilyMemberInfo);
				if (isRefresh)
				{
					await RefreshAccountMembersList();
				}
			}
		}
#pragma warning restore 0162

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
				IsFamilyMemberFatched = true;
				var isFam365 = results.CurrentSubscriptionPlan.Contains("Family 365 Plan");
				IsFamilyPlan = !results.IsFamilyPlan && isFam365 ? isFam365 : results.IsFamilyPlan;
				IsFamTextHidden = !IsFamilyPlan;
				Globals.Instance.UserInfo.NewSubscriptionPlan = results.NewSubscriptionPlan;
				Globals.Instance.UserInfo.CurrentSubscriptionEndDate = results.CurrentSubscriptionEndDate;
				CurrentSubscriptionPlan = $"Plan: {results.CurrentSubscriptionPlan}";
			
				results.AccountMembers.Where(x => x.IsActive == false).ToList().ForEach(x => x.PaymentPlan = "Deactivated");
				results.AccountMembers.Where(x => x.IsPrimary == true).ToList().ForEach(x => x.PaymentPlan = "Account Holder");
				
				FamilyMemberGroupCollection = CreateFamilyGroups(results.AccountMembers);
				AccountMemberSubscriptionInfo = results;
				CanAddFamilyMembers = results.CanAddFamilyMembers;

				accountAddFamilyMemberInfo = await _patientService.PatientGetAddFamilyMemberInfoAsync();
				CurrentReminingMember = accountAddFamilyMemberInfo.FreeFamilyMembersRemaining;
			}
			catch (Exception ex)
			{
				ReportCrash(ex, Title);
			}

			IsBusy = false;
		}

		public async Task<bool> PatientAddFamilyMember(bool showMembersNames = false)
		{
			var isSuccess = false;
			IsBusy = true;
			if (AccountAddFamilyMember.Instance != null)
			{
				StatusResponse resp = await DataUtility.PatientAddFamilyMembersAsync(SettingsValues.ApiURLValue, AccountAddFamilyMember.Instance, CommonAuthSession.Token).ConfigureAwait(false);
				if (resp != null)
				{
					if (resp.StatusCode == StatusCode.Success)
					{
						IsBusy = false;
						isSuccess = true;
						AccountAddFamilyMember.Instance.Clear();
						if (showMembersNames)
						{
							//var membersName = GetAdditionaFamilyMemberNames();
							//await _userDialogs.AlertAsync($"{membersName} have been added to your famiy."
							//	//+Environment.NewLine +
							//	//$"There will be no change to your subscription at this time."
							//	, "Add Family Member").ContinueWith(async (t) =>
							//	{
									AccountAddFamilyMember.Instance.ClearNameList();
									await RefreshAccountMembersList().ConfigureAwait(false);
								//});
						}
					}
					else
					{
						IsBusy = false;
						AccountAddFamilyMember.Instance.Clear();
						if (showMembersNames && AccountAddFamilyMember.Instance.AddedFamilyMembersNames.Count > 0)
						{                       
                            var membersName = GetAdditionaFamilyMemberNames(); //NoChange
                            var uiMsg1 = string.Format(GetText("AddedToFamily"), $"{membersName}");
                            await _userDialogs.AlertAsync(uiMsg1 + Environment.NewLine +
                                GetText("NoChange"), GetText("AddFamilyMember")).ContinueWith(async (t) =>
								{
									AccountAddFamilyMember.Instance.ClearNameList();
									await RefreshAccountMembersList().ConfigureAwait(false);
								});
						}
						else
						{
							AccountAddFamilyMember.Instance.ClearNameList();
							await RefreshAccountMembersList().ConfigureAwait(false);
						}
					}
				}
			}
			return isSuccess;
		}

		protected MvxObservableCollection<FamilMemberGroup> CreateFamilyGroups(List<AccountMember> accountMembers)
		{
			var familyMemberGroup = new List<FamilMemberGroup>();
			foreach (var member in accountMembers)
			{
				familyMemberGroup.Add(new FamilMemberGroup(async ()=> { RefreshAccountProfilePage.Raise(); })
				{
					Member = member
				});
			}

			var ExpandRowsValues = new Dictionary<int, bool>();
			for (int i = 0; i < accountMembers.Count; i++)
			{
				ExpandRowsValues.Add(i, false);
			}
			FillItemsExpand.Raise(ExpandRowsValues);

			return new MvxObservableCollection<FamilMemberGroup>(familyMemberGroup);
		}

		private string GetAdditionaFamilyMemberNames()
		{
			var additionalFamilyMembers = string.Empty;

			//Globals.Instance.UserInfo.PatientID = AccountAddFamilyMember.Instance.PrimaryPatientID;
            var memberCount = AccountAddFamilyMember.Instance.AddedFamilyMembersNames.Count;
			for (int i = 0; i < memberCount; i++)
			{
				var fullName = AccountAddFamilyMember.Instance.AddedFamilyMembersNames[i].FullName;
				if (memberCount > 2 && i == memberCount - 2)
				{
					additionalFamilyMembers += $"{fullName} and ";
					continue;
				}
				if (i == memberCount - 1)
				{
					additionalFamilyMembers += $"{fullName}";
					continue;
				}
				additionalFamilyMembers += $"{fullName}, ";
			}

			return additionalFamilyMembers;
		}

	}
}
