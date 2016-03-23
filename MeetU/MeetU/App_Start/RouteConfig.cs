using System.Web.Mvc;
using System.Web.Routing;

namespace MeetU
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "switch_to_ng_route",
                url: "index",
                defaults: new { controller = "Meetups", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Meetups", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
