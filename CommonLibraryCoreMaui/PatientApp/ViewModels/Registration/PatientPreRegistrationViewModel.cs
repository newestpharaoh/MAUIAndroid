using System.Threading.Tasks;
using MvvmCross.Commands;
using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.ViewModels;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
	public class PatientPreRegistrationViewModel : BaseViewModel
    {
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set 
			{ 
				SetProperty(ref _firstName, value); 
				if(_firstName?.Length > 0)
				{
					WarningText = string.Empty;
				}
			}
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set 
			{ 
				SetProperty(ref _lastName, value);
				if (_lastName?.Length > 0)
				{
					WarningText = string.Empty;
				}
			}
        }

        private string _dateOfBirth = string.Empty;
        public string DateOfBirth
        {
            get { return _dateOfBirth; }
            set 
			{ 
				SetProperty(ref _dateOfBirth, value);
				if (!string.IsNullOrEmpty(_dateOfBirth))
				{
					WarningText = string.Empty;
				}
			}
        }

		private string _warningText;
		public string WarningText
		{
			get { return _warningText; }
			set { SetProperty(ref _warningText, value); }
		}

		public IMvxCommand RegisterCommand => new MvxAsyncCommand(Register);

        public override Task Initialize()
        {
            return base.Initialize();
        }

#pragma warning disable 0162
        private async Task Register()
        {
			WarningText = string.Empty;
			if (string.IsNullOrEmpty(FirstName) ||
				string.IsNullOrEmpty(LastName) ||
				string.IsNullOrEmpty(DateOfBirth))
			{
				WarningText = "Please fill all the required fields!";
				return;
			}
			IsBusy = true;
			StatusResponse resp = await DataUtility.FindPatientAsync(SettingsValues.ApiURLValue, FirstName, LastName, DateOfBirth).ConfigureAwait(false);
			IsBusy = false;
			if (resp != null)
			{
				switch (resp.StatusCode)
				{
					case StatusCode.Payload:
						if (!string.IsNullOrEmpty(resp.Payload))
						{
							WarningText = resp.Payload;
						}
						break;
					case StatusCode.NotFound:
					case StatusCode.NoActiveMatchFoundButEMRMatchFound:
						if (SettingsValues.ECommerce)
						{
							Registration.Instance.FirstName = FirstName;
							Registration.Instance.LastName = LastName;
							Registration.Instance.DOB = DateOfBirth;
							Registration.Instance.IsSelfPay = true;
							Registration.Instance.MPI = string.IsNullOrEmpty(resp.Payload) ? null : resp.Payload;
							await _navigationService.Navigate<PatientSettingsManageSubscriptionPlanViewModel>();
						}
						else
						{
							await _navigationService.Navigate<PatientRegistrationNotPolicyHolderViewModel, PolicyType>(PolicyType.NotCovered);
						}
						break;
					case StatusCode.InActiveMatchFound:
						if (SettingsValues.ECommerce)
						{
							Registration.Instance.FirstName = FirstName;
							Registration.Instance.LastName = LastName;
							Registration.Instance.DOB = DateOfBirth;
							Registration.Instance.IsSelfPay = true;
							Registration.Instance.MPI = string.IsNullOrEmpty(resp.Payload) ? null : resp.Payload;
							await _navigationService.Navigate<PatientSettingsManageSubscriptionPlanViewModel>();
						}
						else
						{
							await _navigationService.Navigate<PatientRegistrationNotPolicyHolderViewModel, PolicyType>(PolicyType.NotPrimaryAccount);
						}
						break;
					case StatusCode.EmailAlreadyInUse:
					case StatusCode.AlreadyRegistered:
						await _navigationService.Navigate<PatientRegistrationSingleMatchFoundViewModel>();
						break;
					case StatusCode.ActivationEmailSent:
						Registration.Instance.Phone = resp.Payload2;
                        await _navigationService.Navigate<PatientRegistrationActivationEmailSentViewModel, string>(resp.Payload);
						break;
					case StatusCode.NotPolicyHolder:
						await _navigationService.Navigate<PatientRegistrationNotPolicyHolderViewModel, PolicyType>(PolicyType.NoPolicyHolder);
						break;
					case StatusCode.MultipleMatches:
						await _navigationService.Navigate<PatientRegistrationMultipleRecordsFoundViewModel>();
						break;
					case StatusCode.MustBe18ToRegister:
						WarningText = resp.Message;
						break;
					default:
                        if (!string.IsNullOrEmpty(resp.Message))
							WarningText = resp.Message;
						break;
                }
			}
        }
#pragma warning restore 0162
    }
}