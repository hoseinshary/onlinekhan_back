using System;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Dispatcher;
using NasleGhalam.ServiceLayer.Configs;
using NasleGhalam.WebApi.ModelBinderAndFormatter;
using Newtonsoft.Json;

namespace NasleGhalam.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Structure Map Config
            var container = StructureMapConfig.Container;

            GlobalConfiguration.Configuration.Services.Replace(
                typeof(IHttpControllerActivator), new StructureMapHttpControllerActivator(container));
            //------------------------------


            //var cors = new EnableCorsAttribute("http://localhost:8080,http://192.168.1.62,http://151.233.58.224:8080", "*", "*");
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            ////------------------------------


            // config formatter
            config.Formatters.XmlFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("multipart/form-data"));
            //config.Formatters.Add(new MultiPartMediaTypeFormatter());
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new CustomJsonFormatter());
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //------------------------------


            // config binder
            config.BindParameter(typeof(string), new StringModelBinder());
            config.BindParameter(typeof(int), new IntegerModelBinder());
            config.BindParameter(typeof(DateTime), new DateTimeModelBinder());
            //------------------------------


            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
