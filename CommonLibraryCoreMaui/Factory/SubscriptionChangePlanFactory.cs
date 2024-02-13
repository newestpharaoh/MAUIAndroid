using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommonLibraryCoreMaui.Models;
using MvvmCross.ViewModels;

namespace CommonLibraryCoreMaui.Factory
{
    public static class SubscriptionChangePlanFactory
    {
        public static PlanType GetPlanType(SubscriptionBase selectedSubscriptionPlan, SubscriptionBase currentSubscriptionPlan)
        {
            if (selectedSubscriptionPlan is IndividualSubscription && currentSubscriptionPlan is FamilySubscription)
            {
                //from family to individual - end of cycle - block changing
                return PlanType.FamilyToIndividual;
            }
            else if (selectedSubscriptionPlan is FamilySubscription && currentSubscriptionPlan is IndividualSubscription)
            {
                //from ind to fam - end of cycle - block changing
                return PlanType.IndividualToFamily;
            }
            else if (selectedSubscriptionPlan is FamilySubscription && currentSubscriptionPlan is OneTimeSubscription)
            {
                //starts immediately
                //from one time to fam
                return PlanType.OneTimeToFamily;
            }
            else if (selectedSubscriptionPlan is Family365Subscription && currentSubscriptionPlan is Individual365Subscription)
            {
                //from ind365 to fam365 - end of cycle - block changing
                return PlanType.Individual365ToFamily365;
            }
            else if (selectedSubscriptionPlan is Family365Subscription && currentSubscriptionPlan is FamilySubscription)
            {
                //from Fam to fam365 - end of cycle - block changing
                return PlanType.FamilyToFamily365;
            }
            else if (selectedSubscriptionPlan is Individual365Subscription && currentSubscriptionPlan is FamilySubscription)
            {
                //from Fam to Undv365 - end of cycle - block changing
                return PlanType.FamilyToIndividual365;
            }
            else if (selectedSubscriptionPlan is Individual365Subscription && currentSubscriptionPlan is IndividualSubscription)
            {
                //from ind to indv365 - end of cycle - block changing
                return PlanType.IndividualToIndividual365;
            }
            else if (selectedSubscriptionPlan is Family365Subscription && currentSubscriptionPlan is IndividualSubscription)
            {
                //from ind to fam365 - end of cycle - block changing
                return PlanType.IndividualToFamily365;
            }
            else if (selectedSubscriptionPlan is Individual365Subscription && currentSubscriptionPlan is OneTimeSubscription)
            {
                //from onetime to indv365 - end of cycle - block changing
                return PlanType.OneTimeToIndividual365;
            }
            else if (selectedSubscriptionPlan is Family365Subscription && currentSubscriptionPlan is OneTimeSubscription)
            {
                //from onetime to fam365 - end of cycle - block changing
                return PlanType.OneTimeToFamily365;
            }
            else if (selectedSubscriptionPlan is IndividualSubscription && currentSubscriptionPlan.Name == "Payment Plans")
            {
                //from onetime to fam365 - end of cycle - block changing
                return PlanType.InActiveToIndividual;
            }
            else if (selectedSubscriptionPlan is FamilySubscription && currentSubscriptionPlan.Name == "Payment Plans")
            {
                //from onetime to fam365 - end of cycle - block changing
                return PlanType.InActiveToFamily;
            }
            else if (selectedSubscriptionPlan is Family365Subscription && currentSubscriptionPlan.Name == "Payment Plans")
            {
                //from onetime to fam365 - end of cycle - block changing
                return PlanType.InActiveToFamily365;
            }
            else if (selectedSubscriptionPlan is Individual365Subscription && currentSubscriptionPlan.Name == "Payment Plans")
            {
                //from onetime to fam365 - end of cycle - block changing
                return PlanType.InActiveToIndividual365;
            }
            else if (selectedSubscriptionPlan is OneTimeSubscription && currentSubscriptionPlan.Name == "Payment Plans")
            {
                //from onetime to fam365 - end of cycle - block changing
                return PlanType.InActiveToOnetime;
            }
            //starts immediately
            //from one time to ind
            return PlanType.OneTimeToIndividual;
        }

        public static SubscriptionPlan Get(PlanType type, List<UIText> lstText, string planName, string currentPlan, string subPlaNname
               , string nextBillingDate, string subscriptionCost, bool isLast4CCNotNull = true, List<UIText> lstTextP=null)
        {
            return
                new SubscriptionPlan()
                {
                    Type = type,
                    Description = GetDescriptionText(type, isLast4CCNotNull, lstText, planName, currentPlan, subPlaNname, nextBillingDate, subscriptionCost, lstTextP)
                };
        }

        //TODO: this function should be refactored
        public static string GetDescriptionText(PlanType type, bool isLast4CCNotNull, List<UIText> lstText, string planName, string currentPlan,
            string subPlaNname, string nextBillingDate, string subscriptionCost, List<UIText> lstTextP = null)
        {
            var newTotal = lstText.Find(i => i.TagName == "NewTotal").Text;// look for this        
            var changePlanOldNew =lstText.Find(i => i.TagName == "ChangePlan").Text.Replace("**","bold");        
            changePlanOldNew = string.Format(changePlanOldNew, currentPlan, subPlaNname);
            var effectiveDate = string.Format(lstText.Find(i => i.TagName == "EffectiveDate").Text, nextBillingDate).Replace("**", "bold"); 
            var paymentDate = lstText.Find(i => i.TagName == "PaymentDate").Text;
            var recurring = lstText.Find(i => i.TagName == "Recurring Monthly").Text;
            var pressContinue = lstText.Find(i => i.TagName == "PressContinue").Text;

            ///
           //var SubscriptionTotal = lstTextP.Find(i => i.TagName == "SubscriptionTotal").Text;
           ////var HSADisclaimer = this.StringFormat(lstTextP.Find(i => i.TagName === "HSADisclaimer").Text, this.appname);
           ////var HSADisclaimer = this.HSADisclaimer.replace("**", "<b>");                                                         // set bold html 
           ////var HSADisclaimer = this.HSADisclaimer.replace("**", "</b>");
           //var PaymentAssociated = lstTextP.Find(i => i.TagName == "PaymentAssociated").Text;
           // var PleaseEnterPmtInfo = lstTextP.Find(i => i.TagName == "PleaseEnterPmtInfo").Text;
           //var FirstName = lstTextP.Find(i => i.TagName == "FirstName").Text;
           //var LastName = lstTextP.Find(i => i.TagName == "LastName").Text;
           //var CreditCardNumber = lstTextP.Find(i => i.TagName == "CreditCardNumber").Text;
           //var ExpirationDate = lstTextP.Find(i => i.TagName == "ExpirationDate").Text;
           //var Month = lstTextP.Find(i => i.TagName == "Month").Text;
           //var Year = lstTextP.Find(i => i.TagName == "Year").Text;
           //var SecurityCode = lstTextP.Find(i => i.TagName == "SecurityCode").Text;
           //var PromotionalCode = lstTextP.Find(i => i.TagName == "PromotionalCode").Text;
           //var BillingAddress = lstTextP.Find(i => i.TagName == "BillingAddress").Text;
           //var Address = lstTextP.Find(i => i.TagName == "Address").Text;
           //var City = lstTextP.Find(i => i.TagName == "City").Text;
           //var State = lstTextP.Find(i => i.TagName == "State").Text;
           //var ZipCode = lstTextP.Find(i => i.TagName == "ZipCode").Text;
           //var PromoCodeInvalid = lstTextP.Find(i => i.TagName == "PromoCodeInvalid").Text;
           //var CreditCardInvalid = lstTextP.Find(i => i.TagName == "CreditCardInvalid").Text;
           //var PlanCost = lstTextP.Find(i => i.TagName == "PlanCost").Text;
           //var DiscountApplied = lstTextP.Find(i => i.TagName == "DiscountApplied").Text;
           //var Total = lstTextP.Find(i => i.TagName == "Total").Text;
           //var MonthlySubscriptionTotal = lstTextP.Find(i => i.TagName == "MonthlySubscriptionTotal").Text;
            ///
            string planText = string.Format("{0}  {1} ", newTotal, subscriptionCost);
            planText += Environment.NewLine;


            if (isLast4CCNotNull)
            {
                switch (type)
                {
                    case PlanType.FamilyToIndividual:
                    case PlanType.FamilyToIndividual365:
                        effectiveDate += " " + lstText.Find(i => i.TagName == "FamilyMembersDeactivate").Text;
                        return changePlanOldNew += Environment.NewLine + Environment.NewLine + effectiveDate;

                    // "New Subscription Total Effective {2}: {0}";
                    case PlanType.IndividualToFamily:
                    case PlanType.OneTimeToFamily:
                    case PlanType.InActiveToFamily:
                        return changePlanOldNew += Environment.NewLine + Environment.NewLine + effectiveDate;         

                    case PlanType.InActiveToIndividual:
                        return changePlanOldNew += Environment.NewLine + Environment.NewLine + effectiveDate;// cardChanged + newBalance + $"{Environment.NewLine}" + changingPlan + $"{Environment.NewLine}" + planName + $"{Environment.NewLine}" + $"{Environment.NewLine}" + newTotal.Replace("{0}:", "{2}:").Replace("{1}", "{0}");           
                                                                                                             // case PlanType.Individual365ToFamily365:
                    case PlanType.IndividualToFamily365:
                    case PlanType.FamilyToFamily365:
                    case PlanType.IndividualToIndividual365:
                        return changePlanOldNew += Environment.NewLine + Environment.NewLine + effectiveDate;
                    //changingPlan + 
                    // $"{Environment.NewLine}" +
                    // planName +
                    // $"{Environment.NewLine}" +
                    //  $"{Environment.NewLine}" +
                    // newTotal.Replace("{0}:", "{2}:").Replace("{1}", "{0}");
                    case PlanType.Individual365ToFamily365:
                    case PlanType.OneTimeToIndividual365:
                    case PlanType.OneTimeToFamily365:
                        return
                             changePlanOldNew += Environment.NewLine + Environment.NewLine + effectiveDate;// changingPlan +
                                                                                                           //$"{Environment.NewLine}" +
                                                                                                           //planName +
                                                                                                           //$"{Environment.NewLine}" +
                                                                                                           //$"{Environment.NewLine}" +
                                                                                                           //newTotal.Replace("{0}:", "{2}:").Replace("{1}/month", "{0}");
                    case PlanType.OneTimeToIndividual:
                        return
                               changePlanOldNew += Environment.NewLine + Environment.NewLine + effectiveDate;//   deactivateNotice +
                                                                                                             //$"{Environment.NewLine}" +
                                                                                                             //       $"{Environment.NewLine}" +
                                                                                                             //       cardChanged + newBalance +
                                                                                                             //       $"{Environment.NewLine}" +
                                                                                                             //       $"{Environment.NewLine}" +
                                                                                                             //       changingPlan + $"{Environment.NewLine}" +
                                                                                                             //       planName 
                                                                                                             //       + $"{Environment.NewLine}" + $"{Environment.NewLine}" +
                                                                                                             //       newTotal.Replace("{0}:", "{2}:").Replace("{1}", "{0}");
                    case PlanType.InActiveToOnetime:
                    case PlanType.InActiveToIndividual365:
                    case PlanType.InActiveToFamily365:
                        return changePlanOldNew += Environment.NewLine + Environment.NewLine + effectiveDate;//  cardChanged + $"{Environment.NewLine}" + $"{Environment.NewLine}" + changingPlan + $"{Environment.NewLine}" 
                        //+ planName 
                        //    + $"{Environment.NewLine}" + $"{Environment.NewLine}" 
                        //    + newTotal.Replace("{0}:", "{2}:").Replace("{1}", "{0}");                
                }
            }
            return string.Empty;
        }
    }

    public class SubscriptionPlan : MvxViewModel
    {
        private PlanType _type;
        public PlanType Type
        {
            get { return _type; }
            set { SetProperty(ref _type, value); }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }
        private string _cost;
        public string Cost
        {
            get { return _cost; }
            set { SetProperty(ref _cost, value); }
        }
        private string _familyMembersCost;
        public string FamilyMembersCost
        {
            get { return _familyMembersCost; }
            set { SetProperty(ref _familyMembersCost, value); }
        }
        private string _totalCost;
        public string TotalCost
        {
            get { return _totalCost; }
            set { SetProperty(ref _totalCost, value); }
        }

        public SubscriptionPlan ShallowCopy()
        {
            return (SubscriptionPlan)this.MemberwiseClone();
        }
    }

    public enum PlanType
    {
        FamilyToIndividual,
        IndividualToFamily,
        OneTimeToFamily,
        OneTimeToIndividual,
        Individual365ToFamily365,
        FamilyToFamily365,
        FamilyToIndividual365,
        IndividualToIndividual365,
        OneTimeToIndividual365,
        OneTimeToFamily365,
        IndividualToFamily365,
        InActiveToIndividual,
        InActiveToOnetime,
        InActiveToFamily,
        InActiveToIndividual365,
        InActiveToFamily365,
    }
}