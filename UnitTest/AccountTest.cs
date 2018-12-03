using DataAccessLayer.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.BusinessLogic;
using Server.DataAccessLayer;
using Server.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest {
    [TestClass]
    public class AccountTest {
        private static string connectionString = "Server=kraka.ucn.dk; Database=dmab0917_1067354; User Id=dmab0917_1067354; Password=Password1! ";
        private Account acc;
        private UserLogic userLogic;
        private IUserDB userDB;
        private User user;

        [TestInitialize]
        public void SetUp() {
            acc = new Account();
            userLogic = new UserLogic(connectionString);
            userDB = new UserDB(connectionString);
            user = userDB.GetUser("g-star-raw@gmail.gcom");
            if (user.ID < 1) {
                userLogic.CreateUserWithPassword("Rune", "G", "G-Street", 9000, "G-Borg",
                                                "g-star-raw@gmail.gcom", 81238123, "SuperTester123!");
            }
        }

        [TestMethod]
        public void CreateUserValidatePassword() {
            string email = "g-star-raw@gmail.gcom";
            string password = "SuperTester123!";

            bool validation = userLogic.ValidatePassword(email, password);

            Assert.AreEqual(true, validation);
        }

        [TestMethod]
        public void CreateUserValidatePasswordExpectedToFail() {
            string email = "g-star-raw@gmail.gcom";
            string password = "WrongPassword";

            bool validation = userLogic.ValidatePassword(email, password);

            Assert.AreEqual(false, validation);
        }

        [TestMethod]
        public void AccountValidateUserLogin() {
            string password = "SuperTester123!";

            bool validation = acc.ValidateLogin(user.Salt, user.HashPassword, password);

            Assert.AreEqual(true, validation);
        }

        [TestMethod]
        public void AccountValidateUserLoginExpectedToFail() {
            string password = "FailPassword";

            bool validation = acc.ValidateLogin(user.Salt, user.HashPassword, password);

            Assert.AreEqual(false, validation);
        }
    }
}
