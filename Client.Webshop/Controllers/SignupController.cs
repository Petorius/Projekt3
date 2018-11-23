using Client.ControlLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Webshop.Controllers
{
    public class SignupController : Controller
    {
        UserController uc = new UserController();

        // GET: Signup
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Signup(string firstName, string lastName, int number, string street, int zip, string city, string email, string password) {

            bool result = uc.CreateUserWithPassword(firstName, lastName, street, zip, city, email, number, password);
            
            if(result) {
                return RedirectToAction("Index", "Login");
            }
            
            return RedirectToAction("Index");
        }
    }
}