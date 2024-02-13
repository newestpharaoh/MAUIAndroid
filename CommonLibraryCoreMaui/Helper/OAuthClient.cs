using System;
using System.Threading.Tasks;
using CommonLibraryCoreMaui.Models;
using Newtonsoft.Json;
using RestSharp;

namespace CommonLibraryCoreMaui
{
    public static class OAuthClient
    {
        //public static DateTime TokenExpirationDate;
        public static RestClient RestClient;
        public static string Token;

        public static async Task<TokenResponse> GetTokenResponseAsync(string sApiDomainURL, string username, string pwd, string ip)
        {
            RestResponse resp = null;
            try
            {
                var restclient = new RestClient(sApiDomainURL);
                RestRequest request = new RestRequest("/token") { Method = Method.Post };
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("username", username);
                request.AddParameter("password", pwd);
                request.AddParameter("ip", ip);
                request.AddParameter("grant_type", "password");
                resp = await restclient.ExecuteTaskAsync(request).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return JsonConvert.DeserializeObject<TokenResponse>(resp.Content);
        }

        //public static RestResponse GetTokenResponse(string sApiDomainURL, string sUsername, string sPwd)
        //{
        //    RestResponse resp = null;
        //    try
        //    {
        //        var restclient = new RestClient(sApiDomainURL);
        //        RestRequest request = new RestRequest("/token") { Method = Method.Post };
        //        request.AddHeader("Accept", "application/json");
        //        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        //        request.AddParameter("username", sUsername);
        //        request.AddParameter("password", sPwd);
        //        request.AddParameter("grant_type", "password");
        //        resp = (RestResponse)restclient.Execute(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return resp;
        //}
    }
}
