namespace CommonLibraryCoreMaui.Models
{
    public class PatientVisitSummary
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
        public string EncoutnerNotes { get; set; }
        public int PrimaryDiagnosisID { get; set; }
        public string PrimaryDiagnosis { get; set; }
        public int SecondaryDiagnosisID { get; set; }
        public string SecondaryDiagnosis { get; set; }
        public string ReferredTo { get; set; }
        public bool Prescription { get; set; }
    }
}
