using BOG.Domain.Model;
using BOG.Lib.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BOG.Tests
{
    [TestClass]
    public class ProductServiceTest
    {
        private ProductService service;
        [TestInitialize]
        public void Init() => service = new ProductService(new TestingContextDB());
        [TestCleanup]
        public void CleanDb()
        {
            var Db = new TestingContextDB();
            Db.Database.EnsureDeleted();
        }
        [TestMethod]
        public void GetProductListService_Test()
        {
            var result = service.GetItemsAsync().GetAwaiter().GetResult();
            Assert.AreEqual(typeof(List<Product>), result.GetType());
        }
        [TestMethod]
        public void GetOneProductService_Test()
        {
            var result = service.GetItemAsync(1).GetAwaiter().GetResult();
            Assert.AreEqual("IPhone 12", result.Name);
        }
        [TestMethod]
        public void AddCustomerService_Test()
        {
            Product product = new Product()
            {
                Name = "SomeProduct",
            };
            bool item = service.AddItemAsync(product).GetAwaiter().GetResult();
            Assert.IsTrue(item);
            var count = service.GetItemsAsync().GetAwaiter().GetResult().Count();
            Assert.AreEqual(2, count);
        }
        [TestMethod]
        public void UpdateCustomerService_Test()
        {
            var product = service.GetItemAsync(1).GetAwaiter().GetResult();
            product.Name = "NoName";
            service.UpdateItemAsync(product).GetAwaiter().GetResult();
            var updatecustomer = service.GetItemAsync(1).GetAwaiter().GetResult();
            Assert.AreEqual("NoName", updatecustomer.Name);
        }
    }
}
