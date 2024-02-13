using CommonLibraryCoreMaui.ViewModels;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class PatientPostVisitSurveyThanksViewModel : BaseNavigationViewModel<string>
	{
		private string _visitID;
		public string VisitID
		{
			get { return _visitID; }
			set { SetProperty(ref _visitID, value); }
		}

		public override void Prepare(string parameter)
		{
			VisitID = parameter;
			base.Prepare();
		}
    }
}
