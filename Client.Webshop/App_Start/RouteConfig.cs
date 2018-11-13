﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Client.Webshop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

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