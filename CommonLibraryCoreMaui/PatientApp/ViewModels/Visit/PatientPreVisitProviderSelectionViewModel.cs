using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.Services;
using CommonLibraryCoreMaui.ViewModels;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class PatientPreVisitProviderSelectionViewModel : BaseViewModel
    {
        public IVisitsService _visitsService;
		RefershTimer providerRefershTimer;

		private MvxObservableCollection<MvxObservableCollection<ActiveProviderInfo>> _providers;
        public MvxObservableCollection<MvxObservableCollection<ActiveProviderInfo>> Providers
        {
            get { return _providers; }
            set { SetProperty(ref _providers, value); }
        }

        private ProviderInfo _selectedProvider;
        public ProviderInfo SelectedProvider
        {
            get { return _selectedProvider; }
            set { SetProperty(ref _selectedProvider, value); }
        }

		private bool _isAvailable;
		public bool IsAvailable
		{
			get { return _isAvailable; }
			set { SetProperty(ref _isAvailable, value); }
		}

		public MvxInteraction<Dictionary<int, Dictionary<int, bool>>> FillItemsExpand { get; } = new MvxInteraction<Dictionary<int, Dictionary<int, bool>>>();

		public async override Task Initialize()
		{
			providerRefershTimer = new RefershTimer(TimeSpan.FromMilliseconds(SettingsValues.ProviderSelectionRefreshPeriod), () =>
			{
				MvxNotifyTask.Create(GetProviders);
				RaisePropertyChanged(() => Providers);
			});
			await base.Initialize();
		}

		private async Task GetProviders()
		{
			IsBusy = true;
			try
			{
				var lstProviders = new MvxObservableCollection<MvxObservableCollection<ActiveProviderInfo>>();
				List<ProviderInfo> list = await _visitsService.PatientStartVisitStep3((int) StartVisit.Instance.PatientID).ConfigureAwait(false);
				var providerGroupedBySpeciality = list?.GroupBy(user => user.SpecialtyName).Select(x => x.FirstOrDefault().SpecialtyName).ToList();
				foreach (string specialty in providerGroupedBySpeciality)
				{
					var providerList = new MvxObservableCollection<ActiveProviderInfo>(list?.Where(x => x.SpecialtyName == specialty).Select(x => new ActiveProviderInfo()
						{ Parent = this, Provider = x ,IsActiveVisitCount =x.ActiveVisitCount=="0"?true:false}).ToList());
					lstProviders.Add(providerList);
				}

				if (lstProviders.Count > 0)
					IsAvailable = true;
				else
					IsAvailable = false;

				var ExpandRowsValues = new Dictionary<int, Dictionary<int, bool>>();
				if (lstProviders != null && lstProviders.Count > 0)
				{
					for (int x = 0; x < lstProviders.Count; x++)
					{
						var rowValues = new Dictionary<int, bool>();
						for (int j = 0; j < lstProviders[x].Count; j++)
						{
							rowValues.Add(j, true);
						}
						ExpandRowsValues.Add(x, rowValues);
					}
				}
				FillItemsExpand.Raise(ExpandRowsValues);
				Providers = lstProviders;
			}
			catch { }
			IsBusy = false;
		}

		public PatientPreVisitProviderSelectionViewModel(IVisitsService visitsService)
        {
            _visitsService = visitsService;
        }

        public async Task Continue(ProviderInfo provider)
        {
            StartVisit.Instance.ProviderID = provider.ProviderID;
            StartVisit.Instance.ProviderName = $"{provider.Name}";
			await _navigationService.Navigate<PatientPreVisitReasonViewModel>();
		}
		
		public override void ViewAppeared()
		{
			base.ViewAppeared();
			MvxNotifyTask.Create(GetProviders);
			providerRefershTimer.Start();
		}

		public override void ViewDisappeared()
		{
			providerRefershTimer.Stop();
			base.ViewDisappeared();
		}
	}

    public class ActiveProviderInfo
    {
        public PatientPreVisitProviderSelectionViewModel Parent { set; get; }
		public ProviderInfo Provider { set; get; }
		public bool IsActiveVisitCount { set; get; }
		public ActiveProviderInfo(ProviderInfo providerInfo, PatientPreVisitProviderSelectionViewModel parent)
        {
            Provider = providerInfo;
			Parent = parent;
          //  IsActiveVisitCount = providerInfo.ActiveVisitCount=="" || providerInfo.ActiveVisitCount == "0"?false: true;
        }

		public ActiveProviderInfo()
		{
		}

		public IMvxCommand ContinueCommand => new MvxAsyncCommand(() => Parent.Continue(Provider));
    }
}
