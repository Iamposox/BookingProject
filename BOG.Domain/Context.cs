using BOG.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;

namespace BOG.Domain
{
    /// <summary>
    /// Default way to connect Operate with DB
    /// </summary>
    public class Context : DbContext
    {
        public string ConnectionString = @"Server=(localdb)\mssqllocaldb;Database=TestDB;Trusted_Connection=True;";
        public Context() {   
            Database.EnsureCreated();  }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           => optionsBuilder.UseSqlServer(ConnectionString);
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    ID = 1,
                    Name = "IPhone 12"
                });
            modelBuilder.Entity<Booking>().HasData(
                new Booking()
                {
                    ID = 1,
                    Statuc = false
                },
                new Booking()
                {
                    ID = 2,
                    Statuc = true
                });
            modelBuilder.Entity<PaymentMethod>().HasData(
                new PaymentMethod()
                {
                    ID = 1,
                    PaymentName = "Безналичный расчёт",
                    DecriptionPayment = "Расчёт через банковскую карту/перевод"
                },
                new PaymentMethod()
                {
                    ID = 2,
                    PaymentName = "Наличный расчёт",
                    DecriptionPayment = "Расчёт купюрами"
                });
            modelBuilder.Entity<AvailableProduct>().HasData(
                new AvailableProduct()
                {
                    ID = 1,
                    Price = 2560,
                    ProductID = 1,
                    Amount = 100
                });
        }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<AvailableProduct> AvailableProduct { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Reserved> Reserveds { get; set; }
    }
}
