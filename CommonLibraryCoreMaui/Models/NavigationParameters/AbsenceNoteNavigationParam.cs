namespace CommonLibraryCoreMaui.Models.NavigationParameters
{
	public class AbsenceNoteNavigationParam : INavigationParam
    {
        public int CurrentVisitStatus { get; set; }
        public int PatientId { get; set; }
		public int? AbsenceNoteId { get; set; }
		public string VisitId { get; set; }
		public string PatientName { get; set; }
	}

	public class VisitAbsentNotesNavigationParam : INavigationParam
	{
		public int CurrentVisitStatus { get; set; }
		public string VisitId { get; set; }
		public int PatientId { get; set; }
		public string PatientName { get; set; }
	}

	public class VisitQuestionnaireNavigationParam : INavigationParam
	{
		public VisitDetailsResponse VisitDetailsResponse { get; set; }
	}

	public class ConfirmDiagnosisAndNotesNavigationParam : INavigationParam
	{
		public string VisitId { get; set; }
		public string PatientName { get; set; }
		public ICDCode PrimaryDiagnosis { get; set; }
		public ICDCode SecondaryDiagnosis { get; set; }
		public string EncounterNotes { get; set; }
	}
}