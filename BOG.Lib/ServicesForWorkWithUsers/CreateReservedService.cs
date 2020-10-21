using BOG.Domain;
using BOG.Domain.Model;
using BOG.Lib.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOG.Lib.ServiceForWorkWithUsers
{
    public class CreateReservedService
    {
        private Reserved reserved;
        private List<Reserved> reservedList;
        private AvailableProduct product;
        private readonly IDataStore<Booking> dataStoreBooking;
        private readonly IDataStore<AvailableProduct> dataStoreAvailableProduct;
        private readonly IDataStore<Reserved> dataStoreReserved;
        private readonly IDataStore<PaymentMethod> dataStorePayment;
        private readonly IDataStore<Customer> dataStoreCustomer;
        public CreateReservedService(Context context)
        {
            reservedList = new List<Reserved>();
            dataStoreBooking = new Services.BookingService(context);
            dataStoreAvailableProduct = new Services.AvailableProductService(context);
            dataStoreReserved = new Services.ReservedService(context);
            dataStorePayment = new Services.PaymentMethodService(context);
            dataStoreCustomer = new Services.CustomerService(context);
        }
        public List<Reserved> GetReserveds { get => reservedList; }
        public Reserved GetReserved { get => reserved; }
        /// <summary>
        /// Method for creating a single reservation in the database
        /// </summary>
        /// <param name="customer">The client that accesses the database</param>
        /// <param name="availableProductID">The ID of available product</param>
        /// <param name="amount">Quantity of the desired product</param>
        /// <returns>Return created reserving on database</returns>
        public async Task<Reserved> CreateReserved(Customer customer, int availableProductID, int amount)
        {
            product = await dataStoreAvailableProduct.GetItemAsync(availableProductID);
            if (product != null)
            {
                if (product.Amount > 0 && (product.Amount - amount) > 0)
                {
                    var bookingList = await dataStoreBooking.GetItemsAsync();
                    var booking = bookingList.Where(x => x.Statuc == true).Single();
                    reserved = new Reserved()
                    {
                        Product = product.Product,
                        Amount = amount,
                        Booking = booking,
                        Customer = customer,
                        TimeOrder = DateTime.Now
                    };
                    product.Amount -= amount;
                    if (!await dataStoreAvailableProduct.UpdateItemAsync(product))
                        return null;
                    if (await dataStorePayment.UpdateItemAsync(reserved.Customer.PaymentMethod)
                        && await dataStoreCustomer.UpdateItemAsync(reserved.Customer)
                        && await dataStoreReserved.AddItemAsync(reserved))
                    {
                        reservedList.Add(reserved);
                        return reserved;
                    }
                    else return null;
                }
                else return null;
            }
            else return null;
        }
    }
}
