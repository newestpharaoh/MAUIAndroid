using Newtonsoft.Json;

namespace CommonLibraryCoreMaui.Models
{
    public class ProviderInfo
    {
        public int ProviderID { get; set; }
        public int PatientID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PreferredName { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string AltPhone { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Notes { get; set; }
        public string MedicalSchool { get; set; }
        public string Degree { get; set; }
        public string GraduationDate { get; set; }
        public int SpecialtyID { get; set; }
        public string SpecialtyName { get; set; }
        public string Photo { get; set; }
        public string ActiveVisitCount { get; set; }
        [JsonIgnore]
        public byte[] PhotoByteArray { get; set; }
        public string Password { get; set; }
        public string MPI { get; set; }
        public int PatientAgeMin { get; set; }
        public int PatientAgeMax { get; set; }
        public bool Established { get; set; }
        public int LanguageID { get; set; }
        public string FullName { get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}
