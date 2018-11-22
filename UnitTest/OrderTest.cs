using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.DataAccessLayer;
using Server.BusinessLogic;
using Server.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace UnitTest {
    [TestClass]
    public class OrderTest {
        private static string connectionString = "Server=kraka.ucn.dk; Database=dmab0917_1067354; User Id=dmab0917_1067354; Password=Password1! ";
        private OrderDB orderDB;
        private OrderLogic orderLogic;
        private CustomerDB customerDB;
        private ProductDB productDB;

        [TestInitialize]
        public void SetUp() {
            orderDB = new OrderDB(connectionString);
            productDB = new ProductDB(connectionString);
            customerDB = new CustomerDB(connectionString);
            orderLogic = new OrderLogic(connectionString);
        }

        [TestMethod]
        public void OrderFindCustomerByMail() {
            Customer c = customerDB.GetByMail("Morten@hotmail.com");

            Assert.AreEqual(c.FirstName, "Morten");
        }

        [TestMethod]
        public void OrderFindCustomerAndUpdate() {
            Customer c = customerDB.GetByMail("Morten@hotmail.com");
            c.FirstName = "Rune";

            customerDB.Update(c, true, true);
            c = customerDB.GetByMail("Morten@hotmail.com");

            Assert.AreEqual(c.FirstName, "Rune");
            c.FirstName = "Morten";
            customerDB.Update(c, true, true);
        }

        [TestMethod]
        public void OrderFindCustomerAndUpdateExpectedToFail() {
            Customer c = customerDB.GetByMail("Morten@hotmail.com");
            c.FirstName = "Rune";

            customerDB.Update(c, true, false);
            c = customerDB.GetByMail("Morten@hotmail.com");

            Assert.AreEqual(c.FirstName, "Morten");
        }

        [TestMethod]
        public void CreateOrderWithReturnedPrimaryKey() {
            Customer c = customerDB.GetByMail("Morten@hotmail.com");
            Invoice i = new Invoice();
            Order o = new Order(c, i);
            o.Orderlines.Add(new OrderLine(2, 200, productDB.Get(1)));

            int res = orderDB.CreateReturnID(o);

            Assert.IsTrue(res > 0);
        }

        [TestMethod]
        public void ValidateOrderLinePricesToPreventInsecureDirectObjectReference() {
            Product p = productDB.Get(1);
            //Price on OrderLine is incorrect and should be caught
            OrderLine ol = new OrderLine(1, 100, p);
            List<OrderLine> orderLines = new List<OrderLine>();
            orderLines.Add(ol);

            Order o = orderLogic.CreateOrder("Rune", "Andersen", "Istedgade 10", 9000, "Aalborg", "denbedste@gmail.com", 12341234, orderLines);


            Assert.IsTrue(o.Validation == false);

        }



    }
}