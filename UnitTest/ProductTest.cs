using BusniessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.DataAccessLayer;
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
    public class ProductTest {
        private static string connectionString = "Server=kraka.ucn.dk; Database=dmab0917_1067354; User Id=dmab0917_1067354; Password=Password1! ";
        private ProductDB productDB;
        private ProductLogic productLogic;


        [TestInitialize]
        public void SetUp() {
            productDB = new ProductDB(connectionString);
            productLogic = new ProductLogic(connectionString);
        }

        [TestMethod]
        public void FindProductTest() {

            Product p = productLogic.GetProduct("productID", 1.ToString());

            Assert.IsNotNull(p);
        }

        [TestMethod]
        public void UpdateProductTestExpectedToFail() {
            Product p = productLogic.GetProduct("productID", 1.ToString());
            p.Name = "Tissot Prime";
            productDB.Update(p, true);

            p = productLogic.GetProduct("productID", 1.ToString());

            Assert.AreEqual(p.Name, "Rolex Oyster");
        }

        [TestMethod]
        public void UpdateProductTest() {
            Product p = productLogic.GetProduct("productID", 1.ToString());
            p.Name = "Tissot Prime";

            productDB.Update(p, true, true);

            p = productLogic.GetProduct("productID", 1.ToString());

            Assert.AreEqual(p.Name, "Tissot Prime");

            Product p1 = productLogic.GetProduct("productID", 1.ToString());
            p1.Name = "Rolex Oyster";
            productDB.Update(p1, true, true);
        }

        [TestMethod]
        public void DeleteProductTestExpectedToFail() {
            Product p = new Product();
            p.ID = 1;
            productDB.Delete(p, true);

            p = productLogic.GetProduct("productID", 1.ToString());

            Assert.IsTrue(p.IsActive);
        }

        // Method to help testing by finding a product
        private Product FindHelperTest(int id) {
            Product p = new Product();
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand()) {
                    cmd.CommandText = "SELECT productid, name from Product where productID = 1";
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        p.ID = reader.GetInt32(reader.GetOrdinal("productid"));
                        p.Name = reader.GetString(reader.GetOrdinal("name"));
                    }
                }
            }
            return p;
        }
    }
}
