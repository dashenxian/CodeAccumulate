using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using Web.Models;
using Serilog;

namespace Web
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            SerializableResponse(actionExecutedContext);
            base.OnException(actionExecutedContext);
        }

        private void SerializableResponse(HttpActionExecutedContext context)
        {
            var response = new HttpResponseMessage();
            var ex = context.Exception;
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Content = new ObjectContent<AjaxResponse>(
                new AjaxResponse(new ErrorInfo
                {
                    Message = ex.Message,
                    Details = ex.StackTrace
                }),
                GlobalConfiguration.Configuration.Formatters.JsonFormatter
                );
            context.Response = response;
            Log.Logger.Error(ex, "未知错误发生在{RequestUri},参数为{@ActionArguments}", context?.Request?.RequestUri, context?.ActionContext?.ActionArguments);
        }
    }
}
