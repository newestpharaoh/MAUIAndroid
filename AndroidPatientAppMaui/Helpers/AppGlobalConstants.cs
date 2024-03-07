using CommonLibraryCoreMaui;
using CommonLibraryCoreMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.Helpers
{
    public class AppGlobalConstants
    {
        //To Manage User Info
        public static int UserId = 0;
        public static string Token = string.Empty;
        public static string LoginEmail = string.Empty;
        public static DateTime? TokenExpirationDate;
        public static UserInfo userInfo = new UserInfo();
        public static RegistrationState RegistrationRespModel = new RegistrationState();

    }
}
