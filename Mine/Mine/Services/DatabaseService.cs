using Mine.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mine.Services
{
    public class DatabaseService : IDataStore<ItemModel>
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public DatabaseService()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(ItemModel).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(ItemModel)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<bool> CreateAsync(ItemModel item)
        {
            Database.InsertAsync(item);
            return Task.FromResult(true);
        }

        public Task<ItemModel> ReadAsync(String id)
        {
            return Database.Table<ItemModel>().Where(i => i.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(ItemModel data)
        {
            var item = await ReadAsync(data.Id);
            if (Database == null)
            {
                return false;
            }
            var result = await Database.UpdateAsync(item);
            return (result == 1);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var item = await ReadAsync(id);
            if (Database == null)
            {
                return false;
            }
            var result = await Database.DeleteAsync(item);
            return (result == 1);

        }

        public Task<List<ItemModel>> IndexAsync(bool flag = false)
        {
            return Database.Table<ItemModel>().ToListAsync();
        }

        public void WipeDataList()
        {
            Database.DropTableAsync<ItemModel>().GetAwaiter().GetResult();
            Database.CreateTablesAsync(CreateFlags.None, typeof(ItemModel)).ConfigureAwait(false).GetAwaiter().GetResult();
        }

    }
}
