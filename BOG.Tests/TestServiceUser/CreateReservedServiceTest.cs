using BOG.Lib.ServiceForWorkWithUsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BOG.Tests.TestServiceUser
{
    [TestClass]
    public class CreateReservedServiceTest
    {
        private CreateReservedService service;
        public CreateReservedServiceTest() { }
        [TestInitialize]
        public void Init() => service = new CreateReservedService(new TestingContextDB());
        //[TestCleanup]
        //public void CleanDb()
        //{
        //    var Db = new TestingContextDB();
        //    Db.Database.EnsureDeleted();
        //}
        static object _lock = new object();
        [TestMethod]
        public void CreateReservedTest()
        {
            try
            {
                ConcurrentBag<bool> bag = new ConcurrentBag<bool>();
                Parallel.For(0, 10, async i =>
                {
                    for (var j = 0; j < 110; j++)
                    {
                        var service = new CreateReservedService(new TestingContextDB());
                        bag.Add(await service.CreateReserved(new Domain.Model.Customer()
                        {
                            PaymentMethod = new Domain.Model.PaymentMethod()
                        }, 2, 1));
                    }
                });
                foreach (var item in bag)
                {
                    Assert.IsTrue(item);
                }
            }
            catch(Exception ex) { string text = ex.Message; }
        }
        public async void Crud()
        {
            await service.CreateReserved(new Domain.Model.Customer()
            {
                PaymentMethod = new Domain.Model.PaymentMethod()
            }, 2, 1);
        }
    }
}
