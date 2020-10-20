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
        [TestInitialize]
        public void Init() => service = new PaymentMethodService(new TestingContextDB());
        [TestCleanup]
        public void CleanDb()
        {
            var Db = new TestingContextDB();
            Db.Database.EnsureDeleted();
        }
        [TestMethod]
        public void GetPaymentMethodListService_Test()
        {
            var result = service.GetItemsAsync().GetAwaiter().GetResult();
            Assert.AreEqual(typeof(List<PaymentMethod>), result.GetType());
        }
        [TestMethod]
        public void GetOnePaymentMethodService_Test()
        {
            var result = service.GetItemAsync(1).GetAwaiter().GetResult();
            Assert.AreEqual("Безналичный расчёт", result.PaymentName);
        }
        [TestMethod]
        public void AddPaymentMethodService_Test()
        {
            PaymentMethod payment = new PaymentMethod() 
            {
                DecriptionPayment = "SomeText",
                PaymentName = "SomeName"
            };
            bool item = service.AddItemAsync(payment).GetAwaiter().GetResult();
            Assert.IsTrue(item);
            var count = service.GetItemsAsync().GetAwaiter().GetResult().Count();
            Assert.AreEqual(3, count);
        }
        [TestMethod]
        public void UpdatePaymentMethodService_Test()
        {
            var payment  = service.GetItemAsync(1).GetAwaiter().GetResult();
            payment.PaymentName = "NoName";
            service.UpdateItemAsync(payment).GetAwaiter().GetResult();
            var updatePayment = service.GetItemAsync(1).GetAwaiter().GetResult();
            Assert.AreEqual("NoName", updatePayment.PaymentName);
        }
    }
}
