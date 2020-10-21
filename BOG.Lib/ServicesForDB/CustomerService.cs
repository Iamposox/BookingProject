using BOG.Domain;
using BOG.Domain.Model;
using BOG.Lib.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOG.Lib.Services
{
    public class CustomerService : IDataStore<Customer>
    {
        private readonly Context _context;
        public CustomerService(Context context) => _context = context;
        public async Task<bool> UpdateItemAsync(Customer _item)
        {
            try
            {
                var entry = _context.Set<Customer>()
                             .Local
                             .FirstOrDefault(f => f.ID == _item.ID);
                if (entry != null)
                    _context.Entry(entry).State = EntityState.Detached;
                _context.Entry(_item).State = EntityState.Modified;
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return false;
            }
        }
        public async Task<bool> AddItemAsync(Customer _item)
        {
            try
            {
                await _context.AddAsync(_item);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public async Task<Customer> GetItemAsync(int _id)
        {
            try
            {
                return await _context.Customers
                .Include(x => x.PaymentMethod)
                .SingleOrDefaultAsync(x => x.ID == _id);
            }
            catch
            {
                return null;
            }
        }
        public async Task<IEnumerable<Customer>> GetItemsAsync(bool forceRefresh = false)
            => await _context.Customers
            .Include(x => x.PaymentMethod)
            .ToListAsync();

    }
}
