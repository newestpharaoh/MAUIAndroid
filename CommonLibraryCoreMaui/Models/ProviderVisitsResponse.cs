using System.Collections.Generic;

namespace CommonLibraryCoreMaui.Models
{
    public class ProviderVisitsResponse
    {
        public List<Visit> Visits { get; set; }
        public int TotalVisitCount { get; set; }
    }

    public class ProviderVisits
    {
        public List<PatientVisitSummary> Visits { get; set; }
        public int TotalVisitCount { get; set; }
    }
}
