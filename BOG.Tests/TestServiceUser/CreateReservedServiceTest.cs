using BOG.Lib.ServiceForWorkWithUsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using BOG.Domain.Model;
using BOG.Lib.Services;

namespace BOG.Tests.TestServiceUser
{
    [TestClass]
    public class CreateReservedServiceTest
    {
        private CreateReservedService service;
        private PaymentMethodService servicePayment;
        private CreateCustomerService serviceCreateCustomer;
        private CustomerService serviceCustomer;
        [TestInitialize]
        public void Init() => service = new CreateReservedService(new TestingContextDB());
        [TestCleanup]
        public void CleanDb()
        {
            var Db = new TestingContextDB();
            Db.Database.EnsureDeleted();
        }
        [TestMethod]
        public void CreateReservedTest()
        {
            var context = new TestingContextDB();
            ConcurrentBag<(bool, Reserved)> bag = new ConcurrentBag<(bool, Reserved)>();
            List<Reserved> reservedsList = new List<Reserved>();
            Parallel.For(0, 10, i =>
            {
                var service = new CreateReservedService(new TestingContextDB());
                for (var j = 0; j < 1000; j++)
                {
                    var result = service.CreateReserved(new Domain.Model.Customer()
                    {
                        PaymentMethod = new Domain.Model.PaymentMethod()
                    }, 2, 1, new Random().Next(1, 3)).GetAwaiter().GetResult();
                    if (result.Item2 == true)
                    {
                        bag.Add((true, result.Item3));
                    }
                }
            });
            var list = service.GetReserveds;
        }
        [TestMethod]
        public void CreateReservedOneTest()
        {
            var context = new TestingContextDB();
            servicePayment = new PaymentMethodService(context);
            service.CreateReserved(customer(context).GetAwaiter().GetResult(),
            2, 1, new Random().Next(1, 3))
                .GetAwaiter()
                .GetResult();
            var serviceReserved = new ReservedService(new TestingContextDB());
            var count = serviceReserved.GetItemsAsync().GetAwaiter().GetResult().Count();
            Assert.AreEqual(2, count);
        }
        private async Task<Customer> customer(TestingContextDB testingContext)
        {
            serviceCreateCustomer = new CreateCustomerService(testingContext);
            await serviceCreateCustomer.CreateCustomer("Danya", "Posoxov", 1);
            serviceCustomer = new CustomerService(testingContext);
            var customer = await serviceCustomer.GetItemAsync(2);
            return customer;
        }
    }
}
