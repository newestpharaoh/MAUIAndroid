using System.Collections.Generic;

namespace CommonLibraryCoreMaui.Models
{
    public class CurrentVInvoiceMain
    {
       public int? SubscriptionOptionPromoID { get; set; }
        public int SubscriptionOptionID { get; set; }
        public int SubscriptionTypeID { get; set; }
        public string SubscriptionOptionNameENG { get; set; }
        public string SubscriptionOptionNameESP { get; set; }
        public string SubscriptionOptionDescripENG { get; set; }
        public string SubscriptionOptionDescripESP { get; set; }
        public decimal CostNominal { get; set; }         // nominal is cost without proration
        public decimal CostInitial { get; set; }         // initial is with proration
        public float DiscountRate { get; set; }
        public decimal DiscountNominal { get; set; }
        public decimal DiscountInitial { get; set; }
        public decimal DueNominal { get; set; }
        public decimal DueInitial { get; set; }
        public string DiscountNominalDescrip { get; set; }
        public string DiscountInitialDescrip { get; set; }
    }
	
}
