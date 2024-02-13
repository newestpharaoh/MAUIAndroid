namespace CommonLibraryCoreMaui.Models
{
    public class WaitingPatient
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

        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

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

        public string WaitTime
        {
            get;
            set;
        }

        public bool VisitEnded
        {
            get;
            set;
        }

        public int PatientID
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        }

        public string ProviderName
        {
            get;
            set;
        }

        public string StartTime
        {
            get;
            set;
        }
    }
}
