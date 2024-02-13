namespace CommonLibraryCoreMaui
{
    public static class SettingsValues
    {
        /***************** Global Configurations ******************/
        // DEV
        public static string ApiURLValue = "https://caretest.normanmd.com/api/api/";
        public static string ChatServerUrlValue = "https://caretest.normanmd.com/chat/";
        public static string WebsyncServerUrl = "https://caretest.normanmd.com/chat/websync.ashx";
        public const string AppLinkingDataHost = "caretest.normanmd.com";
        public static string IcelinkServerURL = "wrtctest.normanmd.com";


#if DEBUG
        public const string NotificationHubName = "notif-hub";
        public const string ListenConnectionString ="";
#else

#endif


        
        //public static bool IsLiveSwitch = true;
        /***************** Global Constants ******************/
        public const string AppLinkingDataPathSelectPaymentPrefix = "/self-paid/select-payment-plan";
        public const string AppLinkingDataPathMakePrivatePrefix = "/patient-set-password-make-private";
        public const string AppLinkingDataPathNewPatientPrefix = "newpatient";

        public const int PollVisitStatusPeriod = 5000;
        public const int WaitingPatientPeriod = 5000;
        public const int ProviderSelectionRefreshPeriod = 15000;
        public const int IdealLogoutPeriod = 3600000;
        public const int MaxTabsOpened = 5;
        public const int PollProviderStatusPeriod = 15000;
        public const int AddOnToastPeriod = 3500;
        public const string FailedAttemptsTimer = "failed_attempts";
        public const string WaitPatientCount = "wait_patient_count";
        public const string QuickPhrase = "quick_phase";
        public const string ProviderDocument = "provider_document";
        public const string PushDeviceToken = "push_device_token";
        public const string AccessCareURL = "";
        public const string CurrentPatientFullName = "current_patient_full_name";
        public const string SaveEncounterNotes = "save_encounter_notes";
        public const string SaveDiagnosis = "save_diagnosis";
        public const string SaveVisitSummary = "save_visit_summary";

        public const string UpdateDemographic = "Update Demographics";
        public const string UpdateMedicalInfo = "Update Medical Information";
        public const string UpdateAccountAccess = "Update Account Access";


        //eCommerce
        public const bool ECommerce = true;

        // Push Notificatons
        public const string CHANNEL_ID = "emd_notification_channel";

        public const int ICD10CodesRefreshPeriod = 3600;

        public static string UpdatedAppReleaseDate
        {
            get
            {
                return "10/18/2021";
            }
        }
    }

    public static class SettingsKeys
    {
        public static string ApiURL = "ApiURL";
        public static string AppName = "AppName";
        public static string ChatServerUrl = "ChatServerUrl";
    }
}
