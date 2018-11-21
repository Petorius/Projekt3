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
        public ActionResult Information() {
            return View();
        }

        [HttpPost]
        public ActionResult Confirmation(string firstName, string lastName, string street, int zip, string city, string email, int number) {

            var webApi = new ValuesController();

            bool flag = webApi.Get();

            if (flag) {
                ViewBag.Message = "Fornavn: " + firstName;
                ViewBag.Message1 = "Efternavn: " + lastName;
                ViewBag.Message2 = "Adresse: " + street;
                ViewBag.Message3 = "Postnummer: " + zip;
                ViewBag.Message4 = "By: " + city;
                ViewBag.Message5 = "Email: " + email;
                ViewBag.Message6 = "Nummer: " + number;

                ViewBag.Message7 = "Payment was successful!";

                List<Orderline> cart = (List<Orderline>)Session["cart"];
                Session.Abandon();
                Session.Clear();
                oc.CreateOrder(firstName, lastName, street, zip, city, email, number, cart);
                return View();
            }
            else {
                
                ViewBag.Message7 = "Payment was unsuccessful!";

                return View();
            }
            
        }
    }
}