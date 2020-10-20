using BOG.Domain;
using BOG.Domain.Model;
using BOG.Lib.Interface;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BOG.Lib.Services
{
    public class ReservedService : IDataStore<Reserved>
    {
        private readonly Context _context;
        public ReservedService(Context context) => _context = context;
        public async Task<bool> UpdateItemAsync(Reserved _item)
        {
            try
            {
                var entry = _context.Set<Reserved>()
                             .Local
                             .FirstOrDefault(f => f.ID == _item.ID);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return false;
            }
        }
        public async Task<bool> AddItemAsync(Reserved _item)
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
        public async Task<Reserved> GetItemAsync(int _id)
            => await _context.Reserveds
            .Include(x => x.Booking)
            .Include(x => x.Customer.PaymentMethod)
            .Include(x => x.Product)
            .SingleOrDefaultAsync(x => x.ID == _id);
        public async Task<IEnumerable<Reserved>> GetItemsAsync(bool forceRefresh = false)
            => await _context.Reserveds
            .Include(x => x.Booking)
            .Include(x => x.Customer.PaymentMethod)
            .Include(x => x.Product)
            .ToListAsync();
    }
}
