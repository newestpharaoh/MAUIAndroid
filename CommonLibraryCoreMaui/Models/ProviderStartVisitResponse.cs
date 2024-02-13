namespace CommonLibraryCoreMaui.Models
{
    public class ProviderStartVisitResponse
    {
        public int PatientID { get; set; }
        public string VisitReason { get; set; }
        public StatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public string Payload { get; set; }
    }
}