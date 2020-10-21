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
    public class PaymentMethodService : IDataStore<PaymentMethod>
    {
        private readonly Context _context;
        public PaymentMethodService(Context context) => _context = context;
        public async Task<bool> UpdateItemAsync(PaymentMethod _item)
        {
            try
            {
                var entry = _context.Set<PaymentMethod>()
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
        public async Task<bool> AddItemAsync(PaymentMethod _item)
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
        public async Task<PaymentMethod> GetItemAsync(int _id)
        {
            try
            {
                return await _context.PaymentMethods.SingleOrDefaultAsync(x => x.ID == _id);
            }
            catch
            {
                return null;
            }
        }
        public async Task<IEnumerable<PaymentMethod>> GetItemsAsync(bool forceRefresh = false)
            => await _context.PaymentMethods.ToListAsync();

    }
}
