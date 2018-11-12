using Client.ControlLayer;
using Client.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Webshop.Controllers
{
    public class SearchController : Controller
    {
        

        ProductController pc = new ProductController();

        [HttpPost]
        public ActionResult Search(string searchString)
        {

            Int32.TryParse(searchString, out int ID);

            Product p = pc.Find(ID);
            
            return View(p);
        }      
    }
}