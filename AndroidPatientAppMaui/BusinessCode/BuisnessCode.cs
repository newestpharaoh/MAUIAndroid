using AndroidPatientAppMaui.ApiProviders;
using CommonLibraryCoreMaui;
using CommonLibraryCoreMaui.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.BusinessCode
{
    public class BuisnessCode : IBusinessCode
    {
        private readonly HttpClient _httpClient;
        IApiProvider _apiProvider;
        private readonly JsonSerializerSettings _serializerSettings;
        NetworkAccess accessType;

        public BuisnessCode(IApiProvider apiProvider)
        {
            try
            {
                //To initialize service providers...
                accessType = Connectivity.Current.NetworkAccess;
                _apiProvider = apiProvider;
                HttpClientHandler handler = new HttpClientHandler();
                //_serializerSettings = new JsonSerializerSettings
                //{
                //    DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                //    NullValueHandling = NullValueHandling.Ignore
                //};
                //_serializerSettings.Converters.Add(new StringEnumConverter());

                _httpClient = new HttpClient(handler);
                TimeSpan ts = TimeSpan.FromMilliseconds(100000);
                _httpClient.Timeout = ts;
            }
            catch (Exception ex)
            { }
        }

        #region  Apis Definations Methods


        #region Account
        public async Task<TokenResponse> CustomerLoginEmail(string email, string password, Action<object> success, Action<object> failed)
        {
            TokenResponse resmodel = new TokenResponse();
            try
            {
                var url = string.Format("{0}api/CustomerRegistration/customerloginemail/" + email + "/" + password + "?fcmtoken=", SettingsValues.ApiURLValue);
                var result = _apiProvider.Get<TokenResponse>(url, null);
                TokenResponse objres = null;
                objres = JsonConvert.DeserializeObject<TokenResponse>(result.RawResult);
                //if (result.Result.isValidateSuccess == true)
                //{
                //    objres = JsonConvert.DeserializeObject<TokenResponse>(result.RawResult);
                //    success.Invoke(objres);
                //}
                //else
                //{

                //    failed.Invoke(objres);
                //}
            }
            catch (Exception exception)
            {
                // CustomControls.ToastControl.ShowErrorToast("Something went wrong!  Please try again.");
            }
            return resmodel;
        }

        public Task<TokenResponse> CustomerLogin(string email, string password, Action<object> success, Action<object> failed)
        {
            throw new NotImplementedException();
        }

        #endregion



        #endregion
    }
}
