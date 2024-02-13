namespace CommonLibraryCoreMaui.Models
{
    public class SelfPayBillingInfo
    {
        public string CardFirstName { get; set; }
        public string CardLastName { get; set; }
        public string CardNumber { get; set; }
        public string CardExpirationMonth { get; set; }
        public string CardExpirationYear { get; set; }
        public string CardSecurityCode { get; set; }
        public string BillingAddress { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZip { get; set; }
        public string SubscriptionOptionID { get; set; }
        public string PromoCode { get; set; }
        public string ResignupCode { get; set; }
    }
}
