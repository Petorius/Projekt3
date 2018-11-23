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

            ViewBag.Message = "Shopping Cart page";

            IEnumerable<Orderline> ViewOrderlines = Session["cart"] as IEnumerable<Orderline>;

            return View(ViewOrderlines);
        }

        public ActionResult UpdateOrderlineQuantity(int id) {
         
            List<Orderline> orderlines = Session["cart"] as List<Orderline>;

            if (orderlines != null ) {
                foreach (Orderline orderline in orderlines.ToList<Orderline>()) {
                    if (orderline.Product.ID == id) {
                        orderline.SubTotal -= orderline.Product.Price;
                        orderline.Quantity -= 1;

                        oc.UpdateOrderLine(orderline.Product.ID, orderline.SubTotal, orderline.Quantity); 
                    }

                    if(orderline.SubTotal == 0) {
                        orderlines.Remove(orderline);
                    }
                }
                
                Session["cart"] = orderlines;
            }
            
            return RedirectToAction("ShoppingCart");
        }

        public ActionResult DeleteOrderline(int id) {

            List<Orderline> orderlines = Session["cart"] as List<Orderline>;

            if (orderlines != null) {
                foreach (Orderline orderline in orderlines.ToList<Orderline>()) {
                    if (orderline.Product.ID == id) {

                        orderlines.Remove(orderline);

                        oc.DeleteOrderLine(orderline.Product.ID, orderline.SubTotal, orderline.Quantity);

                    }
                }

                Session["cart"] = orderlines;
            }


            return RedirectToAction("ShoppingCart");
        }
    }
}