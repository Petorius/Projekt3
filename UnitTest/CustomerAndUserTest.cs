using Server.Domain;
using DataAccessLayer.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.DataAccessLayer;
using Server.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest {
    [TestClass]
    public class CustomerAndUserTest {
        private static string connectionString = "Server=kraka.ucn.dk; Database=dmab0917_1067354; User Id=dmab0917_1067354; Password=Password1! ";
        private IUserDB userDB;
        private ICustomer customerDB;
        private UserLogic userLogic;

        [TestInitialize]
        public void SetUp() {
            userDB = new UserDB(connectionString);
            customerDB = new CustomerDB(connectionString);
            userLogic = new UserLogic(connectionString);
            userDB = new UserDB(connectionString);
            User user = userDB.GetUser("g-star-raw@gmail.gcom");
            if (user.ID < 1) {
                userLogic.CreateUserWithPassword("Rune", "G", "G-Street", 9000, "G-Borg",
                                                "g-star-raw@gmail.gcom", 81238123, "SuperTester123!");
            }
        }

        [TestMethod]
        public void FindCustomerByMail() {
            Customer c = customerDB.GetByMail("g-star-raw@gmail.gcom");

            Assert.AreEqual(c.FirstName, "Rune");
        }

        [TestMethod]
        public void FindUserByMail() {
            User u = userDB.GetUser("g-star-raw@gmail.gcom");

            Assert.AreEqual(u.FirstName, "Rune");
        }

        [TestMethod]
        public void DeleteUserWithMail() {
            bool res = userDB.DeleteUser("g-star-raw@gmail.gcom");

            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void DeleteUserWithMailExpectedToFail() {
            bool res = userDB.DeleteUser("g-star-raw@gmail.gcom", true, false);

            Assert.AreEqual(false, res);
        }

        [TestMethod]
        public void FindCustomerAndUpdate() {
            Customer c = customerDB.GetByMail("g-star-raw@gmail.gcom");
            c.FirstName = "Morten";

            customerDB.Update(c, true, true);
            c = customerDB.GetByMail("g-star-raw@gmail.gcom");

            Assert.AreEqual(c.FirstName, "Morten");
            c.FirstName = "Rune";
            customerDB.Update(c, true, true);
        }


        [TestMethod]
        public void FindCustomerAndUpdateExpectedToFail() {
            Customer c = customerDB.GetByMail("g-star-raw@gmail.gcom");
            c.FirstName = "Morten";

            customerDB.Update(c, true, false);
            c = customerDB.GetByMail("g-star-raw@gmail.gcom");

            Assert.AreEqual(c.FirstName, "Rune");
        }




    }
}
