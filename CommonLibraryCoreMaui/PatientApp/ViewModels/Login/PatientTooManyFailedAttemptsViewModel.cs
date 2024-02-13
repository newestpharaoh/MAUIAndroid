using System.Threading.Tasks;
using CommonLibraryCoreMaui.ViewModels;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class PatientTooManyFailedAttemptsViewModel : BaseViewModel
	{
		private string _timeText;
		public string TimeText
		{
			get { return _timeText; }
			set
			{
				SetProperty(ref _timeText, value);
			}
		}

		public async override Task Initialize()
		{
			TimeText = $"or try again in {FailedAttemptsHelper.ShowWaitingTime()}";
			await base.Initialize();
		}
	}
}
