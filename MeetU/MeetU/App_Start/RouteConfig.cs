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
                defaults: new { controller = "Meetups", action = "Index" }
            );

            routes.MapRoute(
                name: "meetup create",
                url: "Meetup/Create",
                defaults: new { controller = "Meetups", action = "Index" }
            );

            routes.MapRoute(
                name: "Profile page",
                url: "Profile/{user-id}",
                defaults: new { controller = "Meetups", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Meetups", action = "Index" }
            );

            routes.MapRoute(
                name: "any",
                url: "{*any}",
                defaults: new { controller = "Meetups", action = "Index" }
            );
        }
    }
}
