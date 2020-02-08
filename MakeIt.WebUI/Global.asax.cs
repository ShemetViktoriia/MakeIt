using MakeIt.WebUI.App_Start;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MakeIt.WebUI
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            // Autofac and Automapper configurations
            AutofacConfig.RegisterComponents();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //AntiForgeryConfig.RequireSsl = true;
        }
    }
}
