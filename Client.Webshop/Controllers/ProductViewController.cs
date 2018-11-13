using Client.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Webshop.Controllers
{
    public class ProductViewController : Controller
    {
        // GET: ProductView
        public ActionResult Index(int? id)
        {
            ViewBag.Message = "Product view controller ;D";
            Product p = new Product("Johnny", 200, 1, 1, 1, "LKmasdlkmaslkdmalskdlmsad sakkasdlaksld kasmdkmsd ksmd kask kasm ksadnf jen jsaj jsje jasju sj", null, null, null);
            return View(p);
        }
    }
}