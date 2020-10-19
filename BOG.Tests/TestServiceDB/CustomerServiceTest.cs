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
    public class CustomerServiceTest
    {
        private CustomerService service;
        public CustomerServiceTest() { }
        [TestInitialize]
        public void Init() => service = new CustomerService(new TestingContextDB());
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
            Assert.AreEqual(result.GetType(), new List<Customer>().GetType());
        }
        [TestMethod]
        public void GetOneCustomerService_Test()
        {
            var result = service.GetItemAsync(1).GetAwaiter().GetResult();
            Assert.AreEqual(result.Name, "Danya");
        }
        [TestMethod]
        public void AddCustomerService_Test() 
        {
            Customer customer = new Customer()
            {
                LastName = "POsosx",
                PaymentMethod = new PaymentMethod() { PaymentName = "Ща", DecriptionPayment ="osa" },
                Name = "Dasa"
            };
            bool item = service.AddItemAsync(customer).GetAwaiter().GetResult();
            Assert.IsTrue(item);
            var count = service.GetItemsAsync().GetAwaiter().GetResult().Count();
            Assert.AreEqual(count,2);
        }
        [TestMethod]
        public void UpdateCustomerService_Test()
        {
            var customer = service.GetItemAsync(1).GetAwaiter().GetResult();
            customer.LastName = "NoLastName";
            service.UpdateItemAsync(customer).GetAwaiter().GetResult();
            var updatecustomer = service.GetItemAsync(1).GetAwaiter().GetResult();
            Assert.AreEqual("NoLastName", updatecustomer.LastName);
        }
    }
}
