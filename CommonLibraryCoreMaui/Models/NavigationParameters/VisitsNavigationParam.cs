namespace CommonLibraryCoreMaui.Models.NavigationParameters
{
    public class VisitsNavigationParam : INavigationParam
    {
        public int CurrentVisitStatus { get; set; }
    }

	public class PrimaryCareNavigationParam : INavigationParam
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string State { get; set; }
        public MedicalHistoryNavigationParam NavigationParam { get; set; }
    }

	public class ProfileNavigationParam : INavigationParam
	{
		public bool IsProfile { get; set; }
		public int PatientId { get; set; }
		public bool IsEmailEnabled { get; set; }
	}
}