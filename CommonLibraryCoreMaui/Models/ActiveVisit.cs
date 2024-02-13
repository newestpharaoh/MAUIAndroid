namespace CommonLibraryCoreMaui.Models
{
    public class ActiveVisit
    {
        public int PatientID { get; set; }
        public string VisitReason { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int VisitID { get; set; }
		public bool IsClosed { get; set; }
    }
}
