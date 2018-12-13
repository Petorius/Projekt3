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
        public void FindProductAndImageTest() {

            Product p = productLogic.GetProductWithImages("productID", 1.ToString());

            Assert.IsNotNull(p.Images);
        }

        [TestMethod]
        public void FindProductAndReviewTest() {

            Product p = productLogic.GetProductWithImages("productID", 1.ToString());

            Assert.IsNotNull(p.Reviews);
        }

        [TestMethod]
        public void FindProductWithImageAndReviewTest() {

            Product p = productLogic.GetProductWithImagesAndReviews("productID", 1.ToString());

            Assert.IsNotNull(p.Images);
            Assert.IsNotNull(p.Reviews);
        }

        [TestMethod]
        public void FindAllProductsWithImagesTest() {

            IEnumerable<Product> products = productLogic.GetAllProductsWithImages();

            Assert.IsNotNull(products);
        }

        [TestMethod]
        public void FindAllProductsWithImagesAndFindImageTest() {
            List<Image> images = new List<Image>();

            IEnumerable<Product> products = productLogic.GetAllProductsWithImages();
            foreach (Product p in products) {
                    foreach (Image image in p.Images) {
                        images.Add(image);
                    };

            }
            Assert.IsNotNull(images[0]);
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
    }
}
