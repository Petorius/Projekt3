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
        // GET: Buy
        public ActionResult Information()
        {
            long timeNow = DateTime.Now.Ticks;
            List<Orderline> orderlines = Session["cart"] as List<Orderline>;
            if (orderlines != null)
            {
                foreach (Orderline orderLine in orderlines.ToList<Orderline>())
                {
                    if (orderLine.TimeStamp < timeNow)
                    {
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

            var webApi = new ValuesController();

            bool flag = webApi.Get();

            if (flag) {
                ViewBag.Message7 = "Betalingen blev gennemført!";
                Order o = new Order();
                List<Orderline> cart = (List<Orderline>)Session["cart"];
                if (cart != null) {
                    o = oc.CreateOrder(firstName, lastName, street, zip, city, email, number, cart);
                    Session.Abandon();
                }
                return View(o);
            }
            else {
                
                ViewBag.Message7 = "Der skete en fejl med betalingen. Prøv igen";

                return View();
            }
            
        }
    }
}