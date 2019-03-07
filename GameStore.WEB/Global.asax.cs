using GameStore.WEB.App_Start;
using System.Web.Mvc;
using System.Web.Routing;

namespace GameStore.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            NinjectConfigurator.Configuration();

            AutoMapperInitializer.Initialize();

            FilterConfig.RegisterGloabalFilter(GlobalFilters.Filters);
        }
    }
}
