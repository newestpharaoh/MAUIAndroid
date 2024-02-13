namespace CommonLibraryCoreMaui.Models
{
    public class FMVisitQueueMessageModel
    {
        public string ProviderID { get; set; }

        public string VisitID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ReasonForVisit { get; set; }

        public string Timestamp { get; set; }

        public string MessageType { get; set; }

        public string Token { get; set; }
        public string ReadTimestamp { get; set; }
  
        public string SystemMessage { get; set; }

    }
}
