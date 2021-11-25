using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenAPI.ViewModels.Common
{
    public class ApiResult<T>
    {
        public bool IsSuccessed { get; set; }

        public string Message { get; set; }

        public T ResultObj { get; set; }

        public ApiResult(bool IsSuccessed)
        {
            this.IsSuccessed = IsSuccessed;
        }

        // cho trường hợp Failed, gởi ra 1 message 
        public ApiResult(bool IsSuccessed, string Message)
        {
            this.IsSuccessed = IsSuccessed;
            this.Message = Message;
        }

        // cho trường hợp Success, gởi ra ResultObj
        public ApiResult(bool IsSuccessed, string Message, T ResultObj)
        {
            this.IsSuccessed = IsSuccessed;
            this.Message = Message;
            this.ResultObj = ResultObj;
        }
         public ApiResult(bool IsSuccessed, T ResultObj)
        {
            this.IsSuccessed = IsSuccessed;
            this.ResultObj = ResultObj;
        }
    }
}