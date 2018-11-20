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
    public class TagTest {
        private static string connectionString = "Server=kraka.ucn.dk; Database=dmab0917_1067354; User Id=dmab0917_1067354; Password=Password1! ";
        private TagDB TagDB;

        [TestInitialize]
        public void SetUp() {
            TagDB = new TagDB(connectionString);
            Tag tag = new Tag();
            tag.Name = "Rødt";
            TagDB.CreateTag(tag);
        }

        [TestMethod]
        public void GetTagTest() {
            Tag t = new Tag();
            t.Name = TagDB.Get("Rødt").Name;

            Assert.AreEqual("Rødt", t.Name);
        }

        [TestMethod]
        public void GetTagFailIsNull() {

            Tag t = TagDB.Get("Hvid");

            Assert.IsNull(t.Name);
            //Der assertes på navnet da et objekt vil blive bygget og få et automatisk ID uanset hvad
        }

        [ClassCleanup]
        public static void CleanUp()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "Truncate table Tag";
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
