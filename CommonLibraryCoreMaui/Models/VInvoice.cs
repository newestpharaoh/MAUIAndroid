using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraryCoreMaui.Models
{
    public class VInvoice
    {
        //null if none specified in request or requested promo doesn't exist. 
        //Still populated is Promo is InActive or outside of valid date range.
        public int? PromoID { get; set; }
        public string PromoDescription { get; set; }
        public string PromoCode { get; set; }

        //null if none specified in request or requested promo doesn't exist.  
        //True if specified and applies.  
        //False if specified and valid but Promo is InActive or outside of valid date range.
        public bool? PromoApplies { get; set; }

        public bool OneTimeCharge { get; set; }
        public int? ExpirationPeriod { get; set; }

        public VInvoiceItem Main { get; set; }
        public VInvoiceItem AddOn { get; set; }     //can be null if not applicable for givin SubscriptionOption.  Still populated if request specifies a plan member count below the qty included with the SubscriptionOption
        public int IncludedCount { get; set; }  //inluding primary subscriber... so miniumum value is 1
        public int RequestedCount { get; set; }     //echo from request

        //Convenience properties.  All computable from main fields.
        public bool ProratedValuesForInital { get; set; }// TotalCostNominal == TotalCostInitial		//will be false for One-time plans, and all plans when dtAsOf corresponds to beginning of period (Sunday or 1st of month, depending on config settings)
        public int RequestCount_Included { get; set; }// Min(RequestedCount				, IncludedCount	)
        public int RequestCount_AddOn { get; set; }// Max(RequestedCount - IncludedCount, 0				)

        public decimal TotalCostNominal { get; set; }// Ex: 100.00  			// Main.TotalCostNominal 		+ RequestCount_AddOn * AddOn.TotalCostNominal
        public decimal TotalDiscountNominal { get; set; }// Ex: 33.33, 100.00		// Main.TotalDiscountNominal 	+ RequestCount_AddOn * AddOn.TotalDiscountNominal	, all 0 if no promo applied 
        public decimal TotalDueNominal { get; set; }// Ex: 66.67, 0.00		// Main.TotalDueNominal			+ RequestCount_AddOn * AddOn.TotalDueNominal		, same as TotalCostNominal if not promo applied
        public decimal TotalCostInitial { get; set; }// Ex: 100.00  			// Main.TotalCostInitial 		+ RequestCount_AddOn * AddOn.TotalCostInitial
        public decimal TotalDiscountInitial { get; set; }// Ex: 33.33, 100.00		// Main.TotalDiscountInitial 	+ RequestCount_AddOn * AddOn.TotalDiscountInitial	, all 0 if no promo applied 
        public decimal TotalDueInitial { get; set; }// Ex: 66.67, 0.00		// Main.TotalDueInitial			+ RequestCount_AddOn * AddOn.TotalDueInitial		, same as TotalCostInitial if not promo applied

        public decimal AddOnCostNominal { get; set; }// Ex: 100.00  			//								  RequestCount_AddOn * AddOn.TotalCostNominal
        public decimal AddOnDiscountNominal { get; set; }// Ex: 33.33, 100.00		//								  RequestCount_AddOn * AddOn.TotalDiscountNominal	, all 0 if no promo applied 
        public decimal AddOnDueNominal { get; set; }// Ex: 66.67, 0.00		//								  RequestCount_AddOn * AddOn.TotalDueNominal		, same as TotalCostNominal if not promo applied
        public decimal AddOnCostInitial { get; set; }// Ex: 100.00  			//								  RequestCount_AddOn * AddOn.TotalCostInitial
        public decimal AddOnDiscountInitial { get; set; }// Ex: 33.33, 100.00		//								  RequestCount_AddOn * AddOn.TotalDiscountInitial	, all 0 if no promo applied 
        public decimal AddOnDueInitial { get; set; }// Ex: 66.67, 0.00		//								  RequestCount_AddOn * AddOn.TotalDueInitial		, same as TotalCostInitial if not promo applied
    }

    public class VInvoiceItem
    {
        public int? SubscriptionOptionPromoID { get; set; }
        public int SubscriptionOptionID { get; set; }
        public int SubscriptionTypeID { get; set; }
        public string SubscriptionOptionNameENG { get; set; }
        public string SubscriptionOptionNameESP { get; set; }
        public string SubscriptionOptionDescripENG { get; set; }
        public string SubscriptionOptionDescripESP { get; set; }
        public decimal CostNominal { get; set; }//Ex: 100.00 
        public decimal CostInitial { get; set; }//Ex: 100.00			// pro-rated for some plans
        public float DiscountRate { get; set; }//Ex: 0.333, 1

        //Convenience properties.  
        //All computable from main fields.
        public decimal DiscountNominal { get; set; }//Ex: 33.33, 100.00		// CostNominal * DiscountRate
        public decimal DiscountInitial { get; set; }//Ex: 33.33, 100.00		// CostInitial * DiscountRate
        public decimal DueNominal { get; set; }//Ex: 66.67, 0.00		// CostNominal - DiscountNominal
        public decimal DueInitial { get; set; }//Ex: 66.67, 0.00		// CostInitial - DiscountInitial

        //Convenience properties.  
        //Friendly presentation of Discount as either a percentage or an absolute amount.
        public string DiscountNominalDescrip { get; set; }//Ex: "33%", "$33.00", "100%", "$100.00"
        public string DiscountInitialDescrip { get; set; }//Ex: "33%", "$33.00", "100%", "$100.00"
    } 

    public class SubscriptionChangeInvoiceDTO
    {      
        public string ARBPeriod { get; set; }
        public int DayOfARBPeriodCharged { get; set; }
        public string DateOfNextARBCharged { get; set; }
        public int OngoingARBCharge { get; set; }
        public int OneTimeCharge { get; set; }                     
    }
}
