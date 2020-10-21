using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BOG.Lib.Interface
{
    public interface IDataStore<T>
    {
        /// <summary>
        /// Method for updating data in the database
        /// </summary>
        /// <param name="_item">Update item</param>
        /// <returns>Return the result of operation(true or false)</returns>
        Task<bool> UpdateItemAsync(T _item);
        /// <summary>
        /// Method for add item in the database
        /// </summary>
        /// <param name="_item">Add item</param>
        /// <returns>Return the result of operation(true or false)</returns>
        Task<bool> AddItemAsync(T _item);
        /// <summary>
        /// Method for getting one item by its ID 
        /// </summary>
        /// <param name="_id">ID item</param>
        /// <returns>Return the T item</returns>
        Task<T> GetItemAsync(int _id);
        /// <summary>
        /// Method for getting list item
        /// </summary>
        /// <param name="_forceRefresh">Ensure to refresh data before return</param>
        /// <returns>Return IEnumerable<T> items</returns>
        Task<IEnumerable<T>> GetItemsAsync(bool _forceRefresh = false);
    }
}
