using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Client.ControlLayer;
using Client.Domain;

namespace Client.Webshop.Controllers {
    public class ShoppingCartController : Controller {

        OrderController oc = new OrderController();

        // GET: ShoppingCart
        public ActionResult ShoppingCart() {
            ViewBag.Message = "Shopping Cart page";

            
            IEnumerable<Orderline> orderlines = Session["cart"] as IEnumerable<Orderline>;

            return View(orderlines);
        }

        public ActionResult UpdateOrderlineQuantity(int id) {

         
            List<Orderline> orderlines = Session["cart"] as List<Orderline>;

            if (orderlines != null ) {
                foreach (Orderline orderline in orderlines.ToList<Orderline>()) {
                    if (orderline.Product.ID == id) {
                        orderline.SubTotal -= orderline.Product.Price;
                        orderline.Quantity -= 1;

                        oc.UpdateOrderline(orderline.Product.ID, orderline.SubTotal, orderline.Quantity);
                       
                    }

                    if(orderline.SubTotal == 0) {
                        orderlines.Remove(orderline);
                    }
                }
                
                Session["cart"] = orderlines;
            }
            
         
            return RedirectToAction("ShoppingCart");
        }
    }
}