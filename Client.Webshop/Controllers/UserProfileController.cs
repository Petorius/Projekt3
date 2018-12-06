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

        public ActionResult Update(string firstName, string lastName, int number, string street, int zip, string city, string email, string password, string newpassword, string repeatpassword, string existingemail) {

            User user = (User)Session["User"];
            bool res = false;

            if(!email.Equals(user.Email)) {
                User userError = userController.IsEmailAlreadyRegistered(email);
                if (userError.ErrorMessage == "Brugeren findes ikke") {
                    res = true;
                }
                else {
                    TempData["EmailMessage"] = "Venligst vælg en anden E-mail";
                }
            }
            else {
                res = true;
            }
            if(res) {
                Customer c = userController.UpdateCustomer(firstName, lastName, number,
                    email, street, zip, city, existingemail);
                User newUser = userController.GetUser(email);

                if (c.ErrorMessage == "") {
                    Session["User"] = null;
                    Session.Add("User", newUser);
                }
            }
            
            return RedirectToAction("Index");
        }

        public ActionResult Logout() {

            Session["User"] = null;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeleteAccount() {
            User user = (User)Session["user"];
            User errorUser = userController.DeleteUser(user.Email);
            if (errorUser.ErrorMessage == "") {
                Session["user"] = null;
                return RedirectToAction("Index", "Home");
            }
            else {
                return RedirectToAction("Index");
                //Show tempData mesage
            }
        }
    }
}