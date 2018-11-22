using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Webshop.Controllers
{
    public class SignupController : Controller
    {
        // GET: Signup
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Signup(string firstName, string lastName, int number, string street, int zip, string city, string email, string password) {

            //Client controller

            return RedirectToAction("Index", "Login");
        }
    }
}