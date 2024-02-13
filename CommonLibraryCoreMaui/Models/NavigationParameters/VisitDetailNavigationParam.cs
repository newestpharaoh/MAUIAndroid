using System;
namespace CommonLibraryCoreMaui.Models.NavigationParameters
{
    public class VisitDetailNavigationParam : INavigationParam
    {
        public int CurrentVisitStatus { get; set; }
        public string VisitId { get; set; }
        public string ProviderId { get; set; }
		public int PatientId { get; set; }
		public string ProviderName { get; set; }
        public string PatientName { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string ReasonForVisit { get; set; }
        public int GuardianID { get; set; }


    }
    public class PharmacyNavigationParam : INavigationParam
    {
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string BusinessName { get; set; }
        public string StreetAddress { get; set; }

        public MedicalHistoryNavigationParam NavigationParam { get; set; }
    }

    public class MedicalHistoryNavigationParam : INavigationParam
	{
		public int PatientId { get; set; }
		public AdditionalFamilyMember PatientAdditionalFamilyMember { get; set; }
		public MedicalInfoNavigationType NavigationType { get; set; }
		public string Name { get; set; }
	}

    public class MedicalHistoryIssueNavigationParam : MedicalHistoryBaseNavigationParamCommon, INavigationParam
    {
        public Tuple<PrimaryIssue, bool> TupleParam { get; set; }
        public bool IsEdit { get; set; } = false;
    }

    public class MedicalHistoryPharmacyNavigationParam : MedicalHistoryBaseNavigationParamCommon, INavigationParam
    {
        public Tuple<Pharmacy, bool> TupleParam { get; set; }
    }

    public class MedicalHistoryPCPNavigationParam : MedicalHistoryBaseNavigationParamCommon, INavigationParam
    {
        public Tuple<PCP, bool> TupleParam { get; set; }
    }

    public class MedicalHistoryBaseNavigationParamCommon
    {
        public MedicalHistoryNavigationParam NavigationParam { get; set; }
    }

    public enum MedicalInfoNavigationType
	{
		Registration,
		My,
		VisitHistoryPatient,
		AddMember
	}

	public class PostVisitDetailNavigationParam : INavigationParam
	{
		public string VisitId { get; set; }
		public string ProviderId { get; set; }
		public int PatientId { get; set; }
		public string PatientName { get; set; }
       
    }
}