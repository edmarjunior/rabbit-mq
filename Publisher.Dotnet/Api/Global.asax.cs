using SimpleInjector.Integration.WebApi;
using System.Web.Http;

namespace Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(SimpleInjectorContainer.Build());

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
