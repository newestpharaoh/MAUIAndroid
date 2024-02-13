using System.Collections.Generic;

namespace CommonLibraryCoreMaui.Models
{
    public class SubscriptionChangeInfo
    {
        //public List<AccountMember> FamilyMemberList { get; set; }
        //public List<AccountMember> NewFamilyMemberList { get; set; }
        //public int SubscriptionID { get; set; }
        //public int SubscriptionTypeID { get; set; }
        //public string CostDifference { get; set; }
        //public string SubscriptionCost { get; set; }
        //public string Last4ofCC { get; set; }
        //public string NextBillingDate { get; set; }
        //public string AdditonalFamilyMemberAmount { get; set; }
        //public int SubscriptionMemberLimit { get; set; }
        //public string SubscriptionPlanDescription { get; set; }
        //public string SubscriptionPlanName { get; set; }
        //public string CurrentSubscriptionPlanName { get; set; }
        //public string CurrentSubscriptionCost { get; set; }

        //public string DueDifference { get; set; }
        //public string DiscountDifference { get; set; }

        //public string SubscriptionDiscount { get; set; }
        //public string SubscriptionDue { get; set; }
        //public string SubscriptionPromoCode { get; set; }

        //public string CurrentSubscriptionDiscount { get; set; }
        //public string CurrentSubscriptionDue { get; set; }
        //public string CurrentSubscriptionPromoCode { get; set; }
        //public VInvoice CurrentVInvoice { get; set; }
        //public VInvoice NewVInvoice { get; set; }

        //public string GrantedPromotionID { get; set; }   //new!
        //public string GrantedPromotionCode { get; set; }   //new!
        //public string GrantedPromotionDescrip { get; set; }//new!

        public int SubscriptionID { get; set; }
        public int SubscriptionTypeID { get; set; }
        public string CostDifference { get; set; }
        public string DueDifference { get; set; }
        public string DiscountDifference { get; set; }
        public string SubscriptionCost { get; set; }
        public string SubscriptionDiscount { get; set; }
        public string SubscriptionDue { get; set; }
        public string SubscriptionPromoCode { get; set; }
        public string Last4ofCC { get; set; }
        public string NextBillingDate { get; set; }
        public string AdditonalFamilyMemberAmount { get; set; }
        public int SubscriptionMemberLimit { get; set; }
        public string SubscriptionPlanDescription { get; set; }
        public string SubscriptionPlanName { get; set; }
        public List<AccountMember> FamilyMemberList = new List<AccountMember>();
        public List<AccountMember> NewFamilyMemberList = new List<AccountMember>();
        public string CurrentSubscriptionPlanName { get; set; }
        public string CurrentSubscriptionCost { get; set; }
        public string CurrentSubscriptionDiscount { get; set; }
        public string CurrentSubscriptionDue { get; set; }
        public string CurrentSubscriptionPromoCode { get; set; }

        //the details
        public VInvoice CurrentVInvoice { get; set; }
        public VInvoice NewVInvoice { get; set; }
        public SubscriptionChangeInvoiceDTO TheSubscriptionChangeInvoiceDTO { get; set; }
    }
}
