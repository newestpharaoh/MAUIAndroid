using Acr.UserDialogs;
using AndroidPatientAppMaui.ApiProviders;
using AndroidPatientAppMaui.Models;
using CommonLibraryCoreMaui;
using CommonLibraryCoreMaui.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
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

        /// <summary>
        /// To update the user profile...
        /// </summary>
        /// <param name="request"></param>
        /// <param name="success"></param>
        /// <param name="failed"></param>
        /// <returns></returns>
        public async Task<StatusResponse> UpdateProfileImgApi(UpdateProfileImgReqModel request, FileResult MediafileResult, Action<object> success, Action<object> failed)
        {
            StatusResponse result = new StatusResponse();
            try
            {
                //  var url = string.Format("{0}api/CustomerRegistration/addprofilepicture?Customerid=" + request.CustomerId, WebServiceDetails.BaseUri);
                UserDialogs.Instance.ShowLoading("Uploading Image...", MaskType.Clear);
                if (accessType == NetworkAccess.Internet)
                {
                    await Task.Run(async () =>
                    {
                        var req = request;
                        await Task.Delay(100);
                        MultipartFormDataContent multiContent = new MultipartFormDataContent();
                        multiContent.Headers.ContentType.MediaType = "multipart/form-data";
                        MemoryStream streamForimage1 = new MemoryStream();
                        byte[] byteArrayForImage1;
                        HttpContent fileStreamContent = new StreamContent(streamForimage1);
                        if (!string.IsNullOrEmpty(req.imgPath))
                        {
                            using (var streamReader = new StreamReader(req.imgPath))
                            {
                                using (var memstream = new MemoryStream())
                                {
                                    streamReader.BaseStream.CopyTo(memstream);
                                    await Task.Delay(1000);
                                    byteArrayForImage1 = memstream.ToArray();
                                    await Task.Delay(1000);
                                }
                            }
                            streamForimage1 = new MemoryStream(byteArrayForImage1);
                            fileStreamContent = new StreamContent(streamForimage1);
                            fileStreamContent.Headers.ContentDisposition = new
                            System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                            {
                                Name = "file",
                                FileName = "my_uploaded_image.png"
                            };

                            fileStreamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                            fileStreamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");

                            multiContent.Add(fileStreamContent, "file");
                        }

                        //    multiContent.Add(new StringContent(req.CustomerId), "Customerid");
                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.token);
                        var response1 = await _httpClient.PostAsync(request.uri, multiContent);
                        var rawResult = response1.Content.ReadAsStringAsync().Result;
                        var finalresult = JsonConvert.DeserializeObject<StatusResponse>(rawResult);

                        if (string.IsNullOrEmpty(finalresult.ErrorMessage))
                        {
                            if (finalresult.StatusCode == StatusCode.SuccessSeePayload)
                            {
                                success.Invoke(finalresult);
                            }
                        }
                        else
                        {
                            failed.Invoke(finalresult);
                        }
                    }).ConfigureAwait(false);
                }
                else
                {
                    UserDialogs.Instance.Loading().Hide();
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Loading().Hide();
            }
            return result;
        }
        #endregion

        #endregion
    }
}
