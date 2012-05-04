using System;
using System.Web.Mvc;
using System.Web.Routing;
using Zephyr.Initialization;

namespace DemoApp.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static IAppBootstrapper _appBootstrapper;

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{guid}", // URL with parameters
                new { controller = "Home", action = "Index", guid = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "List", // Route name
                "{controller}/{action}/{page}/{items}", // URL with parameters
                new { controller = "Home", action = "List", page=1, items=5 } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            _appBootstrapper = new Zephyr.Web.Mvc.Initialization.MvcAppBootstrapper();
            _appBootstrapper.Run();
        }

        protected void Application_End()
        {
            _appBootstrapper.Dispose();
        }        
    }
}