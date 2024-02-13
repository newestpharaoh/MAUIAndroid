namespace CommonLibraryCoreMaui.Models
{
    public class SecurityQuestion
    {
        public int ID { get; set; }
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }
        public int UserID {get; set;}
        public SecurityQuestion ShallowCopy()
        {
            return (SecurityQuestion)this.MemberwiseClone();
        }

    }
}
