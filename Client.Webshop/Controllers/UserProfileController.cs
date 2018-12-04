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
            if (Session["User"] == null) {
                return RedirectToAction("Index", "Login");
            }
            return View((User)Session["User"]);
        }

        // GET: UserProfile/Edit
        public ActionResult Edit() {
            return View((User)Session["User"]);
        }

        public ActionResult Update(User user) {
            bool isUpdated = userController.UpdateCustomer(user.FirstName, user.LastName, user.Phone, 
                user.Email, user.Address, user.ZipCode, user.City);

            if(isUpdated) {
                Session["User"] = null;
                Session.Add("User", userController.GetUser(user.Email));
            }

            return RedirectToAction("Index");
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