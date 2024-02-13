using System.ComponentModel;

namespace CommonLibraryCoreMaui.Models
{
    [Description("Surgery"), DialogTitle("Surgeries")]
    public class Surgery : PrimaryIssue, IPatientRegistrationMedicalInfoItem, IPatientRegistrationMedicalInfoListItem
	{
		public Surgery()
		{
			IssueType = PrimaryIssueType.Surgery;
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
