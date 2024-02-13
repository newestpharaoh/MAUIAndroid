using System;

namespace CommonLibraryCoreMaui
{
    public static class CommonAuthSession
    {
        public static string FirstName = string.Empty;
        public static string LastName = string.Empty;
        public static string Token = string.Empty;
        //public static string UserName = string.Empty;
        //public static string uPwsd = string.Empty;
        public static bool IsAutheticated;
        private static DateTime? dtTokenExpirationDate;

        public static void ClearSession()
        {
            dtTokenExpirationDate = null;
            Token = string.Empty;
            IsAutheticated = false;
        }

        public static bool IsSessionExpired
        {
            get
            {
                if (dtTokenExpirationDate != null)
                {
                    return dtTokenExpirationDate < DateTime.Now ? true : false;
                }
                return true;
            }
        }

        public static bool IsLoggedIn
        {
            get
            {
                return !string.IsNullOrEmpty(Token) && !IsSessionExpired && IsAutheticated;
            }
        }

        public static void SetTokenExpirationDate(DateTime? dtExp)
        {
            try
            {
                dtTokenExpirationDate = dtExp;
            }
            catch { }
        }
    }

    [SharedPreferencesKey("globals")]
    public sealed class GlobalState : ISaveState
    {
        public string CustomerServicePhone { get; set; }
        public string PatientTitle { get; set; }
        public string ProviderName { get; set; }
        public Models.UserInfo UserInfo { get; set; } = new Models.UserInfo();
    }

    public interface ISaveState { };

    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct)]
    public class SharedPreferencesKey : System.Attribute
    {
        public string Key { get; set; }

        public SharedPreferencesKey(string key)
        {
            this.Key = key;
        }
    }

    [SharedPreferencesKey("session")]
    public class SessionState : ISaveState
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }

        public string AppBrandname { get; set; }
        public bool IsAutheticated { get; set; }
        public System.DateTime? TokenExpirationDate { get; set; }

        public void ClearSession()
        {
            TokenExpirationDate = null;
            Token = string.Empty;
            IsAutheticated = false;
        }

        public bool IsSessionExpired
        {
            get
            {
                if (TokenExpirationDate != null)
                {
                    return TokenExpirationDate < System.DateTime.Now ? true : false;
                }
                return true;
            }
        }

        public bool IsLoggedIn
        {
            get
            {
                return !string.IsNullOrEmpty(Token) && !IsSessionExpired && IsAutheticated;
            }
        }
    }

    public sealed class RegistrationState
    {
        public CommonLibraryCoreMaui.Models.StatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public string PatientID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string MPI { get; set; }
        public string Domain { get; set; }
        public bool IsPrimary { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public System.DateTime? DateAdded { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
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
        public string PromoCode { get; set; }
        public string ResignupCode { get; set; }
        public int SubscriptionOptionID { get; set; }
        public System.Collections.Generic.List<CommonLibraryCoreMaui.Models.SecurityQuestionAnswer> Answers { get; set; } = new System.Collections.Generic.List<CommonLibraryCoreMaui.Models.SecurityQuestionAnswer>();
        public string PreferredName { get; set; }
        public string Language { get; set; }
        public bool? Established { get; set; }
        public bool IsSelfPay { get; set; }
        public CommonLibraryCoreMaui.Models.Subscription Subscription { get; set; }
        public bool IsInProgress { get; set; } = true;
    }
}
