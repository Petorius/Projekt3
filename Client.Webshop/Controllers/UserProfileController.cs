using Client.ControlLayer;
using Client.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Webshop.Controllers {
    public class UserProfileController : Controller {
        private UserController userController = new UserController();

        // GET: UserProfile
        public ActionResult Index() {
            if(Session["User"] == null) {
                return RedirectToAction("Index", "Login");
            }
            return View((User)Session["User"]);
        }

        public ActionResult Edit(User user) {
            userController.UpdateCustomer(user.FirstName, user.LastName, user.Phone, user.Email, user.Address, user.ZipCode, user.City);

            return View((User)Session["User"]);
        }

        public ActionResult Delete() {
            return View((User)Session["User"]);
        }

        public ActionResult Logout() {

            Session["User"] = null;

            return RedirectToAction("Index", "Home");
        }
    }
}