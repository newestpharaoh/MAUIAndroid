using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace CommonLibraryCoreMaui.Models
{
    public class AccountMember : BindableObject
    {
        public int PatientID { get; set; }
        public string DisplayName { get; set; }
        public string DOB { get; set; }
        public string PaymentPlan { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsActive { get; set; }
        public bool IsPrivate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PlanStatus { get; set; }
      //  public bool IsExpanded { get; set; }
        private bool _IsExpanded = false;
        public bool IsExpanded
        {
            get { return _IsExpanded; }
            set
            {
                if (_IsExpanded != value)
                {
                    _IsExpanded = value;
                    OnPropertyChanged("IsExpanded");
                }
            }
        } 
        private string _ImgArrow = "rightarrow.png";
        public string ImgArrow
        {
            get { return _ImgArrow; }
            set
            {
                if (_ImgArrow != value)
                {
                    _ImgArrow = value;
                    OnPropertyChanged("ImgArrow");
                }
            }
        }
        public string GetPaymentPlanHeaderName ()
        {
            return IsActive ? PaymentPlan : "Deactivated";
        }
    }

    //the same - replace wth existing model
    public class AvailableSubscription
    {
        public int OptionID { get; set; }
        public string Name { get; set; }
        public string Cost { get; set; }
        public int MonthlyVisits { get; set; }
        public int TotalOptionMembers { get; set; }
        public string PlanDescription { get; set; }
        public bool IsActive { get; set; }
        public bool IsAddOn { get; set; }
        public bool HasAddOn { get; set; }
        public object NewPlanStartDate { get; set; }
        public object NewPlanRenewalDate { get; set; }
    }

    //to replace with existing subscription addon class
    public class AvailableSubscriptionAddOn
    {
        public int OptionID { get; set; }
        public string Name { get; set; }
        public string Cost { get; set; }
        public int MonthlyVisits { get; set; }
        public int TotalOptionMembers { get; set; }
        public string PlanDescription { get; set; }
        public bool IsActive { get; set; }
        public bool IsAddOn { get; set; }
        public bool HasAddOn { get; set; }
        public object NewPlanStartDate { get; set; }
        public object NewPlanRenewalDate { get; set; }
    }

    public class AccountSubscriptionInfo
    {
        public List<AccountMember> AccountMembers { get; set; }
        public List<SubscriptionBase> AvailableSubscriptions { get; set; }
        public List<SubscriptionBase> AvailableSubscriptionAddOns { get; set; }
        public bool CanAddFamilyMembers { get; set; }
        public bool IsPrepay { get; set; }
        public bool IsFamilyPlan { get; set; }
        public string GrantedPromotionID { get; set; }   //new!
        public string GrantedPromotionCode { get; set; }   //new!
        public string GrantedPromotionDescrip { get; set; }//new!
        public int CurrentSubscriptionTypeID { get; set; }
        public string CurrentSubscriptionPlan { get; set; }
        public string CurrentSubscriptionPlanCost { get; set; }
        public string CurrentSubscriptionPlanDue { get; set; }
  
        public int CurrentSubscriptionAddOnMembers { get; set; }
        public string CurrentSubscriptionAddOnMemberCost { get; set; }
        public string CurrentSubscriptionAddOnMemberDue { get; set; }
        public string CurrentSubscriptionTotalCost { get; set; }
        public string CurrentSubscriptionTotalDue { get; set; }
        public string CurrentSubscriptionEndDate { get; set; }
        public string CurrentSubscriptionPlanDescription { get; set; }

        public VInvoice CurrentVInvoice { get; set; }//for more detailed information
        public int NewSubscriptionTypeID { get; set; }
        public string NewSubscriptionPlan { get; set; }
        public string NewSubscriptionPlanCost { get; set; }

        public string NewSubscriptionPlanDue { get; set; }//after promotional discounts
        public int NewSubscriptionAddOnMembers { get; set; }
        public string NewSubscriptionAddOnMemberCost { get; set; }
        public string NewSubscriptionTotalCost { get; set; }
        public string NewSubscriptionStartDate { get; set; }
		AccountMember SelectedAccountMember { get; set; }
        public string NewSubscriptionPlanDescription { get; set; }
        public VInvoice NewVInvoice { get; set; }//for more detailed information
       // public CurrentVInvoice CurrentVInvoice { get; set; }
    }
}

