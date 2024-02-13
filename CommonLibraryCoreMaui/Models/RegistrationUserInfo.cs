namespace CommonLibraryCoreMaui.Models
{
    public class RegistrationUserInfo
    {
        public long PatientID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? MPI { get; set; }
        public string Domain { get; set; }
        public bool IsPrimary { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PreferredName { get; set; }
        public string Language { get; set; }
        public string DateAdded { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string AltPhone { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public StatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public bool? Established { get; set; }
    }
}