using System.Collections.Generic;
using System.Threading.Tasks;
using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.ViewModels;
using System.Linq;
using MvvmCross.ViewModels;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
	public class PatientVisitDetailViewModel : BaseNavigationViewModel<string>
	{
		public string VisitID;
		private VisitDetailsResponse _visitDetailsResponse;
		public VisitDetailsResponse VisitDetails
		{
			get { return _visitDetailsResponse; }
			set { SetProperty(ref _visitDetailsResponse, value); }
		}

		private List<AbsenceNote> _absenceNotes;
		public List<AbsenceNote> AbsenceNotes
		{
			get { return _absenceNotes; }
			set { SetProperty(ref _absenceNotes, value); }
		}

		private string _statusValue;
		public string StatusValue
		{
			get { return _statusValue; }
			set { SetProperty(ref _statusValue, value); }
		}

		private string _transcriptURL;
		public string TranscriptURL
		{
			get { return _transcriptURL; }
			set { SetProperty(ref _transcriptURL, value); }
		}

		private string _visitedBy;
		public string VisitedBy
		{
			get { return _visitedBy; }
			set { SetProperty(ref _visitedBy, value); }
		}

        private bool _usedMessaging;
        public bool UsedMessaging
        {
            get { return _usedMessaging; }
            set { SetProperty(ref _usedMessaging, value); }
        }

        private bool _usedVideo;
        public bool UsedVideo
        {
            get { return _usedVideo; }
            set { SetProperty(ref _usedVideo, value); }
        }
        private bool _usedAudio;
        public bool UsedAudio
        {
            get { return _usedAudio; }
            set { SetProperty(ref _usedVideo, value); }
        }
        protected override void InitFromBundle(IMvxBundle parameters)
		{
			base.InitFromBundle(parameters);
			VisitID = parameters.Data.Values.FirstOrDefault();
		}

		public async override Task Initialize()
		{
			await LoadVisitDetail();
			await base.Initialize();
		}

		private async Task LoadVisitDetail()
		{
			IsBusy = true;
			VisitDetails = await DataUtility.GetVisitDetailAsync(SettingsValues.ApiURLValue, VisitID.ToString(), CommonAuthSession.Token).ConfigureAwait(false);
			if (VisitDetails != null)
			{
                var subscription = (Globals.Instance.UserInfo.CurrentSubscriptionPlan == "Individual Subscription" || Globals.Instance.UserInfo.CurrentSubscriptionPlan == "Individual 365 Plan") ? true : false;
                Globals.Instance.HadPeviousVisit = subscription? true:false;

                if (VisitDetails.Status != null)
				{
					var absenceNotes = await DataUtility.GetVisitAbsenceNotesAsync(SettingsValues.ApiURLValue, VisitID.ToString(), CommonAuthSession.Token).ConfigureAwait(false);
					if (absenceNotes.Count > 0)
						AbsenceNotes = absenceNotes;
				}
				TranscriptURL = $"{SettingsValues.ChatServerUrlValue}VisitTranscript.aspx?isMobile=true&token={CommonAuthSession.Token}&providerID={VisitDetails.ProviderID}&visitID={VisitDetails.VisitID}";
				StatusValue = VisitDetails.Status;
				VisitedBy = string.IsNullOrEmpty(VisitDetails.Guardian) ? VisitDetails.PatientDisplayName : VisitDetails.Guardian;
				UsedMessaging = VisitDetails.UsedMessaging;
				UsedVideo = VisitDetails.UsedVideo;
				UsedAudio = VisitDetails.UsedAudio;
			}
			IsBusy = false;
		}

		public override void Prepare(string parameter)
		{
			VisitID = parameter;
			base.Prepare();
		}
	}
}
