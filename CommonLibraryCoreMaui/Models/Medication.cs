using System.ComponentModel;

namespace CommonLibraryCoreMaui.Models
{
    [Description("Medication"), DialogTitle("Medications")]
    public class Medication : PrimaryIssue, IPatientRegistrationMedicalInfoItem, IPatientRegistrationMedicalInfoListItem
    {
		public Medication()
		{
			IssueType = PrimaryIssueType.Medication;
		}

        public string Preview
        {
            get
            {
                return string.Format("{0}\n\n{1}", this.Name, !string.IsNullOrEmpty(this.Description) ? $"Comments:\n\n{this.Description}" : "");
            }
        }
    }
}
