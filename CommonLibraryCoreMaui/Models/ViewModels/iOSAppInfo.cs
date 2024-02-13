using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Foundation;

namespace CommonLibraryCoreMaui.Models.ViewModels
{
    public class iOSAppInfo : IAppInfo
    {
        public string AppTitle
        {
            get => GetBundleValue("CFBundleDisplayName");
        }

        //public string Version
        //{
        //    get => GetBundleValue("CFBundleVersion");
        //}

        string GetBundleValue(string key)
        {
          return"NormanMDAPP"; //TOSO:SH
            // return NSBundle.MainBundle.ObjectForInfoDictionary(key)?.ToString();
        }
    }
}
