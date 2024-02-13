using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientApp.BusinessCode
{
    public class ApiResult<T>
    {
        public ApiResult(string rawResult, int code, T result)
        {
            RawResult = rawResult;
            Code = code;
            Result = result;
        }

        public ApiResult(System.IO.Stream rawResult, int code, byte[] result)
        {
            RawResultStram = rawResult;
            Code = code;
            byteResult = result;
        }
        public byte[] byteResult { get; private set; }

        public string RawResult { get; private set; }
        public System.IO.Stream RawResultStram { get; private set; }
        public int Code { get; private set; }

        public T Result { get; private set; }

        public bool IsSuccessful
        {
            get { return Code >= 200 && Code < 300; }
        }
    }
}
