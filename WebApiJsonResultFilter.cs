using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using Web.Models;

namespace Web
{
    public class JsonResultFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var response = actionExecutedContext.Response;
            if (response==null)
            {
                return;
            }
            SerializableResponse(response);
        }

        private void SerializableResponse(HttpResponseMessage response)
        {
            object resultObject;
            if (!response.TryGetContentValue(out resultObject) || resultObject == null)
            {
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new ObjectContent<AjaxResponse>(
                    new AjaxResponse(),
                    GlobalConfiguration.Configuration.Formatters.JsonFormatter
                    );
                return;
            }

            if (resultObject is AjaxResponseBase)
            {
                return;
            }

            response.Content = new ObjectContent<AjaxResponse>(
                new AjaxResponse(resultObject),
                    GlobalConfiguration.Configuration.Formatters.JsonFormatter
                );
        }
    }
}
