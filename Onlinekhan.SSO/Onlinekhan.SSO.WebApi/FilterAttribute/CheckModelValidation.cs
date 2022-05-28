using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Onlinekhan.SSO.Common;

namespace Onlinekhan.SSO.WebApi.FilterAttribute
{
    public class CheckModelValidation : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid)
            {
                return;
            }

            var sb = new StringBuilder();
            foreach (var modelState in actionContext.ModelState)
            {
                foreach (var error in modelState.Value.Errors)
                {
                    sb.Append(error.ErrorMessage + "</br>");
                }
            }

            actionContext.Response = actionContext.ControllerContext.Request
                .CreateResponse(HttpStatusCode.OK,
                    new ClientMessageResult
                    {
                        Message = sb.ToString(),
                        MessageType = MessageType.Error
                    });
        }
    }
}