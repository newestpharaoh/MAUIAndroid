using System;

namespace CommonLibraryCoreMaui.Models
{
    public class GetProviderProfileResponse : ResponseBase, IEquatable<GetProviderProfileResponse>
    {
        public int ProviderID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
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
        public string Photo { get; set; }
        public string Phone { get; set; }

        public bool Equals(GetProviderProfileResponse other)
        {
            if (this.FirstName != other.FirstName) return false;
            if (this.LastName != other.LastName) return false;
            if (this.DOB != other.DOB) return false;
            if (this.Gender != other.Gender) return false;
            if (this.Email != other.Email) return false;
            if (this.Street1 != other.Street1) return false;
            if (this.Street2 != other.Street2) return false;
            if (this.City != other.City) return false;
            if (this.State != other.State) return false;
            if (this.Zip != other.Zip) return false;
            if (this.MedicalSchool != other.MedicalSchool) return false;
            if (this.Degree != other.Degree) return false;
            if (this.GraduationDate != other.GraduationDate) return false;
            if (this.SpecialtyID != other.SpecialtyID) return false;
            if (this.Phone != other.Phone) return false;

            return true;
        }

        public GetProviderProfileResponse ShallowCopy()
        {
            return (GetProviderProfileResponse)this.MemberwiseClone();
        }
    }
}
