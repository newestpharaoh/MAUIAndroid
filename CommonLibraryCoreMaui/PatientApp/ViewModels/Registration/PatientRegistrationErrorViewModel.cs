using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.ViewModels;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class PatientRegistrationErrorViewModel : BaseNavigationViewModel<StatusCode>
	{
		public StatusCode PateintStatusCode;
		public override void Prepare(StatusCode parameter)
		{
			PateintStatusCode = parameter;
			base.Prepare();
		}
	}
}
