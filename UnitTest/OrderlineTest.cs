using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.DataAccessLayer;
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

        [TestInitialize]
        public void SetUp() {
            orderlineDB = new OrderLineDB(connectionString);
        }

        [TestMethod]
        public void OptimisticConcurrenyWithStock() {
            

        }

        [TestMethod]
        public void OptimisticConcurrenyWithStockExpectedToFail() {


        }



        //[ClassCleanup]
        //public static void CleanUp() {
        //    using (SqlConnection connection = new SqlConnection(connectionString)) {
        //        connection.Open();
        //        using (SqlCommand cmd = connection.CreateCommand()) {
        //            cmd.CommandText = "Truncate table Product";
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}
