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
        private readonly IDataStore<Booking> dataStoreBooking;
        private readonly IDataStore<AvailableProduct> dataStoreAvailableProduct;
        private readonly IDataStore<Reserved> dataStoreReserved;
        public CreateReservedService(Context context)
        {
            dataStoreBooking = new Services.BookingService(context);
            dataStoreAvailableProduct = new Services.AvailableProductService(context);
            dataStoreReserved = new Services.ReservedService(context);
        }
        public async Task<bool> CreateReserved(Customer customer, int BookingID, int AvailableProductID)
        {
            try {
                var item = await dataStoreAvailableProduct.GetItemAsync(1);
                if (item.Amount > 1) {
                    var booking = await dataStoreBooking.GetItemAsync(BookingID);
                    reserved = new Reserved()
                    {
                        AvailableProduct = item,
                        Booking = booking,
                        Customer = customer,
                        TimeOrder = DateTime.Now
                    };
                    item.Amount--;
                    return await dataStoreReserved.AddItemAsync(reserved);
                }
                else return false;
            }
            catch(Exception ex)
            {
                return false;
            }
            }
}
}
