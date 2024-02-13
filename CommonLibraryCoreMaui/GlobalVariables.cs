using CommonLibraryCoreMaui.Models;
using System;
using System.Threading.Tasks;

namespace CommonLibraryCoreMaui
{
    public sealed class Globals
    {
        private string customerServicePhone = string.Empty;
        public string PatientTitle { get; set; }
        public string ProviderName { get; set; }
        public bool IsCapsuleVisible { get; set; }
        public bool IsCurative { get; set; }
        public bool HasSeenHomeCurativeDialog { get; set; }
        public bool HasSeenCurativeDialog { get; set; }
        public bool HadPeviousVisit { get; set; }
        public bool IsTermed { get; set; }

        private string appBrandName = string.Empty;

        public Task<string> AppBrandName
        {
            get
            {
                return GetAppName();
            }
        }

        public Task<string> CustomerServicePhone
        {
            get
            {
                return GetCustomerServicePhone();
            }
        }

        public bool IsProvider
        {
            get
            {
                var id = Instance.UserInfo?.ProviderID ?? 0;
                return id != 0;
            }
        }

        public UserInfo UserInfo { get; set; }
        public bool? ResetBadge { get; set; }

        private static readonly Lazy<Globals> lazy = new Lazy<Globals>(() => new Globals());

        public static Globals Instance { get { return lazy.Value; } }

        private Globals()
        {
        }


        private async Task<string> GetCustomerServicePhone()
        {
            if (string.IsNullOrEmpty(customerServicePhone))
            {
                SiteSettings settings = await DataUtility.GetSiteSettingsAsync(SettingsValues.ApiURLValue).ConfigureAwait(false);
                if (settings != null)
                {
                    customerServicePhone = settings.CustomerSupportPhone;
                }
            }
            return customerServicePhone;
        }

        public async Task<string> GetAppName()
        {
            if (string.IsNullOrEmpty(appBrandName))
            {
                SiteSettings settings = await DataUtility.GetSiteSettingsAsync(SettingsValues.ApiURLValue).ConfigureAwait(false);
                if (settings != null)
                   appBrandName = settings.Brand;                  
            }
            return appBrandName;
        }
        public async Task<UITopic> GetUTText(string strTopicname, string locale="en")
        {
            UITopic pageText =null;
            if (string.IsNullOrEmpty(appBrandName))
            {
                pageText = await DataUtility.GetUITopicListAsync(SettingsValues.ApiURLValue, strTopicname, locale).ConfigureAwait(false);
               
            }
            return pageText;
        }
    }

}
