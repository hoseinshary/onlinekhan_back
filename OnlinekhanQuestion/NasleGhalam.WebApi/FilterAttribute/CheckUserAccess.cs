using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Jwt;

namespace NasleGhalam.WebApi.FilterAttribute
{
    public class CheckUserAccess : ActionFilterAttribute
    {
        private readonly short[] _actionBits;
        public CheckUserAccess(params ActionBits[] actionBits)
        {
            _actionBits = actionBits.Select(current => (short)current).ToArray();
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //return;
            var isAuthenticated = false;

            string token = null;
            if (actionContext.Request.Headers.TryGetValues("Token", out var values))
            {
                token = values.FirstOrDefault();
            }

            if (token != null)
            {
                try
                {
                    // decode token and convert to JwtPayload
                    var jsonPayload = JsonWebToken.Decode(token);
                    var tick = DateTime.Now.ToUniversalTime().Ticks;

                    //if token does not expire
                    if (jsonPayload.Exp > tick)
                    {
                        // if public action 
                        if (_actionBits == null ||
                            _actionBits.Length == 0 ||
                            _actionBits[0] == (short)ActionBits.PublicAccess)
                        {
                            isAuthenticated = true;
                        }
                        // if token has access to this action
                        else if (Utility.HasAccess(jsonPayload.Access, _actionBits))
                        {
                            isAuthenticated = true;
                        }
                        //else
                        //{
                        //    isAuthenticated = false;
                        //}

                        if (isAuthenticated)
                        {
                            var lst = jsonPayload.Value.Split('_');

                            actionContext.Request.Properties.Add("_roleLevel", lst[0]);
                            actionContext.Request.Properties.Add("_isAdmin", lst[1]);
                            actionContext.Request.Properties.Add("_user_id", lst[2]);
                            actionContext.Request.Properties.Add("_userType", lst[3]);

                            actionContext.Request.Properties.Add("_access", jsonPayload.Access);
                        }
                    }
                }
                catch //(Exception ex)
                {
                    //ex.ToString();
                    isAuthenticated = false;
                }
            }

            if (!isAuthenticated)
            {
                actionContext.Response = actionContext.ControllerContext.Request
                    .CreateErrorResponse(HttpStatusCode.Unauthorized, "عدم دسترسی");
                //.CreateResponse(HttpStatusCode.Unauthorized, Utility.UnauthorizedMessage());
            }
        }
    }
}