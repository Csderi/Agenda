using Agenda.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Data
{
    public class DataBaseContext
    {

        public readonly SQLiteAsyncConnection Connection;

        public DataBaseContext(string dbPath) 
        {
            Connection = new SQLiteAsyncConnection(dbPath);

            Connection.CreateTableAsync<ToDoItem>().Wait();
        }

        public async Task<int> InsertItemAsyn(ToDoItem item)
        {
            return await Connection.InsertAsync(item);
        }

        public async Task<List<ToDoItem>> GetItemsAsync()
        {
            return await Connection.Table<ToDoItem>().ToListAsync();
        }

        public async Task<int> DeleteItemAsync(ToDoItem item)
        {
            return await Connection.DeleteAsync(item);
        }

        public async Task<ToDoItem> GetItemAsync(int id)
        {
            return await Connection.FindAsync<ToDoItem>(id);
        }

        public async Task<int> UpdateItemAsync(ToDoItem item)
        {
            return await Connection.UpdateAsync(item);
        }

    }
}



