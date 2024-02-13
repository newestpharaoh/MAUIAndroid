using System.ComponentModel;

namespace CommonLibraryCoreMaui.Models
{
    [Description("Allergy"), DialogTitle("Allergies")]
    public class Allergy : PrimaryIssue, IPatientRegistrationMedicalInfoItem, IPatientRegistrationMedicalInfoListItem
    {
		public Allergy()
		{
			IssueType = PrimaryIssueType.Allergy;
		}

        public string Preview
        {
            get
            {
                return string.Format("{0}\n\n{1}", this.Name, !string.IsNullOrEmpty(this.Description) ? $"Comments:\n\n{this.Description}" : "");
            }
        }
    }

    public interface IPatientRegistrationMedicalInfoItem
    {
        string Preview { get; }
    }

    public interface IPatientRegistrationMedicalInfoListItem
    {
        string ListCaption { get; }

        int Position { get; set; }
    }
}
