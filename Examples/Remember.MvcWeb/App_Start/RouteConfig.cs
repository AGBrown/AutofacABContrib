using System.Web.Mvc;
using System.Web.Routing;

namespace Remember.MvcWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");

            //AreaRegistration.RegisterAllAreas();

            routes.MapRoute(
                "Default",                              // Route Name
                "{controller}/{action}/{id}",           // Route URL (pattern)
                new {                                   // Route defaults
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                }
                //,new[] { "Remember.Web.Controllers" }      // Route Namespaces that take preference
            );
        }
    }
}
