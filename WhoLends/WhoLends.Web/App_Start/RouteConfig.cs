using System.Web.Mvc;
using System.Web.Routing;

namespace WhoLends.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                            "LendDetails",
                            "Lends/Details/{Id}",
                            new { controller = "Lends", action = "Details", id = UrlParameter.Optional });
            

            routes.MapRoute(
                            "Default",
                            "{controller}/{action}/{id}",
                             new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
