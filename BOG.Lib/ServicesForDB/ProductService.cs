﻿using BOG.Domain;
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
    public class ProductService:IDataStore<Product>
    {
        private readonly Context _context;
        public ProductService(Context context)
        {
            _context = context;
        }
        public async Task<bool> UpdateItemAsync(Product _item)
        {
            var entry = _context.Set<Product>()
                         .Local
                         .FirstOrDefault(f => f.ID == _item.ID);
            if (entry != null)
            {
                _context.Entry(entry).State = EntityState.Detached;
            }
            _context.Entry(_item).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> AddItemAsync(Product _item)
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
        public async Task<Product> GetItemAsync(int _id) => await _context.Products.SingleOrDefaultAsync(x => x.ID == _id);
        public async Task<IEnumerable<Product>> GetItemsAsync(bool forceRefresh = false) => await _context.Products.ToListAsync();

    }
}