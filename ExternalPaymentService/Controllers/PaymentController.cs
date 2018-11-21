using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExternalPaymentService.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult Payment() {

            if(true) {
                ViewBag.Message = "Payment successful!";
                return View();
            }
            else {
                ViewBag.Message = "Payment failed!";
                return View();
            }
        }
    }
}