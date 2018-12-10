using Client.ControlLayer;
using Client.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Webshop.Controllers {
    public class LoginController : Controller {

        OrderController orderController = new OrderController();
        UserController uc = new UserController();
        AdminController ac = new AdminController();

        // GET: LogIn
        public ActionResult Index(bool? wasRedirected) {

            long timeNow = DateTime.Now.Ticks;
            List<Orderline> orderlines = Session["cart"] as List<Orderline>;
            if (orderlines != null) {
                foreach (Orderline orderLine in orderlines.ToList<Orderline>()) {
                    if (orderLine.TimeStamp < timeNow) {
                        orderlines.Remove(orderLine);

                        orderController.DeleteOrderLine(orderLine.Product.ID, orderLine.SubTotal, orderLine.Quantity);

                    }
                }
                Session["cart"] = orderlines;
            }

            bool error = false;

            if (wasRedirected != null) {
                error = true;
            }

            if (error) {
                ViewBag.Visibility = "visible";
            }
            else {
                ViewBag.Visibility = "hidden";
            }
            
            return View();
        }


        public ActionResult Login(string email, string password) {

            User user = ac.ValidatePassword(email, password);

            if (user.ErrorMessage == "") {
                Session.Add("User", uc.GetUserWithOrdersAndOrderlines(email));
                
                return RedirectToAction("Index", "Home");
            }

            return this.RedirectToAction("Index", new { wasRedirected = true });
            
        }
    }
}