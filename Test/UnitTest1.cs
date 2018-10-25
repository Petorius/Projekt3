using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Data.SqlClient;

namespace Test
{
    [TestClass]
    public class DatabaseTest
    {
        [TestMethod]
        public void TestDatabaseConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

            }
        }
    }
}
