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
        public ActionResult Information(bool? wasRedirected) {
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

        [HttpPost]
        public ActionResult Confirmation(string firstName, string lastName, string street, int zip, string city, string email, int number) {
            // Skal kalde på mail og se om den mail tilhører en person som har et userID.
            // Hvis den tilhører en user, får han en fejl om han den mail ikke kan bruges.
            //if (!uc.isEmailAlreadyRegistered(email)) {

            if (!uc.IsEmailAlreadyRegistered(email)) {

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
            }
            else {
                return RedirectToAction("Information", new { wasRedirected = true });
            }
        }

        [HttpPost]
        public ActionResult GetCustomerInfo(string email) {
            Customer c = new Customer(uc.GetCustomerByMail(email));

            ViewBag.FirstName = c.FirstName;
            ViewBag.LastName = c.LastName;
            ViewBag.Address = c.Address;
            ViewBag.Zip = c.ZipCode;
            ViewBag.City = c.City;
            ViewBag.Email = c.Email;
            ViewBag.Number = c.Phone;
            return RedirectToAction("Information");
        }
    }
}