using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BOG.Lib.Interface
{
    public interface IDataStore<T>
    {
        Task<bool> UpdateItemAsync(T _item);
        Task<bool> AddItemAsync(T _item);
        Task<T> GetItemAsync(int _id);
        Task<IEnumerable<T>> GetItemsAsync(bool _forceRefresh = false);
    }
}
