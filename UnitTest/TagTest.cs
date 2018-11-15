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


namespace UnitTest
{
    [TestClass]
    public class TagTest
    {
        private static string connectionString = "Server=kraka.ucn.dk; Database=dmab0917_1067354; User Id=dmab0917_1067354; Password=Password1! ";
        private TagDB TagDB;


        [TestInitialize]
        public void SetUp()
        {
            TagDB = new TagDB(connectionString);
        }

        [TestMethod]
        public void GetTagTest()
        {
            Tag t = TagDB.Get("Sort");

            Assert.AreEqual(t.Name, "Sort");
        }

        [TestMethod]
        public void FindProductTest()
        {

            Tag t = TagDB.Get("Sort");

            Assert.IsNotNull(t);
        }

        //[ClassCleanup]
        //public static void CleanUp()
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        using (SqlCommand cmd = connection.CreateCommand())
        //        {
        //            cmd.CommandText = "Truncate table Product";
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}

        
    }
}
