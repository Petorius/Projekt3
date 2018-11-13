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
        // GET: ShoppingCart
        public ActionResult ShoppingCart()
        {
            ViewBag.Message = "Shopping Cart page";
            
            return View();
        }

        public ActionResult AddProduct(int id) {

            if(Session["ShoppingCart"] == null) {
                List<Orderline> cart = new List<Orderline> {
                    new Orderline(pc.Find(id), 1)
                };
                Session["ShoppingCart"] = cart;
            }
            else {
                List<Orderline> cart = (List<Orderline>)Session["ShoppingCart"];
                cart.Add(new Orderline(pc.Find(id), 1));
                Session["ShoppingCart"] = cart;
            }

            return View("ShoppingCart");
        }
    }
}