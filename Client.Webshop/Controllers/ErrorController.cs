﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Webshop.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Error()
        {
            return Redirect("https://media.giphy.com/media/5ftsmLIqktHQA/giphy.gif");
        }
    }
}