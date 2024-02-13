using System;

namespace CommonLibraryCoreMaui.Models
{
    public class SubscriptionBase
    {
        public int OptionID { get; set; }
      //  public int SubscriptionPlanTypeId { get; set; }
        public string Name { get; set; }
        public string Cost { get; set; }
        public int MonthlyVisits { get; set; }
        public int TotalOptionMembers { get; set; }
        public string PlanDescription { get; set; }
        public bool IsActive { get; set; }
        public bool IsAddOn { get; set; }
        public bool HasAddOn { get; set; }
        public DateTime? NewPlanStartDate { get; set; }
        public DateTime? NewPlanRenewalDate { get; set; }
        public bool IsFamilyPlan { get; set; }
        public int GrantedPromotionID { get; set; }
        public string GrantedPromotionCode { get; set; }
        public string GrantedPromotionDescrip { get; set; }
        public int CurrentSubscriptionTypeID { get; set; }
    }

    public abstract class Subscription : SubscriptionBase
    {
        public override string ToString()
        {
            return this.Name;
        }

        public abstract decimal GetTotalPrice();
        public abstract string GetCost(string suffix);
    }

    public class FamilySubscription : Subscription
    {
        public AdditionalFamilyMemberSubscription AddOn { get; set; }
        public string SubscriptionAddOnMemberCost { get; set; }
        public int SubscriptionAddOnMembers { get; set; }
        public string SubscriptionTotalCost { get; set; }
        public string SubscriptionPlanName { get; set; }
        public string SubscriptionSubTotalCost { get; set; }

        public override string GetCost(string suffix)
        {
            return $"${GetTotalPrice().ToString("0.##", System.Globalization.CultureInfo.InvariantCulture)} {suffix}";
        }

        public override decimal GetTotalPrice()
        {
            decimal cost = decimal.Parse(this.Cost.Replace("$", ""), System.Globalization.CultureInfo.InvariantCulture);
            return cost + AddOn.GetTotalPrice();
        }
    }

    public class Family365Subscription : Subscription
    {
        public AdditionalFamilyMemberSubscription AddOn { get; set; }
        public string SubscriptionAddOnMemberCost { get; set; }
        public int SubscriptionAddOnMembers { get; set; }
        public string SubscriptionTotalCost { get; set; }
        public string SubscriptionPlanName { get; set; }
        public string SubscriptionSubTotalCost { get; set; }

        public override string GetCost(string suffix)
        {
            return $"${GetTotalPrice().ToString("0.##", System.Globalization.CultureInfo.InvariantCulture)} {suffix}";
        }

        public override decimal GetTotalPrice()
        {
            decimal cost = decimal.Parse(this.Cost.Replace("$", ""), System.Globalization.CultureInfo.InvariantCulture);
            return cost + AddOn.GetTotalPrice();
        }
    }

    public class OneTimeSubscription : Subscription
    {
        public string SubscriptionPlanName { get; set; }
        public string SubscriptionSubTotalCost { get; set; }
        public string SubscriptionTotalCost { get; set; }

        public override string GetCost(string suffix)
        {
            return $"${GetTotalPrice().ToString("0.##", System.Globalization.CultureInfo.InvariantCulture)} {suffix}";
        }

        public override decimal GetTotalPrice()
        {
            return decimal.Parse(this.Cost.Replace("$", ""), System.Globalization.CultureInfo.InvariantCulture);
        }
    }

    public class IndividualSubscription : Subscription
    {
        public string SubscriptionPlanName { get; set; }
        public string SubscriptionSubTotalCost { get; set; }
        public string SubscriptionTotalCost { get; set; }

        public override string GetCost(string suffix)
        {
            return $"${GetTotalPrice().ToString("0.##", System.Globalization.CultureInfo.InvariantCulture)} {suffix}";
        }

        public override decimal GetTotalPrice()
        {
            return decimal.Parse(this.Cost.Replace("$", ""), System.Globalization.CultureInfo.InvariantCulture);
        }
    }

    public class Individual365Subscription : Subscription
    {
        public string SubscriptionPlanName { get; set; }
        public string SubscriptionSubTotalCost { get; set; }
        public string SubscriptionTotalCost { get; set; }

        public override string GetCost(string suffix)
        {
            return $"${GetTotalPrice().ToString("0.##", System.Globalization.CultureInfo.InvariantCulture)} {suffix}";
        }

        public override decimal GetTotalPrice()
        {
            return decimal.Parse(this.Cost.Replace("$", ""), System.Globalization.CultureInfo.InvariantCulture);
        }
    }

    public class AdditionalFamilyMemberSubscription : Subscription
    {
        public int AdditionalFamilyMembers { get; set; }

        public override string GetCost(string suffix)
        {
            return "";
        }

        public override decimal GetTotalPrice()
        {
            return decimal.Parse(this.Cost.Replace("$", ""), System.Globalization.CultureInfo.InvariantCulture) * AdditionalFamilyMembers;
        }
    }
}
