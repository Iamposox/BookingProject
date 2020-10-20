using BOG.Domain;
using BOG.Domain.Model;
using BOG.Lib.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BOG.Lib.ServiceForWorkWithUsers
{
    public class CreateReservedService
    {
        private Reserved reserved;
        private List<Reserved> reservedList;
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
        public async Task<(string,bool, Reserved)> CreateReserved(Customer customer, int bookingID, int availableProductID, int amount)
        {
            var item = await dataStoreAvailableProduct.GetItemAsync(availableProductID);

            if (item.Amount > 0 && (item.Amount - amount)>0)
            {
                var booking = await dataStoreBooking.GetItemAsync(bookingID);
                reserved = new Reserved()
                {
                    Product = item.Product,
                    Amount = amount,
                    Booking = booking,
                    Customer = customer,
                    TimeOrder = DateTime.Now
                };
                item.Amount -= amount;
                if (!await dataStoreAvailableProduct.UpdateItemAsync(item))
                    return ($"Товар не зарезервирован", false, null);
                if (await dataStorePayment.UpdateItemAsync(reserved.Customer.PaymentMethod)
                    && await dataStoreCustomer.UpdateItemAsync(reserved.Customer) 
                    && await dataStoreReserved.AddItemAsync(reserved))
                {
                    reservedList.Add(reserved);
                    return ($"Товар зарезервирован {reserved.TimeOrder}", true, reserved);
                }
                else return ($"Товар незарезервирован", false,null);
            }
            else return ($"Товар незарезервирован", false,null);
        }
    }
}
