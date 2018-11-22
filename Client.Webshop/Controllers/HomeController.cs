﻿using Client.ControlLayer;
using Client.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Webshop.Controllers
{
    public class HomeController : Controller
    {
        ProductController pc = new ProductController();
        OrderController orderController = new OrderController();


        IEnumerable<Product> products = new List<Product>();

        public ActionResult Index()
        {
            long timeNow = DateTime.Now.Ticks;
            List<Orderline> orderlines = Session["cart"] as List<Orderline>;
            if (orderlines != null)
            {
                foreach (Orderline orderLine in orderlines.ToList<Orderline>())
                {
                    if (orderLine.TimeStamp < timeNow)
                    {
                        orderlines.Remove(orderLine);

                        orderController.DeleteOrderLine(orderLine.Product.ID, orderLine.SubTotal, orderLine.Quantity);

                    }
                    products = pc.GetAllProducts();
                }
                Session["cart"] = orderlines;

            }

            return View(products);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}