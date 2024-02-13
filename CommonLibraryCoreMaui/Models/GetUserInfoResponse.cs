using System;

namespace CommonLibraryCoreMaui.Models
{
    public class UserInfo
    {
        public int UserID { get; set; }
        public int PatientID { get; set; }
        public int? ProviderID { get; set; }
        public string Email { get; set; }
        public object Password { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsActive { get; set; }
        public bool IsPrepay { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public object DateAdded { get; set; }
        public string DOB { get; set; }
        public int PaymentID { get; set; }
        public object MemberID { get; set; }
        public int AccountID { get; set; }
        public object AccountSuffix { get; set; }
        public int LoginID { get; set; }
        public object RegistrationDate { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public bool Established { get; set; }
        public object TermsOfUseAcceptanceDate { get; set; }
        public string Avatar { get; set; }
        public int UserRole { get; set; }
        public int Age { get; set; }
        public object Specialty { get; set; }
        public object Rating { get; set; }
        public bool Availability { get; set; }
        public object Status { get; set; }
        public string Title { get; set; }
        public string Education { get; set; }
        public string Notes { get; set; }
        public string Domain { get; set; }
        public bool SecurityQuestionsComplete { get; set; }
        public string CurrentSubscriptionPlan { get; set; }
        public string CurrentSubscriptionEndDate { get; set; }
        public string NewSubscriptionPlan { get; set; }
        public string NewSubscriptionStartDate { get; set; }
        public string NewSubscriptionCost { get; set; }
        public bool IsFamilyPlan { get; set; }
        public bool CanceledForPaymentIssues { get; set; }
        public bool ShowSubscriptionChangeBanner { get; set; }
        public bool IsOnetimePlan { get; set; }
        public bool WasPrepay { get; set; }
        public bool RegistrationFailed { get; set; }
        public bool IsTermed { get; set; }
        public bool ShowSubscriptionChangeInfo()
        {
            if (!string.IsNullOrEmpty(NewSubscriptionPlan) && !string.IsNullOrEmpty(NewSubscriptionStartDate))
            {
                DateTime newSubscriptionStartDate = DateTime.Parse(NewSubscriptionStartDate);
                if (newSubscriptionStartDate > DateTime.Today)
                {
                    return true;
                }
            }

           // if (getUserInfoResponse.CurrentSubscriptionEndDate != null && getUserInfoResponse.NewSubscriptionStartDate != null) { this.planHasChanged = true; }
            return false;
        }

        public bool ShowSubscriptionCanceledInfo()
        {
            if (!string.IsNullOrEmpty(CurrentSubscriptionEndDate) && !IsOnetimePlan && string.IsNullOrEmpty(NewSubscriptionPlan))
                return true;
           
            return false;
        }

        public string GetSubscriptionCancelChangeInfo()
        {
            if (ShowSubscriptionChangeInfo())
            {
                return $"*Your subscription will change from a Family Subscription to an Individual Subscription on {NewSubscriptionStartDate}. At this time, family members will be deactivated. If you did not cancel your plan, please contact customer service at 512-421-5678";
            }
            else
            {
                return $"*Your subscription will end on {CurrentSubscriptionEndDate}. If you did not cancel your plan, please contact customer service at 512-421-5678";
            }
        }
    }

    public class UserInfoState
    {
        public int UserID { get; set; }
        public int PatientID { get; set; }
        public int? ProviderID { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int LoginID { get; set; }
        public bool Availability { get; set; }
        public string Domain { get; set; }
        public string CurrentSubscriptionEndDate { get; set; }
        public string NewSubscriptionPlan { get; set; }
        public string NewSubscriptionStartDate { get; set; }
        public bool HasPromoCode { get; set; }

        public bool IsFamilyPlan { get; set; }

        public bool ShowSubscriptionChangeInfo()
        {
            if (!string.IsNullOrEmpty(NewSubscriptionPlan) && !string.IsNullOrEmpty(NewSubscriptionStartDate))
            {
                DateTime newSubscriptionStartDate = DateTime.Parse(NewSubscriptionStartDate);
                if (newSubscriptionStartDate > DateTime.Today)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ShowSubscriptionCanceledInfo()
        {
            if (string.IsNullOrEmpty(NewSubscriptionPlan) && !string.IsNullOrEmpty(CurrentSubscriptionEndDate))
            {
                DateTime currentSubscriptionEndDate = DateTime.Parse(CurrentSubscriptionEndDate);
                if (currentSubscriptionEndDate > DateTime.Today)
                {
                    return true;
                }
            }
            return false;
        }

        public string GetSubscriptionCancelChangeInfo()
        {
            if (ShowSubscriptionChangeInfo())
            {
                return $"*Your subscription will change from a Family Subscription to an Individual Subscription on {NewSubscriptionStartDate}. At this time, family members will be deactivated. If you did not cancel your plan, please contact customer service at 512-421-5678";
            }
            else
            {
                return $"*Your subscription will end on {CurrentSubscriptionEndDate}. If you did not cancel your plan, please contact customer service at 512-421-5678";
            }
        }
    }
}