using System.Web.Mvc;
using System.Web.Routing;

namespace ECMSS.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "AuthGate",
              url: "AuthGate/{token}",
              defaults: new { controller = "Home", action = "AuthGate", token = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "Root",
                url: "{*.}",
                defaults: new { controller = "Home", action = "Index" }
           );
        }
    }
}