using RestSharp;
using System;
using System.Threading.Tasks;

namespace CommonLibraryCoreMaui
{
    public static class RestClientExtensions
    {
        public static Task<RestResponse> ExecuteTaskAsync(this RestClient @this, RestRequest request)
        {
            if (@this == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<RestResponse>();

            var response =  @this.ExecuteAsync(request);
            if (response.Exception != null)
                tcs.TrySetException(response.Exception);
            else
                tcs.TrySetResult(response.Result);
            ///TODO:SC This may need to change 
            
            //@this.ExecuteAsync(request, () =>
            //{
            //    if (response.ErrorException != null)
            //        tcs.TrySetException(response.ErrorException);
            //    else
            //        tcs.TrySetResult(response);
            //});

            return tcs.Task;
        }
    }
}
