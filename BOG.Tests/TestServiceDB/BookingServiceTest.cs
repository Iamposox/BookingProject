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
    public class BookingServiceTest
    {
        private BookingService service;
        public BookingServiceTest() { }
        [TestInitialize]
        public void Init() => service = new BookingService(new TestingContextDB());
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
            Assert.AreEqual(result.GetType(), new List<Booking>().GetType());
        }
        [TestMethod]
        public void GetOneCustomerService_Test()
        {
            var result = service.GetItemAsync(1).GetAwaiter().GetResult();
            Assert.AreEqual(result.Statuc, false);
        }
        [TestMethod]
        public void AddCustomerService_Test()
        {
            Booking booking = new Booking()
            {
                Statuc = false
            };
            bool item = service.AddItemAsync(booking).GetAwaiter().GetResult();
            Assert.IsTrue(item);
            var count = service.GetItemsAsync().GetAwaiter().GetResult().Count();
            Assert.AreEqual(count, 3);
        }
        [TestMethod]
        public void UpdateCustomerService_Test()
        {
            var customer = service.GetItemAsync(1).GetAwaiter().GetResult();
            customer.Statuc = true;
            service.UpdateItemAsync(customer).GetAwaiter().GetResult();
            var updatecustomer = service.GetItemAsync(1).GetAwaiter().GetResult();
            Assert.AreEqual(true, updatecustomer.Statuc);
        }
    }
}
