using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.Models.NavigationParameters;
using CommonLibraryCoreMaui.ViewModels;
using MvvmCross.Commands;
using System.Threading.Tasks;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
	public class ForgotPasswordUserInfoViewModel : BaseViewModel
    {
		private string _firstName;
		public string FirstName
		{
			get { return _firstName; }
			set { SetProperty(ref _firstName, value); }
		}

		private string _lastName;
		public string LastName
		{
			get { return _lastName; }
			set { SetProperty(ref _lastName, value); }
		}

		private string _dateOfBirth = string.Empty;
		public string DateOfBirth
		{
			get { return _dateOfBirth; }
			set { SetProperty(ref _dateOfBirth, value); }
		}

		private string _email = string.Empty;
		public string Email
		{
			get { return _email; }
			set { SetProperty(ref _email, value); }
		}

		private bool _isErorr = false;
		public bool IsErorr
		{
			get { return _isErorr; }
			set { SetProperty(ref _isErorr, value); }
		}

		private bool _isResponseErorr = false;
		public bool IsResponseErorr
		{
			get { return _isResponseErorr; }
			set { SetProperty(ref _isResponseErorr, value); }
		}

		private bool _isNotFoundErorr = false;
		public bool IsNotFoundErorr
		{
			get { return _isNotFoundErorr; }
			set { SetProperty(ref _isNotFoundErorr, value); }
		}

		private string _errorText;
		public string ErrorText
		{
			get { return _errorText; }
			set { SetProperty(ref _errorText, value); }
		}

		public IMvxCommand SubmitCommand => new MvxAsyncCommand(SubmitAsync);


		public override Task Initialize()
        {
            return base.Initialize();
        }

		private async Task SubmitAsync()
		{
			IsErorr = IsResponseErorr = IsNotFoundErorr = false;
			IsBusy = true;
			if (string.IsNullOrEmpty(FirstName) ||
				string.IsNullOrEmpty(LastName) ||
				string.IsNullOrEmpty(DateOfBirth) ||
				string.IsNullOrEmpty(Email))
			{
				IsBusy = false;
				ErrorText = "Please fill all the required fields!";
				IsErorr = true;
				IsResponseErorr = IsNotFoundErorr = false;
				return;
			}
			if (!System.Text.RegularExpressions.Regex.Match(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
			{
				IsBusy = false;
				ErrorText = "Email is not valid!";
				IsErorr = true;
				IsResponseErorr = IsNotFoundErorr= false;
				return;
			}
			
			ForgotPasswordGetSecurityQuestionResponse resp = await DataUtility.ForgotPasswordGetSecurityQuestion(SettingsValues.ApiURLValue, FirstName, LastName, DateOfBirth, Email).ConfigureAwait(false);
			if (resp != null)
			{
				IsBusy = false;
				switch (resp.StatusCode)
				{
					case StatusCode.Success:
						await _navigationService.Navigate<ForgotPasswordSecurityQuestionsViewModel, ForgotPasswordSecurityQuestionNavigationParam>(new ForgotPasswordSecurityQuestionNavigationParam()
						{
							UserID = resp.UserID,
							QuestionID = resp.QuestionID,
							QuestionText = resp.QuestionText
						});
						break;
					case StatusCode.NotFound:
						ErrorText = "No account was found for the entered information.";
						IsErorr = IsNotFoundErorr = true;
						IsResponseErorr = false;
						break;
					case StatusCode.Lockout:
						break;
					default:
						ErrorText = "Information incorrect. Please review and try again.";
						IsErorr = IsNotFoundErorr = false;
						IsResponseErorr = true;
						break;
				}
			}
		}
	}
}