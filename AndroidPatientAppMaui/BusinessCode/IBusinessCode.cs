using CommonLibraryCoreMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.BusinessCode
{
    public interface IBusinessCode
    {
        #region Apis Declarations  

        #region Account
        Task<TokenResponse> CustomerLogin(string email, string password, Action<object> success, Action<object> failed);

        #endregion

        #endregion
    }
}
