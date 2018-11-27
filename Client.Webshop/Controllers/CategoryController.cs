using Client.ControlLayer;
using Client.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Webshop.Controllers
{
    public class CategoryController : Controller
    {
        TagController tc = new TagController();

        // GET: Category
        public ActionResult GetSalesByCategory(string name)
        {
            Category c = tc.GetSalesByCategory(name);
            return View(c.Products);
        }
    }
}