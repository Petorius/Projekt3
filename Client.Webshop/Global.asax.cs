using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Client.ControlLayer;
using Client.Domain;

namespace Client.Webshop {
    public class MvcApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_OnEnd(Object sender, EventArgs e) {
            OrderController oc = new OrderController();

            List<Orderline> orderlines = Session["cart"] as List<Orderline>;

            if (orderlines != null) {
                foreach (Orderline orderline in orderlines.ToList<Orderline>()) {

                    oc.DeleteOrderLine(orderline.Product.ID, orderline.SubTotal, orderline.Quantity);
                }
            }


        }

        protected void Application_Error(Object sender, EventArgs e) {
            var exception = Server.GetLastError();
            if (exception is HttpRequestValidationException) {
                Server.ClearError();
                Response.Redirect("/Error/Error");
            }
        }
    }
}