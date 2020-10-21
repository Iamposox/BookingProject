using BOG.Domain;
using BOG.Domain.Model;
using BOG.Lib.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BOG.Lib.ServiceForWorkWithUsers
{
    public class CreateCustomerService
    {
        private Customer customer;
        private readonly IDataStore<PaymentMethod> dataStorePayment;
        private readonly IDataStore<Customer> dataStoreCustomer;
        public CreateCustomerService(Context context) 
        {
            dataStorePayment = new Services.PaymentMethodService(context);
            dataStoreCustomer = new Services.CustomerService(context);
        }
        public async Task<bool> CreateCustomer(string name,string lastname, int paymentMethodID)
        {
            customer = new Customer()
            {
                Name = name,
                LastName = lastname
            };
            var payment = await dataStorePayment.GetItemAsync(paymentMethodID);
            customer.PaymentMethod = payment;
            return await dataStoreCustomer.AddItemAsync(customer);
        }
    }
}
