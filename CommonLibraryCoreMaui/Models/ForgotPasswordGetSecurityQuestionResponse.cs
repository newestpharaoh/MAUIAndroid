namespace CommonLibraryCoreMaui.Models
{
    public class ForgotPasswordGetSecurityQuestionResponse : StatusResponse 
    {
        public int UserID { get; set; }
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }
    }
}
