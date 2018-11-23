using Client.ControlLayer;
using Client.Domain;
using PaymentWebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Webshop.Controllers {
    public class BuyController : Controller {
        OrderController oc = new OrderController();
        UserController uc = new UserController();
        // GET: Buy
        public ActionResult Information() {
            long timeNow = DateTime.Now.Ticks;
            List<Orderline> orderlines = Session["cart"] as List<Orderline>;
            if (orderlines != null) {
                foreach (Orderline orderLine in orderlines.ToList<Orderline>()) {
                    if (orderLine.TimeStamp < timeNow) {
                        orderlines.Remove(orderLine);

                        oc.DeleteOrderLine(orderLine.Product.ID, orderLine.SubTotal, orderLine.Quantity);
                    }
                }
                Session["cart"] = orderlines;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Confirmation(string firstName, string lastName, string street, int zip, string city, string email, int number) {
            // Skal kalde på mail og se om den mail tilhører en person som har et userID.
            // Hvis den tilhører en user, får han en fejl om han den mail ikke kan bruges.
            //if (!uc.isEmailAlreadyRegistered(email)) {
                var webApi = new ValuesController();

                bool flag = webApi.Get();

                if (flag) {
                    ViewBag.Message7 = "Betalingen blev gennemført!";
                    Order o = new Order();
                    List<Orderline> cart = (List<Orderline>)Session["cart"];
                    o = oc.CreateOrder(firstName, lastName, street, zip, city, email, number, cart);
                    if (o.Validation) {
                        Session.Abandon();
                        return View(o);
                    }
                    else {
                        Session.Abandon();
                        return Redirect("https://media.giphy.com/media/5ftsmLIqktHQA/giphy.gif");
                    }

                }
                else {

                    ViewBag.Message7 = "Der skete en fejl med betalingen. Prøv igen";

                    return View();
                }
            
            //else {
                
            //}



        }
    }
}