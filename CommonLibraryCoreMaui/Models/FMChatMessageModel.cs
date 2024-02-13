namespace CommonLibraryCoreMaui.Models
{
    public class FMChatMessageModel
    {
        public string Token { get; set; }

        public string ProviderID { get; set; }

        public string VisitID { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Text { get; set; }

        public string UploadedFileName { get; set; }
        public string Timestamp { get; set; }
        public string ReadTimestamp { get; set; }

        public string MessageType { get; set; }

        public string SystemMessage { get; set; }
        public string ReasonForVisit { get; set; }
               
    }
}
