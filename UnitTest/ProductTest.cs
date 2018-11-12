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
        private string connectionString = "Server=kraka.ucn.dk; Database=dmab0917_1067354; User Id=dmab0917_1067354; Password=Password1! ";
        private ProductDB productDB;

        [TestInitialize]
        public void SetUp() {
            productDB = new ProductDB(connectionString);
        }



        [TestMethod]
        public void InsertProductTest() {
            Product p = new Product("Tissot Classic", 4000, 10, 4, 20, "Tissot Classic er designet som det fedeste ur");

            productDB.Create(p);

            Assert.AreEqual(p.Name, FindHelperTest(1).Name);
        }

        [TestMethod]
        public void FindProductTest() {
            Product p = productDB.Get(1);

            Assert.IsNotNull(p);
        }

        [TestMethod]
        public void UpdateProductTestExceptedToFail() {
            Product p = productDB.Get(1);

            productDB.Update(p.ID, "Rune", p.Price, p.Stock, p.MinStock, p.MaxStock, p.Description, true);

            p = productDB.Get(1);

            Assert.AreEqual(p.Name, "Tissot Classic");
        }

        [TestMethod]
        public void UpdateProductTest() {
            Product p = productDB.Get(1);

            productDB.Update(p.ID, "Rune", p.Price, p.Stock, p.MinStock, p.MaxStock, p.Description, true, true);

            p = productDB.Get(1);

            Assert.AreEqual(p.Name, "Rune");
        }

        [TestMethod]
        public void DeleteProductTestExceptedToFail() {
            Product p = new Product();

            productDB.Delete(1, true);

            p = productDB.Get(1);

            Assert.IsNotNull(p);
        }

        [TestMethod]
        public void DeleteProductTest() {
            Product p = new Product();

            productDB.Delete(1, true, true);

            p = productDB.Get(1);

            Assert.IsNull(p);
        }


















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
