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
    public class BookingService:IDataStore<Booking>
    {
        private readonly Context _context;
        public BookingService(Context context) => _context = context;
        public async Task<bool> UpdateItemAsync(Booking _item)
        {
            try
            {
                var entry = _context.Set<Booking>()
                         .Local
                         .FirstOrDefault(f => f.ID == _item.ID);
                //if (entry != null)
                //    _context.Entry(entry).State = EntityState.Detached;
                //_context.Entry(_item).State = EntityState.Modified;
                return await _context.SaveChangesAsync() > 0;
            }
            catch(DbUpdateConcurrencyException ex)
            {
                return false;
            }
        }
        public async Task<bool> AddItemAsync(Booking _item)
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
        public async Task<Booking> GetItemAsync(int _id) 
            => await _context.Bookings.SingleOrDefaultAsync(x => x.ID == _id);
        public async Task<IEnumerable<Booking>> GetItemsAsync(bool forceRefresh = false) 
            => await _context.Bookings.ToListAsync();

    }
}
