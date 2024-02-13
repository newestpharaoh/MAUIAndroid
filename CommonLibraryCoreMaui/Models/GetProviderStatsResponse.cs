namespace CommonLibraryCoreMaui.Models
{
    public class GetProviderStatsResponse : ResponseBase
    {
        public int IncompleteVisits { get; set; }
        public int PatientsWaiting { get; set; }
        public bool Available { get; set; }
    }
}
