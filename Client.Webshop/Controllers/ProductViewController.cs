using Client.ControlLayer;
using Client.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Webshop.Controllers {
    public class ProductViewController : Controller {
        ProductController pc = new ProductController();
        OrderController oc = new OrderController();

        public ActionResult Index(int id) {
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
            Product product = pc.Find(id);

            //ViewBag.Message = "Product view controller ;D";
            //Product p = new Product("Johnny", 200, 1, 1, 1, "LKmasdlkmaslkdmalskdlmsad sakkasdlaksld kasmdkmsd ksmd kask kasm ksadnf jen jsaj jsje jasju sj", null, null, null);
            return View(product);
        }

        
        public ActionResult AddProduct(int id, int quantity, string url) {
            Product product = pc.Find(id);
            decimal subTotal = product.Price * quantity;
            Orderline ol = new Orderline(quantity, subTotal, product);
            bool result = oc.CreateOrderLine(quantity, subTotal, product.ID);

            List<Orderline> cart;
            bool flag = true;

            if (ol != null && result) {
                if (Session["cart"] == null) {
                    cart = new List<Orderline>();
                    cart.Add(ol);
                }
                else {
                    cart = (List<Orderline>)Session["cart"];

                    foreach (Orderline orderline in cart) {
                        if (orderline.Product.ID == ol.Product.ID) {
                            orderline.SubTotal += ol.SubTotal;
                            orderline.Quantity += ol.Quantity;
                            flag = false;
                        }
                    }

                    if (flag) {
                        cart.Add(ol);
                    }
                }
                Session["cart"] = cart;
            }

            if (result) {
                TempData["Message"] = "Produktet blev tilføjet til kurv";
            }
            else {
                TempData["Message"] = "Produktet blev ikke tilføjet";
            }
            

            return Redirect(url);

        }

        public ActionResult CreateReview(string reviewText, int productID, string url) {
            User user = (User)Session["user"];
            bool res = pc.CreateReview(reviewText, productID, user.ID);
            //if(res) {
                return Redirect(url);
            //}

            //return RedirectToAction("Index", "Home");
        }

        public ActionResult DeleteReview() {
            return View();
        }
    }
}