namespace CommonLibraryCoreMaui.Models
{
    public class VisitDetailsResponse : ResponseBase
    {
        public string VisitID { get; set; }
        public string Status { get; set; }
        public string ProviderName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string DOB { get; set; }
        public string Guardian { get; set; }
        public int GuardianID { get; set; }
        public bool EstablishedPatient { get; set; }
        public string MembershipDomain { get; set; }
        public string ReasonForVisit { get; set; }
        public string ReferredToProviderName { get; set; }
        public string PrimaryDiagnosis { get; set; }
        public string SecondaryDiagnosis { get; set; }
        public string EncounterNotes { get; set; }
        public bool Prescription { get; set; }
        public string AbsenceNotes { get; set; }
        public string Transcript { get; set; }
        public int PrimaryDiagnosisID { get; set; }
        public int SecondaryDiagnosisID { get; set; }
        public int PatientID { get; set; }
        public string PatientDisplayName { get; set; }
        public string PatientPreferredName { get; set; }
        public string Language { get; set; }
        public string Age { get; set; }
        public int ProviderID { get; set; }
        public string Gender { get; set; }
        public string GuardianRelationship { get; set; }
        public string PCP { get; set; }
        public string Phone { get; set; }
        public bool UsedMessaging { get; set; }
        public bool UsedVideo { get; set; }
        public bool UsedAudio { get; set; }
        
        public string GuardianString => string.IsNullOrEmpty(Guardian) ? "None" : Guardian;
    }
}
