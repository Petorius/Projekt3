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
            CreateFakeProduct();
        }

        [TestMethod]
        public void OptimisticConcurrenyWithStock() {
            Product p = productDB.Get(1);
            OrderLine ol = new OrderLine(1, 2000, p);

            orderlineDB.Create(ol);

            ol.Product = productDB.Get(1);

            Assert.AreEqual(ol.Product.Stock, 4);
        }

        [TestMethod]
        public void OptimisticConcurrenyWithStockExpectedToFail() {


        }



        [ClassCleanup]
        public static void CleanUp() {
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand()) {
                    cmd.CommandText = "Truncate table Product";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void CreateFakeProduct() {
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand()) {
                    cmd.CommandText = "Insert into Product(Name, Price, Stock, MinStock, MaxStock, Description) values" +
                     " (@Name, @Price, @Stock, @MinStock, @MaxStock, @Description)";
                    cmd.Parameters.AddWithValue("Name", "Rolex");
                    cmd.Parameters.AddWithValue("Price", 2000);
                    cmd.Parameters.AddWithValue("Stock", 5);
                    cmd.Parameters.AddWithValue("MinStock", 2);
                    cmd.Parameters.AddWithValue("MaxStock", 10);
                    cmd.Parameters.AddWithValue("Description", "Mega fed ur");
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}