using System;

namespace CommonLibraryCoreMaui.Models
{
    public class Visit
    {
        public int VisitID { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientDisplayName { get; set; }
        public string Status { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string ProviderName { get; set; }
        public string ReasonForVisit { get; set; }
        public Nullable<bool> Prepay { get; set; }
        public string Domain { get; set; }
    }
}
