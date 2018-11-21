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
        private Order o;

        [TestInitialize]
        public void SetUp() {
            orderDB = new OrderDB(connectionString);
            productDB = new ProductDB(connectionString);
            customerDB = new CustomerDB(connectionString);
            orderLogic = new OrderLogic();
            Customer c = new Customer("Morten", "Albertsen", 12345678, "morten@hotmail.com", "Fyensgade", 9000, "Aalborg");
            Invoice i = new Invoice();
            o = new Order(c, i);
            OrderLine ol = new OrderLine(2, 200, productDB.Get(1));
            OrderLine ol2 = new OrderLine(1, 300, productDB.Get(2));
            o.Orderlines.Add(ol);
            o.Orderlines.Add(ol2);
        }

        [TestMethod]
        public void OrderFindCustomerByMail() {

        }

    }
}