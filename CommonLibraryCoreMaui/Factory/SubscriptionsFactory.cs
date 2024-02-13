using CommonLibraryCoreMaui.Models;

namespace CommonLibraryCoreMaui
{
    public static class SubscriptionsFactory
    {
        /// <summary>
        /// Decides which type of subscription to instantiate.
        /// </summary>
        public static Subscription Get(int id)
        {
            switch (id)
            {
                case 3:
                    return new IndividualSubscription();
                case 5:
                    return new FamilySubscription();
                case 6:
                    return new AdditionalFamilyMemberSubscription();
                case 7:
                    return new Individual365Subscription();
                case 8:
                    return new Family365Subscription();
                default:
                    return new OneTimeSubscription();
            }
        }
        //Android Usage
        public static System.Type GetType(int id)
        {
            switch (id)
            {
                case 3:
                    return typeof(IndividualSubscription);
                case 5:
                    return typeof(FamilySubscription);
                case 6:
                    return typeof(AdditionalFamilyMemberSubscription);
                case 7:
                    return typeof(Individual365Subscription);
                case 8:
                    return typeof(Family365Subscription);
                default:
                    return typeof(OneTimeSubscription);
            }
        }

        public static Subscription Get(string name, bool isFamily)
        {           
                switch (name.ToLower().Replace(" ", ""))
                {
                case "individualsubscription":
                    return new IndividualSubscription() { OptionID = 3 };
                case "familysubscription":
                    return new FamilySubscription() { OptionID = 5 };
                case "additionalfamilymembersubscription":
                    return new AdditionalFamilyMemberSubscription() { OptionID = 6 };
                case "individual365plan":
                    return new Individual365Subscription() { OptionID = 7 };
                case "family365plan":
                    return new Family365Subscription() { OptionID = 8 };
                //case "n/a":
                //    return null;
                default:
                    return new OneTimeSubscription();

            }
        }

		public static bool IsFamilyToIndividualPlan(string oldPlan, string newPlan)
		{
            var oldPlanWithoutSpace = oldPlan?.ToLower().Replace(" ", "");
            var newPlanWithoutSpace = newPlan?.ToLower().Replace(" ", "");

            return oldPlanWithoutSpace == "familysubscription" && newPlanWithoutSpace == "individualsubscription";
		}
        public static bool IsFamilyToIndividual365Plan(string oldPlan, string newPlan)
        {
            var oldPlanWithoutSpace = oldPlan?.ToLower().Replace(" ", "");
            var newPlanWithoutSpace = newPlan?.ToLower().Replace(" ", "");

            return oldPlanWithoutSpace == "familysubscription" && newPlanWithoutSpace == "individual365plan";
        }

        //Android Usage
        public static Subscription Get(AccountSubscriptionInfo info)
        {
            switch (info.CurrentSubscriptionPlan.ToLower().Replace(" ", ""))
            {
                case "individualsubscription":
                    return new IndividualSubscription() {
                        OptionID = 3,
                        SubscriptionTotalCost = info.CurrentSubscriptionTotalCost,
                        SubscriptionPlanName = info.CurrentSubscriptionPlan,
                        SubscriptionSubTotalCost = info.CurrentSubscriptionPlanCost
                    };              
                case "familysubscription":
                    return new FamilySubscription()
                    {
                        OptionID = 5,
                        SubscriptionTotalCost = info.CurrentSubscriptionTotalCost,
                        SubscriptionAddOnMembers = info.CurrentSubscriptionAddOnMembers,
                        SubscriptionAddOnMemberCost = info.CurrentSubscriptionAddOnMemberCost,
                        SubscriptionPlanName = info.CurrentSubscriptionPlan,
                        SubscriptionSubTotalCost = info.CurrentSubscriptionPlanCost

                    };
                case "additionalfamilymembersubscription":
                    return new AdditionalFamilyMemberSubscription() { OptionID = 6 };
                case "individual365plan":
                    return new Individual365Subscription()
                    {
                        OptionID = 7,
                        SubscriptionTotalCost = info.CurrentSubscriptionTotalCost,
                        SubscriptionPlanName = info.CurrentSubscriptionPlan,
                        SubscriptionSubTotalCost = info.CurrentSubscriptionPlanCost
                    };
                case "family365plan":
                    return new Family365Subscription()
                    {
                        OptionID = 8,
                        SubscriptionTotalCost = info.CurrentSubscriptionTotalCost,
                        SubscriptionAddOnMembers = info.CurrentSubscriptionAddOnMembers,
                        SubscriptionAddOnMemberCost = info.CurrentSubscriptionAddOnMemberCost,
                        SubscriptionPlanName = info.CurrentSubscriptionPlan,
                        SubscriptionSubTotalCost = info.CurrentSubscriptionPlanCost
                    };
                case "n/a":
                    return null; //inactive!
                default:
                    return new OneTimeSubscription()
                    {
                        SubscriptionTotalCost = info.CurrentSubscriptionTotalCost,
                        SubscriptionPlanName = info.CurrentSubscriptionPlan,
                        SubscriptionSubTotalCost = info.CurrentSubscriptionPlanCost,
                        PlanDescription = info.CurrentSubscriptionPlanDescription
                    };
            }
        }
        //Android Usage
        public static Subscription Get(SubscriptionChangeInfo info)
        {
            switch (info.SubscriptionID)
            {
                case 3:
                    return new IndividualSubscription() { OptionID = info.SubscriptionID, Name = "Individual Subscription" };
                case 5:
                    return new FamilySubscription() { OptionID = info.SubscriptionID, Name = "Family Subscription" };
                case 6:
                    return new AdditionalFamilyMemberSubscription();
                case 7:
                    return new FamilySubscription() { OptionID = info.SubscriptionID, Name = "Individual 365 Plan" };
                case 8:
                    return new FamilySubscription() { OptionID = info.SubscriptionID, Name = "Family 365 Plan" };
                default:
                    return new OneTimeSubscription() { OptionID = info.SubscriptionID, Name = "72-Hour Access" };
            }
        }
    }

    public enum SubscriptionPlanType
    {
        Individual365 = 7,
        OneTime72 = 2,
        Individial = 3,

        Family365 = 8,
        Family = 5
    }
}
