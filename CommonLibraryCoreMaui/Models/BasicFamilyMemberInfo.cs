namespace CommonLibraryCoreMaui.Models
{
    public class BasicFamilyMemberInfo
    {
        public int? PatientID { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsActive { get; set; }
        public bool IsPrivate { get; set; }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
