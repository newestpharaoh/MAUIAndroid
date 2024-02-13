namespace CommonLibraryCoreMaui.Models
{
    public class AccountAddFamilyMemberInfo
    {
        public bool CanAddFamilyMember { get; set; }
        public bool IncludedInPlan { get; set; }
        public string AddOnCost { get; set; }
        public string ProratedAddOnCost { get; set; }
        public int FreeFamilyMembersRemaining { get; set; }
        public string Last4ofCC { get; set; }
        public string NextBillingDate { get; set; }
        public string SingleAddOnCost { get; set; }
        public bool NoActiveCardOnFile { get; set; }
    }
}
