using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FormularioDinamico
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Account",
                url: "Account/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "AdminCategoria",
                url: "Categoria/{action}/{id}",
                defaults: new { controller = "Categoria", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "AdminSubCategoria",
                url: "SubCategoria/{action}/{id}",
                defaults: new { controller = "SubCategoria", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Categoria",
                url: "{slug}",
                defaults: new { controller = "Categoria", action = "Details" }
            );

            routes.MapRoute(
                name: "SubCategoria",
                url: "{slug}/{subslug}",
                defaults: new { controller = "SubCategoria", action = "Details" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
