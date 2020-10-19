using BOG.Domain;
using BOG.Domain.Model;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BOG.Lib.Services;
using BOG.Lib.Interface;
using System.Threading.Tasks;

namespace BOG.Lib
{
    public class ShopStorage
    {
        private Context _context;
        private DateTime dateTime;
        public ShopStorage()
        {
            _context = new Context();
            dateTime = DateTime.Now;
        }
        public async Task Reserve(string product, int amount, string name, string lastname, string paymentMethod)
        {
            if (_context.Products.Any(x => x.Name == product)
                && _context.PaymentMethods.Any(x => x.PaymentName == paymentMethod))
            {
                //CustomerWrapper customerWrapper = new CustomerWrapper(_context, name, lastname, paymentMethod, product, amount);
                //TransactionWrapper wrapper = new TransactionWrapper(_context, customerWrapper.GetCustomer, dateTime);

            }
        }
    }
}
