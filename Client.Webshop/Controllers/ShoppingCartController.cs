﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Client.ControlLayer;
using Client.Domain;

namespace Client.Webshop.Controllers {
    public class ShoppingCartController : Controller {

        // GET: ShoppingCart
        public ActionResult ShoppingCart() {
            ViewBag.Message = "Shopping Cart page";

            return View();
        }

    }
}