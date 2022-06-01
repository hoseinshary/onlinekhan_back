using System.Web.Http;
using Elmah.Contrib.WebApi;
using Onlinekhan.SSO.Common;
using Onlinekhan.SSO.ServiceLayer.Configs;
using StructureMap.Web.Pipeline;

namespace Onlinekhan.SSO.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            SiteConfig.RegisterAutoMapper();
            GlobalConfiguration.Configuration.Filters.Add(new ElmahHandleErrorApiAttribute());

            Logs.Register();
        }

        protected void Application_EndRequest()
        {
            new HybridLifecycle().FindCache(null).DisposeAndClear();
        }
    }
}
