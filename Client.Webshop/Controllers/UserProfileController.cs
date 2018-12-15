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
        private AdminController adminController = new AdminController();
        private OrderController orderController = new OrderController();

        // GET: UserProfile
        public ActionResult Index() {

            //Checks if orderline session ticks has exceeded, if it has exceeded, removes orderline from session.
            long timeNow = DateTime.Now.Ticks;
            List<Orderline> orderlines = Session["cart"] as List<Orderline>;
            if (orderlines != null) {
                foreach (Orderline orderLine in orderlines.ToList<Orderline>()) {
                    if (orderLine.TimeStamp < timeNow) {
                        orderlines.Remove(orderLine);

                        orderController.DeleteOrderLine(orderLine.Product.ID, orderLine.SubTotal, orderLine.Quantity);

                    }
                }
                Session["cart"] = orderlines;
            }

            if (Session["User"] == null) {
                return RedirectToAction("Index", "Login");
            }
            return View((User)Session["User"]);
        }

        // GET: UserProfile/Edit
        public ActionResult Edit(bool? wasRedirected) {

            bool error = false;

            if (wasRedirected != null) {
                error = true;
            }

            if (error) {
                ViewBag.Visibility = "visible";
            }
            else {
                ViewBag.Visibility = "hidden";
            }
            
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
                    TempData["EditErrorMessage"] = "Venligst vælg en anden E-mail";
                    return RedirectToAction("Edit", new { wasRedirected = true });
                }
            }
            else {
                res = true;
            }
            if(res) {
                Customer c = userController.UpdateCustomer(firstName, lastName, number,
                    email, street, zip, city, existingemail);

                //Update password if desired
                if (password != "" || newpassword != "" || repeatpassword != "") {
                    if (newpassword.Equals(repeatpassword) && newpassword != "" && repeatpassword != "") {
                        User validatedUser = adminController.ValidatePassword(email, password);

                        if (validatedUser.ErrorMessage == "") {
                            User userWithUpdatedPassword = adminController.UpdatePassword(validatedUser.ID, newpassword);
                        }
                        else {
                            TempData["EditErrorMessage"] = "Du skal indtaste dit eksisterende kodeord";
                            return RedirectToAction("Edit", new { wasRedirected = true });
                        }
                    }
                    else {
                        TempData["EditErrorMessage"] = "Du skal indtaste et nyt kodeord, og indtaste det igen";
                        return RedirectToAction("Edit", new { wasRedirected = true });
                    }
                }
                
                
                User newUser = userController.GetUserWithOrdersAndOrderlines(email);

                if (c.ErrorMessage == "") {
                    Session["User"] = null;
                    Session.Add("User", newUser);

                    return RedirectToAction("Index");
                }
                else {
                    TempData["EditErrorMessage"] = "Brugeren findes ikke";
                    return RedirectToAction("Edit", new { wasRedirected = true });
                }
            }

            return RedirectToAction("Edit", new { wasRedirected = true });

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
                
            }
        }
    }
}