using System.Collections.Generic;

namespace CommonLibraryCoreMaui.Models
{
    public class AccountSubscriptionChange
    {
        public int PatientID { get; set; }
        public int NewSubscriptionOptionID { get; set; }
        public List<int> AdditionalFamilyMembersPatientIDs { get; set; }
        public string Locale { get; set; }
        public string PromoCode { get; set; }

        public AccountSubscriptionChange()
        {
            AdditionalFamilyMembersPatientIDs = new List<int>();
        }
    }
}
