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
        [TestInitialize]
        public void Init() => service = new AvailableProductService(new TestingContextDB());
        [TestCleanup]
        public void CleanDb()
        {
            var Db = new TestingContextDB();
            Db.Database.EnsureDeleted();
        }
        [TestMethod]
        public void GetAvailableProductListService_Test()
        {
            var result = service.GetItemsAsync().GetAwaiter().GetResult();
            Assert.AreEqual(typeof(List<AvailableProduct>),result.GetType());
        }
        [TestMethod]
        public void GetOneAvailableProductService_Test()
        {
            var result = service.GetItemAsync(1).GetAwaiter().GetResult();
            Assert.AreEqual("IPhone 12", result.Product.Name);
        }
        [TestMethod]
        public void AddAvailableProductService_Test()
        {
            AvailableProduct product = new AvailableProduct()
            {
                Product = new Product(),
            };
            bool item = service.AddItemAsync(product).GetAwaiter().GetResult();
            Assert.IsTrue(item);
            var count = service.GetItemsAsync().GetAwaiter().GetResult().Count();
            Assert.AreEqual(2, count);
        }
        [TestMethod]
        public void UpdateAvailableProductService_Test()
        {
            var product = service.GetItemAsync(1).GetAwaiter().GetResult();
            product.Amount = 15;
            service.UpdateItemAsync(product).GetAwaiter().GetResult();
            var updateproduct = service.GetItemAsync(1).GetAwaiter().GetResult();
            Assert.AreEqual(15, updateproduct.Amount);
        }
    }
}
