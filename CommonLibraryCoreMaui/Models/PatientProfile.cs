using System;

namespace CommonLibraryCoreMaui.Models
{
    public class PatientProfile: ResponseBase, IEquatable<PatientProfile>
    {
        public int PatientID { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string? PrimaryPhone { get; set; }
        public string AlternatePhone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Photo { get; set; }
        public string Relationship { get; set; }
        public string PreferredName { get; set; }
        public string Age { get; set; }
        public string OtherRelationship { get; set; }
        public string NotificationPreference { get; set; }
        public bool Established { get; set; }
        public int LanguageID { get; set; }
        public string Language { get; set; }
        public string ClinicalAnnotation { get; set; }
        public string AccountAnnotation { get; set; }
        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }

		public bool Equals(PatientProfile other)
		{
			if (this.Title != other.Title) return false;
			if (this.FirstName != other.FirstName) return false;
			if (this.MiddleName != other.MiddleName) return false;
			if (this.LastName != other.LastName) return false;
			if (this.DOB != other.DOB) return false;
			if (this.Gender != other.Gender) return false;
			if (this.Email != other.Email) return false;
			if (this.PrimaryPhone != other.PrimaryPhone) return false;
			if (this.AlternatePhone != other.AlternatePhone) return false;
			if (this.Address1 != other.Address1) return false;
			if (this.Address2 != other.Address2) return false;
			if (this.City != other.City) return false;
			if (this.State != other.State) return false;
			if (this.Zip != other.Zip) return false;
			if (this.PreferredName != other.PreferredName) return false;
			if (this.NotificationPreference != other.NotificationPreference) return false;
			if (this.Language != other.Language) return false;
            if (this.Relationship != other.Relationship) return false;
            if (this.OtherRelationship != other.OtherRelationship) return false;
            return true;
		}

		public PatientProfile ShallowCopy()
		{
			return (PatientProfile)this.MemberwiseClone();
		}
	}
}
