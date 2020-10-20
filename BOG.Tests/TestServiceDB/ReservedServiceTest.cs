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
        [TestInitialize]
        public void Init() => service = new ReservedService(new TestingContextDB());
        [TestCleanup]
        public void CleanDb()
        {
            var Db = new TestingContextDB();
            Db.Database.EnsureDeleted();
        }
        [TestMethod]
        public void GetReservedListService_Test()
        {
            var result = service.GetItemsAsync().GetAwaiter().GetResult();
            Assert.AreEqual(typeof(List<Reserved>), result.GetType());
        }
        [TestMethod]
        public void GetOneReservedService_Test()
        {
            var result = service.GetItemAsync(1).GetAwaiter().GetResult();
            Assert.AreEqual("IPhone 12", result.Product.Name);
        }
        [TestMethod]
        public void AddReservedService_Test()
        {
            Reserved reserved = new Reserved()
            {
                Product = new Product(),
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
            Assert.AreEqual(2, count);
        }
        [TestMethod]
        public void UpdateReservedService_Test()
        {
            var reserved = service.GetItemAsync(1).GetAwaiter().GetResult();
            reserved.Booking.Statuc = true;
            service.UpdateItemAsync(reserved).GetAwaiter().GetResult();
            var updatecustomer = service.GetItemAsync(1).GetAwaiter().GetResult();
            Assert.AreEqual(true, updatecustomer.Booking.Statuc);
        }
    }
}