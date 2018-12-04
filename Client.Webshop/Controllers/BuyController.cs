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
            if (Session["cart"] == null) {
                return RedirectToAction("Index", "Home");
            }
            if (Session["User"] != null) {

                User user = (User)Session["User"];

                TempData["FirstName"] = user.FirstName;
                TempData["LastName"] = user.LastName;
                TempData["Address"] = user.Address;
                TempData["ZipCode"] = user.ZipCode;
                TempData["City"] = user.City;
                TempData["Email"] = user.Email;
                TempData["Phone"] = user.Phone;

            }

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

        public ActionResult Confirmation() {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Confirmation(string firstName, string lastName, string street, int zip, string city, string email, int number) {
            if (Session["cart"] == null) {
                return RedirectToAction("Index", "Home");
            }
            if (!uc.IsEmailAlreadyRegistered(email) || Session["User"] != null) {
                var webApi = new ValuesController();

                bool flag = webApi.Get();

                if (flag) {
                    ViewBag.Message7 = "Betalingen blev gennemført!";
                    Order o = new Order();
                    List<Orderline> cart = (List<Orderline>)Session["cart"];
                    o = oc.CreateOrder(firstName, lastName, street, zip, city, email, number, cart);
                    if (o.Validation) {
                        Session["cart"] = null;
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


        public ActionResult GetCustomerInfo(string prevEmail) {
            Customer c = uc.GetCustomerByMail(prevEmail);
            if (c.ID > 0) {
                TempData["FirstName"] = c.FirstName;
                TempData["LastName"] = c.LastName;
                TempData["Address"] = c.Address;
                TempData["ZipCode"] = c.ZipCode;
                TempData["City"] = c.City;
                TempData["Email"] = c.Email;
                TempData["Phone"] = c.Phone;
            }
            else {
                TempData["Fail"] = "Du har ikke handlet her før med denne mail";
            }

            return RedirectToAction("Information");
        }
    }
}