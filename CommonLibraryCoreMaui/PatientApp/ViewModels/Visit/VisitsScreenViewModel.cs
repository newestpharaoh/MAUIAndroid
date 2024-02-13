using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.ViewModels;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class VisitsScreenViewModel : BaseViewModel
    {
		private int totalvisitCount;
		BasicFamilyMemberInfo selectedFamilyMember;
		private List<BasicFamilyMemberInfo> _familyMemberList;
		public List<BasicFamilyMemberInfo> FamilyMemberList
		{
			get { return _familyMemberList; }
			set { SetProperty(ref _familyMemberList, value); }
		}

		private MvxObservableCollection<PatientVisitSummary> _patientVisitSummaryList;
		public MvxObservableCollection<PatientVisitSummary> PatientVisitSummaryList
		{
			get { return _patientVisitSummaryList; }
			set { SetProperty(ref _patientVisitSummaryList, value); }
		}

		private string _selectedFilter;
		public string SelectedFilter
		{
			get { return _selectedFilter; }
			set
			{
				SetProperty(ref _selectedFilter, value);
				selectedFamilyMember = FamilyMemberList.FirstOrDefault(x => x.DisplayName == SelectedFilter);
				Task.Run(async() => await LoadVisits(false));
			}
		}

		private PatientVisitSummary _selectedVisitSummary;
		public PatientVisitSummary SelectedVisitSummary
		{
			get { return _selectedVisitSummary; }
			set { SetProperty(ref _selectedVisitSummary, value); }
		}

		private bool _isLoadButtonVisible;
		public bool IsLoadButtonVisible
		{
			get { return _isLoadButtonVisible; }
			set { SetProperty(ref _isLoadButtonVisible, value); }
		}

		public int MaxPosition
		{
			get
			{
				if (PatientVisitSummaryList.Count > 10)
					return PatientVisitSummaryList.Count;
				return 10;
			}
		}

		public bool _isVisitListEmpty;
		public bool IsVisitListEmpty
		{
			get { return _isVisitListEmpty; }
			set { SetProperty(ref _isVisitListEmpty, value); }
		}

		public string _visitListEmptyText;
		public string VisitListEmptyText
		{
			get { return _visitListEmptyText; }
			set { SetProperty(ref _visitListEmptyText, value); }
		}

		public IMvxCommand SelectedVisitedSummaryCommand => new MvxAsyncCommand<object>(SelectedVisitSummaryAsync);
		public IMvxCommand LoadMoreVisitsCommand => new MvxAsyncCommand<object>(LoadMoreVisitsAsync);

		public async override Task Initialize()
		{
			await GetFamilyMembersList();
			await LoadVisits(false);
			await base.Initialize();
		}

		public async Task LoadVisits(bool loadMore, int startIndex = 0, int endIndex = 10)
		{
			IsBusy = true;
			IsVisitListEmpty = false;
			VisitListEmptyText = string.Empty;
			try
			{
				ProviderVisits resp;
				if (selectedFamilyMember?.PatientID != null)
				{
					resp = await DataUtility.GetPatientVisitsAsync(SettingsValues.ApiURLValue, selectedFamilyMember.PatientID.Value, CommonAuthSession.Token, startIndex, endIndex);
				}
				else
				{
					resp = await DataUtility.GetPatientAccountVisitsAsync(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.PatientID, CommonAuthSession.Token, startIndex, endIndex);
				}
				IsBusy = false;
				if (resp == null)
					return;
				totalvisitCount = resp.TotalVisitCount;

				if (PatientVisitSummaryList == null)
					PatientVisitSummaryList = new MvxObservableCollection<PatientVisitSummary>();

				if (resp.Visits == null || resp.Visits.Count == 0)
				{
					PatientVisitSummaryList.Clear();
					IsVisitListEmpty = true;
					VisitListEmptyText = "No Visit History";
					return;
				}

				if (!loadMore)
				{
					PatientVisitSummaryList.Clear();
					foreach(var item in resp.Visits)
						PatientVisitSummaryList.Add(item);
				}
				else
				{
					PatientVisitSummaryList.AddRange(resp.Visits);
				}
				IsLoadButtonVisible = totalvisitCount > PatientVisitSummaryList.Count;
				
			}
			catch
			{
			}
		}

		private async Task LoadMoreVisitsAsync(object arg)
		{
			if(IsLoadButtonVisible)
				await LoadVisits(true, MaxPosition, MaxPosition + 10);
		}

		private async Task GetFamilyMembersList()
		{
			IsBusy = true;
			try
			{
				var results = await DataUtility.PatientGetFamilyMemberListAsync(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.PatientID, CommonAuthSession.Token).ConfigureAwait(false);
				if (results == null)
				{
					return;
				}
				FamilyMemberList = new List<BasicFamilyMemberInfo>() { new BasicFamilyMemberInfo() { DisplayName = "All" } };
				FamilyMemberList.AddRange(results.Where(f => !f.IsPrivate));
			}
			catch (Exception ex)
			{
				ReportCrash(ex, Title);
			}
		}

		private async Task SelectedVisitSummaryAsync(object arg)
		{
			await _navigationService.Navigate<PatientVisitDetailViewModel, string>(((PatientVisitSummary)arg).VisitID.ToString());
		}
	}
}
