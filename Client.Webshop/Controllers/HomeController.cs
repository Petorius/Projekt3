using Client.ControlLayer;
using Client.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Webshop.Controllers {
    public class HomeController : Controller {
        ProductController pc = new ProductController();
        OrderController orderController = new OrderController();
        TagController tc = new TagController();


        IEnumerable<Product> products = new List<Product>();

        public ActionResult Index() {

            //Reset session category
            Session["Tag"] = "Bestsellers";

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

            products = pc.GetAllProducts();

            return View(products);
        }

        // GET: Tag
        public ActionResult GetSalesByTag(string name) {
            
            Session["Tag"] = name;

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
            
            Tag t = tc.FindTagByName(name);

            return View("Index", t.Products);
        }
    }
}