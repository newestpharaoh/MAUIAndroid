using System.ComponentModel;

namespace CommonLibraryCoreMaui.Models
{
    [Description("Pharmacy"), DialogTitle("Pharmacy")]
    public class Pharmacy : IPatientRegistrationMedicalInfoItem
    {
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string BusinessName { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string Description { get; set; }
        public bool IsCapsule { get; set; }
        public bool IsCurative { get; set; }
        public string Preview
        {
            get
            {
                return $"Pharmacy\n\n{this.ToString()}";
            }
        }

        public string PharmacyString => DescriptionForVisitHeader();

        public new string ToString()
        {
            string content = string.Empty;
            if (!string.IsNullOrEmpty(BusinessName))
            {
                content = string.Concat(content, BusinessName, "\n");
            }
            if (!string.IsNullOrEmpty(Description))
            {
                content = string.Concat(content, Description, "\n");
            }
            if (!string.IsNullOrEmpty(StreetAddress1))
            {
                content = string.Concat(content, StreetAddress1, "\n");
            }
            if (!string.IsNullOrEmpty(StreetAddress2))
            {
                content = string.Concat(content, StreetAddress2, "\n");
            }
            string addressLine = AddressLine();
            if (!string.IsNullOrEmpty(addressLine))
            {
                content = string.Concat(content, addressLine, "\n");
            }

            return content.Trim();
        }

        public  string ProviderMedicalInfoString()
        {
            string content = string.Empty;
            if (!string.IsNullOrEmpty(BusinessName))
            {
                content = string.Concat(content, BusinessName, "\n");
            }
            if (!string.IsNullOrEmpty(StreetAddress1))
            {
                content = string.Concat(content, StreetAddress1, "\n");
            }
            if (!string.IsNullOrEmpty(StreetAddress2))
            {
                content = string.Concat(content, StreetAddress2, "\n");
            }
            string addressLine = AddressLine();
            if (!string.IsNullOrEmpty(addressLine))
            {
                content = string.Concat(content, addressLine, "\n");
            }
            if (!string.IsNullOrEmpty(Description))
            {
                content = string.Concat(content, Description, "\n");
            }

            return content.Trim();
        }

        public string DescriptionForVisitHeader()
        {
            string content = string.Empty;

            if (!string.IsNullOrEmpty(BusinessName))
            {
                content = string.Concat(content, BusinessName, "\n");
            }

            if (!string.IsNullOrEmpty(StreetAddress1))
            {
                content = string.Concat(content, StreetAddress1, "\n");
            }

            if (!string.IsNullOrEmpty(StreetAddress2))
            {
                content = string.Concat(content, StreetAddress2, "\n");
            }

            string addressLine = AddressLine();
            if (!string.IsNullOrEmpty(addressLine))
            {
                content = string.Concat(content, addressLine, "\n");
            }

            if (!string.IsNullOrEmpty(Description))
            {
                content = string.Concat(content, Description, "\n");
            }

            return content.Trim();
        }

        public string AddressLine()
        {
            if (!string.IsNullOrEmpty(City) && !string.IsNullOrEmpty(State) && !string.IsNullOrEmpty(ZipCode))
            {
                return $"{City}, {State} {ZipCode}";
            }
            else if (!string.IsNullOrEmpty(City) && !string.IsNullOrEmpty(State) && string.IsNullOrEmpty(ZipCode))
            {
                return $"{City}, {State}";
            }
            else if (!string.IsNullOrEmpty(City) && string.IsNullOrEmpty(State) && !string.IsNullOrEmpty(ZipCode))
            {
                return $"{City}, {ZipCode}";
            }
            else if (string.IsNullOrEmpty(City) && !string.IsNullOrEmpty(State) && !string.IsNullOrEmpty(ZipCode))
            {
                return $"{State} {ZipCode}";
            }
            else if (string.IsNullOrEmpty(City) && !string.IsNullOrEmpty(State) && string.IsNullOrEmpty(ZipCode))
            {
                return $"{State}";
            }
            else if (string.IsNullOrEmpty(City) && string.IsNullOrEmpty(State) && !string.IsNullOrEmpty(ZipCode))
            {
                return $"{ZipCode}";
            }
            return string.Empty;
        }

        public bool IsValid()
        {
            string businessName = string.Empty;
            if (!string.IsNullOrEmpty(this.BusinessName))
            {
                businessName = BusinessName.Trim();
            }

            if (string.IsNullOrEmpty(businessName)) return false;

            string streetAddress1 = string.Empty;
            if (!string.IsNullOrEmpty(this.StreetAddress1))
            {
                streetAddress1 = StreetAddress1.Trim();
            }

            if (string.IsNullOrEmpty(streetAddress1)) return false;

            string zip = string.Empty;
            if (!string.IsNullOrEmpty(this.ZipCode))
            {
                zip = ZipCode.Trim();
                if (zip.Length < 5) return false;
            }

            return true;
        }

        public bool IsNotProvided()
        {
            return City is null && State is null && ZipCode is null && BusinessName is null && StreetAddress1 is null && StreetAddress2 is null && Description is null;
        }

        public void SetEmpty()
		{
            City = State = ZipCode = BusinessName = StreetAddress1 = StreetAddress2 = Description = string.Empty;
		}
    }
}
