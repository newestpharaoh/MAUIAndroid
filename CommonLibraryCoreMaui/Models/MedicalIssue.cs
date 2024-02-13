namespace CommonLibraryCoreMaui.Models
{
    public class MedicalIssue
    {
        public int ID { get; set; }
        public PrimaryIssueType IssueType { get; set; }
        public string Value { get; set; }
		public bool IsChecked { get; set; }
		public string Description { get; set; }
    }
}
