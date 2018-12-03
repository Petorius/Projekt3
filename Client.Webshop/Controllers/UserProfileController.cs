using Client.ControlLayer;
using Client.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Webshop.Controllers {
    public class UserProfileController : Controller {
        // GET: UserProfile
        private UserController userController = new UserController();

        public ActionResult Index() {
            if (Session["User"] == null) {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        public ActionResult Logout() {

            Session["User"] = null;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeleteAccount() {
            User user = (User)Session["user"];
            bool isCompleted = userController.DeleteUser(user.Email);
            if (isCompleted) {
                Session["user"] = null;
                return RedirectToAction("Index", "Home");
            }
            else {
                return RedirectToAction("Index");
            }
        }
    }
}