using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using JavaWordGenerat.Web.Models;
using Serilog;

namespace JavaWordGenerat.Web
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
            Log.Error(ex, ex.Message);
        }
    }
}
