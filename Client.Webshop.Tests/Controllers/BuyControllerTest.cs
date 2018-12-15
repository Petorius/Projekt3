using System;
using System.Web.Mvc;
using Client.Webshop.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentWebAPI.Controllers;

namespace Client.Webshop.Tests.Controllers {
    [TestClass]
    public class BuyControllerTest {
        [TestMethod]
        public void TestPaymentSuccesful() {

            // Arrange
            var webApi = new ValuesController();

            // Act
            bool flag = webApi.Get();

            // Assert
            Assert.IsTrue(flag);

        }
    }
}
