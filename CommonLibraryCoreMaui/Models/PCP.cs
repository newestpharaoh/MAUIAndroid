using System.ComponentModel;

namespace CommonLibraryCoreMaui.Models
{
    [Description("Primary Care Provider"), DialogTitle("Primary Care Provider")]
    public class PCP : AddressBase, IPatientRegistrationMedicalInfoItem
    {
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string PracticeName { get; set; }
        public string Specialty { get; set; }

        public string Preview
        {
            get
            {
                string preview = string.Empty;
                if (!string.IsNullOrEmpty(FirstName))
                {
                    preview = $"Name: {this.FirstName} {this.LastName}";
                    if (!string.IsNullOrEmpty(this.City)) preview = string.Concat(preview, $"\nCity: {this.City}");
                    preview = string.Concat(preview, $"\nState: { this.State}");
                }
                return preview;
            }
        }

        public string PCPString => DescriptionForVisitHeader();
        public string PCPStringWithoutPhone => MedicalHistoryPCPValue();


		public new string ToString()
        {
            string content = string.Empty;
            if (!string.IsNullOrEmpty(DisplayName))
            {
                content = string.Concat(content, DisplayName, "\n");
            }
            if (!string.IsNullOrEmpty(Address1))
            {
                content = string.Concat(content, Address1, "\n");
            }
            if (!string.IsNullOrEmpty(Address2))
            {
                content = string.Concat(content, Address2, "\n");
            }

            string addressLine = AddressLine();
            if (!string.IsNullOrEmpty(addressLine))
            {
                content = string.Concat(content, addressLine, "\n");
            }

            if (!string.IsNullOrEmpty(Phone))
            {
                content = string.Concat(content, Phone, "\n");
            }
            return content.Trim();
        }


        public string MedicalHistoryPCPValue()
        {
            string content = string.Empty;
            if (!string.IsNullOrEmpty(DisplayName))
            {
                content = string.Concat(content, DisplayName, "\n");
            }
            if (!string.IsNullOrEmpty(Address1))
            {
                content = string.Concat(content, Address1, "\n");
            }
            if (!string.IsNullOrEmpty(Address2))
            {
                content = string.Concat(content, Address2, "\n");
            }

            string addressLine = AddressLine();
            if (!string.IsNullOrEmpty(addressLine))
            {
                content = string.Concat(content, addressLine, "\n");
            }
            return content.Trim();
        }

        public string DescriptionForVisitHeader()
        {
            string content = string.Empty;

            if (!string.IsNullOrEmpty(DisplayName))
            {
                content = string.Concat(content, DisplayName, "\n");
            }
          
            if (!string.IsNullOrEmpty(Address1))
            {
                content = string.Concat(content, Address1, "\n");
            }
            if (!string.IsNullOrEmpty(Address2))
            {
                content = string.Concat(content, Address2, "\n");
            }
            if (!string.IsNullOrEmpty(Address3))
            {
                content = string.Concat(content, Address3, "\n");
            }

            string addressLine = AddressLine();
            if (!string.IsNullOrEmpty(addressLine))
            {
                content = string.Concat(content, addressLine, "\n");
            }

            if (!string.IsNullOrEmpty(Phone))
            {
                content = string.Concat(content, Phone, "\n");
            }
            if (!string.IsNullOrEmpty(Fax))
            {
                content = string.Concat(content, Fax, "\n");
            }
            if (!string.IsNullOrEmpty(PracticeName))
            {
                content = string.Concat(content, PracticeName, "\n");
            }
            if (!string.IsNullOrEmpty(Specialty))
            {
                content = string.Concat(content, Specialty, "\n");
            }
            return content.Trim();
        }

        public bool IsNotProvided()
        {
            return DisplayName is null && FirstName is null && LastName is null && Address1 is null && Address2 is null && Address3 is null && Phone is null && Fax is null && PracticeName is null && Specialty is null;
        }
    }

    public class AddressBase
    {
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public string AddressLine()
        {
            if (!string.IsNullOrEmpty(City) && !string.IsNullOrEmpty(State) && !string.IsNullOrEmpty(Zip))
            {
                return $"{City}, {State} {Zip}";
            }
            else if (!string.IsNullOrEmpty(City) && !string.IsNullOrEmpty(State) && string.IsNullOrEmpty(Zip))
            {
                return $"{City}, {State}";
            }
            else if (!string.IsNullOrEmpty(City) && string.IsNullOrEmpty(State) && !string.IsNullOrEmpty(Zip))
            {
                return $"{City}, {Zip}";
            }
            else if (string.IsNullOrEmpty(City) && !string.IsNullOrEmpty(State) && !string.IsNullOrEmpty(Zip))
            {
                return $"{State} {Zip}";
            }
            else if (string.IsNullOrEmpty(City) && !string.IsNullOrEmpty(State) && string.IsNullOrEmpty(Zip))
            {
                return $"{State}";
            }
            else if (string.IsNullOrEmpty(City) && string.IsNullOrEmpty(State) && !string.IsNullOrEmpty(Zip))
            {
                return $"{Zip}";
            }
            return string.Empty;
        }

        
    }

    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct)]
    public class DialogTitle : System.Attribute
    {
        public string Name { get; set; }

        public DialogTitle(string name)
        {
            this.Name = name;
        }
    }
}
