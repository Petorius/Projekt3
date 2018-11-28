using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Webshop.Controllers {
    public class UserProfileController : Controller {
        // GET: UserProfile
        public ActionResult Index() {
            if(Session["User"] == null) {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        public ActionResult Logout() {

            Session["User"] = null;

            return RedirectToAction("Index", "Home");
        }
    }
}