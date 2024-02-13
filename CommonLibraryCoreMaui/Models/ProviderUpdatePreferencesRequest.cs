using System.Collections.Generic;

namespace CommonLibraryCoreMaui.Models
{
    public class ProviderPreferences : ResponseBase
    {
        public List<SecurityQuestion> SecurityQuestionAnswer { get; set; }
        public int providerID { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public string NotificationPreference { get; set; }
        public int RepeatAlerts { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
