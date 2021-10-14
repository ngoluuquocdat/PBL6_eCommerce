using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Common
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
        public ApiResult(bool IsSuccessed, string Message)
        {
            this.IsSuccessed = IsSuccessed;
            this.Message = Message;
        }
        public ApiResult(bool IsSuccessed, T ResultObj)
        {
            this.IsSuccessed = IsSuccessed;
            this.ResultObj = ResultObj;
        }
    }
}