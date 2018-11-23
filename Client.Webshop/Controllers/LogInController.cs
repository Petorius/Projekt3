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

        // GET: LogIn
        public ActionResult Index() {

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

            return View();
        }

        public ActionResult Login(string email, string password) {

            bool result = uc.ValidatePassword(email, password);

            if (result) {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index");
        }
    }
}