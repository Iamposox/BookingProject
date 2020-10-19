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
    public class AvailableProductServiceTest
    {
        private AvailableProductService service;
        public AvailableProductServiceTest() { }
        [TestInitialize]
        public void Init() => service = new AvailableProductService(new TestingContextDB());
        [TestCleanup]
        public void CleanDb()
        {
            var Db = new TestingContextDB();
            Db.Database.EnsureDeleted();
        }
        [TestMethod]
        public void GetCustomerService_Test()
        {
            var result = service.GetItemsAsync().GetAwaiter().GetResult();
            Assert.AreEqual(result.GetType(), new List<AvailableProduct>().GetType());
        }
        [TestMethod]
        public void GetOneCustomerService_Test()
        {
            var result = service.GetItemAsync(1).GetAwaiter().GetResult();
            Assert.AreEqual(result.Product.Name, "IPhone 12");
        }
        [TestMethod]
        public void AddCustomerService_Test()
        {
            AvailableProduct product = new AvailableProduct()
            {
                Product = new Product(),
            };
            bool item = service.AddItemAsync(product).GetAwaiter().GetResult();
            Assert.IsTrue(item);
            var count = service.GetItemsAsync().GetAwaiter().GetResult().Count();
            Assert.AreEqual(count, 2);
        }
        [TestMethod]
        public void UpdateCustomerService_Test()
        {
            var product = service.GetItemAsync(1).GetAwaiter().GetResult();
            product.Amount = 15;
            service.UpdateItemAsync(product).GetAwaiter().GetResult();
            var updateproduct = service.GetItemAsync(1).GetAwaiter().GetResult();
            Assert.AreEqual(15, updateproduct.Amount);
        }
    }
}
