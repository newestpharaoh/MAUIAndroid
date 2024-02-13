using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.Models.NavigationParameters;
using CommonLibraryCoreMaui.ViewModels;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

//using MvvmCross.Commands;
using System.Threading.Tasks;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class ForgotPasswordSecurityQuestionsViewModel : BaseNavigationViewModel<ForgotPasswordSecurityQuestionNavigationParam>
    {
		ForgotPasswordSecurityQuestionNavigationParam securityQuestionNavigationParam;

		private string _questionText;
		public string QuestionText
		{
			get { return _questionText; }
			set { SetProperty(ref _questionText, value); }
		}

		private string _answerText;
		public string AnswerText
		{
			get { return _answerText; }
			set { SetProperty(ref _answerText, value); }
		}

		private bool _isErorr = false;
		public bool IsErorr
		{
			get { return _isErorr; }
			set { SetProperty(ref _isErorr, value); }
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

		public override void Prepare(ForgotPasswordSecurityQuestionNavigationParam parameter)
		{
			securityQuestionNavigationParam = parameter;
			QuestionText = parameter.QuestionText;
			base.Prepare();
		}

		private async Task SubmitAsync()
		{
			IsErorr = false;
			ErrorText = string.Empty;
			if (!string.IsNullOrEmpty(AnswerText))
			{
                StatusResponse resp = await DataUtility.ForgotPasswordCheckSecurityQuestion(SettingsValues.ApiURLValue, securityQuestionNavigationParam.UserID, securityQuestionNavigationParam.QuestionID, AnswerText).ConfigureAwait(false);

                if (resp != null)
                {
                    if (resp.StatusCode == StatusCode.Success)
                    {
                        await _navigationService.Navigate<ResetPasswordViewModel, int>(securityQuestionNavigationParam.UserID);
                    }
                    else
                    {
                        IsErorr = true;
                        ErrorText = "Answer incorrect. Please try again.";
                    }
                }
            }
			else
			{
				IsErorr = true;
				ErrorText = "Please fill all the required fields!";
			}
		}
	}
}