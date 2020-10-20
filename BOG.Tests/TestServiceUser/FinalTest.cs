using BOG.Domain;
using BOG.Domain.Model;
using BOG.Lib.ServiceForWorkWithUsers;
using BOG.Lib.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOG.Tests.TestServiceUser
{
    [TestClass]
    public class FinalTest
    {
        private CreateReservedService serviceReserved;
        private CreateCustomerService serviceCreateCustomer;
        private CustomerService serviceCustomer;
        [TestInitialize]
        public void InitReserved() => serviceReserved = new CreateReservedService(new TestingContextDB());
        [TestCleanup]
        public void CleanDb()
        {
            var Db = new TestingContextDB();
            Db.Database.EnsureDeleted();
        }
        [TestMethod]
        public void FinalMultiThreadedTest()
        {
            ConcurrentBag<(bool, Reserved)> bag = new ConcurrentBag<(bool, Reserved)>();
            int AvailableID = 1;
            List<Reserved> reservedsList = new List<Reserved>();
            Parallel.For(0, 10, i =>
            {
                var context = new TestingContextDB();
                var service = new CreateReservedService(context);
                var availableProduct = new AvailableProductService(context);
                for (var j = 0; j < 1000; j++)
                {
                    var product = availableProduct.GetItemAsync(1).GetAwaiter().GetResult();
                    var result = product.Amount > 1 ? service.CreateReserved(customer(context).GetAwaiter().GetResult(), 2, AvailableID, new Random().Next(1, 3))
                    .GetAwaiter()
                    .GetResult() : (null, false,null);
                    if (result.Item2 == true)
                    {
                        bag.Add((true, result.Item3));
                    }
                }
            });
            var service = new ReservedService(new Context());
            var list = service.GetItemsAsync().GetAwaiter().GetResult();
            Assert.AreEqual(list.Count() - 1, bag.Count);
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
