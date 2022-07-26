using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.ViewModels.Teacher;
using NasleGhalam.WebApi.Controllers;

namespace NasleGhalam.WebApi.App_Start
{
    public class HttpNotFoundAwareDefaultHttpControllerSelector : DefaultHttpControllerSelector
    {
    public HttpNotFoundAwareDefaultHttpControllerSelector(HttpConfiguration configuration)
        : base(configuration)
    {
    }
    public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
    {
        HttpControllerDescriptor decriptor = null;
        try
        {
            decriptor = base.SelectController(request);
        }
            catch (HttpResponseException ex)
        {
            var code = ex.Response.StatusCode;
            if (code != HttpStatusCode.NotFound)
            throw;
            var routeValues = request.GetRouteData().Values;
            routeValues["controller"] = "Error";
            routeValues["action"] = "Handle404";
            decriptor = base.SelectController(request);
        }
        return decriptor;
    }
}

    public class HttpNotFoundAwareControllerActionSelector : ApiControllerActionSelector
    {
        public HttpNotFoundAwareControllerActionSelector()
        {
        }

        public static ErrorController errorController;
        public override HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
        
        {
            HttpActionDescriptor decriptor = null;
            try
            {
                decriptor = base.SelectAction(controllerContext);
            }
            catch (HttpResponseException ex)
            {
            /*
                var code = ex.Response.StatusCode;
                if (code != HttpStatusCode.NotFound && code != HttpStatusCode.MethodNotAllowed)
                    throw;
                var routeData = controllerContext.RouteData;
                routeData.Values["action"] = "Handle404";
                var tempUri = controllerContext.Request.RequestUri;
                controllerContext.Controller = errorController;
                controllerContext.ControllerDescriptor = new HttpControllerDescriptor(controllerContext.Configuration,
                    "Error", errorController.GetType());
                ErrorCreateViewModel errorViewModel = new ErrorCreateViewModel() {Route = controllerContext.Request.RequestUri.ToString(),ErrorCode = 404};
                controllerContext.RouteData.Values["ErrorViewModel"] = errorViewModel;
                decriptor = base.SelectAction(controllerContext);
            */
            }
            return decriptor;
        }
    }
}
