using BOG.Domain.Model;
using BOG.Lib.ServiceForWorkWithUsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOG.Tests.TestServiceUser
{
    [TestClass]
    public class CreateCustomerServiceTest
    {
        private CreateCustomerService service;
        public CreateCustomerServiceTest() { }
        [TestInitialize]
        public void Init() => service = new CreateCustomerService(new TestingContextDB());
        [TestCleanup]
        public void CleanDb()
        {
            var Db = new TestingContextDB();
            Db.Database.EnsureDeleted();
        }
        [TestMethod]
        public void CreateCustomerTest() 
        {
            Assert.IsTrue(service.CreateCustomer("Data", "Base", 1).GetAwaiter().GetResult());
        }
    }
}
