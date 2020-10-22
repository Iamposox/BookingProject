using BOG.Domain;
using BOG.Domain.Model;
using BOG.Lib.ServiceForWorkWithUsers;
using BOG.Lib.Services;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
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
            ConcurrentBag<Reserved> bag = new ConcurrentBag<Reserved>();
            List<Reserved> reservedsList = new List<Reserved>();
            Parallel.For(0, 10, i =>
            {
                var context = new TestingContextDB();
                var service = new CreateReservedService(context);
                var availableProduct = new AvailableProductService(context);
                for (var j = 0; j < 1000; j++)
                {
                    var amountOfRandom = new Random().Next(1, 3);
                    var product = availableProduct.GetItemAsync(1).GetAwaiter().GetResult();
                    if (product != null && (product.Amount-amountOfRandom) >= 0)
                    {
                        product.Amount -= amountOfRandom;
                        var result = service.CreateReservedAsync(Customer(context).GetAwaiter().GetResult(), product, amountOfRandom)
                        .GetAwaiter()
                        .GetResult();
                        if (result != null)
                            bag.Add(result);
                    }
                }
            });
            Assert.IsTrue(bag.Count>0);
        }
        /// <summary>
        /// Create customer for FinalMultIThreadedTest
        /// </summary>
        /// <param name="testingContext">Context on Testing database</param>
        /// <returns>Created customer</returns>
        private async Task<Customer> Customer(TestingContextDB testingContext)
        {
            serviceCreateCustomer = new CreateCustomerService(testingContext);
            await serviceCreateCustomer.CreateCustomer("Danya", "Posoxov", 1);
            serviceCustomer = new CustomerService(testingContext);
            var customer = await serviceCustomer.GetItemAsync(2);
            return customer;
        }
    }
}
