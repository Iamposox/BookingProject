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
        public async Task CreateReservedOneTest()
        {
            var context = new TestingContextDB();
            servicePayment = new PaymentMethodService(context);
            var serviceProduct = new AvailableProductService(context);
            var availableProduct = await serviceProduct.GetItemAsync(1);
            service.CreateReservedAsync(customer(context).GetAwaiter().GetResult(),availableProduct
            , new Random().Next(1, 3))
                .GetAwaiter()
                .GetResult();
            var serviceReserved = new ReservedService(new TestingContextDB());
            var count = serviceReserved.GetItemsAsync().GetAwaiter().GetResult().Count();
            Assert.AreEqual(2, count);
        }
        /// <summary>
        /// Create customer for CreateReservedTest
        /// </summary>
        /// <param name="testingContext">Our Test database</param>
        /// <returns>Created customer</returns>
        private async Task<Customer> customer(TestingContextDB testingContext)
        {
            serviceCreateCustomer = new CreateCustomerService(testingContext);
            await serviceCreateCustomer.CreateCustomer("Danya", "Posoxov", 1);
            serviceCustomer = new CustomerService(testingContext);
            return await serviceCustomer.GetItemAsync(2);
        }
    }
}
