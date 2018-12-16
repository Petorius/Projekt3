using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.DataAccessLayer;
using Server.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest {
    [TestClass]
    public class OrderlineTest {
        private static string connectionString = "Server=kraka.ucn.dk; Database=dmab0917_1067354; User Id=dmab0917_1067354; Password=Password1! ";
        private OrderLineDB orderlineDB;
        private ProductDB productDB;

        [TestInitialize]
        public void SetUp() {
            orderlineDB = new OrderLineDB(connectionString);
            productDB = new ProductDB(connectionString);
        }

        [TestMethod]
        public void CreateOrderlineInDesktop() {
            Product p = productDB.Get(1);
            OrderLine ol = new OrderLine(1, 2000, p);
            ol.OrderID = 1;
            
            orderlineDB.CreateInDesktop(ol);

            ol.Product = productDB.Get(1);

            Assert.AreEqual(p.Stock - 1, ol.Product.Stock);
        }

        [TestMethod]
        public void OptimisticConcurrencyWithStock() {
            Product p = productDB.Get(1);
            OrderLine ol = new OrderLine(1, 2000, p);

            orderlineDB.Create(ol);

            ol.Product = productDB.Get(1);

            Assert.AreEqual(p.Stock - 1, ol.Product.Stock);
        }

        [TestMethod]
        public void OptimisticConcurrencyWithStockExpectedToFail() {
            Product p = productDB.Get(1);
            OrderLine ol = new OrderLine(1, 2000, p);

            orderlineDB.Create(ol, true);

            ol.Product = productDB.Get(1);

            Assert.AreEqual(p.Stock , ol.Product.Stock);
        }

        [TestMethod]
        public void OptimisticConCurrencyUpdateStock() {
            Product p = productDB.Get(1);
            OrderLine ol = new OrderLine(2, 2000, p);

            orderlineDB.Update(ol, true, true);

            ol.Product = productDB.Get(1);

            Assert.AreEqual(p.Stock + 1, ol.Product.Stock);
        }

        [TestMethod]
        public void OptimisticConCurrencyUpdateStockFail() {
            Product p = productDB.Get(1);
            OrderLine ol = new OrderLine(2, 2000, p);

            orderlineDB.Update(ol, true, false);

            ol.Product = productDB.Get(1);

            Assert.AreEqual(p.Stock, ol.Product.Stock);
        }

        [TestMethod]
        public void OptimisticConCurrencyDelete() {
            Product p = productDB.Get(1);
            OrderLine ol = new OrderLine(2, 2000, p);

            orderlineDB.Delete(ol, true, true);

            ol.Product = productDB.Get(1);

            Assert.AreEqual(p.Stock + 2, ol.Product.Stock);
        }

        [TestMethod]
        public void OptimisticConCurrencyDeleteFail() {
            Product p = productDB.Get(1);
            OrderLine ol = new OrderLine(2, 2000, p);

            orderlineDB.Delete(ol, true, false);

            ol.Product = productDB.Get(1);

            Assert.AreEqual(p.Stock, ol.Product.Stock);
        }

        [TestMethod]
        public void DeleteOrderlineInDesktop() {
            Product p = productDB.Get(3);
            OrderLine ol = new OrderLine(3, 15000, p);

            orderlineDB.DeleteInDesktop(ol);

            ol.Product = productDB.Get(3);

            Assert.AreEqual(p.Stock + 3, ol.Product.Stock);
        }

        [TestMethod]
        public void DeleteOrderlineInDesktopFail() {
            Product p = productDB.Get(2);
            OrderLine ol = new OrderLine(4, 14000, p);

            orderlineDB.DeleteInDesktop(ol, true, false);

            ol.Product = productDB.Get(2);

            Assert.AreEqual(p.Stock, ol.Product.Stock);
        }

        [TestMethod]
        public void FindOrderline() {
            
            OrderLine ol = orderlineDB.Get(1);

            Assert.IsNotNull(ol);
        }

        [TestMethod]
        public void FindOrderLineByOrderID() {
            List <OrderLine> ol = orderlineDB.GetOrderlinesByOrderID(1);

            Assert.IsNotNull(ol);
        }
    }
}