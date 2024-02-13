using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CommonLibraryCoreMaui.Models;
using Newtonsoft.Json;

//using FM.LiveSwitch;
using ObjCRuntime;
using RestSharp;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CommonLibraryCoreMaui
{
    public static class ApiUtility
    {
        public static bool CancelRequests = false;
        public static bool RaiseProgressEvents = true;
        public static event EventHandler UnauthorizedCallEvent = delegate { };
        public static event EventHandler TooManyFailedAttemptsEvent = delegate { };
        public static event EventHandler SomethingCompleted = delegate { };
        public static event EventHandler SomethingStarted = delegate { };

        public static async Task<T> SendApiRequestAsync<T>(string sApiDomainURL, string sApiPartialURL, string sToken, NameValueCollection httpReqParamValues, RestSharp.Method enumMethod)
        {
            return await SendApiRequestAsyncCommon<T>(enumMethod, sApiDomainURL, sApiPartialURL, sToken, httpReqParamValues, false);
        }

        private static async Task<T> SendApiRequestAsyncCommon<T>(RestSharp.Method method, string sApiDomainURL, string sApiPartialURL, string sToken, object body, bool contentTypeJson, string username = null, string password = null, string ip = null)
        {
            string errorMessage = string.Empty;
            RestResponse resp = null;
            try
            {
                if (RaiseProgressEvents) SomethingStarted(null, EventArgs.Empty);
                var restclient = new RestClient(sApiDomainURL);
                RestRequest request = new RestRequest(sApiPartialURL) { Method = method };

                request.RequestFormat = DataFormat.Json;
                request.Timeout = 30000;
                var options = new JsonSerializerOptions()
                {
                    NumberHandling = JsonNumberHandling.AllowReadingFromString |   JsonNumberHandling.WriteAsString,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                     WriteIndented = true
                };
                options.Converters.Add(new JsonStringEnumConverter());
                if (contentTypeJson)
                {
                    request.AddHeader("Content-Type", "application/json");
                }
                else
                {
                    request.AddHeader("Accept", "application/json");
                    //  request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    request.AddHeader("Content-Type", "application/json");
                }

                if (!string.IsNullOrEmpty(sToken))
                {
                    request.AddParameter("Authorization", string.Format("Bearer {0}", sToken), ParameterType.HttpHeader);
                }

                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(ip))
                {
                    request.AddParameter("username", username);
                    request.AddParameter("password", password);
                    request.AddParameter("ip", ip);
                    request.AddParameter("grant_type", "password");
                }

                if (body != null)
                {
                    if (body is NameValueCollection)
                    {
                        var dictionary = new Dictionary<string, string>();
                        foreach (string key in ((NameValueCollection)body).Keys)
                        {
                            string[] values = ((NameValueCollection)body).GetValues(key);
                            if (values.Length == 1)
                            {
                                dictionary.Add(key, values[0]);
                            }
                        }
                        request.AddJsonBody(dictionary);
                    }
                    else
                    {
                        request.AddJsonBody(body);
                    }
                }

                resp = await restclient.ExecuteTaskAsync(request).ConfigureAwait(false);

                if (resp != null)
                {
                    if (resp.StatusCode == HttpStatusCode.OK)
                    {
                        if (string.IsNullOrEmpty(resp.Content))
                        {
                            errorMessage = "No response data from server.";
                        }
                    }
                    else if (resp.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        UnauthorizedCallEvent(null, EventArgs.Empty);
                    }
                    else if (resp.StatusCode == HttpStatusCode.BadRequest && typeof(T) == typeof(TokenResponse))
                    {
                        //login call
                        errorMessage = string.Empty;
                    }
                    else
                    {
                        errorMessage = $"Could not retrieve data from server.{(string.IsNullOrEmpty(resp.StatusDescription) ? "" : $" {resp.StatusDescription}")}";
                    }
                }
                else  { errorMessage = "Not available. No response from server."; }

                if (string.IsNullOrEmpty(errorMessage))
                {
               //    var responseBody = await resp.Content.Rea();
                  TokenResponse tokenResponse;
                    if (typeof(T) == typeof(TokenResponse))
                    {
                        // JObject token = JObject.Parse(resp.Content);
                        // get JSON result objects into a list
                        // IList<JToken> results = token.Children().ToList();

                        //foreach (JToken result in results)
                        //{
                        //    // JToken.ToObject is a helper method that uses JsonSerializer internally
                        //    TokenResponse tResponse = result.ToObject<TokenResponse>();
                        //    //tResponse.Add(tResponse);
                        //}


                        //   T tokenResponse = JsonConvert.DeserializeObject<T>(resp.Content);
                        //var serializer = new JsonSerializer();
                        //JsonReader sr = new JsonTextReader(new StringReader(resp.Content));
                      
                        tokenResponse = JsonSerializer.Deserialize<TokenResponse>(resp.Content,options);
                        var s = tokenResponse as TokenResponse;
                        if (!string.IsNullOrEmpty(s.error))
                        {
                            if (s.error.ToLowerInvariant().Equals("too many failed logins"))
                            {
                                TooManyFailedAttemptsEvent(null, EventArgs.Empty);
                            }
                        }
                    }

                    if (typeof(T) == typeof(byte[]))
                    {
                        return (T)Convert.ChangeType(resp.RawBytes, typeof(T));
                    }

                    //T ret = JsonConvert.DeserializeObject<T>(resp.Content, new JsonSerializerSettings()
                    //{
                    //    TypeNameHandling = TypeNameHandling.Auto
                    //});
                    var opt = new JsonSerializerOptions
                    {
                        NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString,
                        WriteIndented = true,
                        PropertyNameCaseInsensitive = true,
                        IgnoreNullValues = true
                   
                    };
                     opt.Converters.Add(new JsonStringEnumConverter());

                    T ret = JsonSerializer.Deserialize<T>(resp.Content,opt);

                    StatusResponse check = null;
                    try
                    {
                        check = ret as StatusResponse;
                    }
                    catch { }

                    if (check != null)
                    {
                        if (check.StatusCode == StatusCode.Lockout)
                        {
                            double minutes;
                            string result = System.Text.RegularExpressions.Regex.Replace(check.Payload, "minutes", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                            if (!double.TryParse(result, out minutes))
                            {
                                minutes = 30;
                            }

                            TooManyFailedAttemptsEvent(null, new LockoutTimeEventArgs(minutes));
                        }
                    }

                    return ret;
                }
                else
                {
                    return GetGenericInstanceWithErrorMessage<T>(errorMessage);
                }
            }
            catch (Exception ex)
            {
                T item = (T)Activator.CreateInstance(typeof(T));
                Type t = typeof(T);
                PropertyInfo prop = t.GetProperty("error");
                if (prop != null)
                {
                    prop.SetValue(item, ex.Message);
                    return item;
                }
                return default(T);
            }
            finally
            {
                SomethingCompleted(null, EventArgs.Empty);
            }
        }

        public static async Task<T> SendApiRequestPostAsync<T>(string sApiDomainURL, string sApiPartialURL, string sToken, NameValueCollection httpReqParamValues)
        {
            return await SendApiRequestAsyncCommon<T>(RestSharp.Method.Post, sApiDomainURL, sApiPartialURL, sToken, httpReqParamValues, true);
        }

        public static async Task<T> SendApiRequestPostAsync<T>(string sApiDomainURL, string sApiPartialURL, string sToken, object json)
        {
            return await SendApiRequestAsyncCommon<T>(RestSharp.Method.Post, sApiDomainURL, sApiPartialURL, sToken, json, true);
        }

        public static async Task<T> SendApiRequestPostAsync<T>(string sApiDomainURL, string sApiPartialURL, string username, string password, string ip)
        {
            return await SendApiRequestAsyncCommon<T>(RestSharp.Method.Post, sApiDomainURL, sApiPartialURL, null, null, false, username, password, ip);
        }


        public static async Task<StatusResponse> UploadImageAsync(string sApiDomainURL, string sApiPartialURL, string token, Method enumMethod, byte[] imageBytes)
		{
			string errorMessage = "";
			RestResponse resp = null;
			var statusResponse = new StatusResponse();
			try
			{
				var restclient = new RestClient(sApiDomainURL);
				RestRequest request = new RestRequest(sApiPartialURL) { Method = enumMethod };

				request.RequestFormat = DataFormat.Json;
				request.AlwaysMultipartFormData = true;

				request.AddFile("file", imageBytes, "my_uploaded_image.jpg", "application/octet-stream");

				if (!string.IsNullOrEmpty(token))
				{
					request.AddHeader("Authorization", string.Format("Bearer " + token));
				}

				resp = await restclient.ExecuteTaskAsync(request);

				if (resp != null)
				{
					if (resp.StatusCode == HttpStatusCode.OK)
					{
						if (string.IsNullOrEmpty(resp.Content))
						{
							errorMessage = "No response data from server.";
						}
						else
						{
                            statusResponse = JsonConvert.DeserializeObject<StatusResponse>(resp.Content);
                        }
					}
					else if (resp.StatusCode == HttpStatusCode.Unauthorized)
					{
						UnauthorizedCallEvent(null, EventArgs.Empty);
					}

					else
					{
						errorMessage = string.Format("Could not retrieve data from server. Server response: {0}", resp.StatusDescription);
					}
				}
				else
				{
					errorMessage = "Not available. No response from server.";
				}
			}
			catch
			{
			}
			
			statusResponse.ErrorMessage = statusResponse.Message = errorMessage;
			return statusResponse;
		}

        private static T GetGenericInstanceWithErrorMessage<T>(string msg)
        {
            T item = (T)Activator.CreateInstance(typeof(T));
            Type t = typeof(T);

            if (typeof(ResponseBase).IsAssignableFrom(t))
            {
                ((ResponseBase)(object)item).ErrorMessage = msg;
                return item;
            }

            return default(T);
        }

        private static bool IsStatusResponse<T>()
        {
            T item = (T)Activator.CreateInstance(typeof(T));
            Type t = typeof(T);

            return typeof(ResponseBase).IsAssignableFrom(t);
        }

      
    }


   
}
