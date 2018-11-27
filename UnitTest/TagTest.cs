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
        private TagDB tagDB;

        [TestInitialize]
        public void SetUp() {
            tagDB = new TagDB(connectionString);
        }

        [TestMethod]
        public void GetTagTest() {
            Tag t = new Tag();
            t.Name = tagDB.Get("Safirglas").Name;

            Assert.AreEqual("Safirglas", t.Name);
        }

        [TestMethod]
        public void GetTagFailIsNull() {

            Tag t = tagDB.Get("Hvid");

            Assert.IsNull(t.Name);
        }

        [TestMethod]
        public void GetProductsInTag() {
            Tag t = tagDB.Get("Safirglas");

            Assert.AreEqual(t.Products.Count, 2);
        }
    }
}
