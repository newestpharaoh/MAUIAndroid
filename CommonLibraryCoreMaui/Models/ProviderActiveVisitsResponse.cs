using System.Collections.Generic;

namespace CommonLibraryCoreMaui.Models
{
    public class ProviderActiveVisitsResponse
    {
        public List<ActiveVisit> ActiveVisits { get; set; }
        public StatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }
}