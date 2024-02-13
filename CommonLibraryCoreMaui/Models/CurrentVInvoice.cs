using System.Collections.Generic;

namespace CommonLibraryCoreMaui.Models
{
    public class CurrentVInvoice
    {
        //this probably should be here. this is for ISubscriptionOption in Desktop, which in Mobile is SubscriptionBase
        //public int OptionID { get; set; }
        //public string Name { get; set; }
        //public string UpdatedName { get; set; }
        //public string Cost { get; set; }
        //public int MonthlyVisits { get; set; }
        //public int SubscriptionTypeID { get; set; }
        //public int TotalOptionMembers { get; set; }
        //public string PlanDescription { get; set; }
        //public int IsActive { get; set; }
        //public int IsAddOn { get; set; }
        //public int HasAddOn { get; set; }
        //public string NewPlanStartDate { get; set; }
        //public string NewPlanRenewalDate { get; set; }

        //Should be
        public int? PromoID { get; set; }
        public string PromoCode { get; set; }
        public CurrentVInvoiceMain AddOn { get; set; } //also works as bool?

        public CurrentVInvoiceMain Main { get; set; } //Main

        public int IncludedCount { get; set; }
        public int RequestedCount { get; set; }
        public bool ProratedValuesForInital { get; set; }
        public int RequestCount_Included { get; set; }
        public int RequestCount_AddOn { get; set; }
        public decimal TotalCostNominal { get; set; }                    // nominal is cost without proration
        public decimal TotalDiscountNominal { get; set; }                     // initial is with proration
        public decimal TotalDueNominal { get; set; }
        public decimal TotalCostInitial { get; set; }
        public decimal TotalDiscountInitial { get; set; }
        public decimal TotalDueInitial { get; set; }
        public decimal AddOnCostNominal { get; set; }
        public decimal AddOnDiscountNominal { get; set; }
        public decimal AddOnDueNominal { get; set; }
        public decimal AddOnCostInitial { get; set; }
        public decimal AddOnDiscountInitial { get; set; }
        public decimal AddOnDueInitial { get; set; }
    }
}

   
