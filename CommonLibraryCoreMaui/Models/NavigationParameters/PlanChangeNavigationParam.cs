using CommonLibraryCoreMaui.Factory;

namespace CommonLibraryCoreMaui.Models.NavigationParameters
{
    public class PlanChangeNavigationParam : INavigationParam
	{
		public PlanType Type { get; set; }
		public Subscription Subscription { get; set; }
	}

	public class OrderPlanChangeNavigationParam : INavigationParam
	{
		public PlanType Type { get; set; }
		public SubscriptionChangeInfo Subscription { get; set; }
	}
}
