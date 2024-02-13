namespace CommonLibraryCoreMaui.Models
{
    public class WaitingPatientModel
    {
        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public string FullName => string.Format("{0} {1}", FirstName, LastName);

        public string ReasonForVisit
        {
            get;
            set;
        }

        public string DOB
        {
            get;
            set;
        }

        public string GuardianName
        {
            get;
            set;
        }

        public bool EstablishedPatient
        {
            get;
            set;
        }

        public string MembershipDomain
        {
            get;
            set;
        }

        public string VisitID
        {
            get;
            set;
        }

        public string TimeVisitRequested
        {
            get;
            set;
        }

        public bool VisitEnded
        {
            get;
            set;
        }

        public int PatientId
        {
            get;
            set;
        }
    }
}