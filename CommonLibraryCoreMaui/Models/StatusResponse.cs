namespace CommonLibraryCoreMaui.Models
{
    public class StatusResponse : ResponseBase
    {
        public StatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public string Payload { get; set; }
        public string Payload2 { get; set; }
    }
}
