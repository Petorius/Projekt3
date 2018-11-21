using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Client.Webshop {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           
            routes.MapRoute(
                name: "Login",
                url: "Login",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Buy",
                url: "Checkout/Buy",
                defaults: new { controller = "Buy", action = "Information", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Confirmation",
                url: "Confirmation/",
                defaults: new { controller = "Buy", action = "Confirmation", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "ProductView",
                url: "Product/{id}",
                defaults: new { controller = "ProductView", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Search",
                url: "Search",
                defaults: new { controller = "Search", action = "Search", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ShoppingCart",
                url: "Cart",
                defaults: new { controller = "ShoppingCart", action = "ShoppingCart", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }

            );

        }
    }
}
