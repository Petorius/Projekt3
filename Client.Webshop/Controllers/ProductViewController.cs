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

            Product product = pc.Find(id);

            //ViewBag.Message = "Product view controller ;D";
            //Product p = new Product("Johnny", 200, 1, 1, 1, "LKmasdlkmaslkdmalskdlmsad sakkasdlaksld kasmdkmsd ksmd kask kasm ksadnf jen jsaj jsje jasju sj", null, null, null);
            return View(product);
        }

        [HttpPost]
        public ActionResult AddProduct(int id, int quantity, string url) {
            Product product = pc.Find(id);
            decimal subTotal = product.Price * quantity;
            Orderline ol = new Orderline(quantity, subTotal, product);
            bool result = oc.CreateOrderLine(quantity, subTotal, product.ID);

            return Redirect(url);
        }
    }
}