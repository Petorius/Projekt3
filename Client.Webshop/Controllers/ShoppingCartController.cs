using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Client.ControlLayer;
using Client.Domain;

namespace Client.Webshop.Controllers {
    public class ShoppingCartController : Controller {
        private ProductController pc = new ProductController();
        private OrderController oc = new OrderController();
        // GET: ShoppingCart
        public ActionResult ShoppingCart() {
            ViewBag.Message = "Shopping Cart page";

            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product product, Orderline orderline) {
            orderline.Product = product;
            decimal subTotal = product.Price * orderline.Quantity;
            orderline.SubTotal = subTotal;
            oc.CreateOrderLine(orderline.Quantity, subTotal, product.ID);

            return RedirectToAction("Index");
        }
    }
}