namespace CommonLibraryCoreMaui.Models
{
	public class VisitQuestionnaireResponse
	{
		public string EncounterType { get; set; }
		public string MedicationPrescribed { get; set; }
		public bool IsLabOrdered { get; set; }
		public bool IsRadiologyOrdered { get; set; }
		public bool IsPCPFollowUp { get; set; }
		public string SpecialistName { get; set; }
		public bool IsSentToER { get; set; }
		public bool IsSentToClinic { get; set; }
		public string TelemedFollowUpDate { get; set; }
	}
}
