using System;

namespace Web.Models
{

    public abstract class AjaxResponseBase
    {
        public bool Success { get; set; }

        public ErrorInfo Error { get; set; }
    }
    [Serializable]
    public class AjaxResponse<TResult> : AjaxResponseBase
    {
        public TResult Result { get; set; }
        public AjaxResponse(TResult result)
        {
            Result = result;
            Success = true;
        }
        public AjaxResponse()
        {
            Success = true;
        }
        public AjaxResponse(bool success)
        {
            Success = success;
        }

        public AjaxResponse(ErrorInfo error)
        {
            Error = error;
            Success = false;
        }
    }
    [Serializable]
    public class AjaxResponse : AjaxResponse<object>
    {
        public AjaxResponse()
        {

        }
        public AjaxResponse(bool success)
            : base(success)
        {

        }
        public AjaxResponse(object result)
            : base(result)
        {

        }
        public AjaxResponse(ErrorInfo error)
            : base(error)
        {

        }
    }


    [Serializable]
    public class ErrorInfo
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public string Details { get; set; }
    }
}
