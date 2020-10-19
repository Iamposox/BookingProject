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
    public class ReservedServiceTest
    {
        private ReservedService service;
        public ReservedServiceTest() { }
        [TestInitialize]
        public void Init() => service = new ReservedService(new TestingContextDB());
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
            Assert.AreEqual(result.GetType(), new List<Reserved>().GetType());
        }
        [TestMethod]
        public void GetOneCustomerService_Test()
        {
            var result = service.GetItemAsync(1).GetAwaiter().GetResult();
            Assert.AreEqual(result.AvailableProduct.Product.Name, "IPhone 12");
        }
        [TestMethod]
        public void AddCustomerService_Test()
        {
            Reserved reserved = new Reserved()
            {
                AvailableProduct = new AvailableProduct() 
                {
                    Product = new Product()
                },
                Booking = new Booking(),
                Customer = new Customer() 
                {
                    PaymentMethod = new PaymentMethod()
                },
                TimeOrder = DateTime.Now
            };
            bool item = service.AddItemAsync(reserved).GetAwaiter().GetResult();
            Assert.IsTrue(item);
            var count = service.GetItemsAsync().GetAwaiter().GetResult().Count();
            Assert.AreEqual(count, 2);
        }
        [TestMethod]
        public void UpdateCustomerService_Test()
        {
            var reserved = service.GetItemAsync(1).GetAwaiter().GetResult();
            reserved.Booking.Statuc = true;
            service.UpdateItemAsync(reserved).GetAwaiter().GetResult();
            var updatecustomer = service.GetItemAsync(1).GetAwaiter().GetResult();
            Assert.AreEqual(true, updatecustomer.Booking.Statuc);
        }
    }
}