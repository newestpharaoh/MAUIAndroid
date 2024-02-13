using System.Collections.Generic;

namespace CommonLibraryCoreMaui.Models
{
    public class RegistrationStep2Request
    {
        public long PatientID { get; set; }
        public List<SecurityQuestionAnswer> Answers { get; set; }
    }

    public class SecurityQuestionAnswer
    {
        public int QuestionID { get; set; }

        public string Answer { get; set; }
    }
}
