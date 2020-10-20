using BOG.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BOG.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BOG.Tests
{
    public class TestingContextDB : Context
    {
        public TestingContextDB()
        {
            ConnectionString = @"Server=(localdb)\mssqllocaldb;Initial Catalog=TestDB;Database=helloappdb;Trusted_Connection=True;";
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().HasData(
                new Customer()
                {
                    ID = 1,
                    Name = "Danya",
                    LastName = "Posox",
                    PaymentMethodID = 1,
                });
            modelBuilder.Entity<Reserved>().HasData(
                new Reserved() 
                {
                    ID = 1,
                    BookingID = 1,
                    CustomerID = 1,
                    TimeOrder = DateTime.Now,
                    Amount = 2,
                    ProductID =1
                });

        }
    }
}
