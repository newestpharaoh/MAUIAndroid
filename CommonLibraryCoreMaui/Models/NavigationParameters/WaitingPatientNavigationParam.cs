namespace CommonLibraryCoreMaui.Models.NavigationParameters
{
    public class WaitingPatientNavigationParam : INavigationParam
    {
        public int SelectedWaitingPatient { get; set; }
    }

	public class ForgotPasswordSecurityQuestionNavigationParam : INavigationParam
	{
		public int UserID { get; set; }
		public int QuestionID { get; set; }
		public string QuestionText { get; set; }
	}
}
