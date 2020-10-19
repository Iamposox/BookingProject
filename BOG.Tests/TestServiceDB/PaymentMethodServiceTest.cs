using BOG.Lib.Services;
using BOG.Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BOG.Tests
{
    [TestClass]
    public class PaymentMethodServiceTest
    {
        private PaymentMethodService service;
        public PaymentMethodServiceTest() { }
        [TestInitialize]
        public void Init() => service = new PaymentMethodService(new TestingContextDB());
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
            Assert.AreEqual(result.GetType(), new List<PaymentMethod>().GetType());
        }
        [TestMethod]
        public void GetOneCustomerService_Test()
        {
            var result = service.GetItemAsync(1).GetAwaiter().GetResult();
            Assert.AreEqual(result.PaymentName, "Безналичный расчёт");
        }
        [TestMethod]
        public void AddCustomerService_Test()
        {
            PaymentMethod payment = new PaymentMethod() 
            {
                DecriptionPayment = "SomeText",
                PaymentName = "SomeName"
            };
            bool item = service.AddItemAsync(payment).GetAwaiter().GetResult();
            Assert.IsTrue(item);
            var count = service.GetItemsAsync().GetAwaiter().GetResult().Count();
            Assert.AreEqual(count, 3);
        }
        [TestMethod]
        public void UpdateCustomerService_Test()
        {
            var payment  = service.GetItemAsync(1).GetAwaiter().GetResult();
            payment.PaymentName = "NoName";
            service.UpdateItemAsync(payment).GetAwaiter().GetResult();
            var updatePayment = service.GetItemAsync(1).GetAwaiter().GetResult();
            Assert.AreEqual("NoName", updatePayment.PaymentName);
        }
    }
}
