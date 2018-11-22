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


        [TestInitialize]
        public void SetUp() {
            productDB = new ProductDB(connectionString);
        }

        //[TestMethod]
        //public void InsertProductTest() {
        //    Product p = new Product("Tissot Classic", 4000, 10, 4, 20, "Tissot Classic er designet som det fedeste ur");

        //    productDB.Create(p);

        //    Assert.AreEqual(p.Name, FindHelperTest(1).Name);
        //}

        [TestMethod]
        public void FindProductTest() {

            Product p = productDB.Get(1);

            Assert.IsNotNull(p);
        }

        [TestMethod]
        public void UpdateProductTestExpectedToFail() {
            Product p = productDB.Get(1);
            p.Name = "Tissot Prime";
            productDB.Update(p, true);

            p = productDB.Get(1);

            Assert.AreEqual(p.Name, "Tissot Classic");
        }

        [TestMethod]
        public void UpdateProductTest() {
            Product p = productDB.Get(1);
            p.Name = "Tissot Prime";

            productDB.Update(p, true, true);

            p = productDB.Get(1);

            Assert.AreEqual(p.Name, "Tissot Prime");

            Product p1 = productDB.Get(1);
            p1.Name = "Tissot Classic";
            productDB.Update(p1, true, true);
        }

        //[TestMethod]
        //public void DeleteProductTestExpectedToFail() {
        //    Product p = new Product();
        //    p.ID = 1;
        //    productDB.Delete(p, true);

        //    p = productDB.Get(1);

        //    Assert.IsNotNull(p);
        //}

        //[TestMethod]
        //public void DeleteProductTest() {
        //    Product p = new Product();
        //    p.ID = 1;
        //    productDB.Delete(p, true, true);

        //    p = productDB.Get(1);

        //    Assert.IsNull(p);
        //}

        [ClassCleanup]
        public static void CleanUp() {
            //using (SqlConnection connection = new SqlConnection(connectionString)) {
            //    connection.Open();
            //    using (SqlCommand cmd = connection.CreateCommand()) {
            //        cmd.CommandText = "Truncate table Product";
            //        cmd.ExecuteNonQuery();
            //    }

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
