using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.Models.NavigationParameters;
using MvvmCross.ViewModels;
using System.Linq;
using CommonLibraryCoreMaui.PatientApp.ViewModels;
using System.Collections.Generic;

namespace CommonLibraryCoreMaui.ViewModels
{
    public class PatientRegistrationStepTwoViewModel : BaseViewModel
    {
		private SecurityQuestionsViewModel _securityQuestionsResponse;
		public SecurityQuestionsViewModel SecurityQuestionsResponse
		{
			get { return _securityQuestionsResponse; }
			set
			{
				SetProperty(ref _securityQuestionsResponse, value);
			}
		}

		public bool IsSelfPay
		{
			get { return Registration.Instance.IsSelfPay; }
		}

		public IMvxCommand PreviousCommand => new MvxAsyncCommand(PreviousAsync);
		public IMvxCommand ContinueCommand => new MvxAsyncCommand(ContinueAsync);

		public override async Task Initialize()
        {
			var lstQuestion = await DataUtility.GetSecurityQuestions(SettingsValues.ApiURLValue).ConfigureAwait(false);
			var mvxLstQuestion = new MvxObservableCollection<SecurityQuestion>();
			foreach (var q in lstQuestion)
				mvxLstQuestion.Add(q);

			if(Registration.Instance.Answers?.Count > 1)
				mvxLstQuestion.ToList().ForEach(q => 
				{
					q.AnswerText = Registration.Instance.Answers.Where(a => a.QuestionID == q.ID).Select(a => a.Answer).FirstOrDefault();
				});

			SecurityQuestionsResponse = new SecurityQuestionsViewModel() { SecurityQuestions = mvxLstQuestion};
			

			await base.Initialize();
		}
        
        private async Task ContinueAsync()
        {
			var securityQuestions = SecurityQuestionsResponse.SecurityQuestions.Where(x => !string.IsNullOrEmpty(x.AnswerText)).ToList();
			if (securityQuestions.Count < 3)
			{
				await _userDialogs.AlertAsync("Answers for at least 3 questions are required.");
				return;
			}

			var validAnswrs = (from question in SecurityQuestionsResponse.SecurityQuestions
							   where !string.IsNullOrEmpty(question.AnswerText)
							   select new SecurityQuestionAnswer
							   {
								   QuestionID = question.ID,
								   Answer = question.AnswerText
							   }).ToList();

            RegistrationStep2Request req = new RegistrationStep2Request();

            if (!string.IsNullOrEmpty(Registration.Instance.PatientID))
                req.PatientID = Convert.ToInt64(Registration.Instance.PatientID);

            req.Answers = validAnswrs;

			StatusResponse resp = await DataUtility.RegistrationStep2Async(SettingsValues.ApiURLValue, req).ConfigureAwait(false);
			if (resp != null)
			{
				switch (resp.StatusCode)
				{
					case StatusCode.Success:
						Registration.Instance.Answers = validAnswrs;
						if (Registration.Instance.IsSelfPay)
						{
							await _navigationService.Navigate<PatientRegistrationVerificationViewModel>();
						}
						else
						{
							StatusResponse prepayresp = await DataUtility.RegistrationStep3PrePayAsync(SettingsValues.ApiURLValue, Registration.Instance).ConfigureAwait(false);
							if (prepayresp != null)
							{
								switch (prepayresp.StatusCode)
								{
									case StatusCode.Success:
										int patientId;
										if (int.TryParse(prepayresp.Payload, out patientId))
										{
											await _navigationService.Navigate<PatientMedicalnfoViewModel, MedicalHistoryNavigationParam>(
													new MedicalHistoryNavigationParam() { PatientId = patientId, NavigationType = MedicalInfoNavigationType.Registration });
										}
										break;
									default:
										await _userDialogs.AlertAsync("There was an error please try again.");
										break;
								}
							}
						}

						break;
					case StatusCode.SecurityQuestionMinimumNotMet:
						await _userDialogs.AlertAsync("Security Question Minimum Not Met.");
						break;
					default:
						await _userDialogs.AlertAsync("There was an error please try again.");
						break;
				}

			}

			
        }


		private async Task PreviousAsync()
		{
			var validAnswrs = (from question in SecurityQuestionsResponse.SecurityQuestions
							   where !string.IsNullOrEmpty(question.AnswerText)
							   select new SecurityQuestionAnswer
							   {
								   QuestionID = question.ID,
								   Answer = question.AnswerText
							   }).ToList();
			Registration.Instance.Answers = validAnswrs;
			await _navigationService.Close(this);
		}
	}
}