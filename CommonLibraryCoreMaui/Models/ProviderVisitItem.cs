namespace CommonLibraryCoreMaui
{
    public class ProviderVisitItem
    {
        public string VisitID
        {
            get;
            set;
        }

        public string PatientFirstName
        {
            get;
            set;
        }

        public string PatientLastName
        {
            get;
            set;
        }

        public string FullName => string.Format("{0} {1}", PatientFirstName, PatientLastName);

        public string Status
        {
            get;
            set;
        }

        public string StartTime
        {
            get;
            set;
        }

        public string EndTime
        {
            get;
            set;
        }

        public string ProviderName
        {
            get;
            set;
        }

        public string ReferredToProviderName
        {
            get;
            set;
        }

        public string ReasonForVisit
        {
            get;
            set;
        }

        public string PrimaryDiagnosis
        {
            get;
            set;
        }

        public string SecondaryDiagnosis
        {
            get;
            set;
        }

        public string Prescription
        {
            get;
            set;
        }

        public string AbsenceNotes
        {
            get;
            set;
        }

        public string Transcript
        {
            get;
            set;
        }


    }
}