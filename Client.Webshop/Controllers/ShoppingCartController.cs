using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Client.ControlLayer;
using Client.Domain;
using Client.Webshop.Models;

namespace Client.Webshop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ProductController pc = new ProductController();
        private OrderController oc = new OrderController();
        // GET: ShoppingCart
        public ActionResult ShoppingCart()
        {
            ViewBag.Message = "Shopping Cart page";

            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(int id, int price, int quantity)
        {
            decimal subTotal = price * quantity;
            oc.CreateOrderLine(quantity, subTotal, id);

            return RedirectToAction("Index");
        }
    }
}