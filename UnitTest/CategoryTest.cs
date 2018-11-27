using System;
using Client.ServiceLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.DataAccessLayer;
using Server.Domain;


namespace UnitTest {
    [TestClass]
    public class CategoryTest {
        private static string connectionString = "Server=kraka.ucn.dk; Database=dmab0917_1067354; User Id=dmab0917_1067354; Password=Password1! ";
        CategoryDB categoryDB;

        [TestInitialize]
        public void SetUp() {
            categoryDB = new CategoryDB(connectionString);
        }

        [TestMethod]
        public void TestGetCategory() {
            Client.ServiceLayer.ServiceReference2.Category category = new Client.ServiceLayer.ServiceReference2.Category();
            category.Name = categoryDB.Get("Bornholmer ur").Name;

            Assert.AreEqual("Bornholmer ur", category.Name);
        }

        [TestMethod]
        public void TestGetSalesByCategory() {
            int SalesInCategory = 0;
            Category category = categoryDB.Get("Dansk ur");
            foreach (Product product in category.Products) {
                SalesInCategory += product.Sales;
            }

            Assert.AreEqual(1, SalesInCategory);
        }


    }
}

